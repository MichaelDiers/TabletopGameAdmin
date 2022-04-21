const express = require('express');

const controllers = require('./controllers/controllers');
const middlewares = require('./middlewares/middlewares');
const routers = require('./routers/routers');
const databaseInit = require('./services/database');
const pubSubInit = require('./services/pub-sub');

/**
 * Initializes the app.
 * @param {object} config A configuration object.
 * @param {express.Router} config.router An express router.
 * @param {Express} An express application.
 * @returns The given app if set in config, a new express app otherwise.
 */
const initialize = (config = {}) => {
  const {
    app = express(),
    router = express.Router(),
    viewEngine,
    viewLocalFolder,
    appRoute,
    requestLogging,
    // pug
    lang,
    files,
    gamesCollectionName,
    gameSeriesCollectionName,
    playerMappingsCollectionName,
    database = databaseInit({
      gamesCollectionName,
      gameSeriesCollectionName,
      playerMappingsCollectionName,
    }),
    csurfCookieName,
    startGameTerminationTopic,
    pubSubClient = pubSubInit({ topicName: startGameTerminationTopic }),
  } = config;

  middlewares.baseMiddleware({ router, requestLogging });
  routers.publicRoute({ router });
  middlewares.pugMiddleware({ router, lang, files });
  middlewares.csurfMiddleware({ router, csurfCookieName });

  routers.terminateRoute({
    router: middlewares.terminateMiddleware({ router }),
    controller: controllers.terminateController({ database, pubSubClient }),
  });

  app.set('views', viewLocalFolder);
  app.set('view engine', viewEngine);

  app.use(appRoute, router);

  return app;
};

module.exports = initialize;
