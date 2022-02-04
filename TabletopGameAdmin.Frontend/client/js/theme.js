const handleTheme = () => {
  const body = document.querySelector('body');

  // add .light to body
  document.querySelectorAll('.theme-light').forEach((element) => {
    element.addEventListener('click', () => {
      body.classList.add('light');
    });
  });

  // remove .light from body
  document.querySelectorAll('.theme-dark').forEach((element) => {
    element.addEventListener('click', () => {
      body.classList.remove('light');
    });
  });

  // if menu button is used then close the menu
  document.querySelectorAll('.nav .theme-light, .nav .theme-dark').forEach((element) => {
    element.addEventListener('click', () => {
      document.querySelector('header input').checked = false;
    });
  });
};

handleTheme();
