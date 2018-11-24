using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class CardStackTest
    {
        private class MockCardStack : CardStack {
            public MockCardStack(List<Card> cards) : base(cards) { }
            public override string ToString() { return ""; }
        }

        public CardStackTest() { }

        [Fact]
        public void CardStack_EmptyList_ShouldReturnInvalidOperationException()
        {
            // Arrange
            var cards = new List<Card>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new MockCardStack(cards));
        }

        [Fact]
        public void GetStackSize_ListOfRandomCards_ShouldReturnSizeOfInputList()
        {
            // Arrange
            var cards = CreateListOfMockCards(5);
            var expectedSize = 5;
            var sut = new MockCardStack(cards);

            // Act
            var actualSize = sut.GetStackSize();

            // Assert
            Assert.Equal(expectedSize, actualSize);
        }

        [Fact]
        public void GetLastCard_EmptyList_ShouldThrowsInvalidOperationException()
        {
            // Arrange
            var cards = new List<Card>();
            var sut = new MockCardStack(cards);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.GetAndRemoveLastCard());
        }

        [Fact]
        public void GetLastCard_ListOfRandomCards_ShouldReturnLastCardInList()
        {
            // Arrange 
            var cards = CreateListOfMockCards(5);
            var sut = new MockCardStack(cards);
            var expectedCard = cards[4];

            // Act
            var actualCard = sut.GetAndRemoveLastCard();

            // Assert
            Assert.Equal<Card>(expectedCard, actualCard);
        }

        [Fact]
        public void GetLastCard_ListOfRandomCards_ShouldReturnOriginalListSizeMinus1()
        {
            // Arrange
            var cards = CreateListOfMockCards(5);
            var sut = new MockCardStack(cards);
            var expectedNrOfCards = 4;

            // Act
            Card card = sut.GetAndRemoveLastCard();
            var actualNrOfCards = sut.GetStackSize();

            // Assert
            Assert.Equal(expectedNrOfCards, actualNrOfCards);
        }

        private List<Card> CreateListOfMockCards(int size)
        {
            List<Card> mockCards = new List<Card>();

            for(int i = 0; i < size; i++)
            {
                mockCards.Add(new Mock<Card>().Object);
            }

            return mockCards;
        }
    }
}
