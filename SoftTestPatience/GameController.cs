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
            Console.Write(table.ToString());
            RunGame();
        }

        public string ManageUserInput()
        {
            Console.Write("(type 'new' for newgame or 'exit' to quit game) Enter a new move:\n");
            string input = Console.ReadLine();
            return input;
        }

        public void RunGame()
        {
            string input = ManageUserInput();

            while(input != "exit")
            {

                /*

                // Something like this
                // Depending on how table is being implemented (naming etc)

                if(input == "new")
                {
                    NewGame();
                    return;
                }

                string[] stringValues = input.Split(" ");
                int[] values = new int[stringValues.Length];
                for(int i = 0; i < values.Length; i++)
                {
                    values[i] = Convert.ToInt16(stringValues[i]);
                }

                if(values.Length > 2)
                {
                    List<Card> cards = table.CardStack[values[0]].GetAndRemoveCards(values[2]);
                    table.Stack[values[1]].AddCards(cards);
                } else
                {
                    Card card = table.CardStack[values[0]].GetAndRemoveLastCard();
                    table.Stack[values[1]].AddCard(card);
                }

                Console.Write(table.ToString());
                input = ManageUserInput();*/
            }
            Console.WriteLine("Exiting Game!");
        }
    }
}
