using System;
using System.Drawing;

namespace Vang_de_volger
{
    public class Box : Unit
    {
        public Image myImage = Image.FromFile(@"..\..\Resources\Box.jpg");
        public Point pointTracker = new Point();

        

        //Constructor
        public Box()
        {

        }
    }
}