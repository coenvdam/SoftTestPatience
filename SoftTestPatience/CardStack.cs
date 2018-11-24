using System;
using System.Collections.Generic;
using System.Text;

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
            return new Card();
        }

        public virtual int GetStackSize()
        {
            return cards.Count;
        }

        public virtual string Print()
        {
            throw new NotImplementedException();
        }
    }
}