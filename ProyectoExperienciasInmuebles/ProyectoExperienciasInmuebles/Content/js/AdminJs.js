let pos = 1;

function rotacion() {
    pos++;
    if (pos > 3) pos = 1;
    document.getElementById("banner").src = "/Content/image/banner" + pos + ".jpg";
    document.getElementById("banner").style.opacity = 1;
    document.getElementById("banner").style.transition = "2s";
    setTimeout(opacidad, 1000);
}

function opacidad() {
    document.getElementById("banner").style.opacity = 0.6;
    document.getElementById("banner").style.transition = "2s";
    setTimeout(rotacion, 1000);
}

// Iniciar la rotación al cargar
setTimeout(opacidad, 1000);