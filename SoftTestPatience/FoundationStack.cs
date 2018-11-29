using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    class FoundationStack : CardStack
    {
        internal Suits Suit;

        public FoundationStack(List<Card> cards, Suits suit) : base(cards)
        {
            this.Suit = suit;
        }

        public override bool AddCard(Card card)
        {
            throw new NotImplementedException();
        }

        public Suits GetSuit()
        {
            throw new NotImplementedException();
        }
    }
}
