using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    public class WasteStack : CardStack
    {
        internal int Index;

        public WasteStack(List<Card> cards) : base(cards)
        {
            this.Index = 0;
        }

        public override bool AddCard(Card card)
        {
            throw new InvalidOperationException();
        }

        public int GetIndex()
        {
            return this.Index;
        }

        public void IncrementIndex()
        {
            this.Index++;
            if (this.Index >= this.Cards.Count)
            {
                this.Index = 0;
            }
        }
    }
}
