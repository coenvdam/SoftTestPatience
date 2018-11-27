using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    interface ICardFactory
    {
        List<ICard> GenerateSortedDeck();
        List<ICard> GenerateRandomDeck();
    }

    internal class CardFactory : ICardFactory
    {
        public List<ICard> GenerateSortedDeck()
        {
            throw new NotImplementedException();
        }

        public List<ICard> GenerateRandomDeck()
        {
            throw new NotImplementedException();
        }
    }
}
