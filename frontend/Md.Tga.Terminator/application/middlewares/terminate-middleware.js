const { Router } = require('express');
const uuid = require('uuid');

/**
 * Initializes the middleware for the index route.
 * @param {object} config A configuration object.
 * @param {Router} config.router An express router.
 * @returns The given router if set in config, a new express router otherwise.
 */
const initialize = (config = {}) => {
  const {
    router = Router(),
  } = config;

  router.use('/game/:gameId/:terminationId', (req, res, next) => {
    const { gameId, terminationId } = req.params;
    if (gameId
      && uuid.validate(gameId)
      && uuid.version(gameId) === 4
      && terminationId
      && uuid.validate(terminationId)
      && uuid.version(terminationId) === 4) {
      next();
    } else {
      res.render('terminate/unknown');
    }
  });

  router.use('/submit', (req, res, next) => {
    const { gameId, terminationId, winningSideId } = req.body;
    if (gameId
      && uuid.validate(gameId)
      && uuid.version(gameId) === 4
      && terminationId
      && uuid.validate(terminationId)
      && uuid.version(terminationId) === 4
      && winningSideId
      && uuid.validate(winningSideId)
      && uuid.version(winningSideId) === 4) {
      next();
    } else {
      res.render('terminate/unknown');
    }
  });

  return router;
};

module.exports = initialize;
