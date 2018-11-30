using System;
using System.Collections.Generic;

namespace SoftTestPatience
{
    // Placeholder
    public interface IBoard
    {
        void Reset();
        bool Move(int originStack, int destinationStack, int numberOfCards);
        bool Move(CardStack originStack, CardStack destitationStack);
        bool Move(TableStack originStack, TableStack destinationStack, int numberOfCards);
        void IncrementWasteStack();
        string ToString();
    }

    class Board : IBoard
    {
        internal List<CardStack> Stacks;
        private ICardFactory _cardFactory;

        public Board(ICardFactory cardFactory)
        {
            this._cardFactory = cardFactory;
        }

        public void Reset()
        {
            //Generate cards
            var deck = _cardFactory.GenerateRandomDeck();

            //Make Stacks
            this.Stacks = new List<CardStack>();
            for (int i = 0; i < 7; i++)
            {
                this.Stacks.Add(new TableStack(new List<Card>()));
            }
            this.Stacks.Add(new WasteStack(new List<Card>()));
            for (int i = 0; i < 4; i++)
            {
                this.Stacks.Add(new FoundationStack(new List<Card>(), (Suits)i));
            }

            //Fill TableStacks
            for (int i = 0; i < 7; i++)
            {
                Card lastCard = deck[deck.Count - 1];
                for (int j = 6; j >= i; j--)
                {
                    lastCard = deck[deck.Count - 1];
                    this.Stacks[j].ReturnCard(lastCard);
                    deck.Remove(lastCard);
                }

                lastCard.Flip();
            }

            //Fill WasteStack
            foreach (var card in deck)
            {
                this.Stacks[7].ReturnCard(card);
            }
        }

        public bool Move(int originStack, int destinationStack, int numberOfCards)
        {
            if (originStack > 11 || destinationStack > 11 || numberOfCards <= 0)
            {
                return false;
            }

            if (originStack > 6 || destinationStack > 6)
            {
                if (numberOfCards != 1)
                {
                    return false;
                }
                var origin = this.Stacks[originStack];
                var destination = this.Stacks[destinationStack];

                return Move(origin, destination);
            }
            else
            {
                var origin = (TableStack)this.Stacks[originStack];
                var destination = (TableStack)this.Stacks[destinationStack];

                return this.Move(origin, destination, numberOfCards);
            }
        }

        public bool Move(CardStack originStack, CardStack destinationStack)
        {
            Card card;
            try
            {
                card = originStack.TakeLastCard();
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            if (destinationStack.AddCard(card)) return true;

            originStack.ReturnCard(card);
            return false;
        }

        public bool Move(TableStack originStack, TableStack destinationStack, int numberOfCards)
        {
            List<Card> cards;
            try
            {
                cards = originStack.TakeLastCards(numberOfCards);
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            for (int i = cards.Count - 1; i >= 0; i--)
            {
                if (!destinationStack.AddCard(cards[i]))
                {
                    destinationStack.TakeLastCards(cards.Count - (i+1));
                    for (int j = cards.Count - 1; j >= 0; j--)
                    {
                        originStack.ReturnCard(cards[j]);
                    }
                    return false;
                }
            }

            return true;
        }

        public void IncrementWasteStack()
        {
            ((WasteStack)this.Stacks[7]).IncrementIndex();
        }

        public override string ToString()
        {
            var text = "";
            text += this.Stacks[7].ToString();
            text += "     ";
            for (int i = 8; i < 12; i++)
            {
                text += this.Stacks[i].ToString();
            }

            text += "\n";

            int maxHeightTableStack = 0;
            for (int i = 0; i < 7; i++)
            {
                if (this.Stacks[i].GetStackSize() > maxHeightTableStack)
                {
                    maxHeightTableStack = this.Stacks[i].GetStackSize();
                }
            }

            for (int i = 0; i < maxHeightTableStack; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    text += i >= this.Stacks[j].GetStackSize() ? "     " : this.Stacks[j].Cards[i].ToString();
                }

                text += "\n";
            }

            return text;
        }
    }
}
