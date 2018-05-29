using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MazeLib;

namespace GuiGame
{
    /// <summary>
    /// SingleGame ViewModel
    /// </summary>
    /// <seealso cref="GuiGame.ViewModel" />
    public class StartSingleGameViewModel : ViewModel
    {
        /// <summary>
        /// The model
        /// </summary>
        private SingleModel model;
        
        /// <summary>
        /// The current position
        /// </summary>
        private Position currentPos;
        
        /// <summary>
        /// The position to string
        /// </summary>
        private string posToString;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="StartSingleGameViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public StartSingleGameViewModel(SingleModel model)
        {
            this.model = model;
            model.PropertyChanged += (sender, e) => NotifyPropertyChanged("VM_" + e.PropertyName);
        }
        
        /// <summary>
        /// Updates the initial position.
        /// </summary>
        public void UpdateInitialPos()
        {
            currentPos = model.MazeInitialPos;
            posToString = currentPos.ToString();
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void StartGame(string name, string rows, string cols)
        {
            string generateCommand = "generate " + name + " " + rows + " " + cols;
            model.PropertyChanged += (sender, e) => {
                UpdateInitialPos();
            };
            //at this point, we already registered as a listener, so every change occours in model- we'll notice
            model.SendCommand(generateCommand, 0);
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algoType">Type of the algo.</param>
        public void SolveMaze(string name, string algoType)
        {
            string solveCommand = "solve " + name + " " + algoType;
            model.SendCommand(solveCommand, 1);
            //setting the player to be at the initialPos
            currentPos = VM_MazeInitialPos;
            VM_CurrentPos = currentPos.ToString();
        }

        /// <summary>
        /// Does the restart.
        /// </summary>
        public void DoRestart()
        {
            currentPos = VM_MazeInitialPos;
            VM_CurrentPos = VM_MazeInitialPos.ToString();
        }

        /// <summary>
        /// Gets the vm maze sol.
        /// </summary>
        /// <value>
        /// The vm maze sol.
        /// </value>
        public string VM_MazeSol
        {
            get { return model.MazeSol; }
        }

        /// <summary>
        /// Gets the name of the vm maze.
        /// </summary>
        /// <value>
        /// The name of the vm maze.
        /// </value>
        public string VM_MazeName
        {
            get { return model.MazeName; }
        }

        /// <summary>
        /// Gets the vm maze rows.
        /// </summary>
        /// <value>
        /// The vm maze rows.
        /// </value>
        public int VM_MazeRows
        {
            get { return model.MazeRows; }
        }

        /// <summary>
        /// Gets the vm maze cols.
        /// </summary>
        /// <value>
        /// The vm maze cols.
        /// </value>
        public int VM_MazeCols
        {
            get { return model.MazeCols; }
        }

        /// <summary>
        /// Gets the vm maze string.
        /// </summary>
        /// <value>
        /// The vm maze string.
        /// </value>
        public string VM_MazeString
        {
            get { return model.MazeString; }
        }

        /// <summary>
        /// Gets the vm maze goal position.
        /// </summary>
        /// <value>
        /// The vm maze goal position.
        /// </value>
        public Position VM_MazeGoalPos
        {
            get { return model.MazeGoalPos; }
        }

        /// <summary>
        /// Gets the vm maze initial position.
        /// </summary>
        /// <value>
        /// The vm maze initial position.
        /// </value>
        public Position VM_MazeInitialPos
        {
            get { return model.MazeInitialPos; }
        }

        /// <summary>
        /// Gets the vm goal position.
        /// </summary>
        /// <value>
        /// The vm goal position.
        /// </value>
        public string VM_GoalPos
        {
            get { return VM_MazeGoalPos.ToString(); }
        }

        /// <summary>
        /// Gets the vm maze initial string.
        /// </summary>
        /// <value>
        /// The vm maze initial string.
        /// </value>
        public string VM_MazeInitialStr
        {
            get { return VM_MazeInitialPos.ToString() ; }
        }
        /// <summary>
        /// Gets the vm maze game.
        /// </summary>
        /// <value>
        /// The vm maze game.
        /// </value>
        public Maze VM_MazeGame
        {
            get { return model.MazeGame; }
        }
        /// <summary>
        /// Gets or sets the vm current position.
        /// </summary>
        /// <value>
        /// The vm current position.
        /// </value>
        public string VM_CurrentPos
        {
            get { return posToString; }
            set
            {
                posToString = value;
                NotifyPropertyChanged("VM_CurrentPos");
            }
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="step">The step.</param>
        public void MovePlayer(string step)
        {
            int tempCol = currentPos.Col;
            int tempRow = currentPos.Row;
            switch (step)
            {
                case "left":
                    if (tempCol - 1 < 0 || VM_MazeGame[tempRow, tempCol - 1] == CellType.Wall)
                        break;
                    else
                    {
                        currentPos.Col -= 1;
                        VM_CurrentPos = currentPos.ToString();
                        break;
                    }
                case "right":
                    if (tempCol + 1 >= VM_MazeCols || VM_MazeGame[tempRow, tempCol + 1] == CellType.Wall)
                        break;
                    else
                    {
                        currentPos.Col += 1;
                        VM_CurrentPos = currentPos.ToString();
                        break;
                    }
                case "up":
                    if (tempRow - 1 < 0 || VM_MazeGame[tempRow - 1, tempCol] == CellType.Wall)
                        break;
                    else
                    {
                        currentPos.Row -= 1;
                        VM_CurrentPos = currentPos.ToString();
                        break;
                    }
                case "down":
                    if (tempRow + 1 >= VM_MazeRows || VM_MazeGame[tempRow + 1, tempCol] == CellType.Wall)
                        break;
                    else
                    {
                        currentPos.Row += 1;
                        VM_CurrentPos = currentPos.ToString();
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Moves the player in sol.
        /// </summary>
        public void MovePlayerInSol()
        {
            for (int i = 0; i < VM_MazeSol.Length; i++)
            {
                Thread.Sleep(200);
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Background, new Action(() =>
                     {
                         char step = VM_MazeSol[i];
                         switch (step)
                         {
                             case '0':
                                 currentPos.Col -= 1;
                                 VM_CurrentPos = currentPos.ToString();
                                 break;
                             case '1':
                                 currentPos.Col += 1;
                                 VM_CurrentPos = currentPos.ToString();
                                 break;
                             case '2':
                                 currentPos.Row -= 1;
                                 VM_CurrentPos = currentPos.ToString();
                                 break;
                             case '3':
                                 currentPos.Row += 1;
                                 VM_CurrentPos = currentPos.ToString();
                                 break;
                             default:
                                 break;
                         }
                     }));
            }
        }
    }
}
