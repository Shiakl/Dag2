using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Vang_de_volger
{
    class Field
    {
        const int NUM_OF_TILES = MainForm.x_gridSize * MainForm.y_gridSize; //Number of tiles on the field
        private Tile[] playfield; //Tile class array
        private Box[] _box;
        public Image testImage;

        public Bitmap _buffer; //Bitmap that draws the field
        public Bitmap _unitBuffer; //Bitmap that draws the units (Hero, Box, Villain) on field
        public Size bufferSize; //Size of the bitmap
        public Tile heroTile; // public Point heroPosition = new Point(); //Position of the Hero
        public Tile villainTile; //public Point villainPosition = new Point(); //Position of the Villain
        Hero mouse = new Hero();
        Villain slime = new Villain();

        public Point[] tilePoint = new Point[NUM_OF_TILES];
        private Tile.TILETYPE[] typeArray_Tiles = new Tile.TILETYPE[NUM_OF_TILES];

        //Constructor
        public Field()
        {
            playfield = new Tile[NUM_OF_TILES];
            Create_Tiles();
        }


        //Assign Type values to tiles in a Tile class array depending on playfield size

        int boxRatio = NUM_OF_TILES / 5;

        void ButtonClick(object sender, EventArgs e)
        {
            Application.Restart();
        }

        public void Create_Tiles()
        {
            //Put MYTYPE values in an array
            int i = 0;
            double wallRatio = 0.05;
            double tileRatio = 1 - wallRatio;

            while (i < NUM_OF_TILES)
            {
                if (i <= (NUM_OF_TILES * wallRatio) && i > 0)
                {
                    typeArray_Tiles[i] = Tile.TILETYPE.BLOCK;
                    i++;
                }
                else if (i < (NUM_OF_TILES * wallRatio + boxRatio) && i >= (NUM_OF_TILES * wallRatio))
                {
                    typeArray_Tiles[i] = Tile.TILETYPE.BOX;
                    i++;
                }
                else
                {
                    typeArray_Tiles[i] = Tile.TILETYPE.TILE;
                    i++;
                }
            }

            //Shuffle the tileArray
            Random rndShuffle = new Random();
            Tile.TILETYPE tempType;

            //shuffle 100 times
            for (int shuffle = 0; shuffle < 100; shuffle++)
            {
                for (int j = 1; j < NUM_OF_TILES - 1; j++)
                {
                    //swap 2 tiles
                    int randomTypeNR = rndShuffle.Next(1, NUM_OF_TILES - 2);
                    tempType = typeArray_Tiles[j];
                    typeArray_Tiles[j] = typeArray_Tiles[randomTypeNR];
                    typeArray_Tiles[randomTypeNR] = tempType;
                }
            }
        }

        Point tempPoint;
        //Function for creating the field and all the tiles.
        public void CreateField(Form PlayForm, PictureBox picture)
        {
            //Create buffering bitmap
            bufferSize = new Size(MainForm.x_gridSize * MainForm.tileSize, MainForm.x_gridSize * MainForm.tileSize);
            _buffer = new Bitmap(bufferSize.Width, bufferSize.Height);
            _unitBuffer = new Bitmap(bufferSize.Width, bufferSize.Height);

            //Draw all the tiles and units
            using (Graphics graphics = Graphics.FromImage(_buffer))
            {
                //Create a grid of pictureboxes
                int tilecounter = 0;
                _box = new Box[boxRatio];
                int boxcounter = 0;
                for (int y = 0; y < MainForm.y_gridSize; y++)
                {
                    for (int x = 0; x < MainForm.x_gridSize; x++)
                    {
                        tempPoint.X = x * MainForm.tileSize;
                        tempPoint.Y = y * MainForm.tileSize;
                        playfield[tilecounter] = new Tile(typeArray_Tiles[tilecounter], tempPoint, Image.FromFile(@"..\..\Resources\Tile.jpg"));
                        playfield[tilecounter].Check_Tile_Type();

                        //Draw each tile
                        graphics.DrawImage(playfield[tilecounter].myImage, playfield[tilecounter].pointTracker.X, playfield[tilecounter].pointTracker.Y, MainForm.tileSize, MainForm.tileSize);

                        if (playfield[tilecounter].MyType == Tile.TILETYPE.BOX)
                        {
                            _box[boxcounter] = new Box();
                            _box[boxcounter].pointTracker = playfield[tilecounter].pointTracker;
                            graphics.DrawImage(_box[boxcounter].myImage, _box[boxcounter].pointTracker.X, _box[boxcounter].pointTracker.Y, _box[boxcounter].myImage.Size.Width, _box[boxcounter].myImage.Size.Height);
                            boxcounter++;
                        }

                        tilecounter++;
                    }
                }

                //Add tile neighbours
                for (int tc = 0; tc<NUM_OF_TILES;tc++)
                {
                    //Add neighbours to Array in Tile Class
                    if (tc > MainForm.x_gridSize - 1)
                    {
                        playfield[tc].neighbourTop = playfield[tc - MainForm.x_gridSize];
                    }
                    if (tc < NUM_OF_TILES - 1 - MainForm.x_gridSize)
                    {
                        playfield[tc].neighbourBottom = playfield[tc + MainForm.x_gridSize];
                    }
                    if (tc % MainForm.x_gridSize < MainForm.x_gridSize - 1)
                    {
                        playfield[tc].neighbourRight = playfield[tc + 1];
                    }
                    if (tc % MainForm.x_gridSize > 0)
                    {
                        playfield[tc].neighbourLeft = playfield[tc - 1];
                    }
                    playfield[tc].AddNeighbours();
                }

                //Draw the hero on the field
                mouse.pointTracker.X = playfield[0].pointTracker.X; mouse.pointTracker.Y = playfield[0].pointTracker.Y;
                playfield[0].MyType = Tile.TILETYPE.HERO;
                slime.pointTracker.X = playfield[NUM_OF_TILES - 1].pointTracker.X; slime.pointTracker.Y = playfield[NUM_OF_TILES - 1].pointTracker.Y;
                playfield[NUM_OF_TILES - 1].MyType = Tile.TILETYPE.VILLAIN;

                graphics.DrawImage(mouse.myImage, mouse.pointTracker.X, mouse.pointTracker.Y, mouse.myImage.Size.Width, mouse.myImage.Size.Height);
                graphics.DrawImage(slime.myImage, slime.pointTracker.X, slime.pointTracker.Y, slime.myImage.Size.Width, slime.myImage.Size.Height);

            }
            picture.Image = _buffer;
        }

        //Method for redrawing the game
        public void Draw(PictureBox picture)
        {
            _buffer = new Bitmap(bufferSize.Width, bufferSize.Height);
            using (Graphics graphics = Graphics.FromImage(_buffer))
            {

                //Draw Tiles               
                for (int k = 0; k < NUM_OF_TILES; k++)
                {
                    graphics.DrawImage(playfield[k].myImage, playfield[k].pointTracker.X, playfield[k].pointTracker.Y, MainForm.tileSize, MainForm.tileSize);
                }

                //Draw Boxes
                for (int i = 0; i < boxRatio; i++)
                {
                    graphics.DrawImage(_box[i].myImage, _box[i].pointTracker.X, _box[i].pointTracker.Y, _box[i].myImage.Size.Width, _box[i].myImage.Size.Height);
                }
                //Draw Hero and villain
                graphics.DrawImage(mouse.myImage, mouse.pointTracker.X, mouse.pointTracker.Y, mouse.myImage.Size.Width, mouse.myImage.Size.Height);
                graphics.DrawImage(slime.myImage, slime.pointTracker.X, slime.pointTracker.Y, slime.myImage.Size.Width, slime.myImage.Size.Height);
            }
            picture.Image = _buffer;
        }

        public void Swap_contain(Tile old_Tile, Tile new_Tile)
        {
            Point temppPoint = new Point(0, 0);
            Tile temp_Tile = new Tile(Tile.TILETYPE.TILE, temppPoint, Image.FromFile(@"..\..\Resources\Tile.jpg"));

            temp_Tile.MyType = old_Tile.MyType;
            old_Tile.MyType = new_Tile.MyType;
            new_Tile.MyType = temp_Tile.MyType;
        }

        public void Tile_handler()
        {
            if(MyType == Tile.TILETYPE.TILE)
            {
                //Potential fix: work with enum TILETYPE numbers instead of variable names
                Tile old_Tile = Tile.TILETYPE.TILE;
                Tile new_Tile = Tile.TILETYPE.HERO;

                Swap_contain(old_Tile, new_Tile);

            }
            else if (MyType == Tile.TILETYPE.BLOCK)
            {
                //Stop movement function

            }
            //Add extra type handlers after functionality tests 
        }

        public void Move_check_field(string direction)
        {
            using (Graphics graphics = Graphics.FromImage(_buffer))
            {
                heroTile.Tile_check_movement(mouse.pointTracker, direction);
            }
        }


        public void Villain_random_move(Tile villainTile)
        {
            villainTile = playfield[NUM_OF_TILES - 1];
            villainTile.Possible_moves_villain();
            int move_Numbers = 0;
            int arraycount = 0;
            string[] possible_Directions = new string[4];
            for (int a = 0; a < villainTile.moveArray.Length; a++)
            {
                if (villainTile.moveArrayVillain[a] == true)
                {
                    possible_Directions[arraycount] = villainTile.directions[a];
                    arraycount++;
                }
            }

            Random rndDirection = new Random();
            int villain_Direction = rndDirection.Next(0, arraycount);
            string chosen_Random_Direction = possible_Directions[villain_Direction];
            villainTile.Villain_Move(chosen_Random_Direction);
            slime.pointTracker.X -= MainForm.tileSize;
            Swap_contain(villainTile, villainTile.NeighbourLeft);
        }

        int move_index = 0;
        public void testVillainMove(PictureBox picturet)
        {
            Draw(picturet);
        }
    }
}
