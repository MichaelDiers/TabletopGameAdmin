'''
Access and read environment variables.

Classes:

    Environment

Constants:

    ENV_API_KEY
    ENV_EMAIL
    ENV_PROJECT_ID
    ENV_PUBSUB_SUFFIX
'''
import os

ENV_API_KEY = 'ENV_FUNCTION_API_KEY'
ENV_EMAIL = 'ENV_FUNCTION_EMAIL'
ENV_PROJECT_ID = 'ENV_FUNCTION_PROJECT_ID'
ENV_PUBSUB_SUFFIX = 'ENV_FUNCTION_PUBSUB_SUFFIX'

class Environment:
    '''
        A class for accessing environment variables.

        ...

        Properties
        ----------
            api_key
            email
            project_id
            pubsub_suffix

        Methods
        -------
            None
    '''

    def __init__(self):
        '''
            Intitializes a new instance of the Environment class.

            Parameters
            ----------
            None
        '''
        self.__api_key = self.__get_value(ENV_API_KEY)
        self.__email = self.__get_value(ENV_EMAIL)
        self.__project_id = self.__get_value(ENV_PROJECT_ID)
        self.__pubsub_suffix = self.__get_value(ENV_PUBSUB_SUFFIX)

    @staticmethod
    def __get_value(key):
        '''
            Access an environment variables with the given name 'key'.

            Parameters
            ----------
            key : str
                the name of the environment variable

            Returns
            -------
            the value of the environment key
        '''
        value = os.environ.get(key, None)
        if value:
            return value

        raise Exception(f'{key} is not set')

    @property
    def api_key(self):
        '''
            Gets the property value of the api key environment variable.

            Returns
            -------
            the value of the environment variable as a string
        '''
        return self.__api_key

    @property
    def email(self):
        '''
            Gets the property value of the email environment variable.

            Returns
            -------
            the value of the environment variable as a string
        '''
        return self.__email

    @property
    def project_id(self):
        '''
            Gets the property value of the project id environment variable.

            Returns
            -------
            the value of the environment variable as a string
        '''
        return self.__project_id

    @property
    def pubsub_suffix(self):
        '''
            Gets the property value of the pub/sub suffix environment variable.

            Returns
            -------
            the value of the environment variable as a string
        '''
        return self.__pubsub_suffix
