const app = require('./application/app');
const config = require('./config');

const application = app(
  config({
    appRoute: '/statistics',
    baseName: 'statistics',
    gatewayAddress: 'http://localhost:3000/gateway',
    requestLogging: true,
    gamesCollectionName: 'games-test',
    gameSeriesCollectionName: 'game-series-test',
    gameStatusCollectionName: 'game-status-test',
    playerMappingsCollectionName: 'player-mappings-test',
    gameTerminationResultsCollectionName: 'game-termination-result-test',
  }),
);

const port = 3004;
application.listen(port, () => {
  // eslint-disable-next-line
  console.log(`Example app listening on port ${port}`);
});
