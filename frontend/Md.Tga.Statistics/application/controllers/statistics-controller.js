const createHistory = (data) => {
  const {
    gameSeries,
    playerMappings,
    winningSideIds,
  } = data;

  const history = [{ value: 'Game', class: 'table-header' }];
  gameSeries.players.forEach(({ name }) => {
    const element = { value: name, class: 'table-header' };
    history.push(element);
  });

  playerMappings.sort((a, b) => a.created - b.created);
  playerMappings.forEach(({ parentDocumentId, playerCountryMappings }, index) => {
    history.push({ value: index + 1, class: '' });
    const { winningSideId } = winningSideIds.find(({ gameId }) => gameId === parentDocumentId);

    gameSeries.players.forEach(({ id: playerId }) => {
      const { countryId } = playerCountryMappings.find((value) => value.playerId === playerId);
      const { sideId } = gameSeries.countries.find(({ id }) => id === countryId);
      const element = {
        value: gameSeries.countries.find(({ id }) => id === countryId).name,
        class: winningSideId === sideId ? 'win' : '',
      };

      history.push(element);
    });
  });

  return { entries: history, cols: gameSeries.players.length + 1 };
};

const readData = async ({ database, gameSeriesId }) => {
  const gameSeriesPromise = database.readGameSeries({ documentId: gameSeriesId });
  const gamesPromise = database.readGames({ parentDocumentId: gameSeriesId });

  const games = await gamesPromise;
  const gameIds = games.map(({ documentId }) => documentId);
  const gameStatusPromise = database.readGameStatus({ gameIds });
  const playerMappingsPromise = database.readPlayerMappings({ gameIds });
  const winningSideIdsPromise = database.readWinningSideIds({ gameIds });

  const gameSeries = await gameSeriesPromise;
  const gameStatus = await gameStatusPromise;
  const playerMappings = await playerMappingsPromise;
  const winningSideIds = await winningSideIdsPromise;

  return {
    gameSeries,
    games,
    gameStatus,
    playerMappings,
    winningSideIds,
  };
};

/**
 * Initializes the statistics controller.
 * @param {object} config A configuration object.
 * @param {object} config.database Access the surveys database.
 * @returns The initialized controller object.
 */
const initialize = (config = {}) => {
  const {
    database,
  } = config;

  const controller = {

    /**
     * Loads the requested survey for a participant and renders the view.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    index: async (req, res) => {
      const { gameSeriesId } = req.params;

      const data = await readData({ database, gameSeriesId });

      const history = createHistory(data);

      const options = {
        history,
      };

      res.render('statistics/index', options);
    },
  };

  return controller;
};

module.exports = initialize;
