using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jasonxuKnightsTour
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string results = "";
            int runs = 1;

            // Checks if user entered their own value for number of runs.
            if (!string.IsNullOrEmpty(textBox3.Text) && Convert.ToInt32(textBox3.Text) > 0)
            {
                runs = Convert.ToInt32(textBox3.Text);
            }
      
            // Runs as many times as the user requested (or once if not requested).
            for (int trial = 0; trial < runs; trial++)
            {
                int x = 0;
                int y = 0;

                // Checks if user entered their own starting X value.
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    x = Convert.ToInt32(textBox1.Text);
                }
                // Checks if user entered their own starting Y value.
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    y = Convert.ToInt32(textBox2.Text);
                }

                ChessBoard tour = new ChessBoard(x, y); // New board
                tour.setMoves(tour.getMoves() + 1); // Starts moving, adds 1 to # of moves.
                // Checks legal moves after initial move.
                ArrayList possibleMoves = tour.LegalMoves(tour.getX(), tour.getY()); 
                int[,] board = tour.getBoard();
                int xMovedTo = tour.getX(); // X coordinate of initial move.
                int yMovedTo = tour.getY(); // Y coordinate of initial move.
                
                // Loops as long as moves are possible.
                while (possibleMoves.Count > 0)
                {
                    Random rnd = new Random();
                    // Randomly selects a move in list of possible moves.
                    KnightMoves knightMove = (KnightMoves)possibleMoves[rnd.Next(possibleMoves.Count)];
                    // Gets the new number of moves and sets the coordinate for the next move.
                    board[tour.getX(), tour.getY()] = tour.getMoves();
                    xMovedTo = tour.getX() + knightMove.getX();
                    yMovedTo = tour.getY() + knightMove.getY();
                    tour.setX(xMovedTo);
                    tour.setY(yMovedTo);
                    // Gets the new list of possible moves after moving (for next loop iteration).
                    possibleMoves = tour.LegalMoves(tour.getX(), tour.getY());
                    // Increases number of moves by 1.
                    tour.setMoves(tour.getMoves() + 1);
                }
                board[xMovedTo, yMovedTo] = tour.getMoves();
                tour.setBoard(board);
                tour.setMoves(tour.getMoves());
                string boardWithMoves = tour.PrintBoard();
                // Outputs results information.
                MessageBox.Show("Trial " + (trial + 1) + ": The Knight was able to successfully touch " 
                    + tour.getMoves() + " squares.\n" + boardWithMoves);
                results += "Trial " + (trial + 1) + ": The Knight was able to successfully touch " 
                    + tour.getMoves() + " squares.\n";
            }
            // Writes results to text file.
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\jasonxuNonIntelligentMethod.txt", results);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string results = "";
            int runs = 1;

            // Checks if user entered their own value for number of runs.
            if (!string.IsNullOrEmpty(textBox3.Text) && Convert.ToInt32(textBox3.Text) > 0)
            {
                runs = Convert.ToInt32(textBox3.Text);
            }

            // Runs as many times as the user requested (or once if not requested).
            for (int trial = 0; trial < runs; trial++)
            {
                int x = 0;
                int y = 0;

                // Checks if user entered their own starting X value.
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    x = Convert.ToInt32(textBox1.Text);
                }
                // Checks if user entered their own starting Y value.
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    y = Convert.ToInt32(textBox2.Text);
                }

                ChessBoardHeuristic tour = new ChessBoardHeuristic(x, y); // New board
                tour.setMoves(tour.getMoves() + 1); // Starts moving, adds 1 to # of moves.
                // Checks legal moves after initial move.
                ArrayList possibleMoves = tour.LegalMoves(tour.getX(), tour.getY());
                int[,] board = tour.getBoard();
                int xMovedTo = tour.getX(); // X coordinate of initial move.
                int yMovedTo = tour.getY(); // Y coordinate of initial move.

                // Loops as long as moves are possible.
                while (possibleMoves.Count > 0)
                {
                    int[,] heuristicsBoard = tour.HeuristicsBoard(); // Creates heuristics board
                    // Moves used to determine priority
                    KnightMoves tempMove = (KnightMoves)possibleMoves[0];
                    int xMove = tempMove.getX();
                    int yMove = tempMove.getY();
                    // Value used to store the minimum heuristics value of possible moves.
                    int minHeuristics = heuristicsBoard[tour.getX() + xMove, tour.getY() + yMove];
                    // List used to hold the heuristics value of every legal move.
                    ArrayList possibleMoveHeuristics = new ArrayList();
                    possibleMoveHeuristics.Add(minHeuristics); // Adds the first heuristic value into list.

                    // Increments through every legal move.
                    for (int j = 1; j < possibleMoves.Count; j++)
                    {
                        tempMove = (KnightMoves)possibleMoves[j];
                        xMove = tempMove.getX();
                        yMove = tempMove.getY();
                        // Adds heuristics value to list.
                        possibleMoveHeuristics.Add(heuristicsBoard[tour.getX() + xMove, tour.getY() + yMove]);

                        // Determines smallest heuristics value (highest priority).
                        if (heuristicsBoard[tour.getX() + xMove, tour.getY() + yMove] < minHeuristics)
                        {
                            minHeuristics = heuristicsBoard[tour.getX() + xMove, tour.getY() + yMove];
                        }
                    }

                    // List used to hold highest priorty moves.
                    ArrayList heuristicsMoves = new ArrayList();
                    // Goes through all legal moves.
                    for (int j = 0; j < possibleMoves.Count; j++)
                    {
                        // Adds legal move to high priority list if heuristics value is lowest.
                        if ((int)possibleMoveHeuristics[j] == minHeuristics)
                        {
                            heuristicsMoves.Add(possibleMoves[j]);
                        }
                    }
                    Random rnd = new Random();
                    // Randomly selects a move from the highest priority moves.
                    KnightMoves knightMove = (KnightMoves)heuristicsMoves[rnd.Next(heuristicsMoves.Count)];
                    // Gets the new number of moves and sets the coordinate for the next move.
                    board[tour.getX(), tour.getY()] = tour.getMoves();
                    xMovedTo = tour.getX() + knightMove.getX();
                    yMovedTo = tour.getY() + knightMove.getY();
                    tour.setX(xMovedTo);
                    tour.setY(yMovedTo);
                    // Gets the new list of possible moves after moving (for next loop iteration).
                    possibleMoves = tour.LegalMoves(tour.getX(), tour.getY());
                    // Increases the number of moves by 1.
                    tour.setMoves(tour.getMoves() + 1);
                }
                board[xMovedTo, yMovedTo] = tour.getMoves();
                tour.setBoard(board);
                tour.setMoves(tour.getMoves());
                string boardWithMoves = tour.PrintBoard();
                // Outputs results information.
                MessageBox.Show("Trial " + (trial + 1) + ": The Knight was able to successfully touch "
                    + tour.getMoves() + " squares.\n" + boardWithMoves);
                results += "Trial " + (trial + 1) + ": The Knight was able to successfully touch "
                    + tour.getMoves() + " squares.\n";
            }
            // Writes results to text file.
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\jasonxuHueristicsMethod.txt", results);
        }

    }
}
