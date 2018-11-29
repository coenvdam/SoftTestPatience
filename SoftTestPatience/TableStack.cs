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
            throw new NotImplementedException();
        }

        public List<Card> TakeLastCards(int numberOfCards)
        {
            throw new NotImplementedException();
        }
    }
}