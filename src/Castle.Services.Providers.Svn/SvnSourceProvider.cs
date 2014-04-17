using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Castle.Domain;
using SharpSvn;

namespace Castle.Services.Providers
{
    /// <summary>
    /// Class SvnSourceProvider.
    /// </summary>
    public class SvnSourceProvider : ISourceProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SvnSourceProvider"/> class.
        /// </summary>
        public SvnSourceProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SvnSourceProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SvnSourceProvider(SvnSourceProviderOptions options)
        {
            this.options = options;
        }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        public SvnSourceProviderOptions options { get; set; }

        /// <summary>
        /// Gets the content of the file as a string.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        public string GetFileContent(string path)
        {
            string content = string.Empty;
            using (var client = CreateSvnClient())
            {
                using (var stream = new MemoryStream())
                {
                    client.Write(SvnTarget.FromUri(CreateUri(path)), stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    using (var reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }
            return content;
        }

        /// <summary>
        /// Gets the file and directory information for the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>IEnumerable&lt;SourceFileInfo&gt;.</returns>
        public IEnumerable<SourceFileInfo> GetFiles(string path)
        {
            var files = new List<SourceFileInfo>();
            using (var client = CreateSvnClient())
            {
                Collection<SvnListEventArgs> contents;
                if (client.GetList(CreateUri(path), out contents))
                {
                    // TODO: support branching
                    files.AddRange(MapSourceFileInfo(contents));

                    /* There are 2 different directory structures supported
                     *
                     * Branching by project
                     * https://server/svn/{repository}/{projectRoot}/{trunk|branches}/
                     *
                     * ex: https://server/svn/interactive/quotebuilder/trunk
                     * ex: https://server/svn/interactive/quotebuilder/branches/branch_2/
                     *
                     * Branching by repository
                     * https://server/svn/{trunk|branches}/{repository}/{projectRoot}/
                     *
                     * ex: https://server/svn/interactive/trunk/quotebuilder/
                     * ex: https://server/svn/interactive/branches/branch_2/quotebuilder
                     *
                     */

                    //// determine repository path type by examining root contents
                    //if (contents.SingleOrDefault(x => x.Path == TRUNKFOLDER) == null)
                    //{
                    //    // branching by repository

                    //}
                    //else
                    //{
                    //    // branching by project
                    //    if (branch == TRUNKFOLDER || string.IsNullOrEmpty(branch))
                    //    {
                    //        // open trunk and read contents
                    //        if (client.GetList(CreateUri(root + "/" + TRUNKFOLDER + "/" + path), out contents))
                    //        {
                    //            files.AddRange(MapSourceFileInfo(contents));
                    //        }
                    //    }
                    //    else
                    //    {
                    //        // open specified branch
                    //        if (client.GetList(CreateUri(root + "/" + BRANCHFOLDER + "/" + branch + "/" + path), out contents))
                    //        {
                    //            files.AddRange(MapSourceFileInfo(contents));
                    //        }
                    //    }
                    //}
                }
            }
            return files.OrderBy(x => x.Kind).ThenBy(x => x.Name);
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="days">The days.</param>
        /// <returns>IEnumerable&lt;SourceLogEntry&gt;.</returns>
        public IEnumerable<SourceLogEntry> GetHistory(string path, int days)
        {
            var list = new List<SourceLogEntry>();

            using (var client = CreateSvnClient())
            {
                var start = DateTime.Now.AddDays(days * -1);
                var end = DateTime.Now.AddDays(1);
                var range = new SvnRevisionRange(new SvnRevision(start), new SvnRevision(end));

                Collection<SvnLogEventArgs> logitems;
                client.GetLog(CreateUri(path), new SvnLogArgs(range), out logitems);

                foreach (var logEvent in logitems)
                {
                    list.Add(new SourceLogEntry()
                    {
                        Author = logEvent.Author,
                        LogMessage = logEvent.LogMessage,
                        Revision = logEvent.Revision,
                        Time = logEvent.Time,
                        ChangedPathCount = logEvent.ChangedPaths.Count,
                        Branch = TryGetBranchName(logEvent)
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// Creates the SVN client.
        /// </summary>
        /// <returns>SvnClient.</returns>
        private SvnClient CreateSvnClient()
        {
            var client = new SharpSvn.SvnClient();
            client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(options.UserName, options.Password);
            return client;
        }

        /// <summary>
        /// Creates the URI.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Uri.</returns>
        private Uri CreateUri(string path)
        {
            var baseUri = new Uri(this.options.Server, UriKind.Absolute);
            return new Uri(baseUri, path);
        }

        /// <summary>
        /// Strips the server host information from a file path
        /// </summary>
        private string GetServerRelativePath(Uri fileUri)
        {
            return Regex.Replace(fileUri.ToString(), this.options.Server, "", RegexOptions.IgnoreCase);
        }
        
        /// <summary>
        /// Files the kind of the entry kind from node.
        /// </summary>
        /// <param name="nodeKind">Kind of the node.</param>
        /// <returns>FileEntryKind.</returns>
        private FileEntryKind FileEntryKindFromNodeKind(SvnNodeKind nodeKind)
        {
            switch (nodeKind)
            {
                case SvnNodeKind.Directory: return FileEntryKind.Directory;
                case SvnNodeKind.File: return FileEntryKind.File;
                default: return FileEntryKind.Unknown;
            }
        }

        /// <summary>
        /// Maps the source file information.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns>IEnumerable&lt;SourceFileInfo&gt;.</returns>
        private IEnumerable<SourceFileInfo> MapSourceFileInfo(Collection<SvnListEventArgs> contents)
        {
            if (contents == null)
            {
                yield break;
            }

            foreach (var item in contents)
            {
                if (item.Path != "")
                {
                    yield return new SourceFileInfo()
                    {
                        Name = item.Name,
                        Path = GetServerRelativePath(item.Uri),
                        Kind = FileEntryKindFromNodeKind(item.Entry.NodeKind),
                        ChangeTime = item.Entry.Time,
                        Author = item.Entry.Author,
                        Change = ""
                    };
                }
            }
        }

        /// <summary>
        /// Tries the name of the get branch.
        /// </summary>
        /// <param name="logEvent">The <see cref="SvnLogEventArgs"/> instance containing the event data.</param>
        /// <returns>System.String.</returns>
        private string TryGetBranchName(SvnLogEventArgs logEvent)
        {
            var branch = "trunk";
            if (logEvent.ChangedPaths != null && logEvent.ChangedPaths.Count > 0)
            {
                if (logEvent.ChangedPaths[0].Path.Contains("branches"))
                {
                    // grab the branch name from the first name after "branches"
                    var folders = logEvent.ChangedPaths[0].Path.Split('/').ToList();
                    if (folders.IndexOf("branches") + 1 < folders.Count)
                    {
                        branch = folders[folders.IndexOf("branches") + 1];
                    }
                }
            }
            return branch;
        }

        private const string BRANCHFOLDER = "branches";
        private const string TRUNKFOLDER = "trunk";
    }
}