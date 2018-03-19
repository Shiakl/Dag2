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
        const int x_gridSize = 15;
        const int y_gridSize = 15;
        const int NUM_OF_TILES = x_gridSize*y_gridSize; //Number of tiles on the field
        private Tile[] playfield; //Tile class array

        //Constructor
        public Field()
        {
            playfield = new Tile[NUM_OF_TILES];
        }

        public Tile[] getPlayfield { get { return playfield; } } //get current playfield

        //Assign Type values to tiles in a Tile class array depending on playfield size
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

        PictureBox[,] pb = new PictureBox[y_gridSize,x_gridSize];
        private Bitmap _buffer;
        private Size _bufferSize;
        public void CreateField(Form PlayForm)
        {
            PlayForm.Size = new Size(x_gridSize * Tile.tileSize * 2, y_gridSize * Tile.tileSize + 4 * Tile.tileSize);
            PlayForm.StartPosition = FormStartPosition.Manual;
            PlayForm.Left = 50;
            PlayForm.Top = 50;
            /*Create labels to track tile Neighbours
            string[] labelNeighbours = new string[4];
            labelNeighbours[0] = "Left";
            labelNeighbours[1] = "Right";
            labelNeighbours[2] = "Top";
            labelNeighbours[3] = "Bottom";
            for (int labelAmount = 0; labelAmount < 4; labelAmount++)
            {
                Label txt = new Label();
                txt.Location = new Point(Tile.tileSize * x_gridSize+Tile.tileSize, Tile.tileSize+100 + 20*labelAmount);
                txt.AutoSize = true;
                txt.Text = labelNeighbours[labelAmount] + " Neighbour = ";
                PlayForm.Controls.Add(txt);
            }
            */

            //Create a new field with button
            Button button = new Button();
            button.Location = new Point(Tile.tileSize * x_gridSize + Tile.tileSize, Tile.tileSize + 20);
            button.Text = "Generate Field";
            button.AutoSize = true;
            button.Click += new EventHandler(ButtonClick);
            PlayForm.Controls.Add(button);

            _bufferSize = new Size(x_gridSize * Tile.tileSize, x_gridSize * Tile.tileSize);
            _buffer = new Bitmap(_bufferSize.Width, _bufferSize.Height);
            var picture = new PictureBox
            {
                Name = "FieldBox",
                Size = new Size(_bufferSize.Width, _bufferSize.Height),
                Location = new Point(Tile.tileSize, 0),
            };
            PlayForm.Controls.Add(picture);

            using (Graphics graphics = Graphics.FromImage(_buffer))
            {

                //Create a grid of pictureboxes
                int tilecounter = 0;
            for (int y = 0; y < y_gridSize; y++)
            {
                for (int x = 0; x < x_gridSize; x++)
                {
                        /*Create picture box to represent empty tiles and blocks
                        pb[y, x] = new PictureBox();
                        pb[y, x].Location = new Point(y * Tile.tileSize+ Tile.tileSize, x * Tile.tileSize+ Tile.tileSize);
                        pb[y, x].Width = Tile.tileSize;
                        pb[y, x].Height = Tile.tileSize;
                        pb[y, x].Visible = true;
                        pb[y, x].BorderStyle = BorderStyle.FixedSingle;
                        playfield[tilecounter].Check_Tile_Type();
                        pb[y, x].Image = playfield[tilecounter].myImage;
                        PlayForm.Controls.Add(pb[y, x]);
                        */

                        playfield[tilecounter].Check_Tile_Type();
                        graphics.DrawImage(playfield[tilecounter].myImage, x * Tile.tileSize, y * Tile.tileSize + Tile.tileSize, Tile.tileSize, Tile.tileSize);
   

                    //Add neighbours to Array in Tile Class
                    if (tilecounter > x_gridSize-1)
                    {
                        playfield[tilecounter]._neighbourTop = playfield[tilecounter - x_gridSize];
                    }
                    if (tilecounter<NUM_OF_TILES-1-x_gridSize)
                    {
                        playfield[tilecounter]._neighbourBottom = playfield[tilecounter + x_gridSize];
                    }
                    if (tilecounter % x_gridSize < x_gridSize - 1)
                    {
                        playfield[tilecounter]._neighbourRight = playfield[tilecounter +1];
                    }
                    if (tilecounter % x_gridSize > 0)
                    {
                        playfield[tilecounter]._neighbourLeft = playfield[tilecounter - 1];
                    }
                    tilecounter++;
                    picture.Image = _buffer;
                }
            }
            }

        }
    }


}
