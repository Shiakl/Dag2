using System;

namespace Vang_de_volger
{
    public class Unit : Field
    {
        //[2,2] has been put in to evade error placements.
        protected int[,] startPosition = new int[2, 2];
        protected int speed;

        public Unit(int[,] startPosition, int speed)
        {
            this.startPosition = startPosition;
            this.speed = speed;
        }

        protected void Move()
        {

        }
    }
}