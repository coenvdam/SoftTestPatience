using Moq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class CardTest
    {
        private Card _card;

        //BeforeEach
        public CardTest()
        {
            this._card = new Card(It.IsAny<int>(), It.IsAny<Suits>(), It.IsAny<bool>());
        }

        [Fact]
        public void ToString_UnhiddenAceOfHearts_ShouldReturnAceOfHearts()
        {
            //Arrange
            _card.Value = 1;
            _card.Suit = Suits.Hearts;
            _card.Hidden = false;

            var expected = "[ A\u2665]";

            //Act
            var actual = _card.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_UnhiddenJackOfSpades_ShouldReturnJackOfSpades()
        {
            //Arrange
            _card.Value = 11;
            _card.Suit = Suits.Spades;
            _card.Hidden = false;

            var expected = "[ J\u2660]";

            //Act
            var actual = _card.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_UnhiddenQueenOfClubs_ShouldReturnQueenOfClubs()
        {
            //Arrange
            _card.Value = 12;
            _card.Suit = Suits.Clubs;
            _card.Hidden = false;

            var expected = "[ Q\u2663]";

            //Act
            var actual = _card.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_UnhiddenKingOfDiamonds_ShouldReturnKingOfDiamonds()
        {
            //Arrange
            _card.Value = 13;
            _card.Suit = Suits.Diamonds;
            _card.Hidden = false;

            var expected = "[ K\u2666]";

            //Act
            var actual = _card.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_UnhiddenTenOfDiamonds_ShouldReturnTenOfDiamonds()
        {
            //Arrange
            _card.Value = 10;
            _card.Suit = Suits.Diamonds;
            _card.Hidden = false;

            var expected = "[10\u2666]";

            //Act
            var actual = _card.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToString_HiddenTenOfDiamonds_ShouldReturnHiddenCard()
        {
            //Arrange
            _card.Value = 13;
            _card.Suit = Suits.Diamonds;
            _card.Hidden = true;

            var expected = "[???]";

            //Act
            var actual = _card.ToString();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
