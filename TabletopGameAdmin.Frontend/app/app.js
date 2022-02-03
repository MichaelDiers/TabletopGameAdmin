const express = require('express');

const middlewares = require('./middlewares/index');
const routes = require('./routes/index');

/**
 * Initialize the express app.
 * @param {object} options The initialization options.
 * @param {string} options.viewEngine The name of the view engine.
 * @param {string} options.viewLocalFolder The local folder containing the views.
 * @returns {express.Express} A new express application.
 */
const initialize = (options = {}) => {
  const {
    viewEngine = 'pug',
    viewLocalFolder = './app/views',
  } = options;

  const router = express.Router();

  /**
   * cookie-less routes
   */
  router.use('/', routes.public());
  middlewares.base({ router });
  middlewares.pug({ router });
  router.use('/', routes.home());

  /**
   * cookies required routes
   */
  const app = express();

  app.set('views', viewLocalFolder);
  app.set('view engine', viewEngine);

  app.use(router);
  return app;
};

module.exports = initialize;
