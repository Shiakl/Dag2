using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Field _playZone = new Field();
            _playZone.CreateTiles();
            _playZone.ShuffleTiles();
            _playZone.CreateField(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
