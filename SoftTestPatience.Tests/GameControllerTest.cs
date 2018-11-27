using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class GameControllerTest
    {
        private Fixture _fixture;
        private GameController _gameController;
        private Mock<IBoard> _boardMock;

        //BeforeEach
        public GameControllerTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _boardMock = _fixture.Freeze<Mock<IBoard>>();

            _gameController = _fixture.Create<GameController>();
        }

        [Fact]
        public void ManageUserInput_NoInput_CheckIfCorrectMessageIsPrintedToConsoleForAskingUserInput()
        {
            // Arrange
            string expected = "(type 'new' for newgame or 'exit' to quit game) Enter a new move:\n";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(""))
                {
                    Console.SetIn(sr);

                    // Act
                    _gameController.ManageUserInput();

                    // Assert
                    Assert.Equal(expected, sw.ToString());
                }
            }
        }

        [Fact]
        public void ManageUserInput_MoveStackOf3Cards_ShouldReturnMoveInStringFormat()
        {
            // Arrange
            string expected = "5 7 3";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader("5 7 3"))
                {
                    Console.SetIn(sr);

                    // Act
                    string actual = _gameController.ManageUserInput();

                    // Assert
                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void NewGame_NoInput_ShouldPrintCorrectWelcomeMessage()
        {
            // Arrange
            string expected = "Welcome to Patience!\nType a move like \'5 7 3\' where 5 is the stack to move cards from to stack number 7, and 3 the amount of cards\n";
            Regex regex = new Regex(expected);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader("exit"))
                {
                    Console.SetIn(sr);

                    // Act
                    _gameController.NewGame();

                    // Assert
                    System.Text.RegularExpressions.Match match = regex.Match(sw.ToString());
                    Assert.True(match.Success);
                }
            }
        }

        [Fact]
        public void NewGame_MockBoard_ShouldCallResetMethodOnBoardOnce()
        {
            // Act
            _gameController.NewGame();

            // Assert
            _boardMock.Verify(m => m.Reset(), Times.Once());
        }

        [Fact]
        public void RunGame_Exit_ShouldCallUserInputAndExit()
        {
            // Arrange
            string expected = "Exiting Game!";
            Regex regex = new Regex(expected);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader("exit"))
                {
                    Console.SetIn(sr);

                    // Act
                    _gameController.RunGame();

                    // Assert
                    System.Text.RegularExpressions.Match match = regex.Match(sw.ToString());
                    Assert.True(match.Success);
                }
            }
        }
    }
}