const { Router } = require('express');
const uuid = require('uuid');
const { param, validationResult } = require('express-validator');

const isUuid = (chain) => chain.exists().custom(
  (value) => uuid.validate(value) && uuid.version(value) === 4,
);

const middlewareFunc = (req, res, next) => {
  const errors = validationResult(req);
  if (!errors.isEmpty()) {
    res.render('statistics/unknown');
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
    '/game-series/:gameSeriesId',
    isUuid(param('gameSeriesId')),
    middlewareFunc,
  );

  return router;
};

module.exports = initialize;
