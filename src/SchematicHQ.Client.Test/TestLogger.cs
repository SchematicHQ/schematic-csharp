using NUnit.Framework;

namespace SchematicHQ.Client.Tests
{
    [TestFixture]
    public class LoggerTests
    {
        private StringWriter _stringWriter;
        private ConsoleLogger _logger;

        [SetUp]
        public void SetUp()
        {
            //redirect console output
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
            // The shared tests below exercise every level, so use Debug here.
            // Level-filter behavior is covered by the dedicated tests further down.
            _logger = new ConsoleLogger(LogLevel.Debug);
        }

        [TearDown]
        public void TearDown()
        {
            _stringWriter.Dispose();
            // reset console output writer 
            Console.SetOut(Console.Out);
        }

        [Test]
        public void Error_ShouldLogErrorMessage()
        {
            // Arrange
            var message = "This is an error message";

            // Act
            _logger.Error(message);

            // Assert
            var output = _stringWriter.ToString().Trim();
            Assert.That(output.Contains("[ERROR]"), Is.True);
            Assert.That(output.Contains(message), Is.True);
        }

        [Test]
        public void Test_Warn_ShouldLogWarnMessage()
        {
            // Arrange
            var message = "This is a warning message";

            // Act
            _logger.Warn(message);

            // Assert
            var output = _stringWriter.ToString().Trim();
            Assert.That(output.Contains("[WARN]"), Is.True);
            Assert.That(output.Contains(message), Is.True);
        }

        [Test]
        public void Test_Info_ShouldLogInfoMessage()
        {
            // Arrange
            var message = "This is an info message";

            // Act
            _logger.Info(message);

            // Assert
            var output = _stringWriter.ToString().Trim();
            Assert.That(output.Contains("[INFO]"), Is.True);
            Assert.That(output.Contains(message), Is.True);
        }

        [Test]
        public void Test_Debug_ShouldLogDebugMessage()
        {
            // Arrange
            var message = "This is a debug message";

            // Act
            _logger.Debug(message);

            // Assert
            var output = _stringWriter.ToString().Trim();
            Assert.That(output.Contains("[DEBUG]"), Is.True);
            Assert.That(output.Contains(message), Is.True);
        }

        [Test]
        public void Test_Error_ShouldFormatMessageWithArgs()
        {
            // Arrange
            var message = "Error {0}";
            var arg = "123";

            // Act
            _logger.Error(message, arg);

            // Assert
            var output = _stringWriter.ToString().Trim();
            Assert.That(output.Contains("[ERROR]"), Is.True);
            Assert.That(output.Contains("Error 123"), Is.True);
        }

        [Test]
        public void Test_Warn_ShouldFormatMessageWithArgs()
        {
            // Arrange
            var message = "Warning {0}";
            var arg = "123";

            // Act
            _logger.Warn(message, arg);

            // Assert
            var output = _stringWriter.ToString().Trim();
            Assert.That(output.Contains("[WARN]"), Is.True);
            Assert.That(output.Contains("Warning 123"), Is.True);
        }

        [Test]
        public void Test_Info_ShouldFormatMessageWithArgs()
        {
            // Arrange
            var message = "Info {0}";
            var arg = "123";

            // Act
            _logger.Info(message, arg);

            // Assert
            var output = _stringWriter.ToString().Trim();
            Assert.That(output.Contains("[INFO]"), Is.True);
            Assert.That(output.Contains("Info 123"), Is.True);
        }

        [Test]
        public void Test_Debug_ShouldFormatMessageWithArgs()
        {
            // Arrange
            var message = "Debug {0}";
            var arg = "123";

            // Act
            _logger.Debug(message, arg);

            // Assert
            var output = _stringWriter.ToString().Trim();
            Assert.That(output.Contains("[DEBUG]"), Is.True);
            Assert.That(output.Contains("Debug 123"), Is.True);
        }

        [Test]
        public void DefaultConstructor_DefaultsToWarnLevel()
        {
            // Per the SDK spec: a ConsoleLogger with no level argument must
            // suppress Debug and Info but emit Warn and Error.
            var logger = new ConsoleLogger();

            logger.Debug("debug-msg");
            logger.Info("info-msg");
            logger.Warn("warn-msg");
            logger.Error("error-msg");

            var output = _stringWriter.ToString();
            Assert.That(output.Contains("debug-msg"), Is.False);
            Assert.That(output.Contains("info-msg"), Is.False);
            Assert.That(output.Contains("warn-msg"), Is.True);
            Assert.That(output.Contains("error-msg"), Is.True);
        }

        [Test]
        public void WarnLevel_FiltersDebugAndInfo()
        {
            var logger = new ConsoleLogger(LogLevel.Warn);

            logger.Debug("debug-msg");
            logger.Info("info-msg");
            logger.Warn("warn-msg");
            logger.Error("error-msg");

            var output = _stringWriter.ToString();
            Assert.That(output.Contains("debug-msg"), Is.False);
            Assert.That(output.Contains("info-msg"), Is.False);
            Assert.That(output.Contains("warn-msg"), Is.True);
            Assert.That(output.Contains("error-msg"), Is.True);
        }

        [Test]
        public void InfoLevel_FiltersDebugOnly()
        {
            var logger = new ConsoleLogger(LogLevel.Info);

            logger.Debug("debug-msg");
            logger.Info("info-msg");
            logger.Warn("warn-msg");
            logger.Error("error-msg");

            var output = _stringWriter.ToString();
            Assert.That(output.Contains("debug-msg"), Is.False);
            Assert.That(output.Contains("info-msg"), Is.True);
            Assert.That(output.Contains("warn-msg"), Is.True);
            Assert.That(output.Contains("error-msg"), Is.True);
        }

        [Test]
        public void ErrorLevel_OnlyEmitsError()
        {
            var logger = new ConsoleLogger(LogLevel.Error);

            logger.Debug("debug-msg");
            logger.Info("info-msg");
            logger.Warn("warn-msg");
            logger.Error("error-msg");

            var output = _stringWriter.ToString();
            Assert.That(output.Contains("debug-msg"), Is.False);
            Assert.That(output.Contains("info-msg"), Is.False);
            Assert.That(output.Contains("warn-msg"), Is.False);
            Assert.That(output.Contains("error-msg"), Is.True);
        }

        [Test]
        public void DebugLevel_EmitsAllLevels()
        {
            var logger = new ConsoleLogger(LogLevel.Debug);

            logger.Debug("debug-msg");
            logger.Info("info-msg");
            logger.Warn("warn-msg");
            logger.Error("error-msg");

            var output = _stringWriter.ToString();
            Assert.That(output.Contains("debug-msg"), Is.True);
            Assert.That(output.Contains("info-msg"), Is.True);
            Assert.That(output.Contains("warn-msg"), Is.True);
            Assert.That(output.Contains("error-msg"), Is.True);
        }
    }
}