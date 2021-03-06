﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace SoftTestPatience
{

    public abstract class CardStack
    {
        //Should only be called in this class and by unit tests
        internal List<Card> Cards;

        protected CardStack(List<Card> cards)
        {
            this.Cards = cards;
        }

        public virtual Card TakeLastCard()
        {
            if(Cards.Count == 0)
            {
                throw new InvalidOperationException();
            }

            Card lastCard = Cards[Cards.Count - 1];
            Cards.RemoveAt(Cards.Count - 1);
            return lastCard;
        }

        public virtual Card GetLastCard()
        {
            if (Cards.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return Cards[Cards.Count - 1];
        }

        public int GetStackSize()
        {
            return Cards.Count;
        }

        public virtual bool AddCard(Card card)
        {
            throw new InvalidOperationException();
        }

        public virtual void ReturnCard(Card card)
        {
            this.Cards.Add(card);
        }
    }
}