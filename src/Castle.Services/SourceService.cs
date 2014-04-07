using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Domain;
using Castle.Services.Providers;

namespace Castle.Services
{
    public class SourceService : ServiceBase
    {
        private readonly ISourceProvider SourceProvider;

        public SourceService(ISourceProvider sourceProvider)
        {
            this.SourceProvider = sourceProvider;
        }

        public ServiceResponse<IEnumerable<SourceFileInfo>> GetFileInfo(string path)
        {
            Func<IEnumerable<SourceFileInfo>> func = () =>
            {
                var fileInfo = this.SourceProvider.GetFiles(path);
                return fileInfo;
            };
            return this.Execute(func);
        }
    }
}
