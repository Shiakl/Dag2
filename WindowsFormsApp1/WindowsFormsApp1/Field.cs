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
                                // public Point heroPosition = new Point(); //Position of the Hero
        public Tile heroTile;
        //public Point villainPosition = new Point(); //Position of the Villain
        public Tile villainTile;
        Hero player = new Hero();
        Villain enemy = new Villain();

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
                _box = new Box[boxRatio];
                int tilecounter = 0;
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
                            playfield[tilecounter].MyBox = _box[boxcounter];
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
                player.pointTracker.X = playfield[0].pointTracker.X; player.pointTracker.Y = playfield[0].pointTracker.Y;
                playfield[0].MyType = Tile.TILETYPE.HERO;
                heroTile = playfield[0];
                villainTile = playfield[NUM_OF_TILES - 1];
                enemy.pointTracker.X = playfield[NUM_OF_TILES - 1].pointTracker.X; enemy.pointTracker.Y = playfield[NUM_OF_TILES - 1].pointTracker.Y;
                playfield[NUM_OF_TILES - 1].MyType = Tile.TILETYPE.VILLAIN;
                graphics.DrawImage(player.myImage, player.pointTracker.X, player.pointTracker.Y, player.myImage.Size.Width, player.myImage.Size.Height);
                graphics.DrawImage(enemy.myImage, enemy.pointTracker.X, enemy.pointTracker.Y, enemy.myImage.Size.Width, enemy.myImage.Size.Height);
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
                graphics.DrawImage(player.myImage, player.pointTracker.X, player.pointTracker.Y, player.myImage.Size.Width, player.myImage.Size.Height);
                graphics.DrawImage(enemy.myImage, enemy.pointTracker.X, enemy.pointTracker.Y, enemy.myImage.Size.Width, enemy.myImage.Size.Height);
            }
            picture.Image = _buffer;
        }

        //Swaps two Tile class MyTypes
        public void Swap_MyType(Tile old_Tile, Tile new_Tile)
        {
            Tile.TILETYPE tempType = new Tile.TILETYPE();

            tempType = old_Tile.MyType;
            old_Tile.MyType = new_Tile.MyType;
            new_Tile.MyType = tempType;
        }

        //Swaps two Tile class MyBoxes
        public void Swap_MyBox(Tile old_Tile, Tile new_Tile)
        {
            Box temp_Box = new Box();

            temp_Box = old_Tile.MyBox;
            old_Tile.MyBox = new_Tile.MyBox;
            new_Tile.MyBox = temp_Box;
        }

        /// <summary>
        ///     Method for randomly moving the Villain.   
        /// </summary>
        string chosen_Random_Direction; //For the Villain
        string chosen_Direction;        //For the hero
        string[] possible_Directions = new string[4];
        bool hero_Search = false;
        public void Villain_random_move(Tile villainTile)
        {
            villainTile.Possible_moves_villain();
            int move_Numbers = 0;
            int arraycount = 0;

            for (int scan = 0; scan < 4; scan++)
            {
                if (villainTile.myNeighbours[scan] != null)
                {
                    if (villainTile.myNeighbours[scan].MyType == Tile.TILETYPE.HERO)
                    {
                        hero_Search = true;
                        Move_Unit(enemy, villainTile.all_Directions[scan], villainTile);
                    }
                }
            }
            if (hero_Search == false)
            {
                for (int a = 0; a < villainTile.moveArray.Length; a++)
                {
                    if (villainTile.moveArrayVillain[a] == true)
                    {
                        possible_Directions[arraycount] = villainTile.all_Directions[a];
                        arraycount++;
                    }
                }

                Random rndDirection = new Random();
                int villain_Direction = rndDirection.Next(0, arraycount);
                chosen_Random_Direction = possible_Directions[villain_Direction];
                Move_Unit(enemy, chosen_Random_Direction, villainTile);
            }
        }

        //Count the not-possible moves for the villain, if the move_count is 4 the villain has no possible moves and loses
        int move_count;
        public bool villain_Lose()
        {
            villainTile.Possible_moves_villain();
            move_count = 0;
            for(int cc = 0; cc < 4; cc++)
            {
                if (villainTile.moveArrayVillain[cc] == false)
                {
                    move_count++;
                }
            }
            if (move_count== 4)
            {
                return true;
            }
            return false;
        }

        public void Hero_move(Tile heroTile, int hero_Direction)
        {
            heroTile.Possible_moves();
            if (heroTile.myNeighbours[hero_Direction] != null)
            {
                if (heroTile.myNeighbours[hero_Direction].MyType == Tile.TILETYPE.BOX)
                {
                    heroTile.myNeighbours[hero_Direction].Possible_moves();
                    if (heroTile.myNeighbours[hero_Direction].moveArray[hero_Direction] == true)
                    {
                        chosen_Direction = heroTile.all_Directions[hero_Direction];
                        Move_Unit(heroTile.myNeighbours[hero_Direction].MyBox, chosen_Direction, heroTile.myNeighbours[hero_Direction]);
                        Move_Unit(player, chosen_Direction, heroTile);
                    }
                }
                else if (heroTile.moveArray[hero_Direction] == true)
                {
                    chosen_Direction = heroTile.all_Directions[hero_Direction];
                    Move_Unit(player, chosen_Direction, heroTile);
                }
            }
        }

        public void Box_Move(Tile boxTile, int box_direction)
        {
            if (boxTile.myNeighbours[box_direction].MyType == Tile.TILETYPE.BOX)
            {
                Box_MoveCheck(boxTile.myNeighbours[box_direction],box_direction);
            }
            else
            {

            }
        }

        public void Box_MoveCheck(Tile boxTile, int box_direction)
        {
            if (boxTile.moveArray[box_direction] == true)
            {
                Box_Move(boxTile,box_direction);
            }
            else
            {

            }
        }

        public void Catch_Hero(Tile enemyTile)
        {
            for (int scan = 0; scan < 4; scan++)
            {
                if (enemyTile.myNeighbours[scan].MyType == Tile.TILETYPE.HERO)
                {
                    Move_Unit(enemy,enemyTile.all_Directions[scan],enemyTile);
                }
            }
        }

        /// <summary>
        /// Move a unit after possible move direction is confirmed.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="direction"></param>
        /// <param name="unitTile"></param>
        public void Move_Unit(Unit unit, string direction, Tile unitTile)
        {
            if (unit is Villain)
            {
                //unit.pointTracker.X -= MainForm.tileSize;
                if (direction.Equals(unitTile.all_Directions[0]))
                {
                    enemy.pointTracker.X -= MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourLeft);
                    villainTile = unitTile.neighbourLeft;
                }
                else if (direction.Equals(unitTile.all_Directions[1]))
                {
                    enemy.pointTracker.X += MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourRight);
                    villainTile = unitTile.neighbourRight;
                }
                else if (direction.Equals(unitTile.all_Directions[2]))
                {
                    enemy.pointTracker.Y -= MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourTop);
                    villainTile = unitTile.neighbourTop;
                }
                else if (direction.Equals(unitTile.all_Directions[3]))
                {
                    enemy.pointTracker.Y += MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourBottom);
                    villainTile = unitTile.neighbourBottom;
                }
            }
            else if (unit is Hero)
            {
                //unit.pointTracker.X -= MainForm.tileSize;
                if (direction.Equals(unitTile.all_Directions[0]))
                {
                    player.pointTracker.X -= MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourLeft);
                    heroTile = unitTile.neighbourLeft;
                }
                else if (direction.Equals(unitTile.all_Directions[1]))
                {
                    player.pointTracker.X += MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourRight);
                    heroTile = unitTile.neighbourRight;
                }
                else if (direction.Equals(unitTile.all_Directions[2]))
                {
                    player.pointTracker.Y -= MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourTop);
                    heroTile = unitTile.neighbourTop;
                }
                else if (direction.Equals(unitTile.all_Directions[3]))
                {
                    player.pointTracker.Y += MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourBottom);
                    heroTile = unitTile.neighbourBottom;
                }
            }
            else if (unit is Box)
            {
                //unit.pointTracker.X -= MainForm.tileSize;
                if (direction.Equals(unitTile.all_Directions[0]))
                {
                    unitTile.MyBox.pointTracker.X -= MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourLeft);
                    Swap_MyBox(unitTile, unitTile.neighbourLeft);

                }
                else if (direction.Equals(unitTile.all_Directions[1]))
                {
                    unitTile.MyBox.pointTracker.X += MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourRight);
                    Swap_MyBox(unitTile, unitTile.neighbourRight);
                }
                else if (direction.Equals(unitTile.all_Directions[2]))
                {
                    unitTile.MyBox.pointTracker.Y -= MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourTop);
                    Swap_MyBox(unitTile, unitTile.neighbourTop);
                }
                else if (direction.Equals(unitTile.all_Directions[3]))
                {
                    unitTile.MyBox.pointTracker.Y += MainForm.tileSize;
                    Swap_MyType(unitTile, unitTile.NeighbourBottom);
                    Swap_MyBox(unitTile, unitTile.neighbourBottom);
                }
            }
            
        }
    }
}
