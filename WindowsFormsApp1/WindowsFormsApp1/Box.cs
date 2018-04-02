using System;
using System.Drawing;

namespace Vang_de_volger
{
    public class Box : Unit
    {
        public Image myImage; //Image for the boxes
        public Point pointTracker; //Position of the box Image

        //Constructor setting the Image and Point of the Box class.
        public Box()
        {
            myImage = Image.FromFile(@"..\..\Resources\Box.png");
            pointTracker = new Point();
        }

    }
}