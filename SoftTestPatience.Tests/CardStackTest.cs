using System;
using System.Collections.Generic;
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

        [Fact]
        public void GetLastCard_EmptyList_ShouldThrowsInvalidOperationException()
        {
            // Arrange
            var cards = new List<Card>();
            var sut = new MockCardStack(cards);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.GetLastCard());
        }

        [Fact]
        public void GetLastCard_ListOfRandomCards_ShouldReturnLastCardInList()
        {
            // Arrange 
            var cards = CreateRandomListOfCards();
            var sut = new MockCardStack(cards);
            var expectedCard = cards[cards.Count - 1];

            // Act
            var actualCard = sut.GetLastCard();

            // Assert
            Assert.Equal<Card>(expectedCard, actualCard);
        }

        [Fact]
        public void Print_EmptyList_ShouldPrintPlaceHolderForCard()
        {
            // Arrange
            var cards = new List<Card>();
            var sut = new MockCardStack(cards);
            var expectedMessage = "---";

            // Act
            var actualMessage = sut.Print();

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void Print_ListOfRandomCards_ShouldPrintLastCard()
        {
            // Arrange
            var mockCard = new Mock<Card>();
            mockCard.Setup(mc => mc.ToString()).Returns(" A\u2665");
            List<Card> cards = new List<Card>();
            cards.Add(mockCard.Object);
            var sut = new MockCardStack(cards);
            var expectedMessage = " A\u2665";

            // Act
            var actualMessage = sut.Print();

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        private List<Card> CreateRandomListOfCards()
        {
            List<Card> mockCards = new List<Card>();
            Random rnd = new Random();
            int nrOfCards = rnd.Next(1, 15);

            for (int i = 0; i < nrOfCards; i++) {
                mockCards.Add(new Card() { Value = rnd.Next(1,15), Hidden = rnd.Next(0, 2) == 1 ? true : false, Suit = Suits.Clubs });
            }

            return mockCards;
        }
    }
}
