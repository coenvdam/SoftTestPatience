using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    // Placeholder
    public class Board { }

    class GameController
    {
        private Board table;

        public GameController(Board table)
        {
            this.table = table;
        }

        public void NewGame()
        {
            throw new NotImplementedException();
        }

        public string GetUserInput()
        {
            Console.Write("(type 'new' for newgame or 'exit' to quit game) Enter a new move: ");
            return "";
        }

        public void RunGame()
        {
            throw new NotImplementedException();
        }
    }
}
