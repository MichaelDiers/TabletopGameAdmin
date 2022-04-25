const app = require('./application/app');
const config = require('./config');

const application = app(
  config({
    appRoute: '/terminate',
    baseName: 'terminate',
    gatewayAddress: 'http://localhost:3000/gateway',
    requestLogging: true,
    gamesCollectionName: 'games-test',
    gameSeriesCollectionName: 'game-series-test',
    gameStatusCollectionName: 'game-status-test',
    playerMappingsCollectionName: 'player-mappings-test',
    startGameTerminationTopic: 'START_GAME_TERMINATION_TEST',
  }),
);

const port = 3003;
application.listen(port, () => {
  // eslint-disable-next-line
  console.log(`Example app listening on port ${port}`);
});
