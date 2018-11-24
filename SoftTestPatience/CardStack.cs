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
            throw new NotImplementedException();
        }

        public virtual Card GetLastCard()
        {
            throw new NotImplementedException();
        }

        public virtual int GetStackSize()
        {
            throw new NotImplementedException();
        }

        public virtual string Print()
        {
            throw new NotImplementedException();
        }
    }
}