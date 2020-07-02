using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonxuKnightsTour
{
    /* Data structure used as a coordinate system for the
     * possible moves a Knight can make.
     */
    class KnightMoves
    {
        private int x;
        private int y;

        public KnightMoves(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }
    }
}
