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
        Field _playZone = new Field();
        public MainForm()
        {
            InitializeComponent();
            GenerateField();
        }

        public void GenerateField()
        {
            _playZone.CreateTiles();
            _playZone.ShuffleTiles();
            _playZone.CreateField(this);
            this.Invalidate();
            this.Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                _playZone.Move_check_field("Left");
            }
            else if (e.KeyCode == Keys.Right)
            {
                _playZone.Move_check_field("Right");            
            }
            else if (e.KeyCode == Keys.Up)
            {
                _playZone.Move_check_field("Up");
            }
            else if (e.KeyCode == Keys.Down)
            {
                _playZone.Move_check_field("Down");
            }
        }
    }
}
