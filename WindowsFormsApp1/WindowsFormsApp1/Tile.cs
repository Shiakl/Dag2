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
        public Image myImage; //The tile or wall Image
        public Point pointTracker; //The position of the tile on the field
        public Box MyBox = new Box(); //If the tile contains a box this will be used for reference to that box
        static int _NUM_OF_DIRECTIONS = 4; //Define the amount of directions and neighbours each tile has. See "public string all_Directions".     
        public string[] all_Directions = new string[] { "Left", "Right", "Up", "Down" };  //All specified directions. In this case Left will always have the index 0 for every direction array used as a vector for the movement methods.

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
            Possible_moves();
        }

        //Used for the MyType variable.
        public enum TILETYPE
        {
            BLOCK,
            TILE,
            BOX,
            HERO,
            VILLAIN
        }

        public TILETYPE MyType { get; set; }

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

        /// <summary>
        /// After each tile is initialized it will be assigned a neighbour that has a position adjacent to it. These neighbours are stored in this array.
        /// </summary>
        public Tile[] myNeighbours = new Tile[4];
        public Tile neighbourLeft;
        public Tile neighbourRight;
        public Tile neighbourTop;
        public Tile neighbourBottom;
        public void AddNeighbours()
        {
            myNeighbours[0] = neighbourLeft;
            myNeighbours[1] = neighbourRight;
            myNeighbours[2] = neighbourTop;
            myNeighbours[3] = neighbourBottom;
        }

        public bool[] moveArray = new bool[_NUM_OF_DIRECTIONS];
        public bool[] moveArrayVillain = new bool[_NUM_OF_DIRECTIONS];

        public void Possible_moves()
        {
            for(int i = 0; i < _NUM_OF_DIRECTIONS; i++)
            {
               if(myNeighbours[i] != null)
               {
                    if(MyType == TILETYPE.BOX)
                    {
                        if (myNeighbours[i].MyType == TILETYPE.TILE)
                        {
                            moveArray[i] = true;
                        }
                        else
                        {
                            moveArray[i] = false;
                        }
                    }
                    else
                    {
                        if (myNeighbours[i].MyType == TILETYPE.BLOCK || myNeighbours[i].MyType == TILETYPE.VILLAIN)
                        {
                            moveArray[i] = false;
                        }
                        else
                        {
                            moveArray[i] = true;
                        }
                    }
                }
            }
        }

        public void Possible_moves_villain()
        {
            for (int i = 0; i < 4; i++)
            {
                if(myNeighbours[i] != null)
                {
                    if (myNeighbours[i].MyType == TILETYPE.BLOCK || myNeighbours[i].MyType == TILETYPE.BOX)
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
    }
}
