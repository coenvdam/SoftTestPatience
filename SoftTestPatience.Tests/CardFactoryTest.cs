using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class CardFactoryTest
    {
        private Fixture _fixture;
        private Mock<CardFactory> _cardFactoryMock;

        //BeforeEach
        public CardFactoryTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _cardFactoryMock = _fixture.Create<Mock<CardFactory>>();
            _cardFactoryMock.CallBase = true;
        }

        [Fact]
        public void GenerateSortedDeck_NoInput_ShouldReturnSortedDeck()
        {
            // Arrange
            var expected = new List<Card>()
            {
                new Card(1, Suits.Hearts, true),
                new Card(2, Suits.Hearts, true),
                new Card(3, Suits.Hearts, true),
                new Card(4, Suits.Hearts, true),
                new Card(5, Suits.Hearts, true),
                new Card(6, Suits.Hearts, true),
                new Card(7, Suits.Hearts, true),
                new Card(8, Suits.Hearts, true),
                new Card(9, Suits.Hearts, true),
                new Card(10, Suits.Hearts, true),
                new Card(11, Suits.Hearts, true),
                new Card(12, Suits.Hearts, true),
                new Card(13, Suits.Hearts, true),
                new Card(1, Suits.Diamonds, true),
                new Card(2, Suits.Diamonds, true),
                new Card(3, Suits.Diamonds, true),
                new Card(4, Suits.Diamonds, true),
                new Card(5, Suits.Diamonds, true),
                new Card(6, Suits.Diamonds, true),
                new Card(7, Suits.Diamonds, true),
                new Card(8, Suits.Diamonds, true),
                new Card(9, Suits.Diamonds, true),
                new Card(10, Suits.Diamonds, true),
                new Card(11, Suits.Diamonds, true),
                new Card(12, Suits.Diamonds, true),
                new Card(13, Suits.Diamonds, true),
                new Card(1, Suits.Clubs, true),
                new Card(2, Suits.Clubs, true),
                new Card(3, Suits.Clubs, true),
                new Card(4, Suits.Clubs, true),
                new Card(5, Suits.Clubs, true),
                new Card(6, Suits.Clubs, true),
                new Card(7, Suits.Clubs, true),
                new Card(8, Suits.Clubs, true),
                new Card(9, Suits.Clubs, true),
                new Card(10, Suits.Clubs, true),
                new Card(11, Suits.Clubs, true),
                new Card(12, Suits.Clubs, true),
                new Card(13, Suits.Clubs, true),
                new Card(1, Suits.Spades, true),
                new Card(2, Suits.Spades, true),
                new Card(3, Suits.Spades, true),
                new Card(4, Suits.Spades, true),
                new Card(5, Suits.Spades, true),
                new Card(6, Suits.Spades, true),
                new Card(7, Suits.Spades, true),
                new Card(8, Suits.Spades, true),
                new Card(9, Suits.Spades, true),
                new Card(10, Suits.Spades, true),
                new Card(11, Suits.Spades, true),
                new Card(12, Suits.Spades, true),
                new Card(13, Suits.Spades, true),
            };

            // Act
            var actual = _cardFactoryMock.Object.GenerateSortedDeck();

            // Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Value, actual[i].Value);
                Assert.Equal(expected[i].Suit, actual[i].Suit);
                Assert.Equal(expected[i].Hidden, actual[i].Hidden);
            }
        }

        [Fact]
        public void GenerateRandomDeck_NoInput_ShouldReturnRandomDeck()
        {
            //Arrange
            var sortedList = new List<Card>()
            {
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), _fixture.Create<bool>()),
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), _fixture.Create<bool>()),
                new Card(_fixture.Create<int>(), _fixture.Create<Suits>(), _fixture.Create<bool>())
            };
            _cardFactoryMock.Setup(c => c.GenerateSortedDeck()).Returns(sortedList);

            //Act
            var actualList = _cardFactoryMock.Object.GenerateRandomDeck();

            //Assert
            Assert.Equal(sortedList.Count, actualList.Count);
            foreach (var card in sortedList)
            {
                Assert.Contains(actualList, c => c.Value == card.Value && c.Suit == card.Suit && c.Hidden == card.Hidden);
            }
        }
    }
}
