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
    public class Villain : Unit
    {
        int villainMoveInterval = 500; //interval at which villain moves in milliseconds
        Timer timerVillainMove = new Timer();
        public Image myImage;
        public Point pointTracker;
        

        public Villain()
        {
            myImage = Image.FromFile(@"..\..\Resources\VillainTemp.png");
            pointTracker = new Point();
            timerVillainMove.Interval = villainMoveInterval;
            timerVillainMove.Tick += TimerVillainMove_Tick;

        }

        private void TimerVillainMove_Tick(object sender, EventArgs e)
        {
            //Make the villain do funky stuff
        }
    }
}