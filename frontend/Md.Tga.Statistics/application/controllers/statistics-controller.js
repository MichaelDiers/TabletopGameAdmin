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

const createWinningHistoryBySide = ({ gameSeries, winningSideIds }) => {
  const counter = {};
  gameSeries.sides.forEach(({ id }) => { counter[id] = 0; });
  winningSideIds.forEach(({ winningSideId }) => {
    if (winningSideId) {
      counter[winningSideId] += 1;
    }
  });

  const entries = [];
  for (let i = Math.max(...Object.values(counter)); i > 0; i -= 1) {
    gameSeries.sides.forEach(({ id }) => {
      const entry = { value: '' };
      if (counter[id] === i) {
        entry.value = i;
        entry.classs = 'victory table-header';
      } else if (counter[id] > i) {
        entry.class = 'victory';
      }

      entries.push(entry);
    });
  }

  gameSeries.sides.forEach(({ name }) => { entries.push({ value: name, class: 'table-header' }); });
  return { entries, cols: gameSeries.sides.length };
};

const createWinningHistory = ({ gameSeries, playerMappings, winningSideIds }) => {
  const counter = {};
  gameSeries.players.forEach(({ id }) => { counter[id] = 0; });

  winningSideIds.forEach(({ gameId, winningSideId }) => {
    const { playerCountryMappings } = playerMappings.find(
      ({ parentDocumentId }) => parentDocumentId === gameId,
    );
    playerCountryMappings.forEach(({ countryId, playerId }) => {
      const { sideId } = gameSeries.countries.find(({ id }) => id === countryId);
      if (sideId === winningSideId) {
        counter[playerId] += 1;
      }
    });
  });

  const data = [];
  for (let i = Math.max(...Object.values(counter)); i > 0; i -= 1) {
    gameSeries.players.forEach(({ name, id }) => {
      if (counter[id] === i) {
        data.push({ value: i, class: 'victory table-header', name });
      } else if (counter[id] > i) {
        data.push({ value: '', class: 'victory' });
      } else {
        data.push({ value: '' });
      }
    });
  }

  gameSeries.players.forEach(({ name }) => data.push({ value: name, class: 'table-header' }));
  return { entries: data, cols: gameSeries.players.length };
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
      const winningHistory = createWinningHistory(data);
      const winningHistoryBySide = createWinningHistoryBySide(data);

      const options = {
        history,
        winningHistory,
        winningHistoryBySide,
      };

      res.render('statistics/index', options);
    },
  };

  return controller;
};

module.exports = initialize;
