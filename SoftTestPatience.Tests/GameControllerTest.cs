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
        public void GetUserInput_CheckIfCorrectMessageIsPrintedToConsoleForAskingUserInput()
        {
            Mock<Board> mock = new Mock<Board>();
            GameController sut = new GameController(mock.Object);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                sut.GetUserInput();
                string expected = "(type 'new' for newgame or 'exit' to quit game) Enter a new move: ";
                Assert.Equal(expected, sw.ToString());
            }
        }
    }
}
