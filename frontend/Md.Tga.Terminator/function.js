const app = require('./application/app');
const config = require('./config');

const {
  ENV_GAMES_COLLECTION_NAME: gamesCollectionName,
  ENV_GAME_SERIES_COLLECTION_NAME: gameSeriesCollectionName,
  ENV_PLAYER_MAPPINGS_COLLECTION_NAME: playerMappingsCollectionName,
  ENV_START_GAME_TERMINATION_TOPIC: startGameTerminationTopic,
} = process.env;

exports.vote = app(
  config({
    baseName: 'terminate',
    gamesCollectionName,
    gameSeriesCollectionName,
    playerMappingsCollectionName,
    startGameTerminationTopic,
    requestLogging: false,
  }),
);
