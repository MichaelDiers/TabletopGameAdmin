const uuid = require('uuid');

/**
 * Initializes the vote controller.
 * @param {object} config A configuration object.
 * @param {object} config.database Access the surveys database.
 * @param {object} config.pubSubClient Access the google cloud pub/sub.
 * @returns The initialized controller object.
 */
const initialize = (config = {}) => {
  const {
    database,
    pubSubClient,
  } = config;

  const controller = {

    /**
     * Loads the requested survey for a participant and renders the view.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    index: async (req, res) => {
      const { gameId, terminationId } = req.params;

      const game = await database.readGame({ documentId: gameId });
      if (!game || game.gameTerminations.every((gt) => gt.terminationId !== terminationId)) {
        res.render('terminate/unknown');
        return;
      }

      const gameSeriesPromise = database.readGameSeries({ documentId: game.parentDocumentId });
      const playerMappingsPromise = database.readPlayerMappings({ parentDocumentId: gameId });

      const gameSeries = await gameSeriesPromise;
      const playerMappings = await playerMappingsPromise;

      if (!gameSeries || !playerMappings) {
        res.render('terminate/unknown');
        return;
      }

      const { playerId } = game.gameTerminations.find((gt) => gt.terminationId === terminationId);
      const { name: playerName } = gameSeries.players.find((player) => player.id === playerId);
      const { countryId } = playerMappings.playerCountryMappings.find(
        (mapping) => mapping.playerId === playerId,
      );
      const { name: countryName } = gameSeries.countries.find(
        (country) => country.id === countryId,
      );

      const options = {
        playerName,
        countryName,
        gameId,
        gameName: game.name,
        terminationId,
        formId: uuid.v4(),
        sides: gameSeries.sides,
      };

      res.render('terminate/index', options);
    },

    /**
     * Submit a survey result.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    submit: async (req, res) => {
      const {
        gameId,
        terminationId,
        winningSideId,
      } = req.body;

      const game = await database.readGame({ documentId: gameId });
      if (!game) {
        res.render('terminate/unknown');
        return;
      }

      const { playerId } = game.gameTerminations.find(
        (terminations) => terminations.terminationId === terminationId,
      );
      if (!playerId) {
        res.render('terminate/unknown');
        return;
      }

      const gameSeries = await database.readGameSeries({ documentId: game.parentDocumentId });
      if (!gameSeries) {
        res.render('terminate/unknown');
        return;
      }

      if (gameSeries.sides.every(({ id }) => id !== winningSideId)) {
        res.render('terminate/unknown');
        return;
      }

      await pubSubClient.publish({
        gameSeriesId: game.parentDocumentId,
        gameId,
        terminationId,
        winningSideId,
      });

      res.render(
        'terminate/thankyou',
      );
    },

    /**
     * Render the thank you for participating view.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    thankyou: async (req, res) => {
      res.render(
        'terminate/thankyou',
      );
    },
  };

  return controller;
};

module.exports = initialize;
