window.scrollLock = {
    isLocked: false
};

window.setScrollLock = (shouldLock) => {
    window.scrollLock.isLocked = shouldLock;

    if (shouldLock) {
        document.body.classList.add('nav-open');
    } else {
        document.body.classList.remove('nav-open');
    }
};

window.scrollCarousel = (element, amount) => {
    element.scrollBy({ left: amount, behavior: 'smooth' });
};

window.addEventListener('load', function () {
    setTimeout(() => {
        window.hideLoader();
    }, 1000);
});

window.hideLoader = () => {
    const loader = document.getElementById('loader');
    const app = document.getElementById('app');
    const particlesContainer = document.getElementById('particles-js');

    if (loader) {
        loader.classList.add('hidden');

        setTimeout(() => {
            loader.style.display = 'none';
            if (app) app.style.display = 'block';

            document.body.classList.remove('loading');
            document.body.classList.add('ready');
        }, 500);
    }
};

window.setupScrollTopButton = () => {
    const btn = document.getElementById("scrollTopBtn");

    window.addEventListener("scroll", () => {
        if (window.scrollY > 300) {
            btn.removeAttribute("hidden");
        } else {
            btn.setAttribute("hidden", "true");
        }
    });
};

window.scrollToTop = () => {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
};
