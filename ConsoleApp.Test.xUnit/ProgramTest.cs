using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test.xUnit
{
    public class ProgramTest
    {
        [Fact]
        public void Main_ConsoleOutput_HelloWorld()
        {
            //Arrange
            var main = typeof(Program).Assembly.EntryPoint;
            var stringWriter = new StringWriter();
            var originalConsoleOutput = Console.Out;
            Console.SetOut(stringWriter);

            //Act
            //Program.Main();
            main.Invoke(null, new object[] { Array.Empty<string>()});

            //
            Console.SetOut(originalConsoleOutput);

            Assert.Equal("Hello, World!\r\n", stringWriter.ToString());
            stringWriter.Dispose();

        }

    }
}
