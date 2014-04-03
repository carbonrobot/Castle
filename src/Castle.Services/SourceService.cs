using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Domain;
using Castle.Services.Providers;

namespace Castle.Services
{
    public class SourceService : DomainService
    {
        private readonly ISourceProvider SourceProvider;

        public SourceService(ISourceProvider sourceProvider)
        {
            this.SourceProvider = sourceProvider;
        }

        /// <summary>
        /// Gets the recent change history for all configured repositories.
        /// </summary>
        /// <param name="days">The number of days to include.</param>
        /// <returns>ServiceResponse&lt;IEnumerable&lt;SourceLogEntry&gt;&gt;.</returns>
        public ServiceResponse<IEnumerable<SourceLogEntry>> GetRecentHistory(int days)
        {
            Func<IEnumerable<SourceLogEntry>> func = () =>
            {
                var history = this.SourceProvider.GetRecentHistory(days);
                return history.OrderByDescending(x => x.Time);
            };
            return this.Execute(func);
        }
    }
}
