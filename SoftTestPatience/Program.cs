namespace SoftTestPatience
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameController = new GameController(new Board(new CardFactory()), new ConsoleHelper());
            gameController.RunGame();
        }
    }
}
