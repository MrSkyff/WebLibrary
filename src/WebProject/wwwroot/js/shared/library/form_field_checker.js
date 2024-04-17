// Checked field controll data
let nameIsPassed = false;
let urlIsPassed = false;

//Check for Name
$("#Name").on("blur", nameChecker);
//Check for URL
$("#Url").on("blur", urlChecker);


//Name checker
function nameChecker() {
    //const name = $(this).val();
    const name = $("#Name").val();

    if (!/.{2,}/.test(name)) {
        $("#Name-check").text("Invalid Name: must be at least 2 characters");
        nameIsPassed = false;
    } else {
        $("#Name-check").text("Name is ok.");
        nameIsPassed = true;
    }
}

//Url checker
function urlChecker() {

    //const url = $(this).val();
    const url = $("#Url").val();
    const projectId = $("#Id").val();

    if (!/^[A-Za-z0-9-+_]{2,}$/.test(url)) {
        $("#Url-check").text("Invalid URL: must be at least 2 characters, and must contain only letters, numbers, '-', '+', or '_'.");
    }
    else {
        fetch("/Library/LibraryManagerApi/CheckIsUrlExist?url=" + url + "&UserId=" + userID + "&LibraryId=" + projectId)
            .then((response) => response.json())
            .then((data) => {

                if (data.status == true) {
                    $("#Url-check").text("Url already exists.");
                    urlIsPassed = false;
                }
                else {
                    $("#Url-check").text("Url is ok.");
                    urlIsPassed = true;
                }
            });
    }
}

function formChecker() {
    if (!urlIsPassed || !nameIsPassed) { $("#FormIsNotComplited").text("Form is not complited."); }
    else { $("#FormIsNotComplited").text(""); }
    if (!urlIsPassed) { $("#Url-check").text("Enter a valid url."); }
    if (!nameIsPassed) { $("#Name-check").text("Enter a valid name."); }
}