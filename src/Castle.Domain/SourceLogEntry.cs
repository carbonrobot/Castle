using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castle.Domain
{
    public class SourceLogEntry
    {
        public int ChangedPathCount { get; set; }
        public string Author { get; set; }
        public string LogMessage { get; set; }
        public long Revision { get; set; }
        public DateTime Time { get; set; }
        public string Branch { get; set; }

        public string RelativeTime
        {
            get
            {
                return this.Time.RelativeDate();
            }
        }
        
    }
}
