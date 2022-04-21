const baseMiddleware = require('./base-middleware');
const csurfMiddleware = require('./csurf-middleware');
const pugMiddleware = require('./pug-middleware');
const terminateMiddleware = require('./terminate-middleware');

module.exports = {
  baseMiddleware,
  csurfMiddleware,
  pugMiddleware,
  terminateMiddleware,
};
