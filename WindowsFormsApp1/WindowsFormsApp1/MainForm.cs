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

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
