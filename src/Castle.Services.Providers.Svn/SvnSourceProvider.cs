using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Castle.Domain;
using SharpSvn;

namespace Castle.Services.Providers
{
    public class SvnSourceProvider : ISourceProvider
    {
        public SvnSourceProvider()
        {
        }

        public SvnSourceProvider(SvnSourceProviderOptions options)
        {
            this.options = options;
        }

        public SvnSourceProviderOptions options { get; set; }

        public IEnumerable<SourceFileInfo> GetFiles(string path, string branch = "")
        {
            var files = new List<SourceFileInfo>();
            using (var client = CreateSvnClient())
            {
                Collection<SvnListEventArgs> contents;
                if (client.GetList(CreateUri(path), out contents))
                {
                    foreach (var item in contents)
                    {
                        if (item.Path != "")
                        {
                            files.Add(new SourceFileInfo()
                            {
                                Name = item.Name,
                                Path = item.Uri.ToString(),
                                Kind = FileEntryKindFromNodeKind(item.Entry.NodeKind),
                                ChangeTime = item.Entry.Time,
                                Author = item.Entry.Author,
                                Change = ""
                            });
                        }
                    }
                }
            }
            return files.OrderBy(x => x.Kind).ThenBy(x => x.Name);
        }

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

        private SvnClient CreateSvnClient()
        {
            var client = new SharpSvn.SvnClient();
            client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(options.UserName, options.Password);
            return client;
        }

        private FileEntryKind FileEntryKindFromNodeKind(SvnNodeKind nodeKind)
        {
            switch (nodeKind)
            {
                case SvnNodeKind.Directory: return FileEntryKind.Directory;
                case SvnNodeKind.File: return FileEntryKind.File;
                default: return FileEntryKind.Unknown;
            }
        }

        private Uri CreateUri(string path)
        {
            var baseUri = new Uri(this.options.Server, UriKind.Absolute);
            return new Uri(baseUri, path);
        }

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
    }
}