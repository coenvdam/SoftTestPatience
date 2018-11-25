﻿using System;
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
    }
}
