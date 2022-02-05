const { Builder } = require('selenium-webdriver');
const HomeIndex = require('../pages/home/home-index');

['firefox', 'chrome'].forEach((browser) => {
  describe(browser, () => {
    before(async function before() {
      this.timeout(5000);
      this.driver = await new Builder().forBrowser(browser).build();
    });

    after(async function after() {
      await this.driver.quit();
    });

    describe('app', () => {
      describe('routes', () => {
        describe('home.js', () => {
          it('GET /', async function check() {
            await this.driver.get('http://localhost:3000');
            await (new HomeIndex(this.driver).waitForPage());
          });
        });
      });
    });
  });
});
