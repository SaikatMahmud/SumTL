﻿function UploadImage(id) {
    console.log("function called " + id);
    var file = $('#inputFile')[0].files[0];
    if (file == null) {
        alert("Select an image first!");
        return;
    }
    var formData = new FormData();
    formData.append("file", file);

    $.ajax({
        url: "/Item/ImageUpload/" + id,
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            alert("Image uploaded successfully for item " + id);
            location.reload();
        },
        error: function (error) {
            alert("Internal server error!");
        }
    });
}

function UploadItems() {
    console.log("function called ");
    var file = $('#inputFile')[0].files[0];
    if (file == null) {
        alert("Select an excel file first!");
        return;
    }
    var formData = new FormData();
    formData.append("file", file);
    alert("Please click OK and wait for the success message. Do not close the tab.");
    $.ajax({
        url: "/Item/UploadItemBulk/",
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            alert("Items uploaded successfuly");
            window.location.href = "/Item/Index";
        },
        error: function (error) {
            alert("Internal server error!");
        }
    });
}


function UploadCategories() {
    console.log("function called ");
    var file = $('#inputFile')[0].files[0];
    if (file == null) {
        alert("Select an excel file first!");
        return;
    }
    var formData = new FormData();
    formData.append("file", file);
    alert("Please click OK and wait for the success message. Do not close the tab.");
    $.ajax({
        url: "/Category/UploadCategoryBulk/",
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            alert("Catagories uploaded successfuly");
            window.location.href = "/Category/Index";
        },
        error: function (error) {
            alert("Internal server error!");
        }
    });
}

function DeleteImage(imageId) {
    $.ajax({
        url: "/Item/DeleteImage/" + imageId,
        type: 'DELETE',
        contentType: false,
        processData: false,
        success: function (response) {
            alert("Image deleted successfuly!");
            location.reload();
        },
        error: function (error) {
            alert("Internal server error!");
        }
    });
}
