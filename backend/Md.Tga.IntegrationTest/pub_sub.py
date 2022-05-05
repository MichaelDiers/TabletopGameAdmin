'''
Module pub_sub provides operations for sending google cloud pub/sub messages.
'''
from google.cloud import pubsub_v1

class PubSubClient:
  '''
  Client for sending google cloud pub/sub messages.
  '''

  def __init__(self, project_id: str):
    '''
    Initializes a new instance of the PubSubClient class.

    Args:
        project_id (str): The id of the project to that messages are sent.
    '''
    self.__project_id = project_id
    self.__pub_sub_client = pubsub_v1.PublisherClient()

  def publish(self, topic_id: str, message: str) -> None:
    '''
    Sends the message to the specifies topic.

    Args:
        topic_id (str): The id of the pub/sub topic.
        message (str): A message in json format.
    '''
    topic = self.__pub_sub_client.topic_path(self.__project_id, topic_id)
    response = self.__pub_sub_client.publish(topic, message.encode())
    response.result()
