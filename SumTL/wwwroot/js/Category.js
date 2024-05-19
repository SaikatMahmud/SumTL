
//function renderList(obj){ //handlebar
//    const template = $("#category_list").html();
//    const compiledTemplate = Handlebars.compile(template);
//    debugger;
//    const html = compiledTemplate({ rows: obj });
//    $(".render_data").html(html);
//};


//function renderTable(obj) { //datatable
//    debugger;
//    $('#tblData').DataTable({
//        data: obj,
//        columns: [
//            { data: 'id' },
//            { data: 'categoryName' },
//            {
//                data: 'id', render: function (data, type, row, meta) {
//                    return type === 'display' ?
//                        '<button type="button" onclick="Edit(' + data + ')" class="btn btn-warning">Edit</button>' +
//                        '<button type="button" onclick="Delete(' + data + ')" class="btn btn-danger" > Delete</button>' : data;
//                }
//            }
//            //{ defaultContent: '<button type="button" onclick="Edit("+data.id+")" class="btn btn-warning">Edit</button>' }

//        ]
//    });
//};


//function LoadList() { // for datatable
//    console.log("executed");
//    $.ajax({
//        url: '/Category/GetAll',
//        type: 'GET',
//        success: function (response) {
//            debugger;
//            renderTable(response.data);
//        },
//        error: function (error) {
//            alert("Internal server error!");
//        }
//    });
//}


function LoadList() {
    $('#tblData').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Category/GetAllCustomized",
            "type": "GET",
            "data": function (d) {
                d.search = d.search.value;
            }
        },
        "columns": [
            { data: 'id' },
            { data: 'categoryName' },
            {
                data: 'id', render: function (data, type, row, meta) {
                    return type === 'display' ?
                        '<button type="button" onclick="Edit(' + data + ')" class="btn btn-warning">Edit</button>' +
                        '<button type="button" onclick="Delete(' + data + ')" class="btn btn-danger" > Delete</button>' : data;
                }
            },
        ],
        "language": {
            "searchPlaceholder": "Search with name"
        }
    });
}

//function LoadList() {
//    console.log("executed");
//    $.ajax({
//        url: '/Category/GetAll',
//        type: 'GET',
//        success: function (response) {
//            debugger;
//            if (response.data && response.data.length > 0) {
//                renderList(response.data);
//            } else {
//                $(".render_data").html("Category List is empty!");
//            }
//        },
//        error: function (error) {
//            alert("Internal server error!");
//        }
//    });
//}


function Edit(id) {
    console.log("executed");
    window.location.href = "/Category/Edit/" + id;
}

function SaveFormData() {
    console.log("form data called");
    var formData = new FormData(document.getElementById("CategoryForm"));
    formData.append("Id", $("#Id").val())
    console.log(...formData);

    const DoValidation = () => {
        debugger;
        if (
            !formData.get("CategoryName")
        ) {
            alert('Please input required field(s).');
            return;
        }
        Execute(formData);
    };

    const Execute = (formData) => {
        debugger;
        $.ajax({
            url: '/Category/Edit/',
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
                window.location.href = "/Category/Index";
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
    var formData = new FormData(document.getElementById("CategoryForm"));
    console.log(...formData);

    const DoValidation = () => {
        debugger;
        if (
            !formData.get("CategoryName")
        ) {
            alert('Please input required field(s).');
            return;
        }
        Execute(formData);
    };

    const Execute = (formData) => {
        debugger;
        $.ajax({
            url: '/Category/Create/',
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
                alert("Category created!");
                window.location.href = "/Category/Index";
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
        url: '/Category/Delete/'+id,
        type: 'DELETE',
        success: function (response) {
            debugger;
            if (response.msg != null) {
                alert(response.msg);
                return;
            }
            alert("Category deleted!");
            window.location.href = "/Category/Index";
        },
        error: function (error) {
            debugger;
            alert("Internal server error!");
        }
    });
}
