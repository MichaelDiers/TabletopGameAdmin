const { Router } = require('express');

/**
 * Initializes the middleware for setting pug values.
 * @param {object} config A configuration object.
 * @param {Router} config.router An express router.
 * @param {Router} config.lang The html language value.
 * @param {Router} config.files Address of js, css and favicon files.
 * @returns The given router if set in config, a new express router otherwise.
 */
const initialize = (config = {}) => {
  const {
    router = Router(),
    lang,
    files,
    baseName,
    gatewayAddress,
  } = config;

  router.use((req, res, next) => {
    res.locals.pubLocals = {
      imageUrl: `${gatewayAddress}/${baseName}/public/peace-gd81eb6308_1280.jpg`,
      lang,
      files,
    };

    next();
  });

  return router;
};

module.exports = initialize;
