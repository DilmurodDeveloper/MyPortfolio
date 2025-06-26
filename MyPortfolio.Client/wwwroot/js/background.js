window.initializeBackground = () => {
    const starCanvas = document.getElementById("starCanvas");
    const starCtx = starCanvas?.getContext("2d");

    const particleCanvas = document.getElementById("particleCanvas");
    const pCtx = particleCanvas?.getContext("2d");

    const iconsSrc = [
        "/images/dotnet.png",
        "/images/csharp.png",
        "/images/sqlserver.png",
        "/images/github.png",
    ];

    const numStars = 120;
    const numParticles = 25;
    const sizes = [20, 26, 35];
    const speed = 0.6;

    let stars = [];
    let particles = [];
    let icons = [];

    let mouseX = null, mouseY = null;

    function resize() {
        if (starCanvas) {
            starCanvas.width = window.innerWidth;
            starCanvas.height = window.innerHeight;
        }
        if (particleCanvas) {
            particleCanvas.width = window.innerWidth;
            particleCanvas.height = window.innerHeight;
        }
    }

    function createStars() {
        stars = Array.from({ length: numStars }, () => ({
            x: Math.random() * starCanvas.width,
            y: Math.random() * starCanvas.height,
            r: Math.random() * 1.2,
            alpha: 0.5 + Math.random() * 0.5,
            dx: (Math.random() - 0.5) * 0.3,
            dy: (Math.random() - 0.5) * 0.3,
        }));
    }

    function animateStars() {
        starCtx.clearRect(0, 0, starCanvas.width, starCanvas.height);
        for (let s of stars) {
            starCtx.beginPath();
            starCtx.arc(s.x, s.y, s.r, 0, Math.PI * 2);
            starCtx.fillStyle = `rgba(255,255,255,${s.alpha})`;
            starCtx.fill();
            s.x += s.dx;
            s.y += s.dy;
            if (s.x < 0 || s.x > starCanvas.width) s.dx *= -1;
            if (s.y < 0 || s.y > starCanvas.height) s.dy *= -1;
        }
        requestAnimationFrame(animateStars);
    }

    function loadIcons() {
        return Promise.all(
            iconsSrc.map(src => new Promise(res => {
                const img = new Image();
                img.src = src;
                img.onload = () => res(img);
            }))
        );
    }

    function createParticles() {
        particles = Array.from({ length: numParticles }, () => {
            const size = sizes[Math.floor(Math.random() * sizes.length)];
            const angle = Math.random() * Math.PI * 2;
            return {
                x: Math.random() * particleCanvas.width,
                y: Math.random() * particleCanvas.height,
                size,
                speedX: Math.cos(angle) * speed,
                speedY: Math.sin(angle) * speed,
                angle: 0,
                rotateSpeed: (Math.random() - 0.5) * 0.02,
                img: icons[Math.floor(Math.random() * icons.length)],
                opacity: 0.9
            };
        });
    }

    function animateParticles() {
        pCtx.clearRect(0, 0, particleCanvas.width, particleCanvas.height);
        for (let p of particles) {
            p.x += p.speedX;
            p.y += p.speedY;
            p.angle += p.rotateSpeed;

            if (p.x < p.size) {
                p.x = p.size;
                p.speedX = Math.abs(p.speedX);
            } else if (p.x > particleCanvas.width - p.size) {
                p.x = particleCanvas.width - p.size;
                p.speedX = -Math.abs(p.speedX);
            }
            if (p.y < p.size) {
                p.y = p.size;
                p.speedY = Math.abs(p.speedY);
            } else if (p.y > particleCanvas.height - p.size) {
                p.y = particleCanvas.height - p.size;
                p.speedY = -Math.abs(p.speedY);
            }

            pCtx.save();
            pCtx.globalAlpha = p.opacity;
            pCtx.translate(p.x, p.y);
            pCtx.rotate(p.angle);
            pCtx.drawImage(p.img, -p.size / 2, -p.size / 2, p.size, p.size);
            pCtx.restore();
        }

        requestAnimationFrame(animateParticles);
    }

    async function init() {
        resize();
        createStars();
        animateStars();
        icons = await loadIcons();
        createParticles();
        animateParticles();

        window.addEventListener("resize", () => {
            resize();
            createStars();
            createParticles();
        });

        document.addEventListener("mousemove", e => {
            mouseX = e.clientX;
            mouseY = e.clientY;
        });

        document.addEventListener("mouseleave", () => {
            mouseX = null;
            mouseY = null;
        });
    }

    if (starCanvas && starCtx && particleCanvas && pCtx) {
        init();
    }
};
