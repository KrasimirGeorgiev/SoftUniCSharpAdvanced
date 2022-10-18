namespace PawnWars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var board = new ChessBoard();
            var result = board.Play();
            Console.WriteLine(result);
        }
    }

    public class Pawn
    {
        public Pawn(int rowIndex, int colIndex, ChessColor color)
        {
            MatrixRowIndex = rowIndex;
            MatrixColIndex = colIndex;
            Color = color;
            InitializeDirectionIndex();
        }

        public ChessColor Color { get; set; }

        public int MatrixRowIndex { get; set; }

        public int MatrixColIndex { get; set; }

        public int DirectionIndex { get; set; }

        public void MoveForward()
        {
            MatrixRowIndex += DirectionIndex;
        }

        private void InitializeDirectionIndex()
        {
            if (Color == ChessColor.White)
            {
                DirectionIndex = -1;
            }
            else
            {
                DirectionIndex = 1;
            }
        }
    }

    public class ChessBoard
    {
        private const int BoardSize = 8;
        private const int NumberOfPawnsAllowed = 2;
        private List<Pawn> pawns;

        public ChessBoard()
        {
            pawns = new List<Pawn>();
            InitializeTheBoard();
        }

        public string Play()
        {
            string result = string.Empty;
            if (pawns.Count == NumberOfPawnsAllowed)
            {
                var whitePawn = pawns.Where(p => p.Color == ChessColor.White).Single();
                var blackPawn = pawns.Where(p => p.Color == ChessColor.Black).Single();
                var direction = whitePawn.DirectionIndex;
                while (result == string.Empty)
                {
                    var currentPawn = direction == 1 ? blackPawn : whitePawn;
                    var enemyPawn = currentPawn.Color == ChessColor.White ? blackPawn : whitePawn;
                    var diagonals = GetDiagonals(currentPawn);
                    var isEnemyInRange = IsEnemyInRange(diagonals, enemyPawn);
                    if (isEnemyInRange)
                    {
                        var enemyCoordinates = GetBoardPositionFromMatrixCoordinates(enemyPawn.MatrixRowIndex, enemyPawn.MatrixColIndex);
                        result = $"Game over! {currentPawn.Color} capture on {enemyCoordinates}.";
                    }
                    else
                    {
                        currentPawn.MoveForward();
                        var hasReachedEnd = HasPawnReachedTheEnd(currentPawn);
                        if (hasReachedEnd)
                        {
                            var currentPawnCoordinates = GetBoardPositionFromMatrixCoordinates(currentPawn.MatrixRowIndex, currentPawn.MatrixColIndex);
                            result = $"Game over! {currentPawn.Color} pawn is promoted to a queen at {currentPawnCoordinates}.";
                        }
                    }

                    direction *= -1;
                }
            }

            return result;
        }

        private bool HasPawnReachedTheEnd(Pawn pawn)
        {
            var endIndex = pawn.Color == ChessColor.White ? 0 : 7;
            var hasPawnReachedTheEnd = pawn.MatrixRowIndex == endIndex;
            return hasPawnReachedTheEnd;
        }

        private string GetBoardPositionFromMatrixCoordinates(int row, int col)
        {
            var rowOnTheBoard = GetRowOnChessboardFromMatrixRowIndex(row);
            var colOnTheBoard = GetColOnChessboardFromMatrixColIndex(col);

            return $"{colOnTheBoard}{rowOnTheBoard}";
        }

        private bool IsEnemyInRange(IEnumerable<Tuple<int, int>> diagonals, Pawn enemyPawn)
        {
            var isEnemyInRange = false;
            foreach (var diagonal in diagonals)
            {
                if (diagonal.Item1 == enemyPawn.MatrixRowIndex && diagonal.Item2 == enemyPawn.MatrixColIndex)
                {
                    isEnemyInRange = true;
                    break;
                }
            }

            return isEnemyInRange;
        }

        private List<Tuple<int, int>> GetDiagonals(Pawn pawn)
        {
            var diagonals = new List<Tuple<int, int>>();
            var rowIndex = pawn.MatrixRowIndex + pawn.DirectionIndex;

            var rightDiagonal = pawn.MatrixColIndex + 1;
            var leftDiagonal = pawn.MatrixColIndex - 1;
            if (rightDiagonal < BoardSize)
            {
                diagonals.Add(new Tuple<int, int>(rowIndex, rightDiagonal));
            }

            if (0 <= leftDiagonal)
            {
                diagonals.Add(new Tuple<int, int>(rowIndex, leftDiagonal));
            }

            return diagonals;
        }

        private void InitializeTheBoard()
        {
            //var board = new char[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                var inputBoardRow = Console.ReadLine();
                for (int j = 0; j < BoardSize; j++)
                {
                    var currentCellSymbol = inputBoardRow[j];
                    if (!IsCellEmpty(currentCellSymbol))
                    {
                        ChessColor color = ChessColor.Black;
                        if (currentCellSymbol == 'w')
                        {
                            color = ChessColor.White;
                        }

                        var pawn = new Pawn(i, j, color);
                        pawns.Add(pawn);
                    }
                }
            }
        }

        private bool IsCellEmpty(char symbol)
            => symbol == '-';

        private static int GetRowOnChessboardFromMatrixRowIndex(int rowOnChessBoard)
        {
            switch (rowOnChessBoard)
            {
                case 0: return 8;
                case 1: return 7;
                case 2: return 6;
                case 3: return 5;
                case 4: return 4;
                case 5: return 3;
                case 6: return 2;
                case 7: return 1;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private static char GetColOnChessboardFromMatrixColIndex(int rowOnChessBoard)
        {
            switch (rowOnChessBoard)
            {
                case 0: return 'a';
                case 1: return 'b';
                case 2: return 'c';
                case 3: return 'd';
                case 4: return 'e';
                case 5: return 'f';
                case 6: return 'g';
                case 7: return 'h';
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum ChessColor
    {
        Black,
        White
    }
}
