const { Builder } = require('selenium-webdriver');

/**
 * Constant for fullscreen browser window size.
 */
const fullscreen = 'fullscreen';

/**
 * Mocha after hook.
 * @param runner Mocha runner.
 */
const after = async (runner) => {
  await runner.driver.quit();
};

/**
 * Mocha bedore hook.
 * @param runner Mocha runner.
 * @param {string} browser Name of the browser.
 * @param {object|string} size The browser window size or the string 'fullscreen'.
 * @param {number} size.height The browser window height.
 * @param {number} size.width The browser window width.
 */
const before = async (runner, browser, size) => {
  const mochaRunner = runner;
  if (!mochaRunner.driver) {
    mochaRunner.timeout(5000);
    mochaRunner.driver = await new Builder().forBrowser(browser).build();
  }

  if (size) {
    if (size === fullscreen) {
      await mochaRunner.driver.manage().window().fullscreen();
    } else {
      const {
        height,
        width,
        x = 0,
        y = 0,
      } = size;
      await mochaRunner.driver.manage().window().setRect({
        width, height, x, y,
      });
    }
  }
};

/**
 * A set of predefined brower window sizes.
 */
const sizes = [
  fullscreen,
  { width: 360, height: 720 },
  { width: 600, height: 1000 },
  { width: 1000, height: 1200 },
  { width: 1200, height: 1200 },
];

/**
 * Helper for selenium tests.
 */
module.exports = {
  browsers: ['firefox', 'chrome'],
  sizes,
  after,
  before,
  fullscreen,
  baseAddress: 'http://localhost:3000',
};
