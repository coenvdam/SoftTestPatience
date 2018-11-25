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
        public void ManageUserInput_CheckIfCorrectMessageIsPrintedToConsoleForAskingUserInput()
        {
            Mock<Board> mock = new Mock<Board>();
            GameController sut = new GameController(mock.Object);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut.ManageUserInput();
                string expected = "(type 'new' for newgame or 'exit' to quit game) Enter a new move: ";
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void ManageUserInput_MoveStackOf3Cards_ShouldReturnMoveInStringFormat()
        {
            Mock<Board> mock = new Mock<Board>();
            GameController sut = new GameController(mock.Object);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader("5 7 3"))
                {
                    Console.SetIn(sr);
                    string actual = sut.ManageUserInput();
                    string expected = "5 7 3";
                    Assert.Equal(expected, actual);
                }
            }
        }
    }
}
