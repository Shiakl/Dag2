using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Vang_de_volger
{
    class Tile : Field
    {
        public const int tileSize = 40;

        public enum TILETYPE
        {
            BLOCK,
            TILE,
            BOX,
            HERO,
            VILLAIN
        }

        public Image myImage = Image.FromFile(@"..\..\Resources\Tile.jpg");

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
            else if (MyType == TILETYPE.BOX)
            {
                myImage = Image.FromFile(@"..\..Resources\Box.jpg");
            }
        }
        

        public TILETYPE MyType { get; set; }

        private string _contains;
        public string Contains
        {
            get
            {
                return _contains;
            }
            set
            {
                _contains = value;
            }
        }

        public void Tile_check_movement(Point heroPoint, String direction)
        {
            if (direction.Equals("Left") && moveArray[0]  == true)
            {
                heroPoint.X -= tileSize;
            }
            else if (direction.Equals("Right") && moveArray[1] == true)
            {
                heroPoint.X += tileSize;
            }
            else if (direction.Equals("Up") && moveArray[2] == true)
            {
                heroPoint.Y -= tileSize;
            }
            else if (direction.Equals("Down") && moveArray[3] == true)
            {
                heroPoint.Y += tileSize;
            }
        }

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

        bool[] moveArray = new bool[4];
        private void Possible_moves()
        {
            for(int i = 0; i < 4; i++)
            {
               if( _myNeighbours[i].MyType == TILETYPE.BLOCK)
                {
                    moveArray[i] = false;
                }
                else if (_myNeighbours[i].MyType == TILETYPE.TILE)
                {
                    moveArray[i] = true;
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

    }
}
