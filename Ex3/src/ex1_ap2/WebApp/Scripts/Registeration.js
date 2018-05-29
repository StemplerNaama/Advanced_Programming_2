$("#btnSign").click(function (event) {
    event.preventDefault();
    var apiUrl = "../api/Users"
    var usrInfo = {
        Name: $("#usrName").val(),
        Email: $("#usrEmail").val(),
        Password: $("#usrPsw").val(),
        Losts: 0,
        Wins: 0      
    };

    $.getJSON(apiUrl + "/get/" + usrInfo.Name).done(function (data) {
        console.log(JSON.stringify(data));
        alert("the user already exists in the database");
    })
        .fail(function (jqXHR, textStatus, err) {
            if ($("#usrPsw").val() == $("#usrPswRepeat").val()) {
                $.post(apiUrl, usrInfo)
                    .done(function () {
                    alert("user registered successfully");
                    sessionStorage.setItem("currentUser", usrInfo.Name);
                    window.location.href = "MainPage.html";
                });
            } else {
                alert("different passwords");
            }


        })
})