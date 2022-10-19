namespace Exams.PawnWars
{
    internal class Pawn
    {
        internal Pawn(int rowIndex, int colIndex, ChessPeiceColor color)
        {
            Coordinate = new Coordinate(rowIndex, colIndex);
            Color = color;
            InitializeDirectionIndex();
        }

        internal ChessPeiceColor Color { get; set; }

        internal Coordinate Coordinate { get; set; }

        internal int DirectionIndex { get; set; }

        internal void MoveForward()
        {
            Coordinate.RowIndex += DirectionIndex;
        }

        private void InitializeDirectionIndex()
        {
            if (Color == ChessPeiceColor.White)
            {
                DirectionIndex = -1;
            }
            else
            {
                DirectionIndex = 1;
            }
        }
    }
}
