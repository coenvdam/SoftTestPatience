using System;
using System.Linq;

namespace SoftTestPatience
{
    class GameController
    {
        private IBoard _board;
        private IConsoleHelper _consoleHelper;

        public GameController(IBoard board, IConsoleHelper consoleHelper)
        {
            this._board = board;
            this._consoleHelper = consoleHelper;
        }

        public void RunGame()
        {
            _board.Reset();

            _consoleHelper.SetEncoding();
            _consoleHelper.Write("Welcome to Patience!\nType a command (like 'help') to start!\n");

            while (true)
            {
                _consoleHelper.Write(_board.ToString());

                var input = _consoleHelper.ReadInput();
                var inputArray = input.ToLower().Split(" ");
                var inputList = inputArray.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

                switch (inputList[0])
                {
                    case "help":
                        this.Help();
                        break;
                    case "credits":
                        this.Credits();
                        break;
                    case "exit":
                        _consoleHelper.Write("Thank you for playing!\n");
                        return;
                    case "new":
                        this.NewGame();
                        break;
                    default:
                        var onlyNumbers = true;
                        foreach (var word in inputList)
                        {
                            if (!int.TryParse("123", out int n)) onlyNumbers = false;
                        }

                        if (onlyNumbers)
                        {
                            if (inputList.Count == 2)
                            {
                                this.MoveCard(Int32.Parse(inputList[0]), Int32.Parse(inputList[1]));
                            } else if (inputList.Count == 3)
                            {
                                this.MoveCards(Int32.Parse(inputList[0]), Int32.Parse(inputList[1]), Int32.Parse(inputList[2]));
                            }
                            else
                            {
                                _consoleHelper.Write("I don't know that command. Try something else.\n");
                            }
                        }
                        else
                        {
                            _consoleHelper.Write("I don't know that command. Try something else.\n");
                        }
                        break;
                }

            }
        }

        public void NewGame()
        {
            _board.Reset();
            _consoleHelper.Write("The board has been reset!\n");
        }

        public void MoveCard(int stackFrom, int stackTo)
        {
            this.MoveCards(stackFrom, stackTo, 1);
        }

        public void MoveCards(int stackFrom, int stackTo, int numberOfCards)
        {
            if (_board.Move(stackFrom, stackTo, numberOfCards))
            {
                _consoleHelper.Write("The move was successful.\n");
            }
            else
            {
                _consoleHelper.Write("You can't move the cards like that. Try something else.\n");
            }
        }

        public void IncrementWasteStack()
        {
            _board.IncrementWasteStack();
        }

        public void Help()
        {
            var helpText = "To move a card from one stack to another, first type the number of the stack with the card,\n" +
                           "then the number of the stack where you want to move it to. If you type a third number, it will\n" +
                           "move the amount of cards of that number. The table stacks are numbered 0 - 6, the waste stack\n" +
                           "is number 7 and the foundation stacks are numbered 8 - 11. An example of a command is '5 6 2'\n" +
                           "Besides moving a card, you can also go through the waste stack, with 'increment waste', ask for\n" +
                           "help with 'help', see the credits with 'credits' and start a new game with 'new'.\n";
            _consoleHelper.Write(helpText);
        }

        public void Credits()
        {
            var creditsText = "This game of Patience was made by Dennis Stiekema (ds222tk) and Cornelis Jan (Coen) van Dam (cv222gk)\n" +
                              "for the course Software Testing (2DV610) at Linnaeus University\n";
            _consoleHelper.Write(creditsText);
        }
    }
}
