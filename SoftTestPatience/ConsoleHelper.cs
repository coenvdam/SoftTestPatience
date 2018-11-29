using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    interface IConsoleHelper
    {
        void SetEncoding();
        void Write(string text);
        string ReadInput();
    }

    class ConsoleHelper : IConsoleHelper
    {
        public void SetEncoding()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}
