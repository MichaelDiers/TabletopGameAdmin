const { Firestore } = require('@google-cloud/firestore');

const firestore = new Firestore();

/**
 * Intialize a database object.
 * @param {config} config A configuration object.
 * @param {string} config.gamesCollectionName The name of the collection to be accessed.
 * @returns The intialized database.
 */
const initialize = (config) => {
  const {
    gamesCollectionName,
    gameSeriesCollectionName,
    playerMappingsCollectionName,
  } = config;

  const database = {
    /**
     * Read a game by its document id.
     * @param {object} options An options object.
     * @param {string} options.documentId The id of the game document.
     * @returns A game object if the document exists and null otherwise.
     */
    readGame: async (options) => {
      const {
        documentId,
      } = options;

      const snapshot = await firestore.collection(gamesCollectionName).doc(documentId).get();
      if (snapshot.exists) {
        const document = snapshot.data();
        return document;
      }

      return null;
    },

    /**
     * Read a game series by its document id.
     * @param {object} options An options object.
     * @param {string} options.documentId The id of the game series document.
     * @returns A game series object if the document exists and null otherwise.
     */
    readGameSeries: async (options) => {
      const {
        documentId,
      } = options;

      const snapshot = await firestore.collection(gameSeriesCollectionName).doc(documentId).get();
      if (snapshot.exists) {
        const document = snapshot.data();
        return document;
      }

      return null;
    },

    /**
     * Read player mappings of a game.
     * @param {object} options An options object.
     * @param {string} options.parentDocumentId The id of the parent document.
     * @returns A player mappings object if the document exists and null otherwise.
     */
    readPlayerMappings: async (options) => {
      const {
        parentDocumentId,
      } = options;

      const querySnapshot = await firestore.collection(playerMappingsCollectionName).where('parentDocumentId', '==', parentDocumentId).limit(1).get();
      if (querySnapshot.size === 1) {
        const results = [];
        querySnapshot.forEach((snapshot) => results.push(snapshot.data()));
        return results[0];
      }

      return null;
    },
  };

  return database;
};

module.exports = initialize;
