'''
A google cloud http function.

Functions:
    handle_game_series(pub_sub_client, message_creator, external_id, email, result_creator)
    integration_test(result_creator)
    IntegrationTest(request)

'''
from uuid import uuid4

from environment import Environment
from game_series import GameSeries
from message_creator import MessageCreator
from pub_sub import PubSubClient
from result_creator import ResultCreator

def handle_game_series(pub_sub_client, message_creator, external_id, email, result_creator) -> None:
    '''
        Handle all game series test from creating to checking its existence.

        Parameters
        ----------
        pub_sub_client:
            Client for sending pub/sub messages.

        message_creator:
            Creator for pub/sub messages.
        external_id
            The external id of the game series.

        email:
            The email that is used for organizer and players of the game series.

        result_creator:
            Creator for collecting results of the tests.

        Returns
        -------
        None
        '''
    game_series = GameSeries(pub_sub_client, message_creator, result_creator)
    return game_series.start(external_id, email)

def integration_test(result_creator) -> None:
    '''
        Initializes and executes all integration tests.

        Parameters
        ----------
        result_creator:
            Creator for results as a json string.

        Returns
        -------
        None
    '''
    environment = Environment()
    pub_sub_client = PubSubClient(environment.project_id)
    message_creator = MessageCreator(environment.pubsub_suffix)

    external_game_series_id = uuid4()

    handle_game_series(
        pub_sub_client,
        message_creator,
        external_game_series_id,
        environment.email,
        result_creator)

# pylint: disable=unused-argument,invalid-name
def IntegrationTest(request) -> str:
    '''
        The entry point of the google cloud http function.

        Parameters
        ----------
        request:
            The incoming http request.

        Returns
        -------
        A json string containing the test results.
    '''
    result_creator = ResultCreator()

    try:
        integration_test(result_creator)
    # pylint: disable=broad-except
    except Exception as exception:
        result_creator.add_error(exception)

    return str(result_creator)
