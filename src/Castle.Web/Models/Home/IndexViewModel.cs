﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Domain;

namespace Castle.Web.Models.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Repository> RepositoryList { get; set; }
    }
}