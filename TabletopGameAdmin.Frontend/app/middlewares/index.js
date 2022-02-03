const base = require('./base');
const csurf = require('./csurf');
const pug = require('./pug');

/**
 * Export all middleware functions.
 */
module.exports = {
  base,
  csurf,
  pug,
};
