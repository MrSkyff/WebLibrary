const categoryFormReader = document.querySelector("#formCategory");

//Submit category creation
categoryFormReader.addEventListener("submit", function (event) {
    event.preventDefault();

    const category = {
        Name: document.querySelector("#Name").value,
        Url: document.querySelector("#Url").value,
        Description: document.querySelector("#Description_form").value,
        LibraryId: libraryId,
        Id: document.querySelector("#Id").value || 0,
        UserId: userId,
        ParentId: document.querySelector("#parentCategoryId").value,

    };

    if (category.Id === 0) {

        //######### Add ############
        if (nameIsPassed && urlIsPassed) {

            fetch("/Library/CategoryManagerApi/CreateCategory", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8", },
                body: JSON.stringify(category),
            })
                .then((response) => response.json())
                .then((data) => {

                    if (data) {
                        $("#CategoryList").empty();
                        $("#categoryModalWindow").modal("hide");
                        $("#formCategory input").val("");
                        $("#formCategory textarea").val("");
                        categoryListByLibraryLoad();
                        nameIsPassed = false;
                        urlIsPassed = false;
                    }
                })
                .catch((error) => console.error(error));

        } else {
            $("#FormIsNotComplited").text("Form is not complited");
            if (!urlIsPassed) { $("#Url-check").text("Enter a valid url."); }
            if (!nameIsPassed) { $("#Name-check").text("Enter a valid name."); }
        }
        //######### Add END ############

    } else if (category.Id > 0) {

        //######### Edit ############

        checkName(category.Name);

        const promiseUrlResult = checkCategoryUrl(category.Url, category.LibraryId, category.Id);

        promiseUrlResult.then((urlIsPassed) => {

            if (nameIsPassed === true && urlIsPassed === true) {

                fetch("/Library/CategoryManagerApi/UpdateCategory", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8", },
                    body: JSON.stringify(category),
                })
                    .then((response) => response.json())
                    .then((data) => {

                        if (data) {
                            $("#CategoryList").empty();
                            $("#categoryModalWindow").modal("hide");
                            $("#formCategory input").val("");
                            $("#formCategory textarea").val("");
                            categoryListByLibraryLoad();
                            nameIsPassed = false;
                            urlIsPassed = false;
                        }
                    })
                    .catch((error) => console.error(error));

            } else {
                $("#FormIsNotComplited").text("Form is not complited");
                if (!urlIsPassed) { $("#Url-check").text("Invalid URL: must be at least 2 characters, and must contain only letters, numbers, '-', '+', or '_'."); }
                if (!nameIsPassed) { $("#Name-check").text("Invalid Name: must be at least 2 characters."); }
            }

        });
        //######### Edit END ############

    }

});
