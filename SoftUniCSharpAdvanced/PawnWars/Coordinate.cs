namespace Exams.PawnWars
{
    internal class Coordinate
    {
        internal Coordinate(int rowIndex, int colIndex)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;
        }

        public int RowIndex { get; set; }

        public int ColIndex { get; set; }
    }
}
