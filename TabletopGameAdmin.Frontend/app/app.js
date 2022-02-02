const express = require('express');

const routers = require('./routes/index');

/**
 * Initialize the express app.
 * @returns {express.Express} A new express application.
 */
const initialize = () => {
  const router = express.Router();

  router.use('/', routers.public());

  const app = express();
  app.use(router);
  return app;
};

module.exports = initialize;
