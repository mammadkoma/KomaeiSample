function showCurrentDateTime() {
    let date = Date.now();
    let dayOfWeek = new Intl.DateTimeFormat("fa", { weekday: "long" }).format(date);
    let day = new Intl.DateTimeFormat("fa", { day: "numeric" }).format(date);
    let monthName = new Intl.DateTimeFormat("fa", { month: "long" }).format(date);
    let fullDate = new Intl.DateTimeFormat("fa", { year: "numeric", month: "2-digit", day: "2-digit" }).format(date);
    document.getElementById("persian-date-line1").innerHTML = `امروز : ${dayOfWeek} ${day} ${monthName}`;
    document.getElementById("persian-date-line2").textContent = fullDate;
}

document.addEventListener("DOMContentLoaded", function () {
    showCurrentDateTime();
});

function isDesktop() {
    return window.innerWidth >= 768;
}

function getWindowHeight() {
    return window.innerHeight;
}

window.downloadFile = (fileName, contentType, byteArray) => {
    const blob = new Blob([new Uint8Array(byteArray)], { type: contentType });
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement("a");
    anchorElement.href = url;
    anchorElement.download = fileName ?? "download.pdf";
    anchorElement.click();
    URL.revokeObjectURL(url);
};

window.scrollToTop = () => {
    var element = document.getElementById('topPage');
    if (element)
        element.scrollIntoView();
};