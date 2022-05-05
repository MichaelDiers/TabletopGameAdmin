'''
Module message_creator generate google cloud pub/sub messages.
'''
from uuid import uuid4

class MessageCreator:
  '''
  Generator for google cloud pub/sub messages.
  '''

  def __init__(self, pubsub_suffix: str):
    '''
    Initializes a new instance of the MessageCreator class.

    Args:
        pubsub_suffix (str): A environment dependent pub/sub topic suffix.
    '''
    self.__pubsub_suffix = pubsub_suffix

  def start_game_series_message(self, external_id: str, email: str) -> str:
    '''
    Create a pub/sub message for starting a new game series.

    Args:
        external_id (str): The external id of the new game series.
        email (str): The email address that is used for the organizer and
                     the players of the game series.

    Returns:
        str: A pub/sub message in json format.
    '''
    message = (
      f'{{'
      f'  "processId":"{str(uuid4())}",'
      f'  "gameSeries":{{'
      f'    "externalId":"{external_id}",'
      f'    "name":"IntegrationTest GameSeries",'
      f'    "gameType":"AAG40",'
      f'    "organizer":{{'
      f'      "name":"Oraganizer Name",'
      f'      "email":"{email}"'
      f'    }},'
      f'    "players": ['
      f'      {{'
      f'        "name":"Player1 Name",'
      f'        "email":"{email}"'
      f'      }},'
      f'      {{'
      f'        "name":"Player2 Name",'
      f'        "email":"{email}"'
      f'      }},'
      f'      {{'
      f'        "name":"Player3 Name",'
      f'        "email":"{email}"'
      f'      }},'
      f'      {{'
      f'        "name":"Player4 Name",'
      f'        "email":"{email}"'
      f'      }},'
      f'      {{'
      f'        "name":"Player5 Name",'
      f'        "email":"{email}"'
      f'      }}'
      f'    ]'
      f'  }}'
      f'}}'
    )

    return (f'START_GAME_SERIES_{self.__pubsub_suffix}', message)
