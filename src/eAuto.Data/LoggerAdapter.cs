using eAuto.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace eAuto.Data
{
	public class LoggerAdapter<T> : IAppLogger<T>
	{
		private readonly ILogger<T> _logger;

		public LoggerAdapter(ILoggerFactory loggerFactory, IConfiguration configuration)
		{
			var logger = new LoggerConfiguration()
			.ReadFrom.Configuration(configuration)
			.Enrich.FromLogContext()
			.CreateLogger();

			_logger = loggerFactory.AddSerilog(logger).CreateLogger<T>();
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