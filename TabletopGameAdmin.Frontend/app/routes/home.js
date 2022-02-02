const express = require('express');

const homeController = require('../controllers/home');
const routeHandler = require('../helper/route-handler');

/**
 * Initialize the home route.
 * @param {object} options The options for initializing the route.
 * @param {express.Router} options.router The router for handling the route.
 */
const initialize = (options = {}) => {
  const {
    controller = homeController(),
    handler = routeHandler(),
    router = express.Router(),
  } = options;

  router.get('/', async (req, res) => handler(req, res, controller.home));

  return router;
};

module.exports = initialize;
