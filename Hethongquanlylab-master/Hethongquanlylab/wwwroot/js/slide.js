let slideIndex = 0;
showSlides();


function showSlides() {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    slideIndex++;
    if (slideIndex == slides.length) { slideIndex = 1}

    slides[(slideIndex - 2 + 7) % 7].style.display = "inline-block";
    slides[(slideIndex - 2 + 7) % 7].style.float = "left";

    slides[(slideIndex - 1 + 7) % 7].style.display = "inline-block";
    slides[(slideIndex - 1 + 7) % 7].style.float = "center";

    slides[slideIndex].style.display = "inline-block";
    slides[slideIndex].style.float = "right";
    setTimeout(showSlides, 2000); // Change image every 2 seconds
}

