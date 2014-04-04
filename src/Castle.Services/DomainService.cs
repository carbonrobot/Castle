using System;
using System.Linq;
using System.Collections.Generic;
using Castle.Domain;
using Castle.Services.Providers;

namespace Castle.Services
{
    public class DomainService : DataService
    {
        public DomainService(IDataContext dataContext, ISourceProvider sourceProvider)
            : base(dataContext)
        {
            this.SourceProvider = sourceProvider;
        }

        /// <summary>
        /// Gets a list of all projects for a given repository
        /// </summary>
        /// <param name="repositoryId">The repository id</param>
        /// <returns>A list of all projects sorted by name</returns>
        public ServiceResponse<IEnumerable<Project>> GetProjects(int repositoryId)
        {
            Func<IEnumerable<Project>> func = () =>
            {
                var query = (from p in this.DataContext.AsQueryable<Project>(p => p.Repository)
                             where p.RepositoryId == repositoryId
                             orderby p.Name
                             select p);

                return query.ToList();
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Gets a list of all repositories
        /// </summary>
        /// <returns>A list of all repositories sorted by name</returns>
        public ServiceResponse<IEnumerable<Repository>> GetRepositories()
        {
            Func<IEnumerable<Repository>> func = () =>
            {
                var query = (from p in this.DataContext.AsQueryable<Repository>()
                             select p);

                return query.ToList();
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Gets a repository by its key
        /// </summary>
        /// <returns>The repository and its associated projects</returns>
        public ServiceResponse<Repository> GetRepository(string key)
        {
            Func<Repository> func = () =>
            {
                var query = (from p in this.DataContext.AsQueryable<Repository>(x => x.Projects)
                             where p.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)
                             select p);

                return query.Single();
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Gets the recent change history for all configured repositories.
        /// </summary>
        /// <param name="key">The repository key.</param>
        /// <param name="days">The number of days to include.</param>
        /// <returns>ServiceResponse&lt;IEnumerable&lt;SourceLogEntry&gt;&gt;.</returns>
        public ServiceResponse<IEnumerable<SourceLogEntry>> GetRepositoryHistory(string key, int days)
        {
            Func<IEnumerable<SourceLogEntry>> func = () =>
            {
                var repository = this.DataContext.AsQueryable<Repository>().Single(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
                var history = this.SourceProvider.GetHistory(repository.Path, days);
                return history.OrderByDescending(x => x.Time);
            };
            return this.Execute(func);
        }

        private readonly ISourceProvider SourceProvider;
    }
}