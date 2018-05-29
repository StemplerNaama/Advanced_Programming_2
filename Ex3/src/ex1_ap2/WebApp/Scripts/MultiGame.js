        $(document).ready(function () {
            $("#navBar").load("NavBar.html");
            document.getElementById("mazeRows").value = localStorage.mazeRows;
            document.getElementById("mazeCols").value = localStorage.mazeCols;
        });

var gameH = $.connection.gameHub;

gameH.client.getOppStep = function (direction, cellWidth, cellHeight, playerRowPos, playerColPos, exitRow, exitCol) {
        var canvasId = document.getElementById("oppMazeCanvas");
        var context = canvasId.getContext("2d");

        switch (direction) {
            case "37": //to the left
                //remove the rect

                context.clearRect(cellWidth * playerColPos,
                    cellHeight * playerRowPos, cellWidth, cellHeight);
                //update player's pos
                playerColPos = playerColPos - 1;
                //draw the player
                context.drawImage(oppStartImg, cellWidth * playerColPos,
                    cellHeight * playerRowPos, cellWidth, cellHeight);

                break;

            case "38": //up
                //remove the rect

                context.clearRect(cellWidth * playerColPos,
                    cellHeight * playerRowPos, cellWidth, cellHeight);
                //update player's pos
                playerRowPos = playerRowPos - 1;
                //draw the player
                context.drawImage(oppStartImg, cellWidth * playerColPos,
                    cellHeight * playerRowPos, cellWidth, cellHeight);

                break;
            case "39": //to the right
                //remove the rect
                context.clearRect(cellWidth * playerColPos,
                    cellHeight * playerRowPos, cellWidth, cellHeight);
                //update player's pos
                playerColPos = playerColPos + 1;
                //draw the player
                context.drawImage(oppStartImg, cellWidth * playerColPos,
                    cellHeight * playerRowPos, cellWidth, cellHeight);

                break;
            case "40": //down
                //remove the rect
                context.clearRect(cellWidth * playerColPos,
                    cellHeight * playerRowPos, cellWidth, cellHeight);
                //update player's pos
                playerRowPos = playerRowPos + 1;
                //draw the player
                context.drawImage(oppStartImg, cellWidth * playerColPos,
                    cellHeight * playerRowPos, cellWidth, cellHeight);
                break;
        }
        if ((playerColPos == exitCol) && (playerRowPos == exitRow)) {
            alert("you lost");
            //adding losing
            var player = sessionStorage.currentUser;
            var apiUrl = "../api/Users/Update/";
            $.getJSON(apiUrl + player + "/" + 0)
                .done(function () {
                    alert('Load was performed.');
                });
            window.location.href = "MainPage.html";
        }
    };


    window.onload = function () {
        var gameH = $.connection.gameHub;
        var apiUrl = "../api/Maze"
        $.getJSON(apiUrl).done(function (gamesList) {
            if (gamesList != null) {
                var exist = false;
                var selectBox = document.getElementById("gameList");
                for (game in gamesList.games) {
                    for (var i = 0, opts = document.getElementById("gameList").options; i < opts.length; ++i)
                        if (opts[i].value == gamesList.games[game]) {
                            exist = true;
                        }
                    if (!exist)
                        selectBox.options[selectBox.options.length] = new Option(gamesList.games[game]);
                }
            }
        })


        $.connection.hub.start().done(function () {
            var apiUrl = "../api/Maze"
            //var clientId = $.connection.hub.id;
            $("#btnStartGame").click(function () {
                document.getElementById("loader").style.display = "block";
                var name = $("#mazeName").val();
                var rows = $("#mazeRows").val();
                var cols = $("#mazeCols").val();
                var id = $.connection.hub.id;
                //connect
                gameH.server.startGame(name);
                $.getJSON(apiUrl + "/" + name + "/" + rows + "/" + cols + "/" + id).done(function (maze) {
                    var myMazeBoard = $("#myMazeCanvas").MazeBoard(maze.Name,maze.Maze, maze.Rows, maze.Cols, maze.Start.Row, maze.Start.Col,
                        maze.End.Row, maze.End.Col, myStartImg, myEndImg, myMazeCanvas, "multi");

                    myMazeBoard.Draw();

                    var oppMazeBoard = $("#myMazeCanvas").MazeBoard(maze.Name,maze.Maze, maze.Rows, maze.Cols, maze.Start.Row, maze.Start.Col,
                        maze.End.Row, maze.End.Col, oppStartImg, oppEndImg, oppMazeCanvas, "multi");

                    oppMazeBoard.Draw();
                    document.getElementById("loader").style.display = "none";
                    document.title = name;

                    //myMaze.Move();
                })

            });

            $("#btnJoinGame").click(function () {
                var id = $.connection.hub.id;

                //connect
                document.getElementById("loader").style.display = "block";
                var selectVal = document.getElementById("gameList").value;
                gameH.server.joinGame(selectVal);


                $.getJSON(apiUrl + "/JoinMaze/" + selectVal + "/" + id).done(function (maze) {
                    var oppMazeBoard = $("#oppMazeCanvas").MazeBoard(maze.Name, maze.Maze, maze.Rows, maze.Cols, maze.Start.Row, maze.Start.Col,
                        maze.End.Row, maze.End.Col, oppStartImg, oppEndImg, oppMazeCanvas, "multi");

                    oppMazeBoard.Draw();

                    var myMazeBoard = $("#myMazeCanvas").MazeBoard(maze.Name,maze.Maze, maze.Rows, maze.Cols, maze.Start.Row, maze.Start.Col,
                        maze.End.Row, maze.End.Col, myStartImg, myEndImg, myMazeCanvas, "multi");

                    myMazeBoard.Draw();
                    document.getElementById("loader").style.display = "none";
                    document.title = maze.Name;

                })

            });
        })
    };
