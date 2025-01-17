using Microsoft.Extensions.Logging;
using Moq;

namespace Workspec.Architecture.SmartMocks;

public static class LoggerMockExtensions
{
    public static void VerifyLog(this Mock<ILogger> logger, LogLevel logLevel, string message, Times times)
    {
        logger.Verify(x =>
            x.Log(
                logLevel,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains(message)),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }
}
