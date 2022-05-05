'''
Module result_creator provides a test result generator.
'''
class ResultCreator:
  '''
  A test result generator.
  '''

  def __init__(self):
    '''
    Initializes a new instance of the ResultCreator class.
    '''
    self.__results = []

  def add(self, key: str, value: str) -> None:
    '''
    Add a json entry with key and value.

    Args:
        key (str): The key of the json entry.
        value (str): The value of the json entry.
    '''
    self.__results.append((key, value))

  def add_error(self, value: str) -> None:
    '''
    Add a json entry with key 'error' and the given value.

    Args:
        value (str): The value of the json entry.
    '''
    self.add('error', value)

  def __str__(self) -> str:
    '''
    Create the json formatted string of the object.

    Returns:
        str: A json formatted string.
    '''
    return '{' + ','.join(f'"{key}":"{value}"' for (key, value) in self.__results) + '}'
