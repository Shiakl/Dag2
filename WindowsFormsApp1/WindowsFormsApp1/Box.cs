using System;
using System.Drawing;

namespace Vang_de_volger
{
    public class Box : Unit
    {
        public Image myImage;
        public Point pointTracker;

        //Constructor
        public Box()
        {
            myImage = Image.FromFile(@"..\..\Resources\Box.png");
            pointTracker = new Point();
        }

    }
}