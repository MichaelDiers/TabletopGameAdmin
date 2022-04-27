const app = require('./application/app');
const config = require('./config');

const {
  ENV_GAMES_COLLECTION_NAME: gamesCollectionName,
  ENV_GAME_SERIES_COLLECTION_NAME: gameSeriesCollectionName,
  ENV_GAME_STATUS_COLLECTION_NAME: gameStatusCollectionName,
  ENV_PLAYER_MAPPINGS_COLLECTION_NAME: playerMappingsCollectionName,
  ENV_GAME_TERMINATION_RESULT_COLLECTION_NAME: gameTerminationResultsCollectionName,
} = process.env;

exports.statistics = app(
  config({
    baseName: 'statistics',
    gamesCollectionName,
    gameSeriesCollectionName,
    gameStatusCollectionName,
    gameTerminationResultsCollectionName,
    playerMappingsCollectionName,
    requestLogging: false,
  }),
);
