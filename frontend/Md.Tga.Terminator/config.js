const initialize = (options = {}) => {
  const {
    appRoute = '/',
    baseName,
    gatewayAddress = '/gateway',
    requestLogging = false,
    publicLocalFolder = './application/public',
    publicRoute = '/public',
    viewEngine = 'pug',
    viewLocalFolder = './application/views',
    css = [],
    js = [],
    lang = 'de',
    gamesCollectionName,
    gameSeriesCollectionName,
    playerMappingsCollectionName,
    startGameTerminationTopic,
  } = options;

  const cssFiles = [`${gatewayAddress}/${baseName}${publicRoute}/${baseName}.min.css`, ...css];
  const jsFiles = [`${gatewayAddress}/${baseName}${publicRoute}/${baseName}.min.js`, ...js];
  const favicon = `${gatewayAddress}/${baseName}${publicRoute}/favicon.ico`;
  const csurfCookieName = `_csrf_${baseName}`;

  const config = {
    appRoute,
    gatewayAddress,
    publicLocalFolder,
    publicRoute,
    requestLogging,
    viewEngine,
    viewLocalFolder,
    files: {
      css: cssFiles,
      js: jsFiles,
      favicon,
    },
    lang,
    gamesCollectionName,
    gameSeriesCollectionName,
    playerMappingsCollectionName,
    csurfCookieName,
    startGameTerminationTopic,
  };

  return config;
};

module.exports = initialize;
