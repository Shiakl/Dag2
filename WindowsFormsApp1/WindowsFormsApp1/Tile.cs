using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Vang_de_volger
{
    class Tile
    {
        public const int tileSize = 40;

        public enum TILETYPE
        {
            BLOCK,
            TILE
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

        private Tile[] _myNeighbours = new Tile[4];
        public Tile _neighbourLeft;
        public Tile _neighbourRight;
        public Tile _neighbourTop;
        public Tile _neighbourBottom;
        public void AddNeighbours()
        {
            _myNeighbours[0] = _neighbourLeft;
            _myNeighbours[1] = _neighbourRight;
            _myNeighbours[2] = _neighbourTop;
            _myNeighbours[3] = _neighbourBottom;
        }

        public Tile NeighbourLeft
        {
            get
            {
                return _neighbourLeft;
            }
            set
            {
                _neighbourLeft = value;
            }
        }

        public Tile NeighbourRight
        {
            get
            {
                return _neighbourRight;
            }
            set
            {
                _neighbourRight = value;
            }
        }

        public Tile NeighbourTop
        {
            get
            {
                return _neighbourTop;
            }
            set
            {
                _neighbourTop = value;
            }
        }

        public Tile NeighbourBottom
        {
            get
            {
                return _neighbourBottom;
            }
            set
            {
                _neighbourBottom = value;
            }
        }

    }
}
