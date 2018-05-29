$(document).ready(function () {
    var apiUrl = "../api/Users";
    $.getJSON(apiUrl).done(function (users) {
        var length = users.length;
        var counter = 0;
        var rankTable = document.getElementById("table");
        while (counter != length)
        {
            var row = rankTable.insertRow(counter + 1);
            var cell0 = row.insertCell(0)
            var cell1 = row.insertCell(1)
            var cell2 = row.insertCell(2)
            var cell3 = row.insertCell(3)
            cell0.innerHTML = counter;
            cell1.innerHTML = users[counter].Name;
            cell2.innerHTML = users[counter].Wins;
            cell3.innerHTML = users[counter].Losts;
            counter = counter + 1;
        }
    })
});
