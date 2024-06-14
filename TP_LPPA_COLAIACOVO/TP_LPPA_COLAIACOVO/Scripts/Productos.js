document.querySelectorAll(".carousel").forEach((carousel) => {
    var carouselInner = carousel.querySelector(".carousel-inner");
    var items = carouselInner.querySelectorAll(".carousel-item");

    var totalItems = items.length;
    var itemWidth = items[0].offsetWidth + 15;
    var currentIndex = 0;
    var intervalId = null; // Variable para almacenar el ID del intervalo del carousel

    function showNext() {
        currentIndex++;
        if (currentIndex >= totalItems) {
            currentIndex = 0; // Volver al principio si se alcanza el final
        }
        updateCarousel();
    }

    function showPrev() {
        currentIndex--;
        if (currentIndex < 0) {
            currentIndex = totalItems - 1; // Si se está en el primer elemento, ir al último
        }
        updateCarousel();
    }

    function updateCarousel() {
        var newPosition = -currentIndex * itemWidth;
        carouselInner.style.transform = "translateX(" + newPosition + "px)";
    }

    function startCarousel() {
        if (totalItems <= 5) {
            return;
        }

        if (intervalId === null) { // Verifica si ya hay un temporizador en ejecución
            intervalId = setInterval(showNext, 5000); // Cambia de slide cada 3 segundos
        }
    }

    function stopCarousel() {
        clearInterval(intervalId);
        intervalId = null; // Restablece la variable del temporizador a null
    }

    // Iniciar el carousel
    startCarousel();

    // Agrega los eventos onmouseover y onmouseout a los elementos del carousel
    items.forEach(function (item) {
        item.addEventListener("mouseover", function () {
            showPopup(this);
        });

        item.addEventListener("mouseout", function () {
            hidePopup();
        });
    });
});
// Si hay menos de 5 elementos, el carousel no se mueve
var timer;

//function showPopup(element) {
//    timer = setTimeout(function () {
//        var popup = document.getElementById("popup");
//        popup.style.display = "block";

//        var rect = element.getBoundingClientRect();
//        var popupWidth = popup.offsetWidth;
//        var windowWidth = window.innerWidth;

//        if ((rect.right + popupWidth + 10) > windowWidth) {
//            popup.style.top = rect.top + "px";
//            popup.style.left = (rect.left - popupWidth - 10) + "px"; // 10px de espacio entre el producto y el popup
//        } else {
//            popup.style.top = rect.top + "px";
//            popup.style.left = (rect.right + 10) + "px"; // 10px de espacio entre el producto y el popup
//        }
//        stopCarousel(); // Detiene el carousel cuando se muestra el popup
//    }, 1000);
//}

//function hidePopup() {
//    // Cancela el temporizador cuando se quita el mouse del producto
//    clearTimeout(timer);
//    // Oculta el popup
//    var popup = document.getElementById("popup");
//    popup.style.display = "none";
//    startCarousel(); // Reinicia el carousel cuando se oculta el popup
//}