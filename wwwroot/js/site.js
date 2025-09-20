document.addEventListener("DOMContentLoaded", () => {
    const toggle = document.querySelector('[data-doc-toggle]');
    const sidebar = document.querySelector('[data-doc-sidebar]');

    if (!toggle || !sidebar) {
        return;
    }

    const closeSidebar = () => {
        sidebar.classList.remove('is-open');
        toggle.setAttribute('aria-expanded', 'false');
        document.body.style.overflow = '';
    };

    const openSidebar = () => {
        sidebar.classList.add('is-open');
        toggle.setAttribute('aria-expanded', 'true');
        document.body.style.overflow = 'hidden';
    };

    toggle.addEventListener('click', () => {
        const isOpen = sidebar.classList.contains('is-open');
        if (isOpen) {
            closeSidebar();
        } else {
            openSidebar();
        }
    });

    sidebar.querySelectorAll('a').forEach(link => {
        link.addEventListener('click', () => {
            if (window.innerWidth <= 1024) {
                closeSidebar();
            }
        });
    });
});
