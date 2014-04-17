using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castle.Domain
{
    public class SourceFileInfo
    {
        public FileEntryKind Kind { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime ChangeTime { get; set; }
        public string ChangeRelativeTime
        {
            get
            {
                return this.ChangeTime.RelativeDate();
            }
        }
        public string Author { get; set; }
    }
}
