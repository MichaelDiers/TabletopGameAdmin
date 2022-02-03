const compression = require('compression');
const cookieParser = require('cookie-parser');
const csurf = require('csurf');
const express = require('express');
const helmet = require('helmet');

/**
 * Initialize the basic middleware.
 * @param {object} options The intialization options.
 * @returns {express.Router} The express router that is initialized.
 */
const initialize = (options = {}) => {
  const {
    router = express.Router(),
  } = options;

  router.use(helmet());
  router.use(compression());
  router.use(express.urlencoded({ extended: false }));
  router.use(express.json());
  router.use(cookieParser());

  const csurfProtection = csurf({ cookie: true });
  router.use(csurfProtection);
  router.use((req, res, next) => {
    res.locals.csurfToken = req.csrfToken();
    next();
  });

  return router;
};

module.exports = initialize;
