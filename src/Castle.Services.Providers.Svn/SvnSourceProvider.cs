using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Domain;
using SharpSvn;

namespace Castle.Services.Providers
{
    public class SvnSourceProvider : ISourceProvider
    {
        public SvnSourceProvider() { }
        public SvnSourceProvider(SvnSourceProviderOptions options)
        {
            this.options = options;
        }

        public IEnumerable<SourceLogEntry> GetRecentHistory(int days)
        {
            var list = new List<SourceLogEntry>();

            using (var client = CreateSvnClient())
            {
                var start = DateTime.Now.AddDays(days * -1);
                var end = DateTime.Now.AddDays(1);
                var range = new SvnRevisionRange(new SvnRevision(start), new SvnRevision(end));
                var baseUri = "https://subversionprod/svn/";

                // TODO: try to get the repos dynamo
                // TODO: recent history should be all repos combined from date range
                var repos = new string[] {
                    "AHPTest",
                    "ART",
                    "Archive",
                    "BusinessIntelligence",
                    "Config",
                    "Reporting",
                    "TESTING_DOCS",
                    "admin",
                    "architecture",
                    "c",
                    "claims",
                    "data",
                    "document_management",
                    "externally_hostedx",
                    "finance",
                    "interactive",
                    "java",
                    "migration",
                    "sandbox",
                    "scripts",
                    "src",
                    "travel"
                };

                for (int i = 0; i < repos.Length; i++)
                {
                    try
                    {
                        Collection<SvnLogEventArgs> logitems;
                        client.GetLog(new Uri(baseUri + repos[i]), new SvnLogArgs(range), out logitems);

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
                    catch (Exception)
                    {
                        // TODO: log warning and ignore
                    }
                }
            };

            return list;
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
                    branch = folders[folders.IndexOf("branches") + 1];
                }
            }
            return branch;
        }

        private SvnClient CreateSvnClient()
        {
            var client = new SharpSvn.SvnClient();
            client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(options.UserName, options.Password);
            return client;
        }

        public SvnSourceProviderOptions options { get; set; }
    }
}
