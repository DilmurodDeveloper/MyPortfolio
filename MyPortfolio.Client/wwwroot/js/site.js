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

window.hideLoader = () => {
    const loader = document.getElementById('loader');
    const app = document.getElementById('app');

    if (loader && app) {
        loader.classList.add('fade-out');

        setTimeout(() => {
            loader.style.display = 'none';
            app.style.removeProperty("display"); 
            document.body.classList.remove('loading');
            document.body.classList.add('ready');
        }, 600);
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

window.initParticles = () => {
    const tryLoad = () => {
        const container = document.getElementById('particles-js');

        if (typeof particlesJS !== 'undefined' && container) {
            particlesJS.load('particles-js', 'js/particles-config.json', function () {
                console.log('Particles config loaded successfully!');
            });
        } else {
            console.warn("particlesJS or container not ready, retrying...");
            setTimeout(tryLoad, 200); 
        }
    };

    tryLoad();
};

window.showLoader = () => {
    const loader = document.getElementById('loader');
    const app = document.getElementById('app');

    if (loader && app) {
        loader.style.removeProperty("display");
        loader.classList.remove('fade-out');
        loader.style.opacity = '1';

        document.body.classList.add('loading');
        document.body.classList.remove('ready');
    }
};

window.setupSectionAnimations = () => {
    const sections = document.querySelectorAll('.snap-section');

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
            } else {
                entry.target.classList.remove('visible');
            }
        });
    }, {
        threshold: 0.2
    });

    sections.forEach(section => observer.observe(section));
};

window.scrollToHash = () => {
    const hash = window.location.hash;
    if (hash) {
        const el = document.querySelector(hash);
        if (el) {
            setTimeout(() => {
                el.scrollIntoView({ behavior: 'smooth' });
            }, 400);
        }
    }
};

window.enableScrollHashUpdate = () => {
    const sections = document.querySelectorAll('.snap-section');
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting && entry.target.id) {
                history.replaceState(null, null, `#${entry.target.id}`);
            }
        });
    }, { threshold: 0.6 });

    sections.forEach(section => observer.observe(section));
};
