/**
 * Data used in pug view engine context.
 */
module.exports = {
  files: {
    stylesheets: ['../public/main.min.css'],
    jss: ['../public/client.min.js'],
  },
  header: {
    title: 'Tabletop Game Admin',
  },
  lang: 'de',
  menu: [
    {
      name: 'Dark Theme',
      link: '',
      id: '',
      class: 'theme-dark',
    },
    {
      name: 'Light Theme',
      link: '',
      id: '',
      class: 'theme-light',
    },
  ],
};
