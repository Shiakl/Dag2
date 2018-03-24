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
        public void KeyBoardMove(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                //Add point
                Tile_check_movement("Up"); //Add Tile checker
                //Add point changer
                //Add redraw event caller
            }
            else if (e.KeyCode == Keys.Down)
            {
                //Add point
                Tile_check_movement("Down"); //Add Tile checker
                //Add point changer
                //Add redraw event caller
            }
            else if (e.KeyCode == Keys.Right)
            {
                //Add point
                Tile_check_movement("Right"); //Add Tile checker
                //Add point changer
                //Add redraw event caller
            }
            else if (e.KeyCode == Keys.Left)
            {
                //Add point
                Tile_check_movement("Left"); //Add Tile checker
                //Add point changer
                //Add redraw event caller
            }
        }
    }
}