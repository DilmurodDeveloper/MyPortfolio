window.addEventListener('load', function () {
    setTimeout(() => {
        window.hideLoader();
    }, 1000);
});

window.hideLoader = () => {
    const loader = document.getElementById('loader');
    const app = document.getElementById('app');

    if (loader) loader.style.display = 'none';
    if (app) app.style.display = 'block';

    document.body.classList.remove('loading');
    document.body.classList.add('ready');
};
