using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    class WasteStack : CardStack
    {
        internal int Index;

        public WasteStack(List<Card> cards) : base(cards)
        {
            this.Index = 0;
        }

        public override bool AddCard(Card card)
        {
            throw new NotImplementedException();
        }

        public int GetIndex()
        {
            throw new NotImplementedException();
        }

        public void IncrementIndex()
        {
            throw new NotImplementedException();
        }
    }
}
