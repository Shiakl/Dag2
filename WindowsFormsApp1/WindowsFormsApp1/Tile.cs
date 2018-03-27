using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Vang_de_volger
{
    class Tile: Unit
    {       
        public Image myImage;
        public Point pointTracker;
        public string[] directions = new string[4] { "Left", "Right", "Top", "Bottom" };

        public Tile(TILETYPE type, Point firstPoint, Image baseImage)
        {
            MyType = type;
            Check_Tile_Type();
            pointTracker = firstPoint;
            myImage = baseImage;
            if (MyType == TILETYPE.BLOCK)
            {
                myImage = Image.FromFile(@"..\..\Resources\Block.jpg");
            }
            else if (MyType == TILETYPE.TILE)
            {
                myImage = Image.FromFile(@"..\..\Resources\Tile.jpg");
            }
        }

        public enum TILETYPE
        {
            BLOCK,
            TILE,
            BOX,
            HERO,
            VILLAIN
        }

        public void Check_Tile_Type()
        {
            if(MyType == TILETYPE.BLOCK)
            {
                myImage = Image.FromFile(@"..\..\Resources\Block.jpg");
            }
            else if (MyType == TILETYPE.TILE)
            {
                myImage = Image.FromFile(@"..\..\Resources\Tile.jpg");
            }          
        }

        public void Tile_check_movement(Point heroPoint, String direction)
        {
            if (direction.Equals("Left") && moveArray[0] == true)
            {
                heroPoint.X += MainForm.tileSize;
                
            }
            else if (direction.Equals("Right") && moveArray[1] == true)
            {
                heroPoint.X += MainForm.tileSize;
            }
            else if (direction.Equals("Up") && moveArray[2] == true)
            {
                heroPoint.Y -= MainForm.tileSize;
            }
            else if (direction.Equals("Down") && moveArray[3] == true)
            {
                heroPoint.Y += MainForm.tileSize;
            }
        }

        public TILETYPE MyType { get; set; }

        private Tile[] _myNeighbours = new Tile[4];
        public Tile neighbourLeft;
        public Tile neighbourRight;
        public Tile neighbourTop;
        public Tile neighbourBottom;
        public void AddNeighbours()
        {
            _myNeighbours[0] = neighbourLeft;
            _myNeighbours[1] = neighbourRight;
            _myNeighbours[2] = neighbourTop;
            _myNeighbours[3] = neighbourBottom;
        }

        public bool[] moveArray = new bool[4];
        public bool[] moveArrayVillain = new bool[4];

        private void Possible_moves()
        {
            for(int i = 0; i < 4; i++)
            {
               if( _myNeighbours[i].MyType == TILETYPE.BLOCK || _myNeighbours[i]== null)
                {
                    moveArray[i] = false;
                }
                else
                {
                    moveArray[i] = true;
                }
            }
        }

        public void Possible_moves_villain()
        {
            for (int i = 0; i < 4; i++)
            {
                if(_myNeighbours[i] != null)
                {
                if (_myNeighbours[i].MyType == TILETYPE.BLOCK || _myNeighbours[i].MyType == TILETYPE.BOX)
                {
                    moveArrayVillain[i] = false;
                }
                else
                {
                    moveArrayVillain[i] = true;
                }

                }
            }
        }

        public Tile NeighbourLeft
        {
            get
            {
                return neighbourLeft;
            }
            set
            {
                neighbourLeft = value;
            }
        }

        public Tile NeighbourRight
        {
            get
            {
                return neighbourRight;
            }
            set
            {
                neighbourRight = value;
            }
        }

        public Tile NeighbourTop
        {
            get
            {
                return neighbourTop;
            }
            set
            {
                neighbourTop = value;
            }
        }

        public Tile NeighbourBottom
        {
            get
            {
                return neighbourBottom;
            }
            set
            {
                neighbourBottom = value;
            }
        }

        public void Villain_Move(string direction)
        {

        }
    }
}
