using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace jasonxuKnightsTour
{
    class ChessBoard
    {

        /* Default chess board (all 0s indicates that the Knight
         * has not moved on any squares).
         */
        private int[,] board = new int[8, 8] {
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0}};

        // All possible moves a Knight can make.
        private KnightMoves[] allMoves = new KnightMoves[8] { 
            new KnightMoves(1,2),
            new KnightMoves(1,-2),
            new KnightMoves(-1,2),
            new KnightMoves(-1,-2),
            new KnightMoves(2,1),
            new KnightMoves(2,-1),
            new KnightMoves(-2,1),
            new KnightMoves(-2,-1)};

        private ArrayList possibleMoves; // Used to track legal moves (new squares).

        // Current coordinate on the board.
        private int x;
        private int y;

        private int moves; // Number of moves made.

        public ChessBoard()
        {
            this.setX(0);
            this.setY(0);
            this.setMoves(0);
        }

        public ChessBoard(int x, int y)
        {
            this.setX(x);
            this.setY(y);
            this.setMoves(0);
        }

        /* Returns list of legal moves a Knight is able to make from its current position.
         * Takes into consideration range of chess board and occupied squares.
         */
        public ArrayList LegalMoves(int x, int y)
        {
            possibleMoves = new ArrayList();
            // Checks all 8 possible Knight moves.
            for (int i = 0; i < 8; i++)
            {
                // Adds each possible move to the Knight's current position.
                int movedX = allMoves[i].getX() + x;
                int movedY = allMoves[i].getY() + y;

                // Checks if "moved into position" is legal.
                if ((movedX >= 0 && movedX <= 7) && (movedY >= 0 && movedY <= 7) 
                    && (this.board[movedX, movedY] == 0))
                {
                    // Adds the move to list of legal moves.
                    possibleMoves.Add(allMoves[i]);
                }
            }
            return possibleMoves;
        }

        /* Returns visual representation of
         * the chess board with moves in order.
         */
        public string PrintBoard()
        {
            string boardWithMoves = "";

            // Checks all rows.
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                // Checks all columns.
                for (int j = 0; j < this.board.GetLength(1); j++)
                {
                    // Appends and formats string.
                    boardWithMoves += string.Format("{0, 4}", this.board[i,j].ToString());
                }
                boardWithMoves += '\n';
            }
            return boardWithMoves;
        }

        // Getters and Setters
        public int getX()
        {
            return this.x;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public int getY()
        {
            return this.y;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public int getMoves()
        {
            return this.moves;
        }

        public void setMoves(int moves)
        {
            this.moves = moves;
        }

        public KnightMoves[] getAllMoves()
        {
            return this.allMoves;
        }

        public int[,] getBoard()
        {
            return this.board;
        }

        public void setBoard(int[,] board)
        {
            this.board = board;
        }
    }
}
