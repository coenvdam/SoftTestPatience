using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class CardStackTest
    {
        private class MockCardStack : CardStack { public MockCardStack(List<Card> cards) : base(cards) { } }
        public CardStackTest() { }

        [Fact]
        public void GetStackSize_ListOfRandomCards_ShouldReturnSizeOfInputList()
        {
            // Arrange
            var cards = CreateRandomListOfCards();
            var expectedSize = cards.Count;
            var sut = new MockCardStack(cards);

            // Act
            var actualSize = sut.GetStackSize();

            // Assert
            Assert.Equal(expectedSize, actualSize);
        }

        private List<Card> CreateRandomListOfCards()
        {
            List<Card> mockCards = new List<Card>();
            Random rnd = new Random();
            int nrOfCards = rnd.Next(1, 15);

            for (int i = 0; i < nrOfCards; i++) {
                mockCards.Add(new Card());
            }

            return mockCards;
        }
    }
}
