require('chai').should();
const supertest = require('supertest');

const app = require('../../../../app/app');

describe('app', () => {
  before(function before() {
    this.app = app();
  });

  describe('routes', () => {
    describe('home.js', () => {
      it('GET /', async function check() {
        const response = await supertest(this.app)
          .get('/');
        response.status.should.equal(200);
        response.header['content-type'].should.equal('text/html; charset=utf-8');
      });
    });
  });
});
