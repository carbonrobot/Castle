using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Domain;
using SharpSvn;

namespace Castle.Services
{
    public class SourceProvider
    {
        public SourceProvider(SourceProviderOptions options)
        {
            this.options = options;
        }

        public IEnumerable<SourceLogEntry> GetRecentHistory()
        {
            var list = new List<SourceLogEntry>();

            using (var client = CreateSvnClient())
            {
                var start = DateTime.Now.AddDays(-7);
                var end = DateTime.Now.AddDays(1);
                var range = new SvnRevisionRange(new SvnRevision(start), new SvnRevision(end));
                var baseUri = "https://subversionprod/svn/";

                // TODO: try to get the repos dynamo
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
                                ChangedPathCount = logEvent.ChangedPaths.Count
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

        private SvnClient CreateSvnClient()
        {
            var client = new SharpSvn.SvnClient();
            client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(options.UserName, options.Password);
            return client;
        }

        public SourceProviderOptions options { get; set; }
    }
}
