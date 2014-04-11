using System.Collections.Generic;
using Castle.Domain;

/// <summary>
/// The Providers namespace.
/// </summary>
namespace Castle.Services.Providers
{
    public interface ISourceProvider
    {
        /// <summary>
        /// Gets the content of the file as a string.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        string GetFileContent(string path);

        /// <summary>
        /// Gets the file and directory information for the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>IEnumerable&lt;SourceFileInfo&gt;.</returns>
        IEnumerable<SourceFileInfo> GetFiles(string path);

        /// <summary>
        /// Gets the source commit history for the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="days">The number of days to include.</param>
        /// <returns>IEnumerable&lt;SourceLogEntry&gt;.</returns>
        IEnumerable<SourceLogEntry> GetHistory(string path, int days);
    }
}