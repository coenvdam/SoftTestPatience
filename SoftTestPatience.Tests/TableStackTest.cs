using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class TableStackTest
    {
        private Fixture _fixture;
        private TableStack _tableStack;

        public TableStackTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _fixture.Create<TableStack>();
        }

        [Fact]
        public void AddCard_RightValueRightSuitCard_shouldReturnTrue()
        {
            //Arranged
            var value = _fixture.Create<int>();
            _tableStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                new Card(value, Suits.Diamonds, _fixture.Create<bool>())
            };
            var card = new Card(value - 1, Suits.Spades, _fixture.Create<bool>());

            //Act
            var result = _tableStack.AddCard(card);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AddCard_WrongValueRightSuitCard_shouldReturnFalse()
        {
            //Arranged
            var value = _fixture.Create<int>();
            _tableStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                new Card(value, Suits.Clubs, _fixture.Create<bool>())
            };
            var card = new Card(value, Suits.Hearts, _fixture.Create<bool>());

            //Act
            var result = _tableStack.AddCard(card);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddCard_RightValueWrongSuitCard_shouldReturnFalse()
        {
            //Arranged
            var value = _fixture.Create<int>();
            var suit = _fixture.Create<Suits>();
            _tableStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                new Card(value, suit, _fixture.Create<bool>())
            };
            var card = new Card(value - 1, suit, _fixture.Create<bool>());

            //Act
            var result = _tableStack.AddCard(card);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddCard_WrongValueWrongSuitCard_shouldReturnFalse()
        {
            //Arranged
            var value = _fixture.Create<int>();
            var suit = _fixture.Create<Suits>();
            _tableStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                new Card(value, suit, _fixture.Create<bool>())
            };
            var card = new Card(value, suit, _fixture.Create<bool>());

            //Act
            var result = _tableStack.AddCard(card);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void TakeLastCards_NumberEqualToStackCount_shouldReturnCards()
        {
            //Arrange
            var expectedCards = new List<Card>()
            {
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), true),
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), true),
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), true),
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), true)
            };
            _tableStack.Cards = expectedCards;
            
            //Act
            var actualCards = _tableStack.TakeLastCards(4);

            //Assert
            Assert.Equal(expectedCards.Count, actualCards.Count);
            foreach (var card in expectedCards)
            {
                Assert.Contains(actualCards,
                    c => c.Value == card.Value && c.Suit == card.Suit && c.Hidden == card.Hidden);
            }
        }

        [Fact]
        public void TakeLastCards_NumberHigherThanStackCount_shouldReturnInvalidOperationException()
        {
            //Arrange
            _tableStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                _fixture.Create<Card>()
            };

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => _tableStack.TakeLastCards(5));
        }

        [Fact]
        public void TakeLastCards_NumberLowerThanStackCountButHigherThanUnhiddenCards_shouldReturnInvalidOperationException()
        {
            //Arrange
            _tableStack.Cards = new List<Card>()
            {
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), true),
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), true),
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), false),
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), false)
            };

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => _tableStack.TakeLastCards(3));
        }
    }
}
