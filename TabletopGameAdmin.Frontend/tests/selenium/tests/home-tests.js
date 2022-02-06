const HomeIndex = require('../pages/home/home-index');
const seleniumHelper = require('../selenium-helper');

// iterate browsers
seleniumHelper.browsers.forEach((browser) => {
  describe(`${browser}`, () => {
    // init driver
    before(async function before() {
      await seleniumHelper.before(this, browser);
    });

    // quit driver
    after(async function after() {
      await seleniumHelper.after(this);
    });

    // ierate window sizes
    seleniumHelper.sizes.forEach((size) => {
      // set window size
      before(async function before() {
        await seleniumHelper.before(this, browser, size);
      });

      describe(`${browser}: ${JSON.stringify(size)}`, () => {
        describe('app', () => {
          describe('routes', () => {
            describe('home.js', () => {
              it('GET /', async function check() {
                await (new HomeIndex(this.driver).waitForPage());
              });
            });
          });
        });
      });
    });
  });
});
