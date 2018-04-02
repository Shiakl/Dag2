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
        Field playZone = new Field();
        int villainMoveInterval = 500; //interval at which villain moves in milliseconds
        Timer timerVillainMove = new Timer();
        public const int x_gridSize = 15;  //Amount of tiles in X-direction on the field
        public const int y_gridSize = 15;  //Amount of tiles in Y-direction on the field
        public const int tileSize = 40;    //Size of tiles
        
        Size pbSize;
        Size endPbSize;

        //Constructor handling the field and tile elements.
        public MainForm()
        {
            InitializeComponent();
            pbSize = new Size(x_gridSize * tileSize, x_gridSize * tileSize);
            pbLevel.Size = pbSize;
            pbLevel.Left = 0; pbLevel.Top = 0;
            pause_Label.Left = x_gridSize * tileSize + tileSize;
            restart_Button.Left = x_gridSize * tileSize + tileSize;
            endPbSize = new Size(x_gridSize * tileSize, x_gridSize * tileSize);
            endPb.Size = endPbSize;
            endPb.Left = 0;  endPb.Top = 0;
            endPb.Visible = false;

            //Start the movement of the Villain
            timerVillainMove.Interval = villainMoveInterval;
            timerVillainMove.Tick += TimerVillainMove_Tick;
            GenerateField();
            timerVillainMove.Start();
        }


        //Move handler and win/lose handler of the Villain
        int testcounter = 0;
        public void TimerVillainMove_Tick(object sender, EventArgs e)
        {
            if (playZone.villain_Lose() == true)
            {
                timerVillainMove.Stop();
                endPb.Visible = true;
                paused = true;
            }else if (playZone.Catch_Hero(playZone.villainTile) == true)
            {
                timerVillainMove.Stop();
                endPb.Visible = true;
                paused = true;
            }
            else
            {
                playZone.Villain_random_move(playZone.villainTile);
                playZone.Draw(pbLevel);
                playZone.Draw(pbLevel);
                testcounter++;
                this.Refresh();
            }
        }

        //Generates the field and calls for the Create_Tiles funtion to generate the standard amount of tiles.
        public void GenerateField()
        {
            this.Invalidate();
            playZone.Create_Tiles();
            playZone.CreateField(this,pbLevel,pause_Label,restart_Button);
            this.Refresh();
        }

        // Adds movement via keyinput for the hero using LEFT/RIGHT/UP/DOWN arrow keys
        private bool paused = false;
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (paused == false)
            {
                if (e.KeyCode == Keys.Left)
                {
                    playZone.Hero_move(playZone.heroTile, 0);
                    playZone.Draw(pbLevel);
                }
                else if (e.KeyCode == Keys.Right)
                {
                    playZone.Hero_move(playZone.heroTile, 1);
                    playZone.Draw(pbLevel);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    playZone.Hero_move(playZone.heroTile, 2);
                    playZone.Draw(pbLevel);
                }
                else if (e.KeyCode == Keys.Down)
                {
                    playZone.Hero_move(playZone.heroTile, 3);
                    playZone.Draw(pbLevel);
                }
            }

            //Pause the game
            if (e.KeyCode == Keys.Escape && paused == false)
            {
                paused = true;
                timerVillainMove.Stop();
            }
            else if (e.KeyCode == Keys.Escape && paused == true)
            {
                paused = false;
                timerVillainMove.Start();
            }
        }

        /// <summary>
        /// Handles the behavior for the Villain during the Pause instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pause_Label_Click(object sender, EventArgs e)
        {
            if (paused == false)
            {
                paused = true;
                timerVillainMove.Stop();
            }
            else if (paused == true)
            {
                paused = false;
                timerVillainMove.Start();
            }
        }

        /// <summary>
        /// Restart the game by regenarating the field with the randomised tiles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restart_Button_Click(object sender, EventArgs e)
        {
            timerVillainMove.Stop();
            GenerateField();
            timerVillainMove.Start();
            paused = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
