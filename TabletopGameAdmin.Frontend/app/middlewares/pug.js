const express = require('express');

const data = require('../data/view');

/**
 * Initialize the pug view engine data.
 * @param {object} options The intialization options.
 * @returns {express.Router} The express router that is initialized.
 */
const initialize = (options = {}) => {
  const {
    router = express.Router(),
  } = options;

  router.use((req, res, next) => {
    res.locals.pugData = data;
    next();
  });

  return router;
};

module.exports = initialize;
