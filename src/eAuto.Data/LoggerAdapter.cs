using eAuto.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace eAuto.Data
{
	public class LoggerAdapter<T> : IAppLogger<T>
	{
		private readonly ILogger<T> _logger;

		public LoggerAdapter(ILoggerFactory loggerFactory)
		{
			_logger= loggerFactory.CreateLogger<T>();
		}

		public void LogError(Exception exception, string message, params object[] args)
		{
			_logger.LogError(exception, message, args);
		}

		public void LogInformation(string message, params object[] args)
		{
			_logger.LogInformation(message, args);
		}

		public void LogWarning(string message, params object[] args)
		{
			_logger.LogWarning(message, args);
		}
	}
}