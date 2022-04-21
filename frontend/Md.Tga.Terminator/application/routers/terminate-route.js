const { Router } = require('express');

/**
 * Initialize the index route.
 * @param {object} config A configuration object.
 * @param {Router} config.router An express router.
 * @returns The given router if set in config, a new express router otherwise.
 */
const initialize = (config = {}) => {
  const {
    controller,
    router = Router(),
  } = config;

  router.get('/game/:gameId/:terminationId', controller.index);
  router.post('/submit', controller.submit);
  return router;
};

module.exports = initialize;
