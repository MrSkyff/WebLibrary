const formEditCategory = document.getElementById('categoryModalWindow');

formEditCategory.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;
    const name = button.getAttribute('data-name');
    const description = button.getAttribute('data-description');
    const url = button.getAttribute('data-url');
    const id = button.getAttribute('data-id');


    // Populate the modal inputs with the data
    const nameInput = formEditCategory.querySelector('#Name');
    const urlInput = formEditCategory.querySelector('#Url');
    const descriptionInput = formEditCategory.querySelector('#Description_form');
    const idInput = formEditCategory.querySelector('#Id');

    nameInput.value = name;
    urlInput.value = url;
    descriptionInput.textContent = description;
    idInput.value = id;
});