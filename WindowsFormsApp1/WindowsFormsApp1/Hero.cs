using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vang_de_volger
{
    public class Hero : Unit
    {
        public Image myImage;
        public Point pointTracker;

        public Hero()
        {
            myImage = Image.FromFile(@"..\..\Resources\HeroTemp.png");
            pointTracker = new Point();
        }


    }
}
