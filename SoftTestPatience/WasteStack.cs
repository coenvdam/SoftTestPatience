using System;
using System.Collections.Generic;

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

        public virtual void IncrementIndex()
        {
            this.Index++;
            if (this.Index >= this.Cards.Count)
            {
                this.Index = 0;
            }
        }

        public override string ToString()
        {
            var text = "";

            text += this.Index == 0 ? "[   ]" : "[///]";

            text += this.Cards[this.Cards.Count - (Index + 1)].ToString();

            return text;
        }
    }
}
