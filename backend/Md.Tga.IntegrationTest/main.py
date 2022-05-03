import os
from uuid import uuid4

from google.cloud import pubsub_v1

ENV_EMAIL = 'ENV_FUNCTION_EMAIL'
ENV_PROJECT_ID = 'ENV_FUNCTION_PROJECT_ID'
ENV_PUBSUB_SUFFIX = 'ENV_FUNCTION_PUBSUB_SUFFIX'

PUBSUB_START_GAME_SERIES = 'START_GAME_SERIES_'

def create_start_game_series_message(email, externalId):
    message = (
        f'{{'
        f'  "processId":"{uuid4()}",'
        f'  "gameSeries":{{'
        f'    "externalId":"{externalId}",'
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

    return message

def publish_start_game_series_message(projectId, topicId, message):
     client = pubsub_v1.PublisherClient()
     topic = client.topic_path(projectId, topicId)
     response = client.publish(topic, message.encode())
     print(response.result())

def integration_test():
    email = os.environ.get(ENV_EMAIL)
    projectId = os.environ.get(ENV_PROJECT_ID)
    pubSubSuffix = os.environ.get(ENV_PUBSUB_SUFFIX)

    externalGameSeriesId = uuid4()
    results = []

    try:
        start_game_series_message = create_start_game_series_message(email, externalGameSeriesId)
        publish_start_game_series_message(projectId, PUBSUB_START_GAME_SERIES + pubSubSuffix, start_game_series_message)
        results.append(('GameSeries externalId', externalGameSeriesId))
    except Exception as e:
        results.append(('Error', e))
    
    return '{' + ','.join(f'"{key}":"{value}"' for (key, value) in results) + '}'


def IntegrationTest(request):
    return integration_test()

print(IntegrationTest('email'))
