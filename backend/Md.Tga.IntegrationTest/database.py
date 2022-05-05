'''
Module database provides operations on the firestore database.
'''
from time import sleep
from google.cloud import firestore_v1

from result_creator import ResultCreator

class Database:
  '''
  Access to the google cloud firestore database.
  '''

  def __init__(self, database_suffix: str, wait_count: int = 30, wait_secs: int = 1):
    '''
    Initializes a new instance of the Database class.

    Args:
        database_suffix (str): The suffix of collection names ("test", "stage", "prod").
        wait_count (int, optional): The number of retries for accessing a
                                    database collection entry. Defaults to 30.
        wait_secs (int, optional): The time in seconds between retries. Defaults to 1.
    '''
    self.__firestore = firestore_v1.Client()
    self.__database_suffix = database_suffix
    self.__wait_count = wait_count
    self.__wait_secs = wait_secs

  def __handle_wait(
    self,
    result_creator: ResultCreator,
    read: callable,
    log_key: str = None) -> list:
    '''
    Execute a query until a result is available or a timeout occurs.

    Args:
        result_creator (ResultCreator): A json result generator.
        read (callable): A parameterless callable whose result is an Iterable[Snapshot].
        log_key (str): Add a log entry if a log_key is set and suppress the log otherwise.

    Returns:
        list: A list of dictionaries containing the database collection entities.
              None if no entity is found.
    '''
    for _ in range(self.__wait_count):
      snapshots = read()
      results = []
      for snapshot in snapshots:
        result = snapshot.to_dict()
        result['id'] = snapshot.id
        results.append(result)

      if results:
        if log_key:
          result_creator.add(log_key, ', '.join(data['id'] for data in results))
        return results

      sleep(self.__wait_secs)

    if log_key:
      result_creator.add(log_key, 'No entry found.')

  def __handle_wait_single(
    self,
    result_creator: ResultCreator,
    read: callable,
    log_key: str = None) -> dict:
    '''
    Execute a query until a result is available or a timeout occurs.

    Args:
        result_creator (ResultCreator): A json result generator.
        read (callable): A parameterless callable whose result is an Iterable[Snapshot].
        log_key (str): Add a log entry if a log_key is set and suppress the log otherwise.

    Returns:
        dict: A dictionary containing the database collection entity.
              None if no entity is found.
    '''
    results = self.__handle_wait(result_creator, read, log_key)
    if results and len(results) == 1:
      return results[0]

    return None

  def games(self, result_creator: ResultCreator, game_series_id: str) -> list:
    '''
    Read the game entities with the given parent id until a timeout occurs.

    Args:
        result_creator (ResultCreator): A json result generator.
        game_series_id (str): The id of the parent game series document.

    Returns:
        list: A list of dictionaries containing the games collection entities.
        None if no entity is found.
    '''
    query = lambda: self.__firestore.collection(f'games-{self.__database_suffix}') \
      .where('parentDocumentId', '==', game_series_id).get()
    return self.__handle_wait(result_creator, query, 'Game.Ids')

  def game_series(self, result_creator: ResultCreator, external_id: str) -> dict:
    '''
    Read the game series entity with the given external id until a timeout occurs.

    Args:
        result_creator (ResultCreator): A json result generator.
        external_id (str): The id of the external id of the game series document.

    Returns:
        dict: A dictionary containing the game series collection entity.
        None if no entity is found.
    '''
    query = lambda: self.__firestore.collection(f'game-series-{self.__database_suffix}') \
      .where('externalId', '==', external_id).limit(1).get()
    return self.__handle_wait_single(result_creator, query, 'GameSeries.Id')

  def surveys(self, result_creator: ResultCreator, game_id: str) -> list:
    '''
    Read the surveys entities with the given parent id until a timeout occurs.

    Args:
        result_creator (ResultCreator): A json result generator.
        game_id (str): The id of the parent game document.

    Returns:
        list: A list of dictionaries containing the surveys collection entities.
        None if no entity is found.
    '''
    query = lambda: self.__firestore.collection(f'surveys-{self.__database_suffix}') \
      .where('parentDocumentId', '==', game_id).get()
    return self.__handle_wait(result_creator, query, 'Survey.Ids')
