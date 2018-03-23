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
            /*
             void Form1_KeyPress(object sender, KeyPressEventArgs e)
                {
                    if (e.KeyChar >= 48 && e.KeyChar <= 57)
                    {
                        MessageBox.Show("Form.KeyPress: '" +
                            e.KeyChar.ToString() + "' pressed.");

                        switch (e.KeyChar)
                        {
                            case (char)49:
                            case (char)52:
                            case (char)55:
                                MessageBox.Show("Form.KeyPress: '" +
                                    e.KeyChar.ToString() + "' consumed.");
                                e.Handled = true;
                                break;
                        }
                    }
                }
             */
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