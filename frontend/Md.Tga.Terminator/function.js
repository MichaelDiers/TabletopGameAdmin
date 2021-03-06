const app = require('./application/app');
const config = require('./config');

const {
  ENV_GAMES_COLLECTION_NAME: gamesCollectionName,
  ENV_GAME_SERIES_COLLECTION_NAME: gameSeriesCollectionName,
  ENV_GAME_STATUS_COLLECTION_NAME: gameStatusCollectionName,
  ENV_PLAYER_MAPPINGS_COLLECTION_NAME: playerMappingsCollectionName,
  ENV_START_GAME_TERMINATION_TOPIC: startGameTerminationTopic,
} = process.env;

exports.terminate = app(
  config({
    baseName: 'terminate',
    gamesCollectionName,
    gameSeriesCollectionName,
    gameStatusCollectionName,
    playerMappingsCollectionName,
    startGameTerminationTopic,
    requestLogging: false,
  }),
);
