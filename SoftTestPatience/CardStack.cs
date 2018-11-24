using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace SoftTestPatience
{
    abstract class CardStack
    {
        private List<Card> cards;

        protected CardStack(List<Card> cards)
        {
            this.cards = cards;
        }

        public virtual Card GetLastCard()
        {
            if(cards.Count < 1)
            {
                throw new InvalidOperationException();
            }
            Card lastCard = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return lastCard;
        }

        public virtual int GetStackSize()
        {
            return cards.Count;
        }

        public virtual void Print()
        {
            if (cards.Count == 0)
            {
                Console.Write("---");
            }

            Console.Write(cards[cards.Count - 1].ToString());
        }
    }
}