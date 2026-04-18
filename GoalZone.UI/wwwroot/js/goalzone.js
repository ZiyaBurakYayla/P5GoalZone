document.addEventListener('DOMContentLoaded', function () {
    const liveRows = document.querySelectorAll('.live-match-row, [data-live]');
    if (liveRows.length > 0) {
        setInterval(function () {
            liveRows.forEach(row => {
                const minuteEl = row.querySelector('.minute-badge');
                if (minuteEl) {
                    const current = parseInt(minuteEl.textContent);
                    if (!isNaN(current) && current < 90) {
                        minuteEl.textContent = current + 1;
                    }
                }
            });
        }, 60000);
    }

    const navLinks = document.querySelectorAll('.gz-nav-links a');
    const currentPath = window.location.pathname.toLowerCase();
    navLinks.forEach(link => {
        const href = link.getAttribute('href');
        if (href && href !== '/' && currentPath.startsWith(href.toLowerCase())) {
            link.classList.add('active');
        }
    });
});
