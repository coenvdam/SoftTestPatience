using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xunit;
using Moq;

namespace SoftTestPatience.Tests
{
    public class GameControllerTest
    {
        public GameControllerTest() { }

        [Fact]
        public void ManageUserInput_NoInput_CheckIfCorrectMessageIsPrintedToConsoleForAskingUserInput()
        {
            // Arrange
            Mock<Board> mock = new Mock<Board>();
            GameController sut = new GameController(mock.Object);
            string expected = "(type 'new' for newgame or 'exit' to quit game) Enter a new move:\n";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(""))
                {
                    Console.SetIn(sr);

                    // Act
                    sut.ManageUserInput();

                    // Assert
                    Assert.Equal(expected, sw.ToString());
                }
            }
        }

        [Fact]
        public void ManageUserInput_MoveStackOf3Cards_ShouldReturnMoveInStringFormat()
        {
            // Arrange
            Mock<Board> mock = new Mock<Board>();
            GameController sut = new GameController(mock.Object);
            string expected = "5 7 3";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader("5 7 3"))
                {
                    Console.SetIn(sr);

                    // Act
                    string actual = sut.ManageUserInput();

                    // Assert
                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact] 
        public void NewGame_NoInput_ShouldPrintCorrectWelcomeMessage()
        {
            // Arrange
            Mock<Board> mock = new Mock<Board>();
            GameController sut = new GameController(mock.Object);
            string expected = "Welcome to Patience!\nType a move like \'5 7 3\' where 5 is the stack to move cards from to stack number 7, and 3 the amount of cards\n";

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                sut.NewGame();

                // Assert
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact] 
        public void NewGame_MockBoard_ShouldCallResetMethodOnBoardOnce()
        {
            // Arrange
            Mock<Board> mock = new Mock<Board>();
            GameController sut = new GameController(mock.Object);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                sut.NewGame();

                // Assert
                mock.Verify(m => m.Reset(), Times.Once());
            }
        }
    }
}
