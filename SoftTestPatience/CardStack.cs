using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace SoftTestPatience
{
    public interface ICardStack
    {
        ICard TakeLastCard();
        int GetStackSize();
    }

    abstract class CardStack : ICardStack
    {
        //Should only be called in this class and by unit tests
        internal List<ICard> Cards;

        protected CardStack(List<ICard> cards)
        {
            this.Cards = cards;
        }

        public ICard TakeLastCard()
        {
            if(Cards.Count == 0)
            {
                throw new InvalidOperationException();
            }

            ICard lastCard = Cards[Cards.Count - 1];
            Cards.RemoveAt(Cards.Count - 1);
            return lastCard;
        }

        public int GetStackSize()
        {
            return Cards.Count;
        }
    }
}