using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    // Placeholder
    public class Board { public virtual void Reset() { } }

    class GameController
    {
        private Board table;

        public GameController(Board table)
        {
            this.table = table;
        }

        public void NewGame()
        {
            Console.Write("Welcome to Patience!\nType a move like \'5 7 3\' where 5 is the stack to move cards from to stack number 7, and 3 the amount of cards\n");
            table.Reset();
        }

        public string ManageUserInput()
        {
            Console.Write("(type 'new' for newgame or 'exit' to quit game) Enter a new move:\n");
            string input = Console.ReadLine();
            return input;
        }

        public void RunGame()
        {
            throw new NotImplementedException();
        }
    }
}
