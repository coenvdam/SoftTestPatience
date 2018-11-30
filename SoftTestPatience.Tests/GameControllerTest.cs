using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class GameControllerTest
    {
        private Fixture _fixture;
        private GameController _gameController;
        private Mock<IBoard> _boardMock;
        private Mock<IConsoleHelper> _consoleHelperMock;

        //BeforeEach
        public GameControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _boardMock = _fixture.Freeze<Mock<IBoard>>();
            _consoleHelperMock = _fixture.Freeze<Mock<IConsoleHelper>>();

            _gameController = _fixture.Create<GameController>();
        }

        [Fact]
        public void RunGame_ExitCommand_ShouldExitGame()
        {
            //Arrange
            var boardText = _fixture.Create<string>();

            var welcomeText = "Welcome to Patience!\nType a command (like 'help') to start!\n";

            var exitCommand = "exit";
            var exitText = "Thank you for playing!\n";

            _boardMock.Setup(b => b.ToString()).Returns(boardText);
            _consoleHelperMock.Setup(c => c.ReadInput()).Returns(exitCommand);

            //Act
            _gameController.RunGame();

            //Assert
            _boardMock.Verify(b => b.ToString(), Times.Once);
            _boardMock.Verify(b => b.Reset(), Times.Once);
            _consoleHelperMock.Verify(c => c.SetEncoding(), Times.Once);
            _consoleHelperMock.Verify(c => c.ReadInput(), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(welcomeText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(exitText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(3));
        }

        [Fact]
        public void RunGame_CreditsCommand_ShouldPrintCredits()
        {
            //Arrange
            var boardText = _fixture.Create<string>();

            var welcomeText = "Welcome to Patience!\nType a command (like 'help') to start!\n";

            var creditsCommand = "credits";
            var creditsText = "This game of Patience was made by Dennis Stiekema (ds222tk) and Cornelis Jan (Coen) van Dam (cv222gk)\n" +
                              "for the course Software Testing (2DV610) at Linnaeus University\n";

            var exitCommand = "exit";
            var exitText = "Thank you for playing!\n";

            _boardMock.Setup(b => b.ToString()).Returns(boardText);
            _consoleHelperMock
                .SetupSequence(c => c.ReadInput())
                .Returns(creditsCommand)
                .Returns(exitCommand);

            //Act
            _gameController.RunGame();

            //Assert
            _boardMock.Verify(b => b.ToString(), Times.Exactly(2));
            _boardMock.Verify(b => b.Reset(), Times.Once);
            _consoleHelperMock.Verify(c => c.SetEncoding(), Times.Once);
            _consoleHelperMock.Verify(c => c.ReadInput(), Times.Exactly(2));
            _consoleHelperMock.Verify(c => c.Write(welcomeText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(creditsText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(exitText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(5));
        }

        [Fact]
        public void RunGame_HelpCommand_ShouldPrintHelp()
        {
            //Arrange
            var boardText = _fixture.Create<string>();

            var welcomeText = "Welcome to Patience!\nType a command (like 'help') to start!\n";

            var helpCommand = "help";
            var helpText = "To move a card from one stack to another, first type the number of the stack with the card,\n" +
                           "then the number of the stack where you want to move it to. If you type a third number, it will\n" +
                           "move the amount of cards of that number. The table stacks are numbered 0 - 6, the waste stack\n" +
                           "is number 7 and the foundation stacks are numbered 8 - 11. An example of a command is '5 6 2'\n" +
                           "Besides moving a card, you can also go through the waste stack, with 'increment waste', ask for\n" +
                           "help with 'help', see the credits with 'credits' and start a new game with 'new'.\n";

            var exitCommand = "exit";
            var exitText = "Thank you for playing!\n";

            _boardMock.Setup(b => b.ToString()).Returns(boardText);
            _consoleHelperMock
                .SetupSequence(c => c.ReadInput())
                .Returns(helpCommand)
                .Returns(exitCommand);

            //Act
            _gameController.RunGame();

            //Assert
            _boardMock.Verify(b => b.ToString(), Times.Exactly(2));
            _boardMock.Verify(b => b.Reset(), Times.Once);
            _consoleHelperMock.Verify(c => c.SetEncoding(), Times.Once);
            _consoleHelperMock.Verify(c => c.ReadInput(), Times.Exactly(2));
            _consoleHelperMock.Verify(c => c.Write(welcomeText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(helpText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(exitText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(5));
        }

        [Fact]
        public void RunGame_SuccessfulMoveCommand_ShouldMoveCard()
        {
            //Arrange
            var boardText = _fixture.Create<string>();
            var welcomeText = "Welcome to Patience!\nType a command (like 'help') to start!\n";

            var successfulMoveOriginStack = 5;
            var successfulMoveDestinationStack = 7;
            var successfulMoveCommand = $"{successfulMoveOriginStack} {successfulMoveDestinationStack}";
            var succesfulMoveText = "The move was successful.\n";

            var exitCommand = "exit";
            var exitText = "Thank you for playing!\n";

            _boardMock.Setup(b => b.ToString()).Returns(boardText);
            _consoleHelperMock
                .SetupSequence(c => c.ReadInput())
                .Returns(successfulMoveCommand)
                .Returns(exitCommand);

            _boardMock.Setup(b => b.Move(successfulMoveOriginStack, successfulMoveDestinationStack, 1)).Returns(true);

            //Act
            _gameController.RunGame();

            //Assert
            _boardMock.Verify(b => b.ToString(), Times.Exactly(2));
            _boardMock.Verify(b => b.Reset(), Times.Once);
            _boardMock.Verify(b => b.Move(successfulMoveOriginStack, successfulMoveDestinationStack, 1), Times.Once);
            _consoleHelperMock.Verify(c => c.SetEncoding(), Times.Once);
            _consoleHelperMock.Verify(c => c.ReadInput(), Times.Exactly(2));
            _consoleHelperMock.Verify(c => c.Write(welcomeText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(succesfulMoveText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(exitText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(5));
        }

        [Fact]
        public void RunGame_FailedMoveCommand_ShouldPrintFail()
        {
            //Arrange
            var boardText = _fixture.Create<string>();
            var welcomeText = "Welcome to Patience!\nType a command (like 'help') to start!\n";

            var failedMoveOriginStack = 1;
            var failedMoveDestinationStack = 2;
            var failedMoveNumberOfCards = 3;
            var failedMoveCommand = $"{failedMoveOriginStack} {failedMoveDestinationStack} {failedMoveNumberOfCards}";
            var failedMoveText = "You can't move the cards like that. Try something else.\n";

            var exitCommand = "exit";
            var exitText = "Thank you for playing!\n";

            _boardMock.Setup(b => b.ToString()).Returns(boardText);
            _consoleHelperMock
                .SetupSequence(c => c.ReadInput())
                .Returns(failedMoveCommand)
                .Returns(exitCommand);

            _boardMock.Setup(b => b.Move(failedMoveOriginStack, failedMoveDestinationStack, failedMoveNumberOfCards)).Returns(false);

            //Act
            _gameController.RunGame();

            //Assert
            _boardMock.Verify(b => b.ToString(), Times.Exactly(2));
            _boardMock.Verify(b => b.Reset(), Times.Once);
            _boardMock.Verify(b => b.Move(failedMoveOriginStack, failedMoveDestinationStack, failedMoveNumberOfCards), Times.Once);
            _consoleHelperMock.Verify(c => c.SetEncoding(), Times.Once);
            _consoleHelperMock.Verify(c => c.ReadInput(), Times.Exactly(2));
            _consoleHelperMock.Verify(c => c.Write(welcomeText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(failedMoveText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(exitText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(5));
        }

        [Fact]
        public void RunGame_NewGameCommand_ShouldResetBoard()
        {
            //Arrange
            var boardText = _fixture.Create<string>();
            var welcomeText = "Welcome to Patience!\nType a command (like 'help') to start!\n";

            var newGameCommand = "new";
            var newGameText = "The board has been reset!\n";

            var exitCommand = "exit";
            var exitText = "Thank you for playing!\n";

            _boardMock.Setup(b => b.ToString()).Returns(boardText);
            _consoleHelperMock
                .SetupSequence(c => c.ReadInput())
                .Returns(newGameCommand)
                .Returns(exitCommand);

            //Act
            _gameController.RunGame();

            //Assert
            _boardMock.Verify(b => b.ToString(), Times.Exactly(2));
            _boardMock.Verify(b => b.Reset(), Times.Exactly(2));
            _consoleHelperMock.Verify(c => c.SetEncoding(), Times.Once);
            _consoleHelperMock.Verify(c => c.ReadInput(), Times.Exactly(2));
            _consoleHelperMock.Verify(c => c.Write(welcomeText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(newGameText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(exitText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(5));
        }

        [Fact]
        public void RunGame_UnknownCommand_ShouldPrintUnknown()
        {
            //Arrange
            var boardText = _fixture.Create<string>();
            var welcomeText = "Welcome to Patience!\nType a command (like 'help') to start!\n";

            var unknownCommand = "unknown";
            var unknownText = "I don't know that command. Try something else.\n";

            var exitCommand = "exit";
            var exitText = "Thank you for playing!\n";

            _boardMock.Setup(b => b.ToString()).Returns(boardText);
            _consoleHelperMock
                .SetupSequence(c => c.ReadInput())
                .Returns(unknownCommand)
                .Returns(exitCommand);

            //Act
            _gameController.RunGame();

            //Assert
            _boardMock.Verify(b => b.ToString(), Times.Exactly(2));
            _boardMock.Verify(b => b.Reset(), Times.Once);
            _consoleHelperMock.Verify(c => c.SetEncoding(), Times.Once);
            _consoleHelperMock.Verify(c => c.ReadInput(), Times.Exactly(2));
            _consoleHelperMock.Verify(c => c.Write(welcomeText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(unknownText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(exitText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(5));
        }

        [Fact]
        public void RunGame_IncrementCommand_ShouldPrintUnknown()
        {
            //Arrange
            var boardText = _fixture.Create<string>();
            var welcomeText = "Welcome to Patience!\nType a command (like 'help') to start!\n";

            var incrementCommand = "increment";
            var incrementText = "There is another card on top of the waste stack now.\n";

            var exitCommand = "exit";
            var exitText = "Thank you for playing!\n";

            _boardMock.Setup(b => b.ToString()).Returns(boardText);
            _consoleHelperMock
                .SetupSequence(c => c.ReadInput())
                .Returns(incrementCommand)
                .Returns(exitCommand);

            //Act
            _gameController.RunGame();

            //Assert
            _boardMock.Verify(b => b.ToString(), Times.Exactly(2));
            _boardMock.Verify(b => b.Reset(), Times.Once);
            _boardMock.Verify(b => b.IncrementWasteStack(), Times.Once);
            _consoleHelperMock.Verify(c => c.SetEncoding(), Times.Once);
            _consoleHelperMock.Verify(c => c.ReadInput(), Times.Exactly(2));
            _consoleHelperMock.Verify(c => c.Write(welcomeText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(incrementText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(exitText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(5));
        }

        [Fact]
        public void RunGame_AllCommands_ShouldRunGame()
        {
            //Arrange
            var boardText = _fixture.Create<string>();
            var welcomeText = "Welcome to Patience!\nType a command (like 'help') to start!\n";

            var creditsCommand = "credits";
            var creditsText = "This game of Patience was made by Dennis Stiekema (ds222tk) and Cornelis Jan (Coen) van Dam (cv222gk)\n" +
                              "for the course Software Testing (2DV610) at Linnaeus University\n";

            var helpCommand = "help";
            var helpText = "To move a card from one stack to another, first type the number of the stack with the card,\n" +
                           "then the number of the stack where you want to move it to. If you type a third number, it will\n" +
                           "move the amount of cards of that number. The table stacks are numbered 0 - 6, the waste stack\n" +
                           "is number 7 and the foundation stacks are numbered 8 - 11. An example of a command is '5 6 2'\n" +
                           "Besides moving a card, you can also go through the waste stack, with 'increment waste', ask for\n" +
                           "help with 'help', see the credits with 'credits' and start a new game with 'new'.\n";

            var successfulMoveOriginStack = 5;
            var successfulMoveDestinationStack = 7;
            var successfulMoveCommand = $"{successfulMoveOriginStack} {successfulMoveDestinationStack}";
            var succesfulMoveText = "The move was successful.\n";

            var failedMoveOriginStack = 1;
            var failedMoveDestinationStack = 2;
            var failedMoveNumberOfCards = 3;
            var failedMoveCommand = $"{failedMoveOriginStack} {failedMoveDestinationStack} {failedMoveNumberOfCards}";
            var failedMoveText = "You can't move the cards like that. Try something else.\n";

            var newGameCommand = "new";
            var newGameText = "The board has been reset!\n";

            var unknownCommand = "unknown";
            var unknownText = "I don't know that command. Try something else.\n";

            var incrementCommand = "increment";
            var incrementText = "There is another card on top of the waste stack now.\n";

            var exitCommand = "exit";
            var exitText = "Thank you for playing!\n";

            _boardMock.Setup(b => b.ToString()).Returns(boardText);
            _consoleHelperMock
                .SetupSequence(c => c.ReadInput())
                .Returns(creditsCommand)
                .Returns(helpCommand)
                .Returns(successfulMoveCommand)
                .Returns(failedMoveCommand)
                .Returns(newGameCommand)
                .Returns(unknownCommand)
                .Returns(incrementCommand)
                .Returns(exitCommand);
            
            _boardMock.Setup(b => b.Move(successfulMoveOriginStack, successfulMoveDestinationStack, 1)).Returns(true);
            _boardMock.Setup(b => b.Move(failedMoveOriginStack, failedMoveDestinationStack, failedMoveNumberOfCards)).Returns(false);

            //Act
            _gameController.RunGame();

            //Assert
            _boardMock.Verify(b => b.ToString(), Times.Exactly(8));
            _boardMock.Verify(b => b.Reset(), Times.Exactly(2));
            _boardMock.Verify(b => b.Move(successfulMoveOriginStack, successfulMoveDestinationStack, 1), Times.Once);
            _boardMock.Verify(b => b.Move(failedMoveOriginStack, failedMoveDestinationStack, failedMoveNumberOfCards), Times.Once);
            _boardMock.Verify(b => b.Move(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
            _boardMock.Verify(b => b.IncrementWasteStack(), Times.Once);

            _consoleHelperMock.Verify(c => c.SetEncoding(), Times.Once);
            _consoleHelperMock.Verify(c => c.ReadInput(), Times.Exactly(8));
            _consoleHelperMock.Verify(c => c.Write(welcomeText),Times.Once);
            _consoleHelperMock.Verify(c => c.Write(creditsText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(helpText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(succesfulMoveText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(failedMoveText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(newGameText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(unknownText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(incrementText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(exitText), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(17));
        }

        [Fact]
        public void NewGame_NoInput_ShouldMakeNewGame()
        {
            //Arrange
            var expectedString = "The board has been reset!\n";

            //Act
            _gameController.NewGame();

            //Assert
            _boardMock.Verify(b => b.Reset(), Times.Once);
            _consoleHelperMock.Verify(c => c.Write(expectedString), Times.Once);
        }

        [Fact]
        public void MoveCard_SuccessfulMove_ShouldPrintSuccess()
        {
            //Arrange
            var expectedString = "The move was successful.\n";
            var stackFrom = _fixture.Create<int>();
            var stackTo = _fixture.Create<int>();

            _boardMock.Setup(b => b.Move(stackFrom, stackTo, 1)).Returns(true);

            //Act
            _gameController.MoveCard(stackFrom, stackTo);

            //Assert
            _consoleHelperMock.Verify(c => c.Write(expectedString), Times.Once);
        }

        [Fact]
        public void MoveCard_UnsuccessfulMove_ShouldPrintFailure()
        {
            //Arrange
            var expectedString = "You can't move the cards like that. Try something else.\n";
            var stackFrom = _fixture.Create<int>();
            var stackTo = _fixture.Create<int>();

            _boardMock.Setup(b => b.Move(stackFrom, stackTo, 1)).Returns(false);

            //Act
            _gameController.MoveCard(stackFrom, stackTo);

            //Assert
            _consoleHelperMock.Verify(c => c.Write(expectedString), Times.Once);
        }

        [Fact]
        public void MoveCards_SuccessfulMove_ShouldPrintSuccess()
        {
            //Arrange
            var expectedString = "The move was successful.\n";
            var stackFrom = _fixture.Create<int>();
            var stackTo = _fixture.Create<int>();
            var numberOfCards = _fixture.Create<int>();

            _boardMock.Setup(b => b.Move(stackFrom, stackTo, numberOfCards)).Returns(true);

            //Act
            _gameController.MoveCards(stackFrom, stackTo, numberOfCards);

            //Assert
            _consoleHelperMock.Verify(c => c.Write(expectedString), Times.Once);
        }

        [Fact]
        public void MoveCards_UnsuccessfulMove_ShouldPrintFailure()
        {
            //Arrange
            var expectedString = "You can't move the cards like that. Try something else.\n";
            var stackFrom = _fixture.Create<int>();
            var stackTo = _fixture.Create<int>();
            var numberOfCards = _fixture.Create<int>();

            _boardMock.Setup(b => b.Move(stackFrom, stackTo, numberOfCards)).Returns(false);

            //Act
            _gameController.MoveCards(stackFrom, stackTo, numberOfCards);

            //Assert
            _consoleHelperMock.Verify(c => c.Write(expectedString), Times.Once);
        }

        [Fact]
        public void IncrementWasteStack_NoInput_ShouldIncrementWasteStack()
        {
            //Act
            _gameController.IncrementWasteStack();

            //Assert
            _boardMock.Verify(b => b.IncrementWasteStack(), Times.Once);
        }

        [Fact]
        public void Help_NoInput_ShouldPrintHelp()
        {
            //Arrange
            var helpText = "To move a card from one stack to another, first type the number of the stack with the card,\n" +
                           "then the number of the stack where you want to move it to. If you type a third number, it will\n" +
                           "move the amount of cards of that number. The table stacks are numbered 0 - 6, the waste stack\n" +
                           "is number 7 and the foundation stacks are numbered 8 - 11. An example of a command is '5 6 2'\n" +
                           "Besides moving a card, you can also go through the waste stack, with 'increment waste', ask for\n" +
                           "help with 'help', see the credits with 'credits' and start a new game with 'new'.\n";

            //Act
            _gameController.Help();

            //Assert
            _consoleHelperMock.Verify(c => c.Write(helpText), Times.Once);
        }

        [Fact]
        public void Credits_NoInput_ShouldPrintCredits()
        {
            //Arrange
            var creditsText = "This game of Patience was made by Dennis Stiekema (ds222tk) and Cornelis Jan (Coen) van Dam (cv222gk)\n" +
                              "for the course Software Testing (2DV610) at Linnaeus University\n";

            //Act
            _gameController.Credits();

            //Assert
            _consoleHelperMock.Verify(c => c.Write(creditsText), Times.Once);
        }
    }
}