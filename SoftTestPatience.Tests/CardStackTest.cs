using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class CardStackTest
    {
        private Mock<CardStack> _cardStackMock;

        //BeforeEach
        public CardStackTest()
        {
            this._cardStackMock = new Mock<CardStack>(It.IsAny<List<Card>>());
        }

        [Fact]
        public void TakeLastCard_EmptyList_ShouldThrowsInvalidOperationException()
        {
            // Arrange
            var cards = new List<ICard>();
            _cardStackMock.Object.Cards = cards;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _cardStackMock.Object.TakeLastCard());
        }

        [Fact]
        public void TakeLastCard_ListOfThreeCards_ShouldReturnLastCardInList()
        {
            // Arrange 
            var expectedCard = new Card(It.IsAny<int>(), It.IsAny<Suits>(), It.IsAny<bool>());
            var cards = new List<ICard>()
            {
                new Card(It.IsAny<int>(), It.IsAny<Suits>(), It.IsAny<bool>()),
                new Card(It.IsAny<int>(), It.IsAny<Suits>(), It.IsAny<bool>()),
                expectedCard
            };
            _cardStackMock.Object.Cards = cards;

            // Act
            var actualCard = _cardStackMock.Object.TakeLastCard();

            // Assert
            Assert.Equal(expectedCard, actualCard);
        }

        [Fact]
        public void TakeLastCard_ListOfOneCard_ShouldReturnOnlyCard()
        {
            // Arrange 
            var expectedCard = new Card(It.IsAny<int>(), It.IsAny<Suits>(), It.IsAny<bool>());
            var cards = new List<ICard>()
            {
                expectedCard
            };
            _cardStackMock.Object.Cards = cards;

            // Act
            var actualCard = _cardStackMock.Object.TakeLastCard();

            // Assert
            Assert.Equal(expectedCard, actualCard);
        }

        [Fact]
        public void TakeLastCard_ListOfNoCards_ShouldReturnInvalidOperationException()
        {
            // Arrange
            var cards = new List<ICard>();
            _cardStackMock.Object.Cards = cards;

            // Act & Assert
            Assert.Throws<InvalidOperationException>( () => _cardStackMock.Object.TakeLastCard());
        }

        [Fact]
        public void GetStackSize_ListOfRandomCards_ShouldReturnSizeOfInputList()
        {
            // Arrange 
            var expectedAmount = 2;
            var cards = new List<ICard>()
            {
                new Card(It.IsAny<int>(), It.IsAny<Suits>(), It.IsAny<bool>()),
                new Card(It.IsAny<int>(), It.IsAny<Suits>(), It.IsAny<bool>())
            };
            _cardStackMock.Object.Cards = cards;

            // Act
            var actualAmount = _cardStackMock.Object.GetStackSize();

            // Assert
            Assert.Equal(expectedAmount, actualAmount);
        }
    }
}
