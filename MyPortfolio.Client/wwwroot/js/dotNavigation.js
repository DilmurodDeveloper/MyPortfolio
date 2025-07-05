window.initScrollListener = (dotNetHelper) => {
    const sections = document.querySelectorAll('.snap-section');

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                dotNetHelper.invokeMethodAsync('UpdateActiveSection', entry.target.id);
            }
        });
    }, {
        root: null,
        rootMargin: '0px',
        threshold: 0.6
    });

    sections.forEach(section => observer.observe(section));
};

window.scrollToSection = (id) => {
    const el = document.getElementById(id);
    if (el) {
        el.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
};
