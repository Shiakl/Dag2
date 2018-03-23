using System;
using System.Drawing;

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
        public void KeyBoardMove()
        {

        }

        protected void PickPowerUp()
        {

        }

        public void Die()
        {

        }

        public void Hero_Move(string direction)
        {

        }
    }
}