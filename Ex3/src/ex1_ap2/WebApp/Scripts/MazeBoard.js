(function ($) {
    $.fn.MazeBoard = function (
        mazeName,
        mazeData, // the matrix containing the maze cells
        mazeRows, mazeCols, //the matrix rows and cols
        startRow, startCol, // initial position of the player
        exitRow, exitCol, // the exit position
        playerImage, // player's icon (of type Image)
        exitImage, // exit's icon (of type Image)
        canvasId,
        playerType,
    ) {
        var mazeObj = {
            mazeName: mazeName,
            mazeData: mazeData,
            mazeRows: mazeRows,
            mazeCols: mazeCols,
            startRow: startRow,
            startCol: startCol,
            exitRow: exitRow,
            exitCol: exitCol,
            playerImage: playerImage,
            exitImage: exitImage,
            canvasId: document.getElementById(canvasId),
            playerType: playerType,
            playerRowPos: startRow,
            playerColPos: startCol,
            isEnabled: true,
           // game = $.connection.gameHub,

            //funcs
            Draw: function () {
                //var myCanvas = document.getElementById("mazeCanvas");
                //var myCanvas = document.getElementById(canvasId);
                var context = canvasId.getContext("2d");
                var rows = mazeObj.mazeRows;
                var cols = mazeObj.mazeCols;
                var cellWidth = canvasId.width / cols;
                var cellHeight = canvasId.height / rows;
                for (var i = 0; i < rows; i++) {
                    for (var j = 0; j < cols; j++) {
                        if (mazeObj.mazeData[i * cols + j] == 1) {
                            context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                        }
                    }
                }
                context.drawImage(playerImage, cellWidth * startCol, cellHeight * startRow, cellWidth, cellHeight);
                context.drawImage(exitImage, cellWidth * exitCol, cellHeight * exitRow, cellWidth, cellHeight);
            },

            Move: $(document).keydown(function (e) {
                if (canvasId == myMazeCanvas) {
                    // Handle the arrow keys
                    //var myCanvas = document.getElementById("mazeCanvas");
                    var context = canvasId.getContext("2d");
                    var rows = mazeObj.mazeRows;
                    var cols = mazeObj.mazeCols;
                    var cellWidth = canvasId.width / cols;
                    var cellHeight = canvasId.height / rows;
                    var cpyColPos = mazeObj.playerColPos;
                    var cpyRowPos = mazeObj.playerRowPos;
                    var directionFlag = 0;

                    switch (e.which) {
                        case 37: //to the left
                            if (mazeObj.playerColPos - 1 >= 0 && mazeObj.mazeData[mazeObj.playerRowPos * cols + mazeObj.playerColPos - 1] == 0) {
                                //remove the rect
                                context.clearRect(cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                //update player's pos
                                mazeObj.playerColPos = mazeObj.playerColPos - 1;
                                //draw the player
                                context.drawImage(playerImage, cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                directionFlag = 1;
                            }
                            break;

                        case 38: //up
                            if (mazeObj.playerRowPos - 1 >= 0 && mazeObj.mazeData[(mazeObj.playerRowPos - 1) * cols + mazeObj.playerColPos] == 0) {
                                //remove the rect
                                context.clearRect(cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                //update player's pos
                                mazeObj.playerRowPos = mazeObj.playerRowPos - 1;
                                //draw the player
                                context.drawImage(playerImage, cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                directionFlag = 1;
                            }
                            break;
                        case 39: //to the right
                            if (mazeObj.playerColPos + 1 <= cols - 1 && mazeObj.mazeData[mazeObj.playerRowPos * cols + mazeObj.playerColPos + 1] == 0) {
                                //remove the rect
                                context.clearRect(cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                //update player's pos
                                mazeObj.playerColPos = mazeObj.playerColPos + 1;
                                //draw the player
                                context.drawImage(playerImage, cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                directionFlag = 1;
                            }
                            break;
                        case 40: //down
                            if (mazeObj.playerRowPos + 1 <= rows - 1 && mazeObj.mazeData[(mazeObj.playerRowPos + 1) * cols + mazeObj.playerColPos] == 0) {
                                //remove the rect
                                context.clearRect(cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                //update player's pos
                                mazeObj.playerRowPos = mazeObj.playerRowPos + 1;
                                //draw the player
                                context.drawImage(playerImage, cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                directionFlag = 1;
                            }
                            break;
                        default:
                            break;
                    }
                    //if there was a legal move
                    if (directionFlag == 1) {
                        if (mazeObj.playerColPos == mazeObj.exitCol && mazeObj.playerRowPos == mazeObj.exitRow) {
                            alert("Mazal Tov!!\nYou Won!");
                            if (mazeObj.playerType == "single")
                                window.location.href = "MainPage.html";
                        }


                        //SendMove
                        if (mazeObj.playerType == "multi") {
                            var game = $.connection.gameHub;
                            game.server.sendMove(e.which, mazeObj.mazeName, cellWidth, cellHeight, cpyRowPos, cpyColPos, mazeObj.exitRow, mazeObj.exitCol);
                            if (mazeObj.playerColPos == mazeObj.exitCol && mazeObj.playerRowPos == mazeObj.exitRow) {
                                //adding winning
                                var player = sessionStorage.currentUser;
                                var apiUrl = "../api/Users/Update/";

                                $.getJSON(apiUrl + player+"/"+1)
                                    .done(function () {
                                        alert('Load was performed.');
                                    });
                                window.location.href = "MainPage.html";
                            }
                        }
                    }
                }
            }),

            Solve: $("#btnSolve").click(function () {
                var apiUrl = "../api/Maze";
                var name = $("#mazeName").val();
                var alg = $("#selectAlg").val();

                $.getJSON(apiUrl + "/" + name + "/" + alg).done(function (sol) {          
                    var context = canvasId.getContext("2d");
                    var rows = mazeObj.mazeRows;
                    var cols = mazeObj.mazeCols;
                    var cellWidth = canvasId.width / cols;
                    var cellHeight = canvasId.height / rows;
                    //remove the rect
                    context.clearRect(cellWidth * mazeObj.playerColPos,
                        cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                    //update player's pos to start
                    mazeObj.playerRowPos = mazeObj.startRow;
                    mazeObj.playerColPos = mazeObj.startCol;
                    //draw the player
                    context.drawImage(playerImage, cellWidth * mazeObj.playerColPos,
                        cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);

                    var length = sol.Solution.length;

                    (function fn(i) {
                        switch (sol.Solution[i]) {
                            case "0":
                                //remove the rect
                                context.clearRect(cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                //update player's pos
                                mazeObj.playerColPos = mazeObj.playerColPos - 1;
                                //draw the player
                                context.drawImage(playerImage, cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                break;
                            case "1":
                                context.clearRect(cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                //update player's pos
                                mazeObj.playerColPos = mazeObj.playerColPos + 1;
                                //draw the player
                                context.drawImage(playerImage, cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                break;
                            case "2":
                                //remove the rect
                                context.clearRect(cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                //update player's pos
                                mazeObj.playerRowPos = mazeObj.playerRowPos - 1;
                                //draw the player
                                context.drawImage(playerImage, cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                break;
                            case "3":
                                //remove the rect
                                context.clearRect(cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                //update player's pos
                                mazeObj.playerRowPos = mazeObj.playerRowPos + 1;
                                //draw the player
                                context.drawImage(playerImage, cellWidth * mazeObj.playerColPos,
                                    cellHeight * mazeObj.playerRowPos, cellWidth, cellHeight);
                                direction = "down";
                                break;

                            default:
                                break;
                        }
                        if (i < length) {
                            setTimeout(function () {
                                fn(++i);
                            }, 300)
                        }
                        else if (i == length) {
                            alert("Mazal Tov!!\nYou Won!");
                            window.location.href = "MainPage.html";
                        }           
                    }(0));
                })
            }),
        }
        return mazeObj;


    }
})(jQuery)