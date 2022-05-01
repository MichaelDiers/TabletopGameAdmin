const { Router } = require('express');
const uuid = require('uuid');
const { body, param, validationResult } = require('express-validator');

const isUuid = (chain) => chain.exists().custom(
  (value) => uuid.validate(value) && uuid.version(value) === 4,
);

const middlewareFunc = (req, res, next) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    res.render('terminate/unknown');
  } else {
    next();
  }
};

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

  router.use(
    '/game/:gameId/:terminationId',
    isUuid(param('gameId')),
    isUuid(param('terminationId')),
    middlewareFunc,
  );

  router.use(
    '/submit',
    isUuid(body('gameId')),
    isUuid(body('terminationId')),
    isUuid(body('winningSideId')),
    body('reason').escape(),
    body('rounds').exists().isInt({gt: 0, lt: 40 }),
    middlewareFunc,
  );

  return router;
};

module.exports = initialize;
