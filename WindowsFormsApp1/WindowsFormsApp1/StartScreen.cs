using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Created by: Shivam Akloe[17057620] & Sang Phan Thanh[17119081]

namespace Vang_de_volger
{
    public partial class StartScreen : Form
    {
        //Constructor
        public StartScreen()
        {
            InitializeComponent();
        }

        //Call the MainForm class to start its game and hide the current Startscreen
        private void Start_Button_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }

        //Closes the game
        private void Exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
