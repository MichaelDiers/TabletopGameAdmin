const BasePage = require('../base-page');

/**
 * Describes the home index page.
 */
class HomeIndex extends BasePage {
  /**
   * Creates a new instance of HomeIndex.
   * @param {selenium-webdriver.ThenableWebDriver} driver The selenium web driver.
   */
  constructor(driver) {
    super(driver, 'acfa9f25-b1ee-4219-9e1d-2f20b11b889a');
  }
}

module.exports = HomeIndex;
