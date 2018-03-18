using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Tile : Field
    {
        public const int tileSize = 40;

        public enum TILETYPE
        {
            WALL,
            TILE
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

        private void AddNeighbours()
        {
            _myNeighbours[0] = _neighbourLeft;
            _myNeighbours[1] = _neighbourRight;
            _myNeighbours[2] = _neighbourTop;
            _myNeighbours[3] = _neighbourBottom;
        }
        private Tile _neighbourLeft;
        private Tile _neighbourRight;
        private Tile _neighbourTop;
        private Tile _neighbourBottom;

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
