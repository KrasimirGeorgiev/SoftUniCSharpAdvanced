namespace Exams.PawnWars
{
    using System;

    public class StartUp
    {
        static void Main()
        {
            var board = new ChessBoard();
            var result = board.Play();
            Console.WriteLine(result);
        }
    }
}
