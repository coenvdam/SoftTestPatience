using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SoftTestPatience.Tests")]
namespace SoftTestPatience
{
    internal class Card
    {
        public int Value;
        public Suits Suit;
        public bool Hidden;

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
