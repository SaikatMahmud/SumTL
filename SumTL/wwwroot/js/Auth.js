
function AddUser() {
    console.log("form data called");
    var formData = new FormData(document.getElementById("RegForm"));
    console.log(...formData);

    const DoValidation = () => {
        debugger;
        if (
            !formData.get("UserName") ||
            !formData.get("PasswordHash") ||
            !formData.get("Email")
        ) {
            alert('Please input required field(s).');
            return;
        }
        Execute(formData);
    };

    const Execute = (formData) => {
        debugger;
        $.ajax({
            url: '/Auth/Register/',
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
                alert("Registration Success!");
                window.location.href = "/Home/Index";
            },
            error: function (error) {
                debugger;
                alert("Internal server error!");
            }
        });
    };
    DoValidation();
}

function Login() {
    console.log("form data called");
    var formData = new FormData(document.getElementById("LoginForm"));
    console.log(...formData);

    const DoValidation = () => {
        debugger;
        if (
            !formData.get("UserName") ||
            !formData.get("PasswordHash")
        ) {
            alert('Please input required field(s).');
            return;
        }
        Execute(formData);
    };

    const Execute = (formData) => {
        debugger;
        $.ajax({
            url: '/Auth/Login/',
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
                window.location.href = "/Home/Index";
            },
            error: function (error) {
                debugger;
                alert("Internal server error!");
            }
        });
    };
    DoValidation();
}


function Logout() {
    $.ajax({
        url: '/Auth/Logout/',
        type: 'POST',
        success: function (response) {
            window.location.href = "/Home/Index";
        },
        error: function (error) {
            alert("Internal server error!");
        }
    });
};


