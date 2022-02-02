require('chai').should();
const express = require('express');
const supertest = require('supertest');

const route = require('../../../../app/routes/public');

describe('app', () => {
  before(function before() {
    this.app = express();
    this.app.use('/', route());
  });

  describe('routes', () => {
    describe('public.js', () => {
      it('GET /public/client.min.js', async function check() {
        const response = await supertest(this.app)
          .get('/public/client.min.js');
        response.status.should.equal(200);
        response.header['content-type'].should.equal('application/javascript; charset=UTF-8');
      });

      it('GET /public/main.css', async function cssCheck() {
        const response = await supertest(this.app)
          .get('/public/main.css');
        response.status.should.equal(200);
        response.header['content-type'].should.equal('text/css; charset=UTF-8');
      });

      it('GET /public/main.min.css', async function cssCheck() {
        const response = await supertest(this.app)
          .get('/public/main.min.css');
        response.status.should.equal(200);
        response.header['content-type'].should.equal('text/css; charset=UTF-8');
      });

      it('GET /public/main.css.map', async function cssCheck() {
        const response = await supertest(this.app)
          .get('/public/main.css.map');
        response.status.should.equal(200);
        response.header['content-type'].should.equal('application/json; charset=UTF-8');
      });

      it('GET /public/main.min.css.map', async function cssCheck() {
        const response = await supertest(this.app)
          .get('/public/main.min.css.map');
        response.status.should.equal(200);
        response.header['content-type'].should.equal('application/json; charset=UTF-8');
      });
    });
  });
});
