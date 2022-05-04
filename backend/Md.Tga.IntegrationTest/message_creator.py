'''
Create google cloud pub/sub messages.

Classes:

    MessageCreator
'''
from uuid import uuid4

class MessageCreator:
    '''
        Creator for pub/sub messages.

        ...

        Properties
        ----------
            None

        Methods
        -------
        start_game_series_message(self, externalId, email):
            Create a message for starting a new game series.
    '''
    def __init__(self, pubsub_suffix) -> None:
        '''
            Initializes a new instance of the MessageCreator class.

            Parameters
            ----------
            external_id : str
                the external of the game series
            email: str
                the email is used for the game series organizer and players
        '''
        self.__pubsub_suffix = pubsub_suffix

    def start_game_series_message(self, external_id, email) -> str:
        '''
            Creates a message for starting a new game series.

            Parameters
            ----------
            external_id : str
                the external id of the game series

            email: str
                the email is used for each required email of the game series:
                - organizer
                - players

            Returns
            -------
            the message as a json string
        '''
        message = (
            f'{{'
            f'  "processId":"{uuid4()}",'
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
