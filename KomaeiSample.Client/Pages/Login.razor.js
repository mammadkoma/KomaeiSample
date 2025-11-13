export function closeOffcanvas() {
    const offcanvasEl = document.querySelector("#offcanvasMenu");
    const bsOffcanvas = bootstrap.Offcanvas.getInstance(offcanvasEl);
    if (bsOffcanvas) {
        bsOffcanvas.hide();
    }
}