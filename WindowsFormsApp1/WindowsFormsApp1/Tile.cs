using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Testfield
{
    class Tile : Field
    {
        public enum TILETYPE
        {
            BLOCK,
            BOX,
            TILE
        }

        const int _tileSize_X = 40;
        const int _tileSize_Y = 40;

        Image tileImage = ;

        public TILETYPE MyType { get; set; }

    }
}
