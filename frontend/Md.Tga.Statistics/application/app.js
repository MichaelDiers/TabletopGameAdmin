const express = require('express');

const controllers = require('./controllers/controllers');
const middlewares = require('./middlewares/middlewares');
const routers = require('./routers/routers');
const databaseInit = require('./services/database');

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
    baseName,
    gatewayAddress,
    viewEngine,
    viewLocalFolder,
    appRoute,
    requestLogging,
    // pug
    lang,
    files,
    gamesCollectionName,
    gameSeriesCollectionName,
    gameStatusCollectionName,
    playerMappingsCollectionName,
    gameTerminationResultsCollectionName,
    database = databaseInit({
      gamesCollectionName,
      gameSeriesCollectionName,
      gameStatusCollectionName,
      playerMappingsCollectionName,
      gameTerminationResultsCollectionName,
    }),
  } = config;

  middlewares.baseMiddleware({ router, requestLogging });
  routers.publicRoute({ router });
  middlewares.pugMiddleware({
    router, lang, files, baseName, gatewayAddress,
  });

  routers.footerRoute({
    router,
    controller: controllers.footerController(),
  });

  routers.headerRoute({
    router,
    controller: controllers.headerController(),
  });

  routers.statisticsRoute({
    router: middlewares.statisticsMiddleware({ router }),
    controller: controllers.statisticsController({ database }),
  });

  app.set('views', viewLocalFolder);
  app.set('view engine', viewEngine);

  app.use(appRoute, router);

  return app;
};

module.exports = initialize;
