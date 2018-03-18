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
                if (i <= ((NUM_OF_TILES-1) * tileRatio))
                {
                    playfield[i] = new Tile { MyType = Tile.TILETYPE.TILE };
                    i++;
                }
                else if (i > ((NUM_OF_TILES - 1) * wallRatio+tileRatio))
                {
                    playfield[i] = new Tile { MyType = Tile.TILETYPE.BLOCK };
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
                for(int i = 0; i < NUM_OF_TILES; i++)
                {
                    //swap 2 tiles
                    int secondTileIndex = rndShuffle.Next(NUM_OF_TILES-1);
                    tempTile = playfield[i];
                    playfield[i] = playfield[secondTileIndex];
                    playfield[secondTileIndex] = tempTile;
                }
            }
        }

        PictureBox[,] pb = new PictureBox[y_gridSize,x_gridSize];
        public void CreateField(Form PlayForm)
        {
            //Create labels to track tile Neighbours
            string[] labelNeighbours = new string[4];
            labelNeighbours[0] = "Left";
            labelNeighbours[1] = "Right";
            labelNeighbours[2] = "Top";
            labelNeighbours[3] = "Bottom";
            PlayForm.Size = new Size(x_gridSize*Tile.tileSize*2, y_gridSize * Tile.tileSize+4*Tile.tileSize);
            for(int labelAmount = 0; labelAmount < 4; labelAmount++)
            {
                Label txt = new Label();
                txt.Location = new Point(Tile.tileSize * x_gridSize+Tile.tileSize, Tile.tileSize + 20*labelAmount);
                txt.AutoSize = true;
                txt.Text = labelNeighbours[labelAmount] + " Neighbour = ";
                PlayForm.Controls.Add(txt);
            }
            //

            //Create a grid of pictureboxes
            int tilecounter = 0;
            for (int y = 0; y < y_gridSize; y++)
            {
                for (int x = 0; x < x_gridSize; x++)
                {
                    pb[y, x] = new PictureBox();
                    pb[y, x].Location = new Point(y * Tile.tileSize+ Tile.tileSize, x * Tile.tileSize+ Tile.tileSize);
                    pb[y, x].Width = Tile.tileSize;
                    pb[y, x].Height = Tile.tileSize;
                    pb[y, x].Visible = true;
                    pb[y, x].BorderStyle = BorderStyle.FixedSingle;
                    pb[y, x].BringToFront();
                    playfield[tilecounter].Check_Tile_Type();
                    pb[y, x].Image = playfield[tilecounter].myImage;
                    PlayForm.Controls.Add(pb[y, x]);


                    tilecounter++;
                }
            }

        }
    }


}
