'''
publish google cloud pub/sub messages.

Classes:

    PubSubClient
'''
from google.cloud import pubsub_v1

class PubSubClient:
    '''
        Client for sending messages to google cloud pub/sub.

        ...

        Properties
        ----------
            None

        Methods
        -------
        publish(self, topic_id, message):
            Publish a message to google cloud pub/sub.
    '''
    def __init__(self, project_id):
        '''
            Initializes a new instance of the PubSubClient class.

            Parameters
            ----------
            project_id : str
                the id of the project that is used for sending pub/sub messages
        '''
        self.__project_id = project_id
        self.__pub_sub_client = pubsub_v1.PublisherClient()

    def publish(self, topic_id, message) -> None:
        '''
            Publish a message to google cloud pub/sub.

            Parameters
            ----------
            topic_id : str
                the id of the pub/sub topic

            message: str
                the message that will be published

            Returns
            -------
            None
        '''
        topic = self.__pub_sub_client.topic_path(self.__project_id, topic_id)
        response = self.__pub_sub_client.publish(topic, message.encode())
        response.result()
