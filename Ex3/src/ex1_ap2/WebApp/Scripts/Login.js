$("#btnSign").click(function (event) {
    event.preventDefault();
    var apiUrl = "/api/Users"
    var usrInfo = {
        Name: $("#usrName").val(),
        Email: "mkl",
        Password: $("#usrPsw").val(),
        Losts: 0,
        Wins: 0
    };
        $.post(apiUrl + "/CheckPassword", usrInfo)
            .done(function (data) {
                alert("login successful");
                sessionStorage.setItem("currentUser", usrInfo.Name);

                window.location.href = "MainPage.html";
            })
            .fail(function (jqXHR, textStatus, err) {
                alert("invalid password or user doesn't exist");
            })
})