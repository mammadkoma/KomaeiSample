export function onLoad() {
    const closeLinks = document.querySelectorAll("#offcanvasMenu .nav-link");
    const offcanvasEl = document.querySelector("#offcanvasMenu");
    if (!offcanvasEl) return;
    closeLinks.forEach(link => {
        link.addEventListener("click", () => {
            const bsOffcanvas = bootstrap.Offcanvas.getInstance(offcanvasEl);
            if (bsOffcanvas) {
                bsOffcanvas.hide();
            }
        });
    });

    const exitButton = document.querySelector("#offcanvasMenu #exitButton");
    if (exitButton) {
        exitButton.addEventListener("click", () => {
            const bsOffcanvas = bootstrap.Offcanvas.getInstance(offcanvasEl);
            if (bsOffcanvas) {
                bsOffcanvas.hide();
            }
        });
    }
}