using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    // Placeholder
    public interface IBoard
    {
        void Reset();
        bool Move(CardStack originStack, CardStack destitationStack);
        bool Move(TableStack originStack, TableStack destinationStack, int numberOfCards);
        string ToString();
    }

    class Board : IBoard
    {
        internal List<CardStack> Stacks;
        private ICardFactory _cardFactory;

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public bool Move(CardStack originStack, CardStack destinationStack)
        {
            throw new NotImplementedException();
        }

        public bool Move(TableStack originStack, TableStack destinationStack, int numberOfCards)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
