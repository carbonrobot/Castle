using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Domain;

namespace Castle.Services.Providers
{
    public interface ISourceProvider
    {
        IEnumerable<SourceLogEntry> GetHistory(string path, int days);
    }
}
