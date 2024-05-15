$(document).ready(function () {
    LoadList();
});


const renderList = (obj) => {
    const template = $("#category_list").html();
    const compiledTemplate = Handlebars.compile(template);
    debugger;
    const html = compiledTemplate({ rows: obj });
    $(".render_data").html(html);
};

var LoadList = function () {
    console.log("executed");
    $.ajax({
        url: '/Category/GetAll',
        type: 'get',
        success: function (data) {
            debugger;
            if (data.result && data.result.length > 0) {
                renderList(data.result);
            } else {
                $(".render_data").html("Category List is empty!");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}
