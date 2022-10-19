namespace Exams.PawnWars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class ChessBoard
    {
        private const int BoardSize = 8;
        private const int NumberOfPawnsAllowed = 2;
        private const int WhitePiecesEndIndex = 0;
        private const int BlackPiecesEndIndex = 7;
        private const char ChessBoardColumnStartSymbol = 'a';
        private const char WhitePieceSymbol = 'w';
        private const char EmptySpaceSymbol = '-';

        private List<Pawn> pawns;

        internal ChessBoard()
        {
            pawns = new List<Pawn>();
            InitializeTheBoard();
        }

        internal string Play()
        {
            string result = string.Empty;
            if (pawns.Count == NumberOfPawnsAllowed)
            {
                var whitePawn = pawns.Where(p => p.Color == ChessPeiceColor.White).Single();
                var blackPawn = pawns.Where(p => p.Color == ChessPeiceColor.Black).Single();
                var direction = whitePawn.DirectionIndex;
                while (result == string.Empty)
                {
                    var currentPawn = direction == 1 ? blackPawn : whitePawn;
                    var enemyPawn = currentPawn.Color == ChessPeiceColor.White ? blackPawn : whitePawn;
                    var diagonals = GetDiagonals(currentPawn);
                    var isEnemyInRange = IsEnemyInRange(diagonals, enemyPawn);
                    if (isEnemyInRange)
                    {
                        var enemyCoordinates = GetBoardPositionFromMatrixCoordinates(enemyPawn.Coordinate.RowIndex, enemyPawn.Coordinate.ColIndex);
                        result = $"Game over! {currentPawn.Color} capture on {enemyCoordinates}.";
                    }
                    else
                    {
                        currentPawn.MoveForward();
                        var hasReachedEnd = HasPawnReachedTheEnd(currentPawn);
                        if (hasReachedEnd)
                        {
                            var currentPawnCoordinates = GetBoardPositionFromMatrixCoordinates(currentPawn.Coordinate.RowIndex, currentPawn.Coordinate.ColIndex);
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
            var endIndex = pawn.Color == ChessPeiceColor.White ? WhitePiecesEndIndex : BlackPiecesEndIndex;
            var hasPawnReachedTheEnd = pawn.Coordinate.RowIndex == endIndex;
            return hasPawnReachedTheEnd;
        }

        private string GetBoardPositionFromMatrixCoordinates(int row, int col)
        {
            var rowOnTheBoard = GetRowOnChessboardFromMatrixRowIndex(row);
            var colOnTheBoard = GetColOnChessboardFromMatrixColIndex(col);

            return $"{colOnTheBoard}{rowOnTheBoard}";
        }

        private bool IsEnemyInRange(IEnumerable<Coordinate> diagonals, Pawn enemyPawn)
        {
            var isEnemyInRange = false;
            foreach (var diagonal in diagonals)
            {
                if (diagonal.RowIndex == enemyPawn.Coordinate.RowIndex && diagonal.ColIndex == enemyPawn.Coordinate.ColIndex)
                {
                    isEnemyInRange = true;
                    break;
                }
            }

            return isEnemyInRange;
        }

        private List<Coordinate> GetDiagonals(Pawn pawn)
        {
            var diagonals = new List<Coordinate>();
            var rowIndex = pawn.Coordinate.RowIndex + pawn.DirectionIndex;

            var rightDiagonal = pawn.Coordinate.ColIndex + 1;
            var leftDiagonal = pawn.Coordinate.ColIndex - 1;
            if (rightDiagonal < BoardSize)
            {
                diagonals.Add(new Coordinate(rowIndex, rightDiagonal));
            }

            if (0 <= leftDiagonal)
            {
                diagonals.Add(new Coordinate(rowIndex, leftDiagonal));
            }

            return diagonals;
        }

        private void InitializeTheBoard()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                var inputBoardRow = Console.ReadLine();
                for (int j = 0; j < BoardSize; j++)
                {
                    var currentCellSymbol = inputBoardRow[j];
                    if (!IsCellEmpty(currentCellSymbol))
                    {
                        ChessPeiceColor color = ChessPeiceColor.Black;
                        if (currentCellSymbol == WhitePieceSymbol)
                        {
                            color = ChessPeiceColor.White;
                        }

                        var pawn = new Pawn(i, j, color);
                        pawns.Add(pawn);
                    }
                }
            }
        }

        private bool IsCellEmpty(char symbol)
            => symbol == EmptySpaceSymbol;

        private static int GetRowOnChessboardFromMatrixRowIndex(int rowOnChessBoard)
        {
            var boardRow = BoardSize - rowOnChessBoard;
            return boardRow;
        }

        private static char GetColOnChessboardFromMatrixColIndex(int rowOnChessBoard)
        {
            var boardCol = (char)(ChessBoardColumnStartSymbol + rowOnChessBoard);
            return boardCol;
        }
    }
}
