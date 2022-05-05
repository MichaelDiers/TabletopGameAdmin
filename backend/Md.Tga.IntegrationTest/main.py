'''
A google cloud http function that executes integration tests.
'''
from uuid import uuid4

from database import Database
from environment import Environment
from game_series import GameSeries
from message_creator import MessageCreator
from pub_sub import PubSubClient
from result_creator import ResultCreator

API_KEY_NAME = 'api_key'

def integration_test(request, result_creator: ResultCreator) -> None:
  '''
  Execute a complete integeration test.

  Args:
      request (any): An incoming google function flask.Request.
      result_creator (ResultCreator): A json result generator.
  '''
  environment = Environment()
  if not request \
    or not request.args \
    or API_KEY_NAME not in request.args \
    or request.args[API_KEY_NAME] != environment.api_key:
    result_creator.add_error('forbidden')
    return

  database = Database(environment.pubsub_suffix.lower())
  pub_sub_client = PubSubClient(environment.project_id)
  message_creator = MessageCreator(environment.pubsub_suffix)

  external_game_series_id = str(uuid4())
  game_series = GameSeries(pub_sub_client, message_creator, database) \
    .start(result_creator, external_game_series_id, environment.email)

  games = database.games(result_creator, game_series['id'])
  if not games:
    return

  surveys = database.surveys(result_creator, games[0]['id'])
  if not surveys:
    return

# pylint: disable=unused-argument,invalid-name
def IntegrationTest(request) -> str:
  '''
  The entry point of the google cloud http function.

  Args:
      request (any): An incoming google function flask.Request.

  Returns:
      str: The test result as a json string.
  '''
  result_creator = ResultCreator()

  try:
    integration_test(request, result_creator)
  # pylint: disable=broad-except
  except Exception as exception:
    result_creator.add_error(exception)

  return str(result_creator)
