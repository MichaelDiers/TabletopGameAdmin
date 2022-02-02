require('chai').should();
const express = require('express');
const supertest = require('supertest');

const route = require('../../../../app/routes/home');

describe('app', () => {
  before(function before() {
    this.app = express();
    this.app.use('/', route());
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
