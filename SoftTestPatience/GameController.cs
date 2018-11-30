using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    class GameController
    {
        internal IBoard Board;
        private IConsoleHelper _consoleHelper;

        public GameController(IBoard board, IConsoleHelper consoleHelper)
        {
            this.Board = board;
            this._consoleHelper = consoleHelper;
        }

        public void RunGame()
        {
            throw new NotImplementedException();
        }

        public void NewGame()
        {
            throw new NotImplementedException();
        }

        public void MoveCard(int stackFrom, int stackTo)
        {
            throw new NotImplementedException();
        }

        public void MoveCards(int stackFrom, int stackTo, int numberOfCards)
        {
            throw new NotImplementedException();
        }

        public void IncrementWasteStack()
        {
            throw new NotImplementedException();
        }

        public void Help()
        {
            throw new NotImplementedException();
        }

        public void Credits()
        {
            throw new NotImplementedException();
        }
    }
}
