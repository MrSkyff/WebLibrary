//Update parent category for modal window
//Before add and before open for edit
function categoryUpdate() {

    fetch('/Library/CategoryManagerApi/GetCategoryListByLibrary?libraryUrl=' + libraryUrl + '&userId=' + userId, {
        method: "GET",
    })
        .then(response => response.json())
        .then(data => {

            const selectElement = $("#parentCategoryId");
            $("#parentCategoryId").empty();
            selectElement.append($('<option>', { value: 0, text: 'No parent category' }));

            data = data.filter(category => category.parentId === 0);
            data.forEach(category => {

                selectElement.append($('<option>', { value: category.id, text: category.name }));

            });
        })
        .catch(error => {
            console.error('Error fetching categories:', error);
        });
}

//Update categories for each can, and form field checks.
function categoryUpdateForEdit(parentId, categoryUrl = '0') {

    $("#Url-check").text("");
    $("#Name-check").text("");

    fetch('/Library/CategoryManagerApi/GetCategoryListByLibrary?libraryUrl=' + libraryUrl + '&userId=' + userId, {
        method: "GET",
    })
        .then(response => response.json())
        .then(data => {

            const selectElement = $("#parentCategoryId");
            $("#parentCategoryId").empty();
            selectElement.append($('<option>', { value: 0, text: 'No parent category' }));

            data = data.filter(category => category.parentId === 0);
            data.forEach(category => {

                if (categoryUrl !== category.url) {
                    if (parentId === category.id) {
                        selectElement.append($(`<option value="${category.id}" selected>${category.name}</option>`));
                    } else {
                        selectElement.append($(`<option value="${category.id}">${category.name}</option>`));
                    }
                }

            });
        })
        .catch(error => {
            console.error('Error fetching categories:', error);
        });
}