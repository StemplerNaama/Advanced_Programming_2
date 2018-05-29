using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuiGame
{
    /// <summary>
    /// Interaction logic for MazeBoardControl.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MazeBoardControl : UserControl
    {
        /// <summary>
        /// The player rect
        /// </summary>
        private Rectangle player;
        //private event KeyEventArgs keyDown;
        /// <summary>
        /// Initializes a new instance of the <see cref="MazeBoardControl"/> class.
        /// </summary>
        public MazeBoardControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the exit image file.
        /// </summary>
        /// <value>
        /// The exit image file.
        /// </value>
        public ImageSource ExitImageFile
        {
            get { return (ImageSource)GetValue(ExitImageFileProperty); }
            set { SetValue(ExitImageFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExitImageFile.  
        //This enables animation, styling, binding, etc...
        /// <summary>
        /// The exit image file property
        /// </summary>
        public static readonly DependencyProperty ExitImageFileProperty =
            DependencyProperty.Register("ExitImageFile", typeof(ImageSource),
                typeof(MazeBoardControl), new PropertyMetadata(default(ImageSource)));

        /// <summary>
        /// Gets or sets the player image file.
        /// </summary>
        /// <value>
        /// The player image file.
        /// </value>
        public ImageSource PlayerImageFile
        {
            get { return (ImageSource)GetValue(PlayerImageFileProperty); }
            set { SetValue(PlayerImageFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerImageFile.
        //This enables animation, styling, binding, etc...
        /// <summary>
        /// The player image file property
        /// </summary>
        public static readonly DependencyProperty PlayerImageFileProperty =
            DependencyProperty.Register("PlayerImageFile", typeof(ImageSource),
                typeof(MazeBoardControl), new PropertyMetadata(default(ImageSource)));

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        public int MazeRows
        {
            get { return (int)GetValue(MazeRowsProperty); }
            set { SetValue(MazeRowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeRows.
        //This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze rows property
        /// </summary>
        public static readonly DependencyProperty MazeRowsProperty =
            DependencyProperty.Register("MazeRows", typeof(int), typeof(MazeBoardControl),
                new PropertyMetadata(0));

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>
        /// The maze cols.
        /// </value>
        public int MazeCols
        {
            get { return (int)GetValue(MazeColsProperty); }
            set { SetValue(MazeColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeRows.
        //This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze cols property
        /// </summary>
        public static readonly DependencyProperty MazeColsProperty =
            DependencyProperty.Register("MazeCols", typeof(int), typeof(MazeBoardControl),
                new PropertyMetadata(0));

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>
        /// The maze string.
        /// </value>
        public string MazeString
        {
            get { return (string)GetValue(MazeStringProperty); }
            set { SetValue(MazeStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.
        //This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze string property
        /// </summary>
        public static readonly DependencyProperty MazeStringProperty =
            DependencyProperty.Register("MazeString", typeof(string), typeof(MazeBoardControl),
                new PropertyMetadata(""));

        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        /// <value>
        /// The current position.
        /// </value>
        public string CurrentPos
        {
            get { return (string)GetValue(CurrentPosProperty); }
            set { SetValue(CurrentPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pos.
        //This enables animation, styling, binding, etc...
        /// <summary>
        /// The current position property
        /// </summary>
        public static readonly DependencyProperty CurrentPosProperty =
            DependencyProperty.Register("CurrentPos", typeof(string), typeof(MazeBoardControl),
                new PropertyMetadata(OnPositionChanged));

        /// <summary>
        /// Gets or sets the goal position.
        /// </summary>
        /// <value>
        /// The goal position.
        /// </value>
        public string GoalPos
        {
            get { return (string)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPos.
        //This enables animation, styling, binding, etc...
        /// <summary>
        /// The goal position property
        /// </summary>
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeBoardControl),
                new PropertyMetadata(""));

        /// <summary>
        /// Called when [position changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((MazeBoardControl)d).player != null && ((MazeBoardControl)d).CurrentPos != null)
            {
                string s = ((MazeBoardControl)d).CurrentPos;
                string[] arr = (s.Substring(1, s.Length - 2)).Split(',');
                int rows = int.Parse(arr[0]);
                int cols = int.Parse(arr[1]);
                Grid.SetRow(((MazeBoardControl)d).player, rows);
                Grid.SetColumn(((MazeBoardControl)d).player, cols);
            }
        }

        /// <summary>
        /// Draws the maze.
        /// </summary>
        public void DrawMaze() {
            double height = MazeGrid.Height / MazeRows;
            double width = MazeGrid.Width / MazeCols;
            string mazeStr = MazeString;
            for (int i =0; i<MazeRows; i++)
            {
                MazeGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < MazeCols; i++)
            {
                MazeGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            int mazeLength = MazeRows * MazeCols + 2*MazeRows;
            int onGrid, enterCounter = 0;
            for(int i =0; i< mazeLength; i++)
            {
                if (mazeStr[i] == '\r')
                    enterCounter++;
                else if(mazeStr[i] == '1')
                {
                    Rectangle wall = new Rectangle()
                    {
                        Fill = Brushes.Black,
                        Height = height,
                        Width = width
                    };
                    onGrid = i - 2 * enterCounter;
                    MazeGrid.Children.Add(wall);
                    Grid.SetRow(wall, onGrid / MazeCols);
                    Grid.SetColumn(wall, onGrid % MazeCols);     
                }
                else if(mazeStr[i] == '#' || mazeStr[i] == '*')
                {
                    ImageBrush brushPic = new ImageBrush();
                    //exit pos
                    if (mazeStr[i] == '#')
                    {
                        brushPic.ImageSource = ExitImageFile;
                        Rectangle wall = new Rectangle()
                        {
                            Fill = brushPic,
                            Height = height,
                            Width = width
                        };
                        onGrid = i - 2 * enterCounter;
                        MazeGrid.Children.Add(wall);
                        Grid.SetRow(wall, onGrid / MazeCols);
                        Grid.SetColumn(wall, onGrid % MazeCols);
                    }
                    //enterance pos
                    else
                    {
                        brushPic.ImageSource = PlayerImageFile;
                        player = new Rectangle()
                        {
                            Fill = brushPic,
                            Height = height,
                            Width = width
                        };
                        onGrid = i - 2 * enterCounter;
                        MazeGrid.Children.Add(player);
                        Grid.SetRow(player, onGrid / MazeCols);
                        Grid.SetColumn(player, onGrid % MazeCols);
                    }
                }
            }
        }

        /// <summary>
        /// Boards the loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void BoardLoaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("loaded happend");
            DrawMaze();
        }
    }
}
