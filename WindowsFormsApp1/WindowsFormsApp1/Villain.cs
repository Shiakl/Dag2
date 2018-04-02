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
        public Image myImage; //Image for the villain
        public Point pointTracker; //Position for the villain Image
        
        //Constructor setting the Image and Point of the Villain class.
        public Villain()
        {
            myImage = Image.FromFile(@"..\..\Resources\Villain.png");
            pointTracker = new Point();
        }
    }
}