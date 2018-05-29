using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace GuiGame
{
    /// <summary>
    /// MultiGame ViewModel
    /// </summary>
    /// <seealso cref="GuiGame.ViewModel" />
    public class StartMultiGameViewModel : ViewModel
    {
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        
        /// <summary>
        /// The game selected
        /// </summary>
        private string gameSelected;
        
        /// <summary>
        /// My current position
        /// </summary>
        private Position myCurrentPos;
        
        /// <summary>
        /// My position to string
        /// </summary>
        private string myPosToString;
        
        /// <summary>
        /// The other position to string
        /// </summary>
        private string otherPosToString;
        
        /// <summary>
        /// The model
        /// </summary>
        private MultiModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartMultiGameViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public StartMultiGameViewModel(MultiModel model)
        {
            this.model = model;
            model.PropertyChanged += (sender, e) => NotifyPropertyChanged("VM_" + e.PropertyName);
        }

        /// <summary>
        /// Gets or sets the vm games list.
        /// </summary>
        /// <value>
        /// The vm games list.
        /// </value>
        public ObservableCollection<string> VM_GamesList
        {
            get { return model.GamesList; }
            set
            {
                if(value != null)
                {
                    NotifyPropertyChanged("VM_GamesList");
                }
            }
        }

        /// <summary>
        /// Gets or sets the vm selected game.
        /// </summary>
        /// <value>
        /// The vm selected game.
        /// </value>
        public string VM_SelectedGame
        {
            get { return gameSelected; }
            set
            {
                if (gameSelected == value)
                    return;
                gameSelected = value;
                NotifyPropertyChanged("VM_SelectedGame");
            }
        }

        /// <summary>
        /// Updates the initial position.
        /// </summary>
        public void UpdateInitialPos()
        {
            model.InitPosPropertyChanged -= (sender, e) => {
                UpdateInitialPos();
            };
            myCurrentPos = model.MazeInitialPos;
            myPosToString = myCurrentPos.ToString();
            VM_MyCurrentPos = myPosToString;
            otherPosToString = myPosToString;
            VM_OtherCurrentPos = otherPosToString;
            model.OtherCurrentPos.ToString();     
        }

        /// <summary>
        /// Gets or sets the name of the vm maze.
        /// </summary>
        /// <value>
        /// The name of the vm maze.
        /// </value>
        public string VM_MazeName
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("VM_MazeName");
            }
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
            get { return VM_MazeInitialPos.ToString(); }
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
        /// Gets or sets the vm my current position.
        /// </summary>
        /// <value>
        /// The vm my current position.
        /// </value>
        public string VM_MyCurrentPos
        {
            get { return myPosToString; }
            set
            {
                myPosToString = value;
                NotifyPropertyChanged("VM_MyCurrentPos");
            }
        }

        /// <summary>
        /// Gets or sets the vm other current position.
        /// </summary>
        /// <value>
        /// The vm other current position.
        /// </value>
        public string VM_OtherCurrentPos
        {
            get { return model.OtherCurrentPos.ToString(); }
              set
            {
                otherPosToString = value;
                NotifyPropertyChanged("VM_OtherCurrentPos");
            }
        }

        /// <summary>
        /// Starts the multi game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void StartMultiGame(string name, string rows, string cols)
        {
            VM_MazeName = name;
            Console.WriteLine("VM_MazeName: " + VM_MazeName);
            string startMultiCommand = "start " + name + " " + rows + " " + cols;
            model.InitPosPropertyChanged += (sender, e) => {
                UpdateInitialPos();
                Console.WriteLine("VM_MazeName: " + VM_MazeName);
            };
            //at this point, we already registered as a listener, so every change occours in model- we'll notice
            model.SendCommand(startMultiCommand, 0);
            Console.WriteLine("VM_MazeName: " + VM_MazeName);
        }

        /// <summary>
        /// Joins the multi game.
        /// </summary>
        /// <param name="name">The name.</param>
        public void JoinMultiGame(string name)
        {
            VM_MazeName = name;
            Console.WriteLine("VM_MazeName: " + VM_MazeName);
            string joinMultiCommand = "join " + name;
            model.InitPosPropertyChanged += (sender, e) => {
                UpdateInitialPos();
                Console.WriteLine("VM_MazeName: " + VM_MazeName);
            };
            //at this point, we already registered as a listener, so every change occours in model- we'll notice
            model.SendCommand(joinMultiCommand, 1);
            Console.WriteLine("VM_MazeName: " + VM_MazeName);
        }

        /// <summary>
        /// Closes the multi game.
        /// </summary>
        public void CloseMultiGame()
        {
            model.CloseCommand();
        }

        /// <summary>
        /// Lists the command.
        /// </summary>
        public void ListCommand()
        {
            model.SendCommand("list", 2);
        }

        /// <summary>
        /// Moves my player.
        /// </summary>
        /// <param name="step">The step.</param>
        public void MoveMyPlayer(string step)
        {
            int tempCol = myCurrentPos.Col;
            int tempRow = myCurrentPos.Row;
            switch (step)
            {
                case "left":
                    if (tempCol - 1 < 0 || VM_MazeGame[tempRow, tempCol - 1] == CellType.Wall)
                        break;
                    else
                    {
                        myCurrentPos.Col -= 1;
                        Console.WriteLine("in left");
                        VM_MyCurrentPos = myCurrentPos.ToString();
                        Console.WriteLine("VM_MyCurrentPos: " + VM_MyCurrentPos);
                        model.MyDirectionCommand = "play " + step;
                        break;
                    }
                case "right":
                    if (tempCol + 1 >= VM_MazeCols || VM_MazeGame[tempRow, tempCol + 1] == CellType.Wall)
                        break;
                    else
                    {
                        myCurrentPos.Col += 1;
                        Console.WriteLine("in right");
                        VM_MyCurrentPos = myCurrentPos.ToString();
                        Console.WriteLine("VM_MyCurrentPos: " + VM_MyCurrentPos);
                        model.MyDirectionCommand = "play " + step;
                        break;
                    }
                case "up":
                    if (tempRow - 1 < 0 || VM_MazeGame[tempRow - 1, tempCol] == CellType.Wall)
                        break;
                    else
                    {
                        myCurrentPos.Row -= 1;
                        Console.WriteLine("in up");
                        VM_MyCurrentPos = myCurrentPos.ToString();
                        Console.WriteLine("VM_MyCurrentPos: " + VM_MyCurrentPos);
                        model.MyDirectionCommand = "play " + step;
                        break;
                    }
                case "down":
                    if (tempRow + 1 >= VM_MazeRows || VM_MazeGame[tempRow + 1, tempCol] == CellType.Wall)
                        break;
                    else
                    {
                        myCurrentPos.Row += 1;
                        Console.WriteLine("in down");
                        VM_MyCurrentPos = myCurrentPos.ToString();
                        Console.WriteLine("VM_MyCurrentPos: " + VM_MyCurrentPos);
                        model.MyDirectionCommand = "play " +step;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
