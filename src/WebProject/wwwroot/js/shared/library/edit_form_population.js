const formEditLibrary = document.getElementById('libraryModalWindow');

formEditLibrary.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;
    const name = button.getAttribute('data-name');
    const description = button.getAttribute('data-description');
    const url = button.getAttribute('data-url');
    const id = button.getAttribute('data-id');


    // Populate the modal inputs with the data
    const nameInput = formEditLibrary.querySelector('#Name');
    const urlInput = formEditLibrary.querySelector('#Url');
    const descriptionInput = formEditLibrary.querySelector('#Description');
    const idInput = formEditLibrary.querySelector('#Id');

    nameInput.value = name;
    urlInput.value = url;
    descriptionInput.value = description;
    idInput.value = id;
});