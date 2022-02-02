const express = require('express');

/**
 * Initialize the public route for css and js files.
 * @param {object} options The options for initializing the route.
 * @param {string} options.publicLocalFolder The local folder for public files.
 * @param {string} options.publicRoute The url for public files.
 * @param {express.Router} options.router The router for handling the route.
 * @param {boolean} options.showIndexOfPublic True indicates that an index is
 *  show for the public url and false otherwise.
 */
const initialize = (options = {}) => {
  const {
    publicLocalFolder = './app/public',
    publicRoute = '/public',
    router = express.Router(),
    showIndexOfPublic = false,
  } = options;

  const statics = express.static(publicLocalFolder, { index: showIndexOfPublic });
  router.use(publicRoute, statics);

  return router;
};

module.exports = initialize;
