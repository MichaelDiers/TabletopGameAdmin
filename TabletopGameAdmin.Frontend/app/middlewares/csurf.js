const csurf = require('csurf');
const express = require('express');

/**
 * Initialize the CSRF middleware.
 * @param {object} options The intialization options.
 * @returns {express.Router} The express router that is initialized.
 */
const initialize = (options = {}) => {
  const {
    router = express.Router(),
  } = options;

  const csurfProtection = csurf({ cookie: true });
  router.use(csurfProtection);
  router.use((req, res, next) => {
    res.locals.csurfToken = req.csrfToken();
    next();
  });

  return router;
};

module.exports = initialize;
