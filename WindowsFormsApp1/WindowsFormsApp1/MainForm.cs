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
        public const int tileSize = 40;
        
        Size pbSize;

        public MainForm()
        {
            InitializeComponent();
            pbSize = new Size(x_gridSize * tileSize, x_gridSize * tileSize);
            pbLevel.Size = pbSize;
            timerVillainMove.Interval = villainMoveInterval;
            timerVillainMove.Tick += TimerVillainMove_Tick;
            GenerateField();
            timerVillainMove.Start();
        }



        int testcounter = 0;
        public void TimerVillainMove_Tick(object sender, EventArgs e)
        {
            if (playZone.villain_Lose() == true)
            {
                timerVillainMove.Stop();
                textBox1.Text = "Villain Lost!";
            }else if (playZone.Catch_Hero(playZone.villainTile) == true)
            {
                timerVillainMove.Stop();
                textBox1.Text = "Hero Lost!";
            }
            else
            {
                playZone.Villain_random_move(playZone.villainTile);
                playZone.Draw(pbLevel);

                //textBox1.Text = testcounter.ToString();
                testcounter++;
                this.Refresh();
            }
        }

        public void GenerateField()
        {
            this.Invalidate();
            playZone.Create_Tiles();
            playZone.CreateField(this,pbLevel);
            this.Refresh();
        }

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


        private void reset_Button_Click(object sender, EventArgs e)
        {
            timerVillainMove.Stop();
            GenerateField();
            timerVillainMove.Start();
        }

        //Pause the game with the button
        private void pause_Button_Click(object sender, EventArgs e)
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


    }
}
