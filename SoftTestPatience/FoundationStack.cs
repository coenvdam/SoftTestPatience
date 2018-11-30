using System.Collections.Generic;

namespace SoftTestPatience
{
    public class FoundationStack : CardStack
    {
        internal Suits Suit;

        public FoundationStack(List<Card> cards, Suits suit) : base(cards)
        {
            this.Suit = suit;
        }

        public override bool AddCard(Card card)
        {
            if (this.Cards.Count != 0 || this.Suit != card.Suit)
            {
                var lastCard = this.Cards[this.Cards.Count - 1];
                if (lastCard.Suit != card.Suit || lastCard.Value + 1 != card.Value)
                {
                    return false;
                }
            }

            this.Cards.Add(card);
            return true;

        }

        public Suits GetSuit()
        {
            return this.Suit;
        }

        public override string ToString()
        {
            if (this.Cards.Count == 0)
            {
                switch (this.Suit)
                {
                    case Suits.Hearts:
                        return "[\u2665\u2665\u2665]";
                    case Suits.Diamonds:
                        return "[\u2666\u2666\u2666]";
                    case Suits.Clubs:
                        return "[\u2663\u2663\u2663]";
                    case Suits.Spades:
                        return "[\u2660\u2660\u2660]";
                }
            }

            return this.Cards[this.Cards.Count - 1].ToString();
        }
    }
}
