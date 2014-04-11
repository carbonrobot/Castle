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

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceService"/> class.
        /// </summary>
        /// <param name="sourceProvider">The source provider.</param>
        public SourceService(ISourceProvider sourceProvider)
        {
            this.SourceProvider = sourceProvider;
        }

        /// <summary>
        /// Gets the file information.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>ServiceResponse&lt;IEnumerable&lt;SourceFileInfo&gt;&gt;.</returns>
        public ServiceResponse<IEnumerable<SourceFileInfo>> GetFileInfo(string path)
        {
            Func<IEnumerable<SourceFileInfo>> func = () =>
            {
                var fileInfo = this.SourceProvider.GetFiles(path);
                return fileInfo;
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>ServiceResponse&lt;System.String&gt;.</returns>
        public ServiceResponse<string> GetFileContent(string path)
        {
            Func<string> func = () =>
            {
                var content = this.SourceProvider.GetFileContent(path);
                return content;
            };
            return this.Execute(func);
        }
    }
}
