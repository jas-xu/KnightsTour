using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jasonxuKnightsTour
{
    /* Inherits from the ChessBoard class.
     * This class is used for the heuristics method.
     */
    class ChessBoardHeuristic : ChessBoard
    {

        public ChessBoardHeuristic(int x, int y)
        {
            setX(x);
            setY(y);
            setMoves(0);
        }

        /* Creates a chessboard where the squares are numbered
         * by priority. Each square represents how many moves a Knight
         * can make from that square. The lower the number, the higher
         * the priority it is for the Knight to move onto that square.
         */
        public int[,] HeuristicsBoard()
        {
            // New chessboard for holding heuristics values.
            int[,] hueristicsBoard = new int[8, 8];

            // All rows for the heuristics board.
            for (int i = 0; i < 8; i++)
            {
                // All columns for the heuristics board.
                for (int j = 0; j < 8; j++)
                {
                    // Heuristic value for each square based on the legal moves that can be made.
                    hueristicsBoard[i, j] = this.LegalMoves(i, j).Count;
                }
            }
            return hueristicsBoard;
        }

    }
}
