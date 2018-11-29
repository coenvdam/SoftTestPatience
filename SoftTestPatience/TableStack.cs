using System;
using System.Collections.Generic;

namespace SoftTestPatience
{
    class TableStack : CardStack
    {
        public TableStack(List<Card> cards) : base(cards)
        {
        }

        public override bool AddCard(Card card)
        {
            var lastCard = this.Cards[this.Cards.Count - 1];
            if ((((int)card.Suit > 1 || (int)lastCard.Suit < 2) &&
                 ((int)card.Suit < 2 || (int)lastCard.Suit > 1)) || card.Value != lastCard.Value - 1)
            {
                return false;
            }

            this.Cards.Add(card);
            return true;
        }

        public List<Card> TakeLastCards(int numberOfCards)
        {
            if (numberOfCards > this.Cards.Count
                || this.Cards[this.Cards.Count - numberOfCards].Hidden)
            {
                throw new InvalidOperationException();
            }

            var returnCards = new List<Card>();
            var stackSize = this.Cards.Count;
            for (int i = stackSize; i > stackSize - numberOfCards; i--)
            {
                returnCards.Add(this.Cards[i]);
                this.Cards.Remove(this.Cards[i]);
            }

            return returnCards;
        }
    }
}