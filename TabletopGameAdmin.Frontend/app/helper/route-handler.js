/**
 * Initialize an error handler for routes.
 * @param {object} options An object containing the initialization options.
 * @param {function} options.logger A function that logs the error: (error) => {}
 * @returns {function} An async function that takes an express.Request,
 *  express.Response and an async function that takes an express.Request and
 *  and express.Response.
 */
const initialize = (options = {}) => {
  const {
    logger = console.error,
  } = options;

  const handle = async (req, res, asynFunc) => {
    try {
      await asynFunc(req, res);
    } catch (error) {
      logger(error);
    }
  };

  return handle;
};

module.exports = initialize;
