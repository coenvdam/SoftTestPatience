using AutoFixture;
using AutoFixture.AutoMoq;
using System;
using System.IO;
using Xunit;

namespace SoftTestPatience.Tests
{
    public class ConsoleHelperTest
    {
        private Fixture _fixture;
        private ConsoleHelper _consoleHelper;

        //BeforeEach
        public ConsoleHelperTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());

            _consoleHelper = _fixture.Create<ConsoleHelper>();
        }

        //Smoke test
        [Fact]
        public void SetEncoding_NoInput_ShouldSetEncoding()
        {
            _consoleHelper.SetEncoding();
        }

        [Fact]
        public void Write_Text_ShouldWriteText()
        {
            //Arrange
            var text = _fixture.Create<string>();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(_fixture.Create<string>()))
                {
                    Console.SetIn(sr);

                    //Act
                    _consoleHelper.Write(text);

                    //Assert
                    Assert.Equal(text, sw.ToString());
                }
            }
        }

        [Fact]
        public void ReadInput_NoInput_ShouldReturnInput()
        {
            //Arrange
            var expectedInput = _fixture.Create<string>();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(expectedInput))
                {
                    Console.SetIn(sr);

                    //Act
                    var actualInput = _consoleHelper.ReadInput();

                    //Assert
                    Assert.Equal(expectedInput, actualInput);
                }
            }
        }
    }
}
