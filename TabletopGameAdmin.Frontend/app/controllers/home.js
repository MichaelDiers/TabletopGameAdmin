/**
 * Initialize the home controller.
 * @param {object} options The initialization options.
 * @param {string} options.title The title of the view.
 * @returns {object} The home initialized controller.
 */
const initialize = (options = {}) => {
  const {
    title = 'TabletopGameAdmin',
  } = options;

  const controller = {
    home: async (req, res) => {
      res.render('home/index', { title });
    },
  };

  return controller;
};

module.exports = initialize;
