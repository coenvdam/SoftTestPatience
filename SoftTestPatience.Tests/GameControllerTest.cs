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
                string expected = "Enter a new move: ";
                Assert.Equal(expected, sw.ToString());
            }
        }
    }
}
