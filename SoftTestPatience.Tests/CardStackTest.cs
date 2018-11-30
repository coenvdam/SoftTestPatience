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
            this._cardStackMock.CallBase = true;
        }

        [Fact]
        public void TakeLastCard_EmptyList_ShouldThrowsInvalidOperationException()
        {
            // Arrange
            var cards = new List<Card>();
            _cardStackMock.Object.Cards = cards;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _cardStackMock.Object.TakeLastCard());
        }

        [Fact]
        public void TakeLastCard_ListOfThreeCards_ShouldReturnLastCardInList()
        {
            // Arrange 
            var expectedCard = new Card(It.IsAny<int>(), It.IsAny<Suits>(), It.IsAny<bool>());
            var cards = new List<Card>()
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
            var cards = new List<Card>()
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
            var cards = new List<Card>();
            _cardStackMock.Object.Cards = cards;

            // Act & Assert
            Assert.Throws<InvalidOperationException>( () => _cardStackMock.Object.TakeLastCard());
        }

        [Fact]
        public void GetStackSize_ListOfRandomCards_ShouldReturnSizeOfInputList()
        {
            // Arrange 
            var expectedAmount = 2;
            var cards = new List<Card>()
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

        [Fact]
        public void AddCard_Card_ShouldReturnInvalidOperationException()
        {
            //Arrange
            var card = It.IsAny<Card>();

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => _cardStackMock.Object.AddCard(card));
        }

        [Fact]
        public void ReturnCard_Card_ShouldAddCardToCards()
        {
            //Arrange
            var expectedCount = 1;
            var card = It.IsAny<Card>();
            _cardStackMock.Object.Cards = new List<Card>();

            //Act
            _cardStackMock.Object.ReturnCard(card);

            //Assert
            Assert.Equal(expectedCount, _cardStackMock.Object.Cards.Count);
        }
    }
}
