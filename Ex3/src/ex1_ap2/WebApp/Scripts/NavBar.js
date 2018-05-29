function multiBtn() {
    if (sessionStorage.getItem("currentUser") == null) {
        alert("only registered player can play the Multi game")
        window.location.href = "mainPage.html";
    }
    else {
        window.location.href = "multiGame.html";
    }
}

function registerBtn() {
    if (sessionStorage.getItem("currentUser") != null) {
        alert("You are already registered");
        window.location.href = "mainPage.html";
    }
    else {
        window.location.href = "register.html";
    }
}
if (sessionStorage.getItem("currentUser") != null) {
    $("#login").text("Log off");
    $("#register").text("Hello " + sessionStorage.getItem("currentUser"));

    $("#login").click(function () {
        sessionStorage.removeItem("currentUser");
        $("#login").text("Login");
        $("#register").text("Register");
    })
}
