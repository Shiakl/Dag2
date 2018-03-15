using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testfield
{
    class Field
    {
        const int NUM_OF_TILES = 100; //Number of tiles on the field
        const int fieldsize_x = 20;
        const int fieldsize_y = 20;
        private Tile[] playfield; //array of all tiles

        public Field()
        {
            playfield = new Tile[NUM_OF_TILES];
            CreateTiles();
        }

        public Tile[] getPlayfield { get { return playfield; } } //get current playfield

        //Create 100 tile playfield consisting of various tiles
        public void CreateTiles()
        {
           int i = 0;
           double boxRatio = 0.2;
           double blockRatio = 0.05;
           double tileRatio = 1 - boxRatio - blockRatio;
           while(i<NUM_OF_TILES)
            {
                if (i <= ((NUM_OF_TILES-1) * tileRatio))
                {
                    playfield[i] = new Tile { MyType = Tile.TILETYPE.TILE };
                    i++;
                }
                else if (i > ((NUM_OF_TILES - 1) * tileRatio) && i <= ((NUM_OF_TILES - 1) * (blockRatio+tileRatio)))
                {
                    playfield[i] = new Tile { MyType = Tile.TILETYPE.BOX };
                    i++;
                }
                else if (i > ((NUM_OF_TILES - 1) * blockRatio+tileRatio))
                {
                    playfield[i] = new Tile { MyType = Tile.TILETYPE.BLOCK };
                    i++;
                }
            }
        }

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
    }
}
