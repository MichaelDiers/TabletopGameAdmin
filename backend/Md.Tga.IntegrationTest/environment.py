'''
Module environment provides operations for accessing environment variables.
'''
import os

ENV_API_KEY = 'ENV_FUNCTION_API_KEY'
ENV_EMAIL = 'ENV_FUNCTION_EMAIL'
ENV_PROJECT_ID = 'ENV_FUNCTION_PROJECT_ID'
ENV_PUBSUB_SUFFIX = 'ENV_FUNCTION_PUBSUB_SUFFIX'

class Environment:
  '''
  Access and read environment variables.
  '''

  def __init__(self):
    '''
    Initializes a new instance of the Environment class.
    '''
    self.__api_key = self.__get_value(ENV_API_KEY)
    self.__email = self.__get_value(ENV_EMAIL)
    self.__project_id = self.__get_value(ENV_PROJECT_ID)
    self.__pubsub_suffix = self.__get_value(ENV_PUBSUB_SUFFIX)

  @staticmethod
  def __get_value(key: str) -> str:
    '''
    Reads an environment variable by the given key.

    Args:
        key (str): The name of the environment variable.

    Raises:
        Exception: Raised if the environment variable does not exist.

    Returns:
        str: The value of the environment variable.
    '''
    value = os.environ.get(key, None)
    if value:
      return value
    raise Exception(f'{key} is not set')

  @property
  def api_key(self) -> str:
    '''
    Gets the value of the api_key environment variable.

    Returns:
        str: The value of the environment variable.
    '''
    return self.__api_key

  @property
  def email(self) -> str:
    '''
    Gets the value of the email environment variable.

    Returns:
        str: The value of the environment variable.
    '''
    return self.__email

  @property
  def project_id(self) -> str:
    '''
    Gets the value of the project_id environment variable.

    Returns:
        str: The value of the environment variable.
    '''
    return self.__project_id

  @property
  def pubsub_suffix(self):
    '''
    Gets the value of the pubsub_suffix environment variable.

    Returns:
        str: The value of the environment variable.
    '''
    return self.__pubsub_suffix
