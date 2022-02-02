require('chai').should();
const supertest = require('supertest');

const app = require('../../../app/app');

describe('app', () => {
  before(function before() {
    this.app = app();
  });

  describe('app.js', () => {
    it('GET /public/client.min.js', async function check() {
      const response = await supertest(this.app)
        .get('/public/client.min.js');
      response.status.should.equal(200);
      response.header['content-type'].should.equal('application/javascript; charset=UTF-8');
    });
  });
});
