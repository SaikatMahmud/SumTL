$(document).ready(function () {
    LoadList();
});

function renderList(obj){ //handlebar
    const template = $("#item_list").html();
    const compiledTemplate = Handlebars.compile(template);
    debugger;
    const html = compiledTemplate({ rows: obj });
    $(".render_data").html(html);
};

function LoadList() {
    console.log("executed");
    $.ajax({
        url: '/Item/GetAll',
        type: 'GET',
        success: function (response) {
            debugger;
            if (response.data && response.data.length > 0) {
                renderList(response.data);
            } else {
                $(".render_data").html("Item List is empty!");
            }
        },
        error: function (error) {
            alert("Internal server error!");
        }
    });
}


function Edit(id) {
    console.log("executed");
    window.location.href = "/Item/Edit/" + id;
}

function SaveFormData() {
    console.log("form data called");
    var formData = new FormData(document.getElementById("ItemForm"));
    formData.append("Id", $("#Id").val())
    console.log(...formData);

    const DoValidation = () => {
        debugger;
        if (
            !formData.get("ItemName") ||
            !formData.get("ItemUnit") ||
            !formData.get("Quantity") ||
            !formData.get("CategoryId")
        ) {
            alert('Please input required field(s).');
            return;
        }
        Execute(formData);
    };

    const Execute = (formData) => {
        debugger;
        $.ajax({
            url: '/Item/Edit/',
            type: 'PUT',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                debugger;
                if (response.msg != null){
            alert(response.msg);
            return;
                }
                alert("Update success!");
                window.location.href = "/Item/Index";
            },
            error: function (error) {
                debugger;
                alert("Internal server error!");
            }
        });
    };
    DoValidation();
}

function AddFormData() {
    console.log("form data called");
    var formData = new FormData(document.getElementById("ItemForm"));
    console.log(...formData);

    const DoValidation = () => {
        debugger;
        if (
            !formData.get("ItemName") ||
            !formData.get("ItemUnit") ||
            !formData.get("Quantity") ||
            !formData.get("CategoryId")
        ) {
            alert('Please input required field(s).');
            return;
        }
        Execute(formData);
    };

    const Execute = (formData) => {
        debugger;
        $.ajax({
            url: '/Item/Create/',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                debugger;
                if (response.msg != null) {
                    alert(response.msg);
                    return;
                }
                alert("Item created!");
                window.location.href = "/Item/Index";
            },
            error: function (error) {
                debugger;
                alert("Internal server error!");
            }
        });
    };
    DoValidation();
}

function Delete(id) {
    $.ajax({
        url: '/Item/Delete/'+id,
        type: 'DELETE',
        success: function (response) {
            debugger;
            if (response.msg != null) {
                alert(response.msg);
                return;
            }
            alert("Item deleted!");
            window.location.href = "/Item/Index";
        },
        error: function (error) {
            debugger;
            alert("Internal server error!");
        }
    });
}
