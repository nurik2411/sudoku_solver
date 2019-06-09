using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public static char[,] board = new char[,]{
                {'0','0','0','7','0','0','3','0','1'},
                {'3','0','0','9','0','0','0','0','0'},
                {'0','4','0','3','1','0','2','0','0'},
                {'0','6','0','4','0','0','5','0','0'},
                {'0','0','0','0','0','0','0','0','0'},
                {'0','0','1','0','0','8','0','4','0'},
                {'0','0','6','0','2','1','0','5','0'},
                {'0','0','0','0','0','9','0','0','8'},
                {'8','0','5','0','0','4','0','0','0'}};
        public static bool SudokuSolve(char[,] board)
        {
            if (board == null || board.GetLength(0) < 9 || board.GetLength(1) < 9)
            {
                return false;
            }
            return SudokuSolveHelper(board, 0, 0);
        }

        private static bool SudokuSolveHelper(char[,] board, int row, int col)
        {
            if (row > 8)
            {
                return true;
            }

            var visit = board[row, col];
            var isBlank = visit == '0';

            var nextRow = col == 8 ? (row + 1) : row;
            var nextCol = col == 8 ? 0 : (col + 1);

            if (!isBlank)
            {
                return SudokuSolveHelper(board, nextRow, nextCol);
            }

            var availableNumbers = getAvailableNumbers(board, row, col);

            foreach (var option in availableNumbers)
            {
                board[row, col] = option;

                var result = SudokuSolveHelper(board, nextRow, nextCol);

                if (result)
                {
                    return true;
                }

                board[row, col] = '0';
            }

            return false;
        }

        private static HashSet<Char> getAvailableNumbers(char[,] board, int currentRow, int currentCol)
        {
            var numbers = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var available = new HashSet<char>(numbers);

            // check by row
            for (int col = 0; col < 9; col++)
            {
                var visit = board[currentRow, col];
                var isDigit = visit != '0';

                if (isDigit)
                {
                    available.Remove(visit);
                }
            }

            // check by col
            for (int row = 0; row < 9; row++)
            {
                var visit = board[row, currentCol];
                var isDigit = visit != '0';

                if (isDigit)
                {
                    available.Remove(visit);
                }
            }

            // check by 3 * 3 matrix 
            var startRow = currentRow / 3 * 3;
            var startCol = currentCol / 3 * 3;
            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    var visit = board[row, col];
                    var isDigit = visit != '0';

                    if (isDigit)
                    {
                        available.Remove(visit);
                    }
                }
            }

            return available;
        }
        static void Main(string[] args)
        {
            var board2 = new char[,]{
                {'.','.','.','7','.','.','3','.','1'},
                {'3','.','.','9','.','.','.','.','.'},
                {'.','4','.','3','1','.','2','.','.'},
                {'.','6','.','4','.','.','5','.','.'},
                {'.','.','.','.','.','.','.','.','.'},
                {'.','.','1','.','.','8','.','4','.'},
                {'.','.','6','.','2','1','.','5','.'},
                {'.','.','.','.','.','9','.','.','8'},
                {'8','.','5','.','.','4','.','.','.'}};

            string sudoku = "";

            Console.WriteLine(SudokuSolve(board));
            for(int i = 0; i<9; i++)
            {
                for(int j = 0; j<9; j++)
                {
                    sudoku += board[i, j].ToString() + " ";
                    if (j == 8)
                    {
                        sudoku += "\n";
                    }
                }
            }
            Console.WriteLine(sudoku);

        }
    }
}
