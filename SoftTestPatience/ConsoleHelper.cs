using System;
using System.Collections.Generic;
using System.Text;

namespace SoftTestPatience
{
    interface IConsoleHelper
    {
        void SetEncoding();
        void Write();
        string ReadInput();
    }

    class ConsoleHelper : IConsoleHelper
    {
        public void SetEncoding()
        {
            throw new NotImplementedException();
        }

        public void Write()
        {
            throw new NotImplementedException();
        }

        public string ReadInput()
        {
            throw new NotImplementedException();
        }
    }
}
