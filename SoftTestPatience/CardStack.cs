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
            if(cards.Count == 0)
            {
                throw new InvalidOperationException();
            }

            this.cards = cards;
        }

        public virtual Card GetAndRemoveLastCard()
        {
            if(cards.Count == 0)
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

        public abstract override string ToString();
    }
}