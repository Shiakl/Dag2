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
        private Tile[] _playfield; //Tile class array
        private Box[] _box;

        private Bitmap _buffer; //Bitmap that draws the field       
        private Size _bufferSize; //Size of the bitmap
        public Tile heroTile;
        public Tile villainTile;
        private Hero _player = new Hero();
        private Villain _enemy = new Villain();

        private Tile.TILETYPE[] _typeArray_Tiles = new Tile.TILETYPE[NUM_OF_TILES];

        //Constructor
        public Field()
        {
            _playfield = new Tile[NUM_OF_TILES];
            _Assign_Types();
        }


        //Assign Type values to tiles in a Tile class array depending on playfield size
        private const double _boxRatio = 0.2; //Determine the ratio of boxes:tiles
        private int _boxAmount = Convert.ToInt32(Math.Floor(NUM_OF_TILES* _boxRatio));       
        private void _Assign_Types()
        {
            //Put MYTYPE values in an array
            int i = 0;
            double wallRatio = 0.05; //Determines the ratio of blocks:tiles
            double tileRatio = 1 - wallRatio;

            while (i < NUM_OF_TILES)
            {
                if (i <= (NUM_OF_TILES * wallRatio) && i > 0)
                {
                    _typeArray_Tiles[i] = Tile.TILETYPE.BLOCK;
                    i++;
                }
                else if (i <= (NUM_OF_TILES * wallRatio + _boxAmount) && i > (NUM_OF_TILES * wallRatio))
                {
                    _typeArray_Tiles[i] = Tile.TILETYPE.BOX;
                    i++;
                }
                else
                {
                    _typeArray_Tiles[i] = Tile.TILETYPE.TILE;
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
                    //swap 2 types
                    int randomTypeNR = rndShuffle.Next(1, NUM_OF_TILES - 2);
                    tempType = _typeArray_Tiles[j];
                    _typeArray_Tiles[j] = _typeArray_Tiles[randomTypeNR];
                    _typeArray_Tiles[randomTypeNR] = tempType;
                }
            }
        }

/// <summary>
/// First the method _Assign_Types is called to create an array with shuffled types.
/// Then the bitmap that's responsible for holding all the images is initialized.
/// The double for loop then creates a grid of Tile classes.
/// If a 'myType' of one of those tiles was a BOX then a box class is created added to the Tile's myBox.
/// 
/// </summary>
        private Point _tempPoint;
        public void CreateField(Form PlayForm, PictureBox picture, Label button1, Label button2)
        {
            _Assign_Types();

            //Create buffering bitmap
            _bufferSize = new Size(MainForm.x_gridSize * MainForm.tileSize, MainForm.y_gridSize * MainForm.tileSize);
            _buffer = new Bitmap(_bufferSize.Width, _bufferSize.Height);

            //Draw all the tiles and units
            using (Graphics graphics = Graphics.FromImage(_buffer))
            {
                //Create a grid of pictureboxes
                _box = new Box[_boxAmount];
                int tilecounter = 0;
                int boxcounter = 0;
                for (int y = 0; y < MainForm.y_gridSize; y++)
                {
                    for (int x = 0; x < MainForm.x_gridSize; x++)
                    {
                        _tempPoint.X = x * MainForm.tileSize;
                        _tempPoint.Y = y * MainForm.tileSize;
                        _playfield[tilecounter] = new Tile(_typeArray_Tiles[tilecounter], _tempPoint, Image.FromFile(@"..\..\Resources\Tile.jpg"));
                        _playfield[tilecounter].Check_Tile_Type();

                        //Draw each tile
                        graphics.DrawImage(_playfield[tilecounter].myImage, _playfield[tilecounter].pointTracker.X, _playfield[tilecounter].pointTracker.Y, MainForm.tileSize, MainForm.tileSize);

                        if (_playfield[tilecounter].MyType == Tile.TILETYPE.BOX)
                        {
                            _box[boxcounter] = new Box();
                            _box[boxcounter].pointTracker = _playfield[tilecounter].pointTracker;
                            _playfield[tilecounter].MyBox = _box[boxcounter];
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
                        _playfield[tc].neighbourTop = _playfield[tc - MainForm.x_gridSize];
                    }
                    if (tc < NUM_OF_TILES - MainForm.x_gridSize)
                    {
                        _playfield[tc].neighbourBottom = _playfield[tc + MainForm.x_gridSize];
                    }
                    if (tc % MainForm.x_gridSize < MainForm.x_gridSize - 1)
                    {
                        _playfield[tc].neighbourRight = _playfield[tc + 1];
                    }
                    if (tc % MainForm.x_gridSize > 0)
                    {
                        _playfield[tc].neighbourLeft = _playfield[tc - 1];
                    }
                    _playfield[tc].AddNeighbours();
                }

                //Draw the hero and villain on the field and assign their tile.
                _player.pointTracker.X = _playfield[0].pointTracker.X; _player.pointTracker.Y = _playfield[0].pointTracker.Y;
                _playfield[0].MyType = Tile.TILETYPE.HERO;
                heroTile = _playfield[0];
                villainTile = _playfield[NUM_OF_TILES - 1];
                _playfield[NUM_OF_TILES - 1].MyType = Tile.TILETYPE.VILLAIN;
                _enemy.pointTracker.X = _playfield[NUM_OF_TILES - 1].pointTracker.X; _enemy.pointTracker.Y = _playfield[NUM_OF_TILES - 1].pointTracker.Y;
                graphics.DrawImage(_player.myImage, _player.pointTracker.X, _player.pointTracker.Y, _player.myImage.Size.Width, _player.myImage.Size.Height);
                graphics.DrawImage(_enemy.myImage, _enemy.pointTracker.X, _enemy.pointTracker.Y, _enemy.myImage.Size.Width, _enemy.myImage.Size.Height);
            }
            picture.Image = _buffer;
        }

        //Method for redrawing the game
        public void Draw(PictureBox picture)
        {
            _buffer = new Bitmap(_bufferSize.Width, _bufferSize.Height);
            using (Graphics graphics = Graphics.FromImage(_buffer))
            {

                //Draw Tiles               
                for (int k = 0; k < NUM_OF_TILES; k++)
                {
                    graphics.DrawImage(_playfield[k].myImage, _playfield[k].pointTracker.X, _playfield[k].pointTracker.Y, MainForm.tileSize, MainForm.tileSize);
                }

                //Draw Boxes
                for (int i = 0; i < _boxAmount; i++)
                {
                    graphics.DrawImage(_box[i].myImage, _box[i].pointTracker.X, _box[i].pointTracker.Y, _box[i].myImage.Size.Width, _box[i].myImage.Size.Height);
                }
                //Draw Hero and villain
                graphics.DrawImage(_player.myImage, _player.pointTracker.X, _player.pointTracker.Y, _player.myImage.Size.Width, _player.myImage.Size.Height);
                graphics.DrawImage(_enemy.myImage, _enemy.pointTracker.X, _enemy.pointTracker.Y, _enemy.myImage.Size.Width, _enemy.myImage.Size.Height);
                              
            }
            picture.Image = _buffer;
        }

        //Swaps two Tile class MyTypes
        private void _Swap_MyType(Tile old_Tile, Tile new_Tile)
        {
            Tile.TILETYPE tempType = new Tile.TILETYPE();

            tempType = old_Tile.MyType;
            old_Tile.MyType = new_Tile.MyType;
            new_Tile.MyType = tempType;
        }

        //Swaps two Tile class MyBoxes
        private void _Swap_MyBox(Tile old_Tile, Tile new_Tile)
        {
            Box temp_Box = new Box();

            temp_Box = old_Tile.MyBox;
            old_Tile.MyBox = new_Tile.MyBox;
            new_Tile.MyBox = temp_Box;
        }

        /// <summary>
        ///     Method for randomly moving the Villain.   
        /// </summary>
        private string _chosen_Random_Direction; //For the Villain
        private string _chosen_Direction;        //For the hero
        private string[] _possible_Directions = new string[4];
        private bool _hero_Search = false;
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
                        _hero_Search = true;
                        Move_Unit(_enemy, villainTile.all_Directions[scan], villainTile);
                    }
                }
            }
            if (_hero_Search == false)
            {
                for (int a = 0; a < villainTile.moveArray.Length; a++)
                {
                    if (villainTile.moveArrayVillain[a] == true)
                    {
                        _possible_Directions[arraycount] = villainTile.all_Directions[a];
                        arraycount++;
                    }
                }

                Random rndDirection = new Random();
                int villain_Direction = rndDirection.Next(0, arraycount);
                _chosen_Random_Direction = _possible_Directions[villain_Direction];
                Move_Unit(_enemy, _chosen_Random_Direction, villainTile);
            }
        }

        //Count the not-possible moves for the villain, if the move_count is 4 the villain has no possible moves and loses
        private int _move_count;
        public bool villain_Lose()
        {
            villainTile.Possible_moves_villain();
            _move_count = 0;
            for(int vd = 0; vd < 4; vd++)
            {
                if (villainTile.moveArrayVillain[vd] == false)
                {
                    _move_count++;
                }
            }
            if (_move_count== 4)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Handles moving behaviour of the heroTile. Interactions with the BOX, TILE and VILLAIN objects are handled here.
        /// </summary>
        /// <param name="heroTile">Tile object of the heroTile needed here.</param>
        /// <param name="hero_Direction">Take an int value that corresponds with the moveArray row numbers.</param>
        private List<Tile> _tiles_to_swap = new List<Tile>(); /// tiles_to_swap is used to swap the tile in the direction with the current tile
        private List<Box> _boxes_to_push = new List<Box>(); /// boxes_to_push is called and used when a hero encounters a box. It will check the neighbour of the box using the current direction of the hero.
        private int _boxpushcounter = 0;
        public void Hero_move(Tile heroTile, int hero_Direction)
        {
            _boxpushcounter = 0;
            heroTile.Possible_moves();
            if (heroTile.myNeighbours[hero_Direction] != null)
            {
                if (heroTile.myNeighbours[hero_Direction].MyType == Tile.TILETYPE.BOX)
                {
                    heroTile.myNeighbours[hero_Direction].Possible_moves();
                    if (Check_Box_Row(heroTile,hero_Direction) == true)
                    {
                        _chosen_Direction = heroTile.all_Directions[hero_Direction];
                        for(int b = _boxes_to_push.Count()-1; b>=0; b--)
                        {
                        Move_Unit(_boxes_to_push[b], _chosen_Direction, _tiles_to_swap[b]);
                        }

                        Move_Unit(_player, _chosen_Direction, heroTile);
                    }
                }
                else if (heroTile.moveArray[hero_Direction] == true)
                {
                    _chosen_Direction = heroTile.all_Directions[hero_Direction];
                    Move_Unit(_player, _chosen_Direction, heroTile);
                }
            }
            _boxes_to_push.Clear();
            _tiles_to_swap.Clear();
        }      

        /// <summary>
        /// Function added to check the TILETYPE of a neighbour in the chosen direction from the point of a TILETYPE.BOX object.
        /// This will loop on until an other object than TILETYPE.BOX is detected.
        /// </summary>
        /// <param name="heroTile">The tile the hero is on.</param>
        /// <param name="direction">The direction player wants to move.</param>
        /// <returns></returns>
        public bool Check_Box_Row(Tile heroTile, int direction)
        {
            if (heroTile.myNeighbours[direction] != null)
            {
                if (heroTile.myNeighbours[direction].MyType == Tile.TILETYPE.BOX)
                {
                    _boxes_to_push.Add(heroTile.myNeighbours[direction].MyBox);
                    _tiles_to_swap.Add(heroTile.myNeighbours[direction]);
                    return Check_Box_Row(heroTile.myNeighbours[direction], direction);
                }
                else if (heroTile.myNeighbours[direction].MyType == Tile.TILETYPE.TILE)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else return false;
        }

        /// <summary>
        /// Function added to enable Villain to catch the Hero
        /// </summary>
        /// <param name="enemyTile"> The tile the villain is on.</param>
        /// <returns></returns>
        public bool Catch_Hero(Tile enemyTile)
        {
            for (int scan = 0; scan < 4; scan++)
            {
                if (enemyTile.myNeighbours[scan] != null)
                {
                    if (enemyTile.myNeighbours[scan].MyType == Tile.TILETYPE.HERO)
                    {
                        Move_Unit(_enemy, enemyTile.all_Directions[scan], enemyTile);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Move a unit after possible move direction is confirmed.
        /// </summary>
        /// <param name="unit"> Villain,hero or Box childclass that executes the move.</param>
        /// <param name="direction"> The direction the unit is moving</param>
        /// <param name="unitTile"> The tile the unit is on.</param>
        private void Move_Unit(Unit unit, string direction, Tile unitTile)
        {
            if (unit is Villain)
            {
                if (direction.Equals(unitTile.all_Directions[0]))
                {
                    _enemy.pointTracker.X -= MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourLeft);
                    villainTile = unitTile.neighbourLeft;
                }
                else if (direction.Equals(unitTile.all_Directions[1]))
                {
                    _enemy.pointTracker.X += MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourRight);
                    villainTile = unitTile.neighbourRight;
                }
                else if (direction.Equals(unitTile.all_Directions[2]))
                {
                    _enemy.pointTracker.Y -= MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourTop);
                    villainTile = unitTile.neighbourTop;
                }
                else if (direction.Equals(unitTile.all_Directions[3]))
                {
                    _enemy.pointTracker.Y += MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourBottom);
                    villainTile = unitTile.neighbourBottom;
                }
            }
            else if (unit is Hero)
            {
                if (direction.Equals(unitTile.all_Directions[0]))
                {
                    _player.pointTracker.X -= MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourLeft);
                    heroTile = unitTile.neighbourLeft;
                }
                else if (direction.Equals(unitTile.all_Directions[1]))
                {
                    _player.pointTracker.X += MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourRight);
                    heroTile = unitTile.neighbourRight;
                }
                else if (direction.Equals(unitTile.all_Directions[2]))
                {
                    _player.pointTracker.Y -= MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourTop);
                    heroTile = unitTile.neighbourTop;
                }
                else if (direction.Equals(unitTile.all_Directions[3]))
                {
                    _player.pointTracker.Y += MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourBottom);
                    heroTile = unitTile.neighbourBottom;
                }
            }
            else if (unit is Box)
            {
                if (direction.Equals(unitTile.all_Directions[0]))
                {
                    unitTile.MyBox.pointTracker.X -= MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourLeft);
                    _Swap_MyBox(unitTile, unitTile.neighbourLeft);

                }
                else if (direction.Equals(unitTile.all_Directions[1]))
                {
                    unitTile.MyBox.pointTracker.X += MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourRight);
                    _Swap_MyBox(unitTile, unitTile.neighbourRight);
                }
                else if (direction.Equals(unitTile.all_Directions[2]))
                {
                    unitTile.MyBox.pointTracker.Y -= MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourTop);
                    _Swap_MyBox(unitTile, unitTile.neighbourTop);
                }
                else if (direction.Equals(unitTile.all_Directions[3]))
                {
                    unitTile.MyBox.pointTracker.Y += MainForm.tileSize;
                    _Swap_MyType(unitTile, unitTile.NeighbourBottom);
                    _Swap_MyBox(unitTile, unitTile.neighbourBottom);
                }
            }
            
        }
    }
}
