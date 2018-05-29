//default settings
document.getElementById("mazeRows").value = localStorage.mazeRows;
document.getElementById("mazeCols").value = localStorage.mazeCols;
document.getElementById("selectAlg").value = localStorage.searchAlgo;

$("#btnStartGame").click(function () {
    document.getElementById("loader").style.display = "block";

    var apiUrl = "../api/Maze"
    var name = $("#mazeName").val();
    var rows = $("#mazeRows").val();
    var cols = $("#mazeCols").val();

    $.getJSON(apiUrl + "/" + name + "/" + rows + "/" + cols).done(function (maze) {
        var myMazeBoard = $("#myMazeCanvas").MazeBoard(maze.Name, maze.Maze, maze.Rows, maze.Cols, maze.Start.Row, maze.Start.Col,
            maze.End.Row, maze.End.Col, startImg, endImg, myMazeCanvas, "single");
        myMazeBoard.Draw();
        document.getElementById("loader").style.display = "none";
        document.title = name;
    })
});