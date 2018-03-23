using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Vang_de_volger
{
    class Field
    {
        const int NUM_OF_TILES = MainForm.x_gridSize* MainForm.y_gridSize; //Number of tiles on the field
        private Tile[] playfield; //Tile class array
        private Box[] _box;

        public Bitmap _buffer; //Bitmap that draws the field
        public Bitmap _unitBuffer; //Bitmap that draws the field
        public Size bufferSize; //Size of the bitmap
        private Image _heroImage = Image.FromFile(@"..\..\Resources\HeroTemp.png");
        public Point heroPosition = new Point(); //Position of the Hero
        public Tile heroTile;
        private Image _villainImage = Image.FromFile(@"..\..\Resources\VillainTemp.png");
        public Point villainPosition = new Point(); //Position of the Villain
        public Tile villainTile;
        private Point[] tileLocation = new Point[NUM_OF_TILES]; //Array holding all tile position

        //Constructor
        public Field()
        {
            playfield = new Tile[NUM_OF_TILES];
            mouse = new Hero();
            slime = new Villain();
        }

        Hero mouse;
        Villain slime;

        //Assign Type values to tiles in a Tile class array depending on playfield size
        int boxRatio = NUM_OF_TILES / 5;
        public void CreateTiles()
        {
            int i = 0;
            double wallRatio = 0.05;
            double tileRatio = 1 - wallRatio;
            
           while(i<NUM_OF_TILES)
            {
                if (i <= (NUM_OF_TILES* wallRatio) && i>0 )
                {
                    playfield[i] = new Tile { MyType = Tile.TILETYPE.BLOCK };
                    i++;
                }
                else if(i<(NUM_OF_TILES * wallRatio + boxRatio)&& i >= (NUM_OF_TILES * wallRatio))
                {
                    playfield[i] = new Tile { MyType = Tile.TILETYPE.BOX};
                    i++;
                }
                else
                {
                    playfield[i] = new Tile { MyType = Tile.TILETYPE.TILE };
                    i++;
                }
            }
        }

        //Shuffle the Tiles class array
        public void ShuffleTiles()
        {
            Random rndShuffle = new Random();
            Tile tempTile;

            //shuffle 100 times
            for(int shuffle = 0; shuffle<100; shuffle++)
            {
                for(int i = 1; i < NUM_OF_TILES-1; i++)
                {
                    //swap 2 tiles
                    int secondTileIndex = rndShuffle.Next(1, NUM_OF_TILES-2);
                    tempTile = playfield[i];
                    playfield[i] = playfield[secondTileIndex];
                    playfield[secondTileIndex] = tempTile;
                }
            }
        }

        void ButtonClick(object sender, EventArgs e)
        {
            Application.Restart();
        }
        //public PictureBox picture = new PictureBox();
        public void CreateField(Form PlayForm,PictureBox picture)
        {
            //Create buffering bitmap
            bufferSize = new Size(MainForm.x_gridSize * MainForm.tileSize, MainForm.x_gridSize * MainForm.tileSize);
            _buffer = new Bitmap(bufferSize.Width, bufferSize.Height);
            /*
            var picture = new PictureBox
            {
                Name = "FieldBox",
                Size = new Size(bufferSize.Width, bufferSize.Height),
                Location = new Point(0, 0),
            };
            PlayForm.Controls.Add(picture);
            */
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
                    playfield[tilecounter].Check_Tile_Type();

                    //Add positions of each tile to the Point Array
                    tileLocation[tilecounter] = new Point(x * MainForm.tileSize, y * MainForm.tileSize);
                    //Draw each tile
                    graphics.DrawImage(playfield[tilecounter].myImage, tileLocation[tilecounter].X, tileLocation[tilecounter].Y, MainForm.tileSize, MainForm.tileSize);

                    if (playfield[tilecounter].MyType == Tile.TILETYPE.BOX)
                    {
                        _box[boxcounter] = new Box();
                        _box[boxcounter].pointTracker = tileLocation[tilecounter];
                        graphics.DrawImage(_box[boxcounter].myImage, _box[boxcounter].pointTracker.X, _box[boxcounter].pointTracker.Y, _box[boxcounter].myImage.Size.Width, _box[boxcounter].myImage.Size.Height);
                        boxcounter++;
                    }

                    //Add neighbours to Array in Tile Class
                    if (tilecounter > MainForm.x_gridSize -1)
                    {
                        playfield[tilecounter].neighbourTop = playfield[tilecounter - MainForm.x_gridSize];
                    }
                    if (tilecounter<NUM_OF_TILES-1- MainForm.x_gridSize)
                    {
                        playfield[tilecounter].neighbourBottom = playfield[tilecounter + MainForm.x_gridSize];
                    }
                    if (tilecounter % MainForm.x_gridSize < MainForm.x_gridSize - 1)
                    {
                        playfield[tilecounter].neighbourRight = playfield[tilecounter +1];
                    }
                    if (tilecounter % MainForm.x_gridSize > 0)
                    {
                        playfield[tilecounter].neighbourLeft = playfield[tilecounter - 1];
                    }
                    tilecounter++;                   
                }
            }
                //Draw the hero on the field
                heroPosition.X = tileLocation[0].X; heroPosition.Y = tileLocation[0].Y;
                playfield[0].MyType = Tile.TILETYPE.HERO;
                playfield[0] = heroTile;
                villainPosition.X = tileLocation[NUM_OF_TILES-1].X; villainPosition.Y = tileLocation[NUM_OF_TILES-1].Y;
                playfield[NUM_OF_TILES-1].MyType = Tile.TILETYPE.VILLAIN;
                playfield[NUM_OF_TILES-1] = villainTile;

                graphics.DrawImage(_heroImage, heroPosition.X, heroPosition.Y, _heroImage.Size.Width, _heroImage.Size.Height);
                graphics.DrawImage(_villainImage, villainPosition.X, villainPosition.Y, _villainImage.Size.Width, _villainImage.Size.Height);

            }
                picture.Image = _buffer;
        }

        /*
        public void Draw(PictureBox picture)
        {
            _buffer = new Bitmap(bufferSize.Width, bufferSize.Height);
            using (Graphics graphics = Graphics.FromImage(_buffer))
            {
                for(int i = 0; i < boxRatio; i++)
                {
                    graphics.DrawImage(_box[i].myImage, _box[i].pointTracker.X, _box[i].pointTracker.Y, _box[i].myImage.Size.Width, _box[i].myImage.Size.Height);
                }
                graphics.DrawImage(_heroImage, heroPosition.X, heroPosition.Y, _heroImage.Size.Width, _heroImage.Size.Height);
                graphics.DrawImage(_villainImage, villainPosition.X, villainPosition.Y, _villainImage.Size.Width, _villainImage.Size.Height);
            }
            picture.Image = _buffer;
        }
        */

        public void Move_check_field(string direction)
        {
            heroTile.Tile_check_movement(heroPosition, direction);

        }

        public void Villain_random_move()
        {

        }

    }


}
