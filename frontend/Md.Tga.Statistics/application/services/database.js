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
    gameStatusCollectionName,
    playerMappingsCollectionName,
    gameTerminationResultsCollectionName,
  } = config;

  const database = {

    /**
     * Reads games by the game series id.
     * @param {object} options An options object.
     * @param {string} options.parentDocumentId The id of the game series parent document.
     * @returns An array of games.
     */
    readGames: async (options) => {
      const {
        parentDocumentId,
      } = options;

      const snapshot = await firestore.collection(gamesCollectionName)
        .where('parentDocumentId', '==', parentDocumentId).get();
      const documents = [];
      if (snapshot.size > 0) {
        snapshot.forEach((documentSnapshot) => {
          const document = documentSnapshot.data();
          document.documentId = documentSnapshot.id;
          document.created = document.created.toDate();
          documents.push(document);
        });
      }

      return documents;
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
        document.documentId = documentId;
        document.created = document.created.toDate();
        return document;
      }

      return null;
    },

    readGameStatus: async (options) => {
      const { gameIds } = options;
      const snapshot = await firestore.collection(gameStatusCollectionName).get();
      const documents = [];
      if (snapshot.size > 0) {
        snapshot.forEach((documentSnapshot) => {
          const document = documentSnapshot.data();
          if (gameIds.includes(document.parentDocumentId)) {
            document.documentId = documentSnapshot.id;
            document.created = document.created.toDate();
            documents.push(document);
          }
        });
      }

      return documents;
    },

    /**
     * Read player mappings of a game.
     * @param {object} options An options object.
     * @param {string} options.parentDocumentId The id of the parent document.
     * @returns A player mappings object if the document exists and null otherwise.
     */
    readPlayerMappings: async (options) => {
      const {
        gameIds,
      } = options;

      const querySnapshot = await firestore.collection(playerMappingsCollectionName).get();
      const documents = [];
      if (querySnapshot.size > 0) {
        querySnapshot.forEach((snapshot) => {
          const document = snapshot.data();
          if (gameIds.includes(document.parentDocumentId)) {
            document.documentId = snapshot.id;
            document.created = document.created.toDate();
            documents.push(document);
          }
        });
      }

      return documents;
    },

    readGameTerminations: async (options) => {
      const { gameIds } = options;
      const querySnapshot = await firestore.collection(gameTerminationResultsCollectionName).get();
      const documents = [];
      if (querySnapshot.size > 0) {
        querySnapshot.forEach((snapshot) => {
          const document = snapshot.data();
          document.documentId = snapshot.id;
          document.created = document.created.toDate();
          if (gameIds.includes(document.parentDocumentId)) {
            documents.push(document);
          }
        });
      }

      return documents;
    },
  };

  return database;
};

module.exports = initialize;
