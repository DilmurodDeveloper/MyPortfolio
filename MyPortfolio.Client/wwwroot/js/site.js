window.scrollLock = {
    isLocked: false
};

window.observeAndStoreSection = () => {
    const sections = document.querySelectorAll('.snap-section');
    const dots = {
        home: document.getElementById('dot-home'),
        about: document.getElementById('dot-about'),
        skills: document.getElementById('dot-skills'),
        education: document.getElementById('dot-education'),
        experience: document.getElementById('dot-experience')
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const id = entry.target.id;
                sessionStorage.setItem("activeSection", id);
                history.replaceState(null, null, `#${id}`);

                Object.values(dots).forEach(dot => dot.classList.remove('active'));
                if (dots[id]) dots[id].classList.add('active');
            }
        });
    }, {
        threshold: 0.6
    });

    sections.forEach(section => observer.observe(section));
};

window.setupScrollSnap = () => {
    const sections = document.querySelectorAll('.snap-section');
    let isScrolling = false;
    let currentIndex = 0;

    const scrollTo = (i) => {
        if (window.scrollLock.isLocked) return;

        if (i >= 0 && i < sections.length) {
            const id = sections[i].id;
            sections[i].scrollIntoView({ behavior: 'smooth' });
            history.replaceState(null, null, `#${id}`);
            sessionStorage.setItem("activeSection", id);
            currentIndex = i;
        }
    };

    const goToHash = () => {
        const hash = window.location.hash.substring(1);
        if (hash) {
            sections.forEach((s, i) => {
                if (s.id === hash) currentIndex = i;
            });
            const section = document.getElementById(hash);
            if (section) section.scrollIntoView({ behavior: 'auto' });
        }
    };

    window.addEventListener('wheel', (e) => {
        if (isScrolling || window.scrollLock.isLocked) return;

        isScrolling = true;
        if (e.deltaY > 0) scrollTo(currentIndex + 1);
        else scrollTo(currentIndex - 1);

        setTimeout(() => isScrolling = false, 1000);
    }, { passive: true });

    let touchY = 0;
    window.addEventListener('touchstart', e => touchY = e.touches[0].clientY);
    window.addEventListener('touchend', e => {
        if (window.scrollLock.isLocked) return;

        const deltaY = touchY - e.changedTouches[0].clientY;
        if (Math.abs(deltaY) > 50 && !isScrolling) {
            isScrolling = true;
            if (deltaY > 0) scrollTo(currentIndex + 1);
            else scrollTo(currentIndex - 1);
            setTimeout(() => isScrolling = false, 1000);
        }
    });

    window.addEventListener("popstate", goToHash);

    goToHash();
};

window.scrollToSectionById = (id) => {
    if (window.scrollLock.isLocked) return;

    const section = document.getElementById(id);
    if (section) {
        section.scrollIntoView({ behavior: 'smooth' });
        history.replaceState(null, null, `#${id}`);
        sessionStorage.setItem("activeSection", id);
    }
};

window.setScrollLock = (shouldLock) => {
    window.scrollLock.isLocked = shouldLock;

    if (shouldLock) {
        document.body.classList.add('nav-open');
    } else {
        document.body.classList.remove('nav-open');
    }
};
