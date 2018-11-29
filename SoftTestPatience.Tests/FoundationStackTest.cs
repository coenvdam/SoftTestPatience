using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class FoundationStackTest
    {
        private Fixture _fixture;
        private FoundationStack _foundationStack;

        public FoundationStackTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _foundationStack = _fixture.Create<FoundationStack>();
        }

        [Fact]
        public void AddCard_RightValueRightSuitCard_ShouldReturnTrue()
        {
            //Arranged
            var value = _fixture.Create<int>();
            var suit = _fixture.Create<Suits>();
            _foundationStack.Suit = suit;
            _foundationStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                new Card(value, suit, _fixture.Create<bool>())
            };
            var card = new Card(value + 1, suit, _fixture.Create<bool>());

            //Act
            var result = _foundationStack.AddCard(card);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AddCard_WrongValueRightSuitCard_ShouldReturnFalse()
        {
            //Arranged
            var value = _fixture.Create<int>();
            var suit = _fixture.Create<Suits>();
            _foundationStack.Suit = suit;
            _foundationStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                new Card(value, suit, _fixture.Create<bool>())
            };
            var card = new Card(value, suit, _fixture.Create<bool>());

            //Act
            var result = _foundationStack.AddCard(card);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddCard_RightValueWrongSuitCard_ShouldReturnFalse()
        {
            //Arranged
            var value = _fixture.Create<int>();
            _foundationStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                new Card(value, Suits.Clubs, _fixture.Create<bool>())
            };
            var card = new Card(value + 1, Suits.Spades, _fixture.Create<bool>());

            //Act
            var result = _foundationStack.AddCard(card);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddCard_WrongValueWrongSuitCard_ShouldReturnFalse()
        {
            //Arranged
            var value = _fixture.Create<int>();
            _foundationStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                new Card(value, Suits.Clubs, _fixture.Create<bool>())
            };
            var card = new Card(value, Suits.Spades, _fixture.Create<bool>());

            //Act
            var result = _foundationStack.AddCard(card);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddCard_EmptyStack_ShouldReturnTrue()
        {
            //Arrange
            var suit = _fixture.Create<Suits>();
            _foundationStack.Cards = new List<Card>();
            _foundationStack.Suit = suit;
            var card = new Card(_fixture.Create<int>(), suit, _fixture.Create<bool>());

            //Act
            var result = _foundationStack.AddCard(card);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GetSuit_Suit_ShouldReturnSuit()
        {
            //Arrange
            var expectedSuit = _fixture.Create<Suits>();
            _foundationStack.Suit = expectedSuit;

            //Act
            var actualSuit = _foundationStack.GetSuit();

            //Assert
            Assert.Equal(expectedSuit, actualSuit);
        }
    }
}
