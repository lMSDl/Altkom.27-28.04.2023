using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test.xUnit
{
    public class LoggerTest
    {
        [Fact]
        public void Log_AnyMessage_EventInvoked()
        {
            //Arrage
            var log = new Logger();
            const string ANY_MESSAGE = "a";
            var result = false;
            log.MessageLogged += (sender, args) => result = true;

            //Act
            log.Log(ANY_MESSAGE);
        
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Log_AnyMessage_ValidEventInvoked()
        {
            //Arrage
            var log = new Logger();
            const string ANY_MESSAGE = "a";
            object? eventSender = null;
            Logger.LoggerEventArgs? loggerEventArgs = null;
            log.MessageLogged += (sender, args) => { eventSender = sender; loggerEventArgs = args as Logger.LoggerEventArgs; };
            var timeStart = DateTime.Now;

            //Act
            log.Log(ANY_MESSAGE);

            //Assert
            var timeStop = DateTime.Now;
            Assert.Equal(log, eventSender);
            Assert.NotNull(loggerEventArgs);
            Assert.Equal(ANY_MESSAGE, loggerEventArgs!.Message);
            Assert.InRange(loggerEventArgs.DateTime, timeStart, timeStop);

        }

        [Fact]
        public void GetLogAsync_DateRange_LoggedMessageAsync()
        {
            //Arrage
            var logger = new Logger();
            const string ANY_MESSAGE = "a";
            DateTime RANGE_FROM = DateTime.Now;
            logger.Log(ANY_MESSAGE);
            DateTime RANGE_TO = DateTime.Now;
            string result = null;

            //Act
            var task = logger.GetLogsAsync(RANGE_FROM, RANGE_TO);
            try
            {
                task.Wait();
                result = task.Result;
            }
            catch { }

            //Assert
            Assert.True(task.IsCompletedSuccessfully);
            Assert.Contains(ANY_MESSAGE, result);
            Assert.True(DateTime.TryParseExact(result.Split(": ")[0], "dd.MM.yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
        }
    }
}
