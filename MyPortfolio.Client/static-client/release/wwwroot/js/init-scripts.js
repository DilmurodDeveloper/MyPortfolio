window.addEventListener('load', function () {
    var loader = document.getElementById('loader');
    var path = window.location.pathname.toLowerCase();

    if (path === '/admin/dashboard' || path === '/admin/login') {
        loader.style.display = 'none';
    } else {
        setTimeout(() => {
            loader.style.display = 'none';
        }, 1000);
    }
});

window.hideLoader = () => {
    const loader = document.getElementById('loader');
    const app = document.getElementById('app');

    if (loader) loader.style.display = 'none';
    if (app) app.style.display = 'block';

    document.body.classList.remove('loading');
    document.body.classList.add('ready');
};

window.setLang = (code) => {
    localStorage.setItem("lang", code);
    location.reload();
};
