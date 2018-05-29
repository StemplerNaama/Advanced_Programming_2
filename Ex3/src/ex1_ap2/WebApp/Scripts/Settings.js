//default settings
document.getElementById("mazeRows").value = localStorage.mazeRows;
document.getElementById("mazeCols").value = localStorage.mazeCols;
document.getElementById("selectAlg").value = localStorage.searchAlgo;

$("#btnSave").click(function () {
    localStorage.setItem("mazeRows", $("#mazeRows").val());
    localStorage.setItem("mazeCols", $("#mazeCols").val());
    var selectVal = document.getElementById("selectAlg").value;
    localStorage.setItem("searchAlgo", selectVal);
});