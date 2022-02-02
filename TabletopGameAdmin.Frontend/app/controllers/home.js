/**
 * Initialize the home controller.
 * @returns {object} The home initialized controller.
 */
const initialize = () => {
  const controller = {
    home: async (req, res) => {
      res.send('hello world').end();
    },
  };

  return controller;
};

module.exports = initialize;
