'''
Json result creator.

Classes:

    ResultCreator
'''
class ResultCreator:
    '''
        Creator for a json string that contains test results.

        ...

        Properties
        ----------
            None

        Methods
        -------
        add(self, key, value):
            add a json entry to the result
        add_error(self, value):
            add a json value as an error to the result
        add_ok(self, key):
            add a success value to the json result
        __str__(self):
            creates the json value
    '''

    def __init__(self) -> None:
        '''
            Initializes a new instance of the ResultCreator class.

            Parameters
            ----------
            None
        '''
        self.__results = []

    def add(self, key, value) -> None:
        '''
            Add a json entry to the result.

            Parameters
            ----------
            key : str
                the json key of the entry

            value: str
                the json value of the entry

            Returns
            -------
            None
        '''
        self.__results.append((key, value))

    def add_error(self, value) -> None:
        '''
            Add an error as a json entry to the result.

            Parameters
            ----------

            value: str
                the json value of the error entry

            Returns
            -------
            None
        '''
        self.add('error', value)

    def add_ok(self, key) -> None:
        '''
            Add a success value to the json result.

            Parameters
            ----------

            key: str
                the json key of the entry

            Returns
            -------
            None
        '''
        self.add(key, 'ok')

    def __str__(self) -> str:
        '''
            Create a json string for the added json entries.

            Parameters
            ----------
            None
            
            Returns
            -------
            the result as a json string
        '''
        return '{' + ','.join(f'"{key}":"{value}"' for (key, value) in self.__results) + '}'
