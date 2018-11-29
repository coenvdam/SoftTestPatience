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
            var lastCard = this.Cards[this.Cards.Count - 1];
            if (lastCard.Suit != card.Suit || lastCard.Value + 1 != card.Value)
            {
                return false;
            }

            this.Cards.Add(card);
            return true;

        }

        public Suits GetSuit()
        {
            return this.Suit;
        }
    }
}
