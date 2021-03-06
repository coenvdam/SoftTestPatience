﻿using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class BoardTest
    {
        private Fixture _fixture;
        private Board _board;
        private Mock<ICardFactory> _cardFactoryMock;

        //BeforeEach
        public BoardTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _cardFactoryMock = _fixture.Freeze<Mock<ICardFactory>>();

            _board = _fixture.Create<Board>();
        }

        [Fact]
        public void Reset_NoInput_ShouldResetBoard()
        {
            //Arrange
            var expectedStackCount = 12;
            var expectedWasteStackCount = 24;
            var expectedFoundationStackCount = 0;
            var deck = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                deck.Add(_fixture.Create<Card>());
            }

            _cardFactoryMock.Setup(c => c.GenerateRandomDeck()).Returns(deck);

            //Act
            _board.Reset();

            //Assert
            _cardFactoryMock.Verify(c => c.GenerateRandomDeck(), Times.Once);
            Assert.Equal(expectedStackCount, _board.Stacks.Count);
            for (int i = 1; i < 8; i++)
            {
                Assert.Equal(i, _board.Stacks[i - 1].Cards.Count);
            }
            Assert.Equal(expectedWasteStackCount, _board.Stacks[7].Cards.Count);
            foreach (var card in _board.Stacks[7].Cards)
            {
                Assert.False(card.Hidden);
            }
            for (int i = 0; i < 4; i++)
            {
                Assert.Equal(expectedFoundationStackCount, _board.Stacks[8 + i].Cards.Count);
            }
        }

        [Fact]
        public void Move_StackNumberHigherThanStackNumbers_ShouldReturnFalse()
        {
            //Arrange
            var originStack = 15;
            var destinationStack = 8;
            var numberOfCards = 2;

            //Act
            var result = _board.Move(originStack, destinationStack, numberOfCards);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Move_NumberHigherThanOneButNoTwoTableStacks_ShouldReturnFalse()
        {
            //Arrange
            var originStack = 4;
            var destinationStack = 8;
            var numberOfCards = 2;

            //Act
            var result = _board.Move(originStack, destinationStack, numberOfCards);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Move_NumberHigherThanOneAndTwoTableStacksButUnsuccesfulMove_ShouldReturnFalse()
        {
            //Arrange
            var originStack = 0;
            var destinationStack = 1;
            var numberOfCards = _fixture.Create<int>();

            var originStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());
            var destinationStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());

            _board.Stacks = new List<CardStack>()
            {
                originStackMock.Object,
                destinationStackMock.Object
            };

            originStackMock.Setup(o => o.TakeLastCards(numberOfCards)).Throws(new InvalidOperationException());

            //Act
            var result = _board.Move(originStack, destinationStack, numberOfCards);

            //Assert
            originStackMock.Verify(o => o.TakeLastCards(numberOfCards), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(It.IsAny<Card>()), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public void Move_DifferentStacksOneCardSuccesfulMove_ShouldReturnTrue()
        {
            //Arrange
            var originStack = 0;
            var destinationStack = 8;
            var numberOfCards = 1;
            var card = _fixture.Create<Card>();
            var originStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());
            originStackMock.Setup(o => o.GetLastCard()).Throws(new InvalidOperationException());
            var destinationStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());

            _board.Stacks = new List<CardStack>()
            {
                originStackMock.Object,
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                destinationStackMock.Object
            };

            originStackMock.Setup(o => o.TakeLastCard()).Returns(card);
            destinationStackMock.Setup(o => o.AddCard(card)).Returns(true);

            //Act
            var result = _board.Move(originStack, destinationStack, numberOfCards);

            //Assert
            originStackMock.Verify(o => o.TakeLastCard(), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(card), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void Move_CardFitsOnDestinationStackAndOriginStackIsNotEmpty_ShouldReturnTrue()
        {
            //Arrange
            var card = _fixture.Create<Card>();
            var cardMock = new Mock<Card>(_fixture.Create<int>(), _fixture.Create<Suits>(), _fixture.Create<bool>());
            var originStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());
            var destinationStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());

            originStackMock.Setup(o => o.TakeLastCard()).Returns(card);
            originStackMock.Setup(o => o.GetLastCard()).Returns(cardMock.Object);
            destinationStackMock.Setup(o => o.AddCard(card)).Returns(true);

            //Act
            var result = _board.Move(originStackMock.Object, destinationStackMock.Object);

            //Assert
            originStackMock.Verify(o => o.TakeLastCard(), Times.Once);
            originStackMock.Verify(o => o.GetLastCard(), Times.Once);
            cardMock.Verify(o => o.Flip(), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(card), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void Move_CardFitsOnDestinationStackAndOriginStackIsEmpty_ShouldReturnTrue()
        {
            //Arrange
            var card = _fixture.Create<Card>();
            var originStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());
            var destinationStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());

            originStackMock.Setup(o => o.TakeLastCard()).Returns(card);
            originStackMock.Setup(o => o.GetLastCard()).Throws(new InvalidOperationException());
            destinationStackMock.Setup(o => o.AddCard(card)).Returns(true);

            //Act
            var result = _board.Move(originStackMock.Object, destinationStackMock.Object);

            //Assert
            originStackMock.Verify(o => o.TakeLastCard(), Times.Once);
            originStackMock.Verify(o => o.GetLastCard(), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(card), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void Move_TakeLastCardThrowsException_ShouldReturnFalse()
        {
            //Arrange
            var originStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());
            var destinationStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());

            originStackMock.Setup(o => o.TakeLastCard()).Throws(new InvalidOperationException());

            //Act
            var result = _board.Move(originStackMock.Object, destinationStackMock.Object);

            //Assert
            originStackMock.Verify(o => o.TakeLastCard(), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(It.IsAny<Card>()), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public void Move_CardDoesNotFitOnDestinationStack_ShouldReturnFalse()
        {
            //Arrange
            var card = _fixture.Create<Card>();
            var originStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());
            var destinationStackMock = new Mock<CardStack>(_fixture.Create<List<Card>>());

            originStackMock.Setup(o => o.TakeLastCard()).Returns(card);
            originStackMock.Setup(o => o.ReturnCard(card));
            destinationStackMock.Setup(o => o.AddCard(card)).Returns(false);

            //Act
            var result = _board.Move(originStackMock.Object, destinationStackMock.Object);

            //Assert
            originStackMock.Verify(o => o.TakeLastCard(), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(card), Times.Once);
            originStackMock.Verify(o => o.ReturnCard(card), Times.Once);
            Assert.False(result);
        }

        [Fact]
        public void Move_CardsFitOnDestinationStackAndOriginStackIsNotEmpty_ShouldReturnTrue()
        {
            //Arrange
            var cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                _fixture.Create<Card>()
            };
            var cardMock = new Mock<Card>(_fixture.Create<int>(), _fixture.Create<Suits>(), _fixture.Create<bool>());
            var numberOfCards = _fixture.Create<int>();
            var originStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());
            var destinationStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());

            originStackMock.Setup(o => o.TakeLastCards(numberOfCards)).Returns(cards);
            originStackMock.Setup(o => o.GetLastCard()).Returns(cardMock.Object);
            destinationStackMock.Setup(o => o.AddCard(It.IsAny<Card>())).Returns(true);

            //Act
            var result = _board.Move(originStackMock.Object, destinationStackMock.Object, numberOfCards);

            //Assert
            originStackMock.Verify(o => o.TakeLastCards(numberOfCards), Times.Once);
            originStackMock.Verify(o => o.GetLastCard(), Times.Once);
            cardMock.Verify(o => o.Flip(), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(It.IsAny<Card>()), Times.Exactly(3));
            Assert.True(result);
        }

        [Fact]
        public void Move_CardsFitOnDestinationStackAndOriginStackIsEmpty_ShouldReturnTrue()
        {
            //Arrange
            var cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                _fixture.Create<Card>()
            };
            var numberOfCards = _fixture.Create<int>();
            var originStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());
            originStackMock.Setup(o => o.GetLastCard()).Throws(new InvalidOperationException());
            var destinationStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());

            originStackMock.Setup(o => o.TakeLastCards(numberOfCards)).Returns(cards);
            destinationStackMock.Setup(o => o.AddCard(It.IsAny<Card>())).Returns(true);

            //Act
            var result = _board.Move(originStackMock.Object, destinationStackMock.Object, numberOfCards);

            //Assert
            originStackMock.Verify(o => o.TakeLastCards(numberOfCards), Times.Once);
            originStackMock.Verify(o => o.GetLastCard(), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(It.IsAny<Card>()), Times.Exactly(3));
            Assert.True(result);
        }

        [Fact]
        public void Move_TakeLastCardsThrowsException_ShouldReturnFalse()
        {
            //Arrange
            var numberOfCards = _fixture.Create<int>();
            var originStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());
            var destinationStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());

            originStackMock.Setup(o => o.TakeLastCards(numberOfCards)).Throws(new InvalidOperationException());

            //Act
            var result = _board.Move(originStackMock.Object, destinationStackMock.Object, numberOfCards);

            //Assert
            originStackMock.Verify(o => o.TakeLastCards(numberOfCards), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(It.IsAny<Card>()), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public void Move_CardsDoNotFitOnDestinationStack_ShouldReturnFalse()
        {
            //Arrange
            var numberOfCards = _fixture.Create<int>();
            var cards = new List<Card>()
            {
                _fixture.Create<Card>(),
                _fixture.Create<Card>(),
                _fixture.Create<Card>()
            };
            var originStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());
            var destinationStackMock = new Mock<TableStack>(_fixture.Create<List<Card>>());

            originStackMock.Setup(o => o.TakeLastCards(numberOfCards)).Returns(cards);
            originStackMock.Setup(o => o.ReturnCard(It.IsAny<Card>()));
            destinationStackMock.SetupSequence(o => o.AddCard(It.IsAny<Card>())).Returns(true).Returns(false);

            //Act
            var result = _board.Move(originStackMock.Object, destinationStackMock.Object, numberOfCards);

            //Assert
            originStackMock.Verify(o => o.TakeLastCards(numberOfCards), Times.Once);
            destinationStackMock.Verify(d => d.AddCard(It.IsAny<Card>()), Times.Exactly(2));
            destinationStackMock.Verify(d => d.TakeLastCards(1), Times.Once);
            originStackMock.Verify(o => o.ReturnCard(It.IsAny<Card>()), Times.Exactly(3));
            Assert.False(result);
        }

        [Fact]
        public void IncrementWasteStack_NoInput_ShouldIncrementWasteStack()
        {
            //Arrange
            var wasteStackMock = new Mock<WasteStack>(_fixture.Create<List<Card>>());
            _board.Stacks = new List<CardStack>()
            {
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                _fixture.Create<CardStack>(),
                wasteStackMock.Object
            };

            //Act
            _board.IncrementWasteStack();

            //Assert
            wasteStackMock.Verify(w => w.IncrementIndex(), Times.Once);
        }

        [Fact]
        public void ToString_Board_ShouldReturnStringOfBoard()
        {
            //Arrange
            var tableCardString = _fixture.Create<string>();
            var cardMock = new Mock<Card>(_fixture.Create<int>(), _fixture.Create<Suits>(), _fixture.Create<bool>());
            cardMock.Setup(c => c.ToString()).Returns(tableCardString);

            var cardList = new List<Card>()
            {
                cardMock.Object,
                cardMock.Object
            };

            var wasteStackString = _fixture.Create<string>();
            var wasteStackMock = new Mock<WasteStack>(_fixture.Create<List<Card>>());
            wasteStackMock.Setup(w => w.ToString()).Returns(wasteStackString);

            var foundationStackString = _fixture.Create<string>();
            var foundationStackMock = new Mock<FoundationStack>(_fixture.Create<List<Card>>(), _fixture.Create<Suits>());
            foundationStackMock.Setup(f => f.ToString()).Returns(foundationStackString);

            var stackList = new List<CardStack>()
            {
                new TableStack(cardList),
                new TableStack(cardList),
                new TableStack(cardList),
                new TableStack(cardList),
                new TableStack(cardList),
                new TableStack(cardList),
                new TableStack(cardList),
                wasteStackMock.Object,
                foundationStackMock.Object,
                foundationStackMock.Object,
                foundationStackMock.Object,
                foundationStackMock.Object
            };
            _board.Stacks = stackList;

            var expectedString = "";
            expectedString += wasteStackString;
            expectedString += "     ";
            for (int i = 0; i < 4; i++)
            {
                expectedString += foundationStackString;
            }

            expectedString += "\n";
            for (int i = 0; i < 7; i++)
            {
                expectedString += tableCardString;
            }

            expectedString += "\n";

            for (int i = 0; i < 7; i++)
            {
                expectedString += tableCardString;
            }

            expectedString += "\n";

            //Act
            var actualString = _board.ToString();

            //Assert
            Assert.Equal(expectedString, actualString);
        }
    }
}

