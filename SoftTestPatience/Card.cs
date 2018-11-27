﻿using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SoftTestPatience.Tests")]
namespace SoftTestPatience
{
    public interface ICard
    {
        string ToString();
    }
    internal class Card : ICard
    {
        //Should only be called in this class and by unit tests
        internal int Value;
        internal Suits Suit;
        internal bool Hidden;

        public Card(int value, Suits suit, bool hidden)
        {
            this.Value = value;
            this.Suit = suit;
            this.Hidden = hidden;
        }

        public override string ToString()
        {
            if (Hidden)
            {
                return "???";
            }

            var toString = "";
            if (this.Value != 10)
            {
                toString += " ";
            }

            switch (this.Value)
            {
                case 1:
                    toString += "A";
                    break;
                case 11:
                    toString += "J";
                    break;
                case 12:
                    toString += "Q";
                    break;
                case 13:
                    toString += "K";
                    break;
                default:
                    toString += Value.ToString();
                    break;
            }

            switch (this.Suit)
            {
                case Suits.Clubs:
                    toString += "\u2663";
                    break;
                case Suits.Diamonds:
                    toString += "\u2666";
                    break;
                case Suits.Hearts:
                    toString += "\u2665";
                    break;
                case Suits.Spades:
                    toString += "\u2660";
                    break;
            }

            return toString;
        }
    }
}
