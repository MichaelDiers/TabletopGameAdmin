'''
Create and check existence of GameSeries entities.

Classes:

    GameSeries
'''
class GameSeries:
    '''
        A class to represent operations on game series entities.

        ...

        Properties
        ----------
            None

        Methods
        -------
        start(self, external_id, email):
            Start a new game series.
    '''

    def __init__(self, pub_sub_client, message_creator, result_creator):
        '''
            Initializes a new instance of the GameSeries class.

            Parameters
            ----------
            pub_sub_client : PubSubClient
                a pub/sub client for publishing messages to topic
            message_creator: MessageCreator
                generator for pub/sub messages
            result_creator
                generator for a json result summary
        '''
        self.__pub_sub_client = pub_sub_client
        self.__message_creator = message_creator
        self.__result_creator = result_creator

    def start(self, external_id, email):
        '''
            Sends a message to pub/sub for starting a new game series.

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
            None
        '''
        self.__result_creator.add('GameSeriesExternalId', external_id)
        (topic_id, message) = self.__message_creator.start_game_series_message(external_id, email)
        self.__pub_sub_client.publish(topic_id, message)
        self.__result_creator.add_ok('StartGameSeries')
