const formatData = (data, baseData, cols, captions) => {
  const formatted = { entries: [], cols };
  for (let i = Math.max(...Object.values(data)); i > 0; i -= 1) {
    baseData.forEach(({ id }) => {
      if (data[id] === i) {
        formatted.entries.push({ value: i, class: 'victory table-header' });
      } else if (data[id] > i) {
        formatted.entries.push({ value: '', class: 'victory table-header' });
      } else {
        formatted.entries.push({ value: '', class: '' });
      }
    });
  }

  captions.forEach((value) => { formatted.entries.push({ value, class: 'table-header' }); });
  return formatted;
};

const formatMatrixData = (data, baseDataDim1, baseDataDim2, cols, captionsDim1, captionsDim2) => {
  const formatted = { entries: captionsDim1.map((value) => ({ value, class: 'table-header' })), cols };
  baseDataDim2.forEach(({ id: dim2Id }, index) => {
    formatted.entries.push({ value: captionsDim2[index], class: '' });
    baseDataDim1.forEach(({ id: dim1Id }) => {
      formatted.entries.push({ value: data[dim1Id][dim2Id], class: '' });
    });
  });

  return formatted;
};

const evaluateData = ({ gameSeries, games }) => {
  const gameHistory = { entries: [{ value: 'Spiel', class: 'table-header' }, ...gameSeries.players.map(({ name }) => ({ value: name, class: 'table-header' })), { value: 'Runden', class: 'table-header' }], cols: gameSeries.players.length + 2 };

  const winningSidesData = {};
  gameSeries.sides.forEach(({ id }) => {
    winningSidesData[id] = 0;
  });

  const playedCountriesData = {};
  const playedSidesData = {};
  const winningPlayersData = {};
  gameSeries.players.forEach(({ id }) => {
    playedCountriesData[id] = {};
    playedSidesData[id] = {};
    winningPlayersData[id] = 0;

    gameSeries.countries.forEach(({ id: countryId }) => {
      playedCountriesData[id][countryId] = 0;
    });
    gameSeries.sides.forEach(({ id: sideId }) => {
      playedSidesData[id][sideId] = 0;
    });
  });

  games.sort((a, b) => a.created - b.created);
  games.forEach(({ winningSideId, playerCountryMappings, rounds }, index) => {
    if (winningSideId) {
      winningSidesData[winningSideId] += 1;
    }

    if (playerCountryMappings) {
      const countries = gameSeries.countries.filter(({ sideId }) => sideId === winningSideId)
        .map(({ id }) => id);
      playerCountryMappings.filter(({ countryId }) => countries.includes(countryId))
        .forEach(({ playerId }) => { winningPlayersData[playerId] += 1; });

      gameHistory.entries.push({ value: index + 1, class: '' });
      gameSeries.players.forEach(({ id }) => {
        const { countryId } = playerCountryMappings.find(({ playerId }) => playerId === id);
        playedCountriesData[id][countryId] += 1;

        const { name, sideId } = gameSeries.countries.find((country) => country.id === countryId);
        gameHistory.entries.push({ value: name, class: (sideId === winningSideId ? 'win' : '') });
        playedSidesData[id][sideId] += 1;
      });
      gameHistory.entries.push({ value: rounds, class: '' });
    }
  });

  // format data
  const playedCountries = formatMatrixData(
    playedCountriesData,
    gameSeries.players,
    gameSeries.countries,
    gameSeries.players.length + 1,
    ['', ...gameSeries.players.map(({ name }) => name)],
    gameSeries.countries.map(({ name }) => name),
  );
  const playedSides = formatMatrixData(
    playedSidesData,
    gameSeries.players,
    gameSeries.sides,
    gameSeries.players.length + 1,
    ['', ...gameSeries.players.map(({ name }) => name)],
    gameSeries.sides.map(({ name }) => name),
  );
  const winningBySide = formatData(
    winningSidesData,
    gameSeries.sides,
    gameSeries.sides.length,
    gameSeries.sides.map(({ name }) => name),
  );
  const winningByPlayers = formatData(
    winningPlayersData,
    gameSeries.players,
    gameSeries.players.length,
    gameSeries.players.map(({ name }) => name),
  );

  return {
    gameHistory,
    winningBySide,
    winningByPlayers,
    playedSides,
    playedCountries,
  };
};

const readData = async ({ database, gameSeriesId }) => {
  const gameSeriesPromise = database.readGameSeries({ documentId: gameSeriesId });
  const gamesPromise = database.readGames({ parentDocumentId: gameSeriesId });

  const games = await gamesPromise;

  const gameIds = games.map(({ documentId }) => documentId);
  const gameStatusPromise = database.readGameStatus({ gameIds });
  const playerMappingsPromise = database.readPlayerMappings({ gameIds });

  const gameStatus = await gameStatusPromise;

  gameStatus.forEach(({ rounds, winningSideId, parentDocumentId }) => {
    const game = games.find(({ documentId }) => documentId === parentDocumentId);
    if (game) {
      game.rounds = rounds;
      game.winningSideId = winningSideId;
    }
  });

  const gameSeries = await gameSeriesPromise;
  const playerMappings = await playerMappingsPromise;
  playerMappings.forEach(({ parentDocumentId, playerCountryMappings }) => {
    const game = games.find(({ documentId }) => documentId === parentDocumentId);
    if (game) {
      game.playerCountryMappings = playerCountryMappings;
    }
  });

  return {
    gameSeries,
    games,
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

      const options = evaluateData(data);
      res.render('statistics/index', options);
    },
  };

  return controller;
};

module.exports = initialize;
