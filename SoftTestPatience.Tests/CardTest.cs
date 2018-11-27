using Moq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class CardTest
    {
        private Mock<Card> _cardMock;

        //BeforeEach
        public CardTest()
        {
            this._cardMock = new Mock<Card>(It.IsAny<int>(), It.IsAny<Suits>(), It.IsAny<bool>());
            //Needed to make sure ToString() is called and not mocked.
            _cardMock.CallBase = true;
        }

        [Fact]
        public void ToString_UnhiddenAceOfHearts_ShouldReturnAceOfHearts()
        {
            //Arrange
            _cardMock.Object.Value = 1;
            _cardMock.Object.Suit = Suits.Hearts;
            _cardMock.Object.Hidden = false;

            var expected = " A\u2665";

            //Act
            var actual = _cardMock.Object.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_UnhiddenJackOfSpades_ShouldReturnJackOfSpades()
        {
            //Arrange
            _cardMock.Object.Value = 11;
            _cardMock.Object.Suit = Suits.Spades;
            _cardMock.Object.Hidden = false;

            var expected = " J\u2660";

            //Act
            var actual = _cardMock.Object.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_UnhiddenQueenOfClubs_ShouldReturnQueenOfClubs()
        {
            //Arrange
            _cardMock.Object.Value = 12;
            _cardMock.Object.Suit = Suits.Clubs;
            _cardMock.Object.Hidden = false;

            var expected = " Q\u2663";

            //Act
            var actual = _cardMock.Object.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_UnhiddenKingOfDiamonds_ShouldReturnKingOfDiamonds()
        {
            //Arrange
            _cardMock.Object.Value = 13;
            _cardMock.Object.Suit = Suits.Diamonds;
            _cardMock.Object.Hidden = false;

            var expected = " K\u2666";

            //Act
            var actual = _cardMock.Object.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_UnhiddenTenOfDiamonds_ShouldReturnTenOfDiamonds()
        {
            //Arrange
            _cardMock.Object.Value = 10;
            _cardMock.Object.Suit = Suits.Diamonds;
            _cardMock.Object.Hidden = false;

            var expected = "10\u2666";

            //Act
            var actual = _cardMock.Object.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_HiddenTenOfDiamonds_ShouldReturnHiddenCard()
        {
            //Arrange
            _cardMock.Object.Value = 13;
            _cardMock.Object.Suit = Suits.Diamonds;
            _cardMock.Object.Hidden = true;

            var expected = "???";

            //Act
            var actual = _cardMock.Object.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
