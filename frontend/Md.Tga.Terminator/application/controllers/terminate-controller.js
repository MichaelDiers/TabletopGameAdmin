const uuid = require('uuid');

const readData = async ({
  database, gameId, terminationId, winningSideId,
}) => {
  const gameStatusPromise = database.isClosed({ documentId: gameId });
  const gamePromise = database.readGame({ documentId: gameId });
  const playerMappingsPromise = database.readPlayerMappings({ parentDocumentId: gameId });

  const gameStatusIsClosed = await gameStatusPromise;
  if (gameStatusIsClosed === true) {
    return { valid: false, view: 'terminate/closed' };
  }

  const game = await gamePromise;
  if (!game || game.gameTerminations.every((gt) => gt.terminationId !== terminationId)) {
    return { valid: false, view: 'terminate/unknown' };
  }

  const gameSeriesPromise = database.readGameSeries({ documentId: game.parentDocumentId });

  const gameTermination = game.gameTerminations.find((gt) => gt.terminationId === terminationId);

  const gameSeries = await gameSeriesPromise;
  if (!gameTermination || !gameSeries) {
    return { valid: false, view: 'terminate/unknown' };
  }

  const player = gameSeries.players.find((p) => p.id === gameTermination.playerId);

  const playerMappings = await playerMappingsPromise;
  if (!player || !playerMappings) {
    return { valid: false, view: 'terminate/unknown' };
  }

  const playerMapping = playerMappings.playerCountryMappings.find(
    ({ playerId }) => playerId === player.id,
  );

  const country = gameSeries.countries.find(
    ({ id }) => id === playerMapping.countryId,
  );

  if (!playerMapping || !country) {
    return { valid: false, view: 'terminate/unknown' };
  }

  if (winningSideId && gameSeries.sides.every(({ id }) => winningSideId !== id)) {
    return { valid: false, view: 'terminate/unknown' };
  }

  return {
    game,
    gameSeries,
    gameStatusIsClosed,
    playerMapping,
    country,
    player,
    valid: true,
  };
};

/**
 * Initializes the terminate controller.
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

      const data = await readData({ gameId, terminationId, database });
      const { valid, view } = data;
      if (valid === false) {
        res.render(view);
      } else {
        const {
          country: {
            name: countryName,
          },
          game: {
            name: gameName,
          },
          gameSeries: {
            sides,
          },
          player: {
            name: playerName,
          },
        } = data;
        const options = {
          playerName,
          countryName,
          gameId,
          gameName,
          terminationId,
          formId: uuid.v4(),
          sides,
        };

        res.render('terminate/index', options);
      }
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
        reason,
      } = req.body;
      const data = await readData({
        gameId, terminationId, database, winningSideId,
      });
      const { valid, view } = data;
      if (valid === false) {
        res.render(view);
      } else {
        const {
          gameSeries: {
            documentId: gameSeriesId,
          },
        } = data;

        await pubSubClient.publish({
          gameSeriesId,
          gameId,
          terminationId,
          winningSideId,
          reason,
        });

        res.render(
          'terminate/thankyou',
          {
            pushStateUrl: '../../thankyou',
          },
        );
      }
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
