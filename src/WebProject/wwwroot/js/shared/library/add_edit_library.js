const libraryFormReader = document.querySelector("#libraryModalForm");

const idChecker = document.querySelector("#Id").value;

libraryFormReader.addEventListener("submit", function (event) {
    event.preventDefault();

    const libraryDataProject = {
        Name: document.querySelector("#Name").value,
        Url: document.querySelector("#Url").value,
        Description: document.querySelector("#Description").value,
        Id: document.querySelector("#Id").value || 0,
        UserId: userID,
    };

    //Check for Name
    nameChecker();
    //Check for URL
    urlChecker();

    if (libraryDataProject.Id === 0) {

        //########## Add new ###########
        if (nameIsPassed && urlIsPassed) {

            fetch("/Library/LibraryManagerApi/CreateProject", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8", },
                body: JSON.stringify(libraryDataProject),
            })
                .then((response) => response.json())
                .then((data) => {
                    if (data) {
                        $("#LibraryList").empty();
                        $("#libraryModalWindow").modal("hide");
                        $("#libraryModalForm input").val("");
                        $("#libraryModalForm textarea").val("");
                        contentLoad();
                        nameIsPassed = false;
                        urlIsPassed = false;
                    }
                })
                .catch((error) => console.error(error));

        } else {
            formChecker();
        }
        //#####################

    } else if (libraryDataProject.Id > 0) {

        //######### Edit ############
        if (nameIsPassed === true && urlIsPassed === true) {

            fetch("/Library/LibraryManagerApi/UpdateProject", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8", },
                body: JSON.stringify(libraryDataProject),
            })
                .then((response) => response.json())
                .then((data) => {
                    if (!data) {
                        $("#LibraryList").empty();
                        $("#libraryModalWindow").modal("hide");
                        contentLoad();
                        nameIsPassed = false;
                        urlIsPassed = false;
                    }
                })
                .catch((error) => console.error(error));

        } else {
            formChecker();
        }
        //#####################

    }

});