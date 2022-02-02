const home = require('./home');
const publicRouter = require('./public');

/**
 * Export the available routers.
 */
module.exports = {
  home,
  public: publicRouter,
};
