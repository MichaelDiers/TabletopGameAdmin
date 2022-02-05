const { By, until } = require('selenium-webdriver');

/**
 * Base page for all pages used in selenium context.
 */
class BasePage {
  /**
   * Creates a new instance of BasePage.
   * @param {selenium-webdriver.ThenableWebDriver} driver The selenium web driver.
   * @param {string} id The id of the page.
   */
  constructor(driver, id) {
    this.driver = driver;
    this.id = id;
  }

  /**
   * Waits until the page with this.id is displayed.
   */
  async waitForPage() {
    await this.driver.wait(until.elementLocated(By.id(this.id)));
  }
}

module.exports = BasePage;
