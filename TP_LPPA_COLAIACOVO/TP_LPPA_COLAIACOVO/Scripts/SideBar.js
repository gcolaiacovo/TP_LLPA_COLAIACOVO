function showSection(sectionId) {
    // Ocultar todas las secciones
    var sections = document.getElementsByClassName('section');
    for (var i = 0; i < sections.length; i++) {
        sections[i].classList.remove('active');
    }

    // Mostrar la sección seleccionada
    var section = document.getElementById(sectionId);
    if (section) {
        section.classList.add('active');
    }
}