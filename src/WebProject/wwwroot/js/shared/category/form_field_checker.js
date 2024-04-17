let nameIsPassed = false;
let urlIsPassed = false;

$("#Name").on("blur", function () {

    const name = $(this).val();

    if (!/.{2,}/.test(name)) {
        $("#Name-check").text("Invalid Name: must be at least 2 characters");
        nameIsPassed = false;
    } else {
        $("#Name-check").text("Name is ok.");
        nameIsPassed = true;
    }

});


$("#Url").on("blur", function () {

    const url = $(this).val();
    const categoryId = $("#Id").val();

    if (!/^[A-Za-z0-9-+_]{2,}$/.test(url)) {
        $("#Url-check").text("Invalid URL: must be at least 2 characters, and must contain only letters, numbers, '-', '+', or '_'.");
    }
    else {
        fetch("/library/categorymanagerapi/checkisurlexist?url=" + url + '&libraryid=' + libraryId + "&categoryid=" + categoryId)
            .then((response) => response.json())
            .then((data) => {

                if (data.status == true) {
                    $("#Url-check").text("url already exists.");
                    urlIsPassed = false;
                }
                else {
                    $("#Url-check").text("Url is ok.");
                    urlIsPassed = true;
                }
            });
    }

});




function checkName(_name) {
    if (!/.{2,}/.test(_name)) {
        $("#Name-check").text("Invalid Name: must be at least 2 characters");
    } else {
        //$("#Name-check").text("Name is ok.");
        nameIsPassed = true;
    }
}

//Check category url is used and match the requeriments
async function checkCategoryUrl(_url, _libraryId, _categoryId) {
    if (!/^[A-Za-z0-9-+_]{2,}$/.test(_url)) {
        $("#Url-check").text("Invalid URL: must be at least 2 characters, and must contain only letters, numbers, '-', '+', or '_'.");
    }
    else {
        try {
            const response = await fetch("/Library/CategoryManagerApi/CheckIsUrlExist?url=" + _url + '&libraryId=' + _libraryId + "&categoryId=" + _categoryId);
            const data = await response.json();

            if (data.status == true) {
                $("#Url-check").text("Url already exists.");
                return false;
            }
            else {
                $("#Url-check").text("Url is ok.");
                return true;
                //return data.status;
            }
        }
        catch (error) {
            console.error(error);
            return false;
        }
    }
}