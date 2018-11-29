using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    interface ICardFactory
    {
        List<Card> GenerateSortedDeck();
        List<Card> GenerateRandomDeck();
    }

    internal class CardFactory : ICardFactory
    {
        public List<Card> GenerateSortedDeck()
        {
            throw new NotImplementedException();
        }

        public List<Card> GenerateRandomDeck()
        {
            throw new NotImplementedException();
        }
    }
}
