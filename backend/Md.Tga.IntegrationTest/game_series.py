'''
Module game_series provides operations on game series database entities.
'''
from pub_sub import PubSubClient
from message_creator import MessageCreator
from result_creator import ResultCreator
from database import Database

class GameSeries:
  '''
  Send pub/sub messages for creating game series entities. Access the
  created entities by a firestore database client.
  '''
  def __init__(
    self,
    pub_sub_client: PubSubClient,
    message_creator: MessageCreator,
    database: Database):
    '''
    Initializes a new instance of the GameSeries class.

    Args:
        pub_sub_client (PubSubClient): Client for sending google cloud pub/sub messages.
        message_creator (MessageCreator): A generator for google cloud pub/sub messages.
        database (Database): Client for accessing google cloud firestore.
    '''
    self.__pub_sub_client = pub_sub_client
    self.__message_creator = message_creator
    self.__database = database

  def start(self, result_creator: ResultCreator, external_id: str, email: str) -> dict:
    '''
    Start a new game series and read the created data from the database.

    Args:
        result_creator (ResultCreator): A json result generator.
        external_id (str): The external id of the new game series.
        email (str): The email address that is used for the organizer and
                     players of the game series.

    Returns:
        dict: The game series entity as a dictionary.
    '''
    result_creator.add('GameSeriesExternalId', external_id)
    (topic_id, message) = self.__message_creator.start_game_series_message(external_id, email)
    self.__pub_sub_client.publish(topic_id, message)

    return self.__database.game_series(result_creator, external_id)
