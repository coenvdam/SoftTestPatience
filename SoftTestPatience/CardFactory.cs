using System;
using System.Collections.Generic;

namespace SoftTestPatience
{
    interface ICardFactory
    {
        List<Card> GenerateSortedDeck();
        List<Card> GenerateRandomDeck();
    }

    internal class CardFactory : ICardFactory
    {
        public virtual List<Card> GenerateSortedDeck()
        {
            var deck = new List<Card>();
            for (int suit = 0; suit < 4; suit++)
            {
                for (int value = 1; value < 14; value++)
                {
                    deck.Add(new Card(value, (Suits) suit, true));
                }
            }

            return deck;
        }

        public List<Card> GenerateRandomDeck()
        {
            var deck = this.GenerateSortedDeck();

            int deckCount = deck.Count;
            while (deckCount > 1)
            {
                deckCount--;
                int k = new Random().Next(deckCount + 1);
                var value = deck[k];
                deck[k] = deck[deckCount];
                deck[deckCount] = value;
            }

            return deck;
        }
    }
}