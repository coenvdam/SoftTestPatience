using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class CardStackTest
    {
        public CardStackTest() { }

        [Fact]
        public void GetStackSize_ListOfRandomCards_ShouldReturnSizeOfInputList()
        {
            // Arrange
            var cards = CreateRandomListOfCards();
            var expectedSize = cards.Count;
            var sut = new Mock<CardStack>(cards).Object;

            // Act
            var actualSize = sut.GetStackSize();

            // Assert
            Assert.Equal(expectedSize, actualSize);
        }

        private List<Mock<Card>> CreateRandomListOfCards()
        {
            List<Mock<Card>> mockCards = new List<Mock<Card>>();
            Random rnd = new Random();
            int nrOfCards = rnd.Next(1, 15);

            for (int i = 0; i < nrOfCards; i++) {
                mockCards.Add(new Mock<Card>());
            }

            return mockCards;
        }
    }
}
