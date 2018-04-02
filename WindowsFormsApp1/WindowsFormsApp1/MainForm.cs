using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vang_de_volger
{
    public partial class MainForm : Form
    {
        private Field _playZone = new Field();
        private int _villainMoveInterval = 500; //interval at which villain moves in milliseconds
        private Timer _timerVillainMove = new Timer(); //Create a timer to make the villain move
        public const int x_gridSize = 15;  //Amount of tiles in X-direction on the field
        public const int y_gridSize = 15;  //Amount of tiles in Y-direction on the field
        public const int tileSize = 40; //set how big the tiles are, this value should match the tile Image
        private bool _paused = false;  //bool to track whether the paused button was pressed

        private Image _victoryImage = Image.FromFile(@"..\..\Resources\Victory.png");
        private Image _loseImage = Image.FromFile(@"..\..\Resources\Lose.png");
        private Image _pauseImage = Image.FromFile(@"..\..\Resources\Paused.png");

        private Size pbSize;
        private Size endPbSize;

        //Constructor handling the field and tile elements.
        public MainForm()
        {
            InitializeComponent();

            //Set the size for the picturebox drawing the game and the locations of the buttons and other screens.
            pbSize = new Size(x_gridSize * tileSize, x_gridSize * tileSize);
            pbLevel.Size = pbSize;
            pbLevel.Left = 0; pbLevel.Top = 0;
            pause_Label.Left = x_gridSize * tileSize + tileSize;
            restart_Button.Left = x_gridSize * tileSize + tileSize;
            endPbSize = new Size(x_gridSize * tileSize, x_gridSize * tileSize);
            endPb.Size = endPbSize;
            endPb.Left = 0;  endPb.Top = 0;
            endPb.Visible = false;
            endPb.BackColor = Color.Transparent;

            GenerateField(); //Create the field with all the tiles and units

            //Start the movement of the Villain
            _timerVillainMove.Interval = _villainMoveInterval;
            _timerVillainMove.Tick += TimerVillainMove_Tick;
            _timerVillainMove.Start();
        }

        //Generates the field and calls for the Create_Tiles funtion to generate the standard amount of tiles.
        public void GenerateField()
        {
            this.Invalidate();
            _playZone.CreateField(this, pbLevel, pause_Label, restart_Button);
            this.Refresh();
        }

        //Move handler and win/lose handler of the Villain
        public void TimerVillainMove_Tick(object sender, EventArgs e)
        {
            //Check to see if the villain has any moves left through a bool in the Field class.
            if (_playZone.villain_Lose() == true) 
            {
                _timerVillainMove.Stop();
                endPb.Visible = true;
                endPb.Image = _victoryImage;
                _paused = true;
            }
            //Check to see if the villain has succesfully caught the player through a bool in the Field class.
            else if (_playZone.Catch_Hero(_playZone.villainTile) == true)
            {
                _timerVillainMove.Stop();
                endPb.Visible = true;
                endPb.Image = _loseImage;
                _paused = true;
            }
            else
            {
                _playZone.Villain_random_move(_playZone.villainTile);
                _playZone.Draw(pbLevel);
                _playZone.Draw(pbLevel);
                this.Refresh();
            }
        }

        // Adds movement via keyinput for the hero using LEFT/RIGHT/UP/DOWN arrow keys
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_paused == false)
            {
                if (e.KeyCode == Keys.Left)
                {
                    _playZone.Hero_move(_playZone.heroTile, 0); //Value 0 is the direction Left
                    _playZone.Draw(pbLevel);
                }
                else if (e.KeyCode == Keys.Right)
                {
                    _playZone.Hero_move(_playZone.heroTile, 1); //Value 1 is the direction Right
                    _playZone.Draw(pbLevel);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    _playZone.Hero_move(_playZone.heroTile, 2); //Value 2 is the direction Up
                    _playZone.Draw(pbLevel);
                }
                else if (e.KeyCode == Keys.Down)
                {
                    _playZone.Hero_move(_playZone.heroTile, 3); //Value 3 is the direction Down
                    _playZone.Draw(pbLevel);
                }
            }

            //Pause the game
            if (e.KeyCode == Keys.Escape && _paused == false)
            {
                _paused = true;
                endPb.Visible = true;
                endPb.Image = _pauseImage;
                _timerVillainMove.Stop();
            }
            else if (e.KeyCode == Keys.Escape && _paused == true)
            {
                _paused = false;
                endPb.Visible = false;
                _timerVillainMove.Start();
            }
        }


/// <summary>
/// The game will pause(the "paused" bool is set to true) when the pause label is clicked. 
/// When the "paused" bool is set to true movement of the villain and hero will be locked.
/// </summary>
        private void pause_Label_Click(object sender, EventArgs e)
        {
            if (_paused == false)
            {
                _paused = true;
                _timerVillainMove.Stop();
                endPb.Visible = true;
                endPb.Image = _pauseImage;
            }
            else if (_paused == true)
            {
                _paused = false;
                endPb.Visible = false;
                _timerVillainMove.Start();
            }
        }

        // Restart the game by regenarating the field with the randomised tiles.
        private void restart_Button_Click(object sender, EventArgs e)
        {
            _timerVillainMove.Stop();
            GenerateField();
            _timerVillainMove.Start();
            endPb.Visible = false;
            _paused = false;
        }
    }
}
