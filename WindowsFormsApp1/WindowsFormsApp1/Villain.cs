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
        public Image myImage;
        public Point pointTracker;
        

        public Villain()
        {
            myImage = Image.FromFile(@"..\..\Resources\VillainTemp.png");
            pointTracker = new Point();
        }

    }
}