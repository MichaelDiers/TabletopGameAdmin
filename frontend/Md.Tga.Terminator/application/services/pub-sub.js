const { PubSub } = require('@google-cloud/pubsub');
const uuid = require('uuid');

const pubSubClient = new PubSub();

/**
 * Initializes a pub/sub client.
 * @param {object} options An options object.
 * @param {string} options.topicName The name of the pub/sub topic.
 * @returns A new pub/sub client.
 */
const initialize = (options) => {
  const { topicName } = options;

  const client = {
    /**
     * Publish a message to pub/sub.
     * @param {object} message The message that is sent.
     * @param {string} message.gameSeriesId The id of the game series.
     * @param {string} message.gameId The id of the game.
     * @param {Array} message.terminationId The termination id of the player.
     * @param {Array} message.winningSideId The side that should win the game.
     */
    publish: async (message) => {
      const {
        gameSeriesId,
        gameId,
        terminationId,
        winningSideId,
        reason,
      } = message;

      const json = `{"processId":"${uuid.v4()}","gameSeriesId":"${gameSeriesId}","gameId":"${gameId}","terminationId":"${terminationId}","winningSideId":"${winningSideId}","reason":"${reason}"}`;

      const data = Buffer.from(json);
      await pubSubClient
        .topic(topicName)
        .publishMessage({ data });
    },
  };

  return client;
};

module.exports = initialize;
