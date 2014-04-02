using System;
using System.Linq;
using System.Collections.Generic;
using Castle.Domain;
using Castle.Services.Providers;

namespace Castle.Services
{
    public class ProjectService : DataService
    {
        public ProjectService(IDataContext dataContext, ISourceProvider sourceProvider)
            : base(dataContext)
        {
            this.SourceProvider = sourceProvider;
        }

        /// <summary>
        /// Gets a list of all projects
        /// </summary>
        /// <returns>An list of all projects sorted by group name and project name</returns>
        public ServiceResponse<IEnumerable<Project>> GetProjects()
        {
            Func<IEnumerable<Project>> func = () =>
            {
                var query = (from p in this.DataContext.AsQueryable<Project>(p => p.Team)
                             orderby p.Team.Name, p.Name
                             select p);

                return query.ToList();
            };
            return this.Execute(func);
        }

        private readonly ISourceProvider SourceProvider;
    }
}