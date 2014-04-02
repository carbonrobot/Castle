using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castle.Services.Logging
{
    /// <summary>
    /// Custom interface for logging messages
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Detailed diagnostic messages for development. Not normally written to the log file, but only to console.
        /// The lazy overload Debug(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Debug(string message, params object[] formatting);

        /// <summary>
        /// Detailed diagnostic messages for development. Not normally written to the log file, but only to console.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(Func<string> message);

        /// <summary>
        /// Exceptions that prevent normal use of the application.
        /// The lazy overload Error(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Error(string message, params object[] formatting);

        /// <summary>
        /// Exceptions that prevent normal use of the application.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(Func<string> message);

        /// <summary>
        /// Severe errors that typically cause termination of the application.
        /// The lazy overload Fatal(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Fatal(string message, params object[] formatting);

        /// <summary>
        /// Severe errors that typically cause termination of the application.
        /// </summary>
        /// <param name="message">The message.</param>
        void Fatal(Func<string> message);

        /// <summary>
        /// Runtime events such as initialization, startup and shutdown.
        /// The lazy overload Info(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Info(string message, params object[] formatting);

        /// <summary>
        /// Runtime events such as initialization, startup and shutdown.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(Func<string> message);

        /// <summary>
        /// Initializes the instance for the logger name
        /// </summary>
        /// <param name="loggerName">Name of the logger</param>
        void InitializeFor(string loggerName);

        /// <summary>
        /// Unexpected results or exceptions where the application is able to continue executing on its on.
        /// The lazy overload Warn(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Warn(string message, params object[] formatting);

        /// <summary>
        /// Unexpected results or exceptions where the application is able to continue executing on its on.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(Func<string> message);
    }

    /// <summary>
    /// Ensures a default constructor for the logger type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILog<T> where T : new()
    {
    }
}
