using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vang_de_volger
{
    public class Hero : Unit
    {
        public Image myImage; //Image for the hero
        public Point pointTracker; //Position of the hero Image

        //Constructor setting the Image and Point of the Hero class.
        public Hero()
        {
            myImage = Image.FromFile(@"..\..\Resources\Hero.png");
            pointTracker = new Point();
        }    
    }
}
