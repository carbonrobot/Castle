using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Services.Logging;
using NLog;

namespace Castle.Web.Logging
{
    /// <summary>
    /// NLog logger implementing special ILog class
    /// </summary>
    public class NLogLog : ILog, ILog<NLogLog>
    {
        public void Debug(string message, params object[] formatting)
        {
            if (_logger.IsDebugEnabled) _logger.Debug(message, formatting);
        }

        public void Debug(Func<string> message)
        {
            if (_logger.IsDebugEnabled) _logger.Debug(message());
        }

        public void Error(string message, params object[] formatting)
        {
            // don't check for enabled at this level
            _logger.Error(message, formatting);
        }

        public void Error(Func<string> message)
        {
            // don't check for enabled at this level
            _logger.Error(message());
        }

        public void Error(Func<string> message, Exception exception)
        {
            _logger.ErrorException(message(), exception);
        }

        public void Fatal(string message, params object[] formatting)
        {
            // don't check for enabled at this level
            _logger.Fatal(message, formatting);
        }

        public void Fatal(Func<string> message)
        {
            // don't check for enabled at this level
            _logger.Fatal(message());
        }

        public void Fatal(Func<string> message, Exception exception)
        {
            _logger.FatalException(message(), exception);
        }

        public void Info(string message, params object[] formatting)
        {
            if (_logger.IsInfoEnabled) _logger.Info(message, formatting);
        }

        public void Info(Func<string> message)
        {
            if (_logger.IsInfoEnabled) _logger.Info(message());
        }

        public void InitializeFor(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        public void Warn(string message, params object[] formatting)
        {
            if (_logger.IsWarnEnabled) _logger.Warn(message, formatting);
        }

        public void Warn(Func<string> message)
        {
            if (_logger.IsWarnEnabled) _logger.Warn(message());
        }

        private Logger _logger;
    }
}
