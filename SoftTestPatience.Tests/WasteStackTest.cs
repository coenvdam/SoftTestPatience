using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class WasteStackTest
    {
        private Fixture _fixture;
        private WasteStack _wasteStack;

        public WasteStackTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _wasteStack = _fixture.Create<WasteStack>();
        }

        [Fact]
        public void AddCard_Card_ShouldReturnInvalidOperationException()
        {
            //Arrange
            var card = _fixture.Create<Card>();

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => _wasteStack.AddCard(card));
        }

        [Fact]
        public void GetIndex_NoInput_ShouldReturnIndex()
        {
            //Arrange
            var expectedIndex = _fixture.Create<int>();
            _wasteStack.Index = expectedIndex;

            //Act
            var actualIndex = _wasteStack.GetIndex();

            //Assert
            Assert.Equal(expectedIndex, actualIndex);
        }

        [Fact]
        public void IncrementIndex_IndexLowerThanCardCount_ShouldReturnIncrementedIndex()
        {
            //Arrange
            _wasteStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                _fixture.Create<Card>()
            };
            var expectedIndex = 3;
            _wasteStack.Index = 2;

            //Act
            _wasteStack.IncrementIndex();

            //Assert
            Assert.Equal(expectedIndex, _wasteStack.Index);
        }

        [Fact]
        public void IncrementIndex_IndexEqualToCardCount_ShouldReturnResetIndex()
        {
            //Arrange
            _wasteStack.Cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                _fixture.Create<Card>()
            };
            var expectedIndex = 0;
            _wasteStack.Index = 3;

            //Act
            _wasteStack.IncrementIndex();

            //Assert
            Assert.Equal(expectedIndex, _wasteStack.Index);
        }
    }
}
