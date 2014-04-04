using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Castle.Web.Models.Source
{
    public class SourceViewModel
    {
        public IEnumerable<Domain.Repository> RepositoryList { get; set; }
    }
}