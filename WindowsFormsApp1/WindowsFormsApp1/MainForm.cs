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
        int villainMoveInterval = 1000; //interval at which villain moves in milliseconds
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
        }



        public void TimerVillainMove_Tick(object sender, EventArgs e)
        {
            playZone.Find_Villain_Tile();
            playZone.Draw(pbLevel);
        }

        public void GenerateField()
        {
            this.Invalidate();
            playZone.CreateTiles();
            playZone.ShuffleTiles();
            playZone.CreateField(this,pbLevel);
            this.Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                playZone.Move_check_field("Left");
            }
            else if (e.KeyCode == Keys.Right)
            {
                playZone.Move_check_field("Right");            
            }
            else if (e.KeyCode == Keys.Up)
            {
                playZone.Move_check_field("Up");
            }
            else if (e.KeyCode == Keys.Down)
            {
                playZone.Move_check_field("Down");
            }
        }

        //Spacebar activates this function as well
        private void button1_Click(object sender, EventArgs e)
        {
            //_playZone.Draw();
            GenerateField();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            playZone.Draw(pbLevel);
        }
    }
}
