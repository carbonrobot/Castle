using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Creates a new repository with the given name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        /// <returns>ServiceResponse&lt;Repository&gt;.</returns>
        public ServiceResponse<Repository> CreateRepository(string name, string path)
        {
            Func<Repository> func = () =>
            {
                var repository = new Repository()
                {
                    Name = name,
                    Path = path
                };
                return this.DataContext.Save(repository);
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Deletes a project by its key
        /// </summary>
        public ServiceResponse DeleteProject(string key)
        {
            Action func = () =>
            {
                var repository = this.DataContext.AsQueryable<Project>().Single(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
                this.DataContext.Delete(repository);
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Deletes a repository by its key
        /// </summary>
        public ServiceResponse DeleteRepository(string key)
        {
            Action func = () =>
            {
                var repository = this.DataContext.AsQueryable<Repository>().Single(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
                this.DataContext.Delete(repository);
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Gets a project by its key
        /// </summary>
        /// <returns>The project</returns>
        public ServiceResponse<Project> GetProject(string key)
        {
            Func<Project> func = () =>
            {
                var query = (from p in this.DataContext.AsQueryable<Project>(x => x.Repository)
                             where p.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)
                             select p);

                return query.Single();
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Creates a new project for a repository
        /// </summary>
        /// <returns>The project</returns>
        public ServiceResponse<Project> CreateProject(string repositoryKey, string projectName, string path)
        {
            Func<Project> func = () =>
            {
                var repository = this.DataContext.AsQueryable<Repository>().Single(x => x.Key.Equals(repositoryKey, StringComparison.InvariantCultureIgnoreCase));
                var project = new Project()
                {
                    RepositoryId = repository.Id,
                    Name = projectName,
                    Path = path
                };
                return this.DataContext.Save(project);
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Gets the recent change history for project.
        /// </summary>
        /// <param name="key">The Project key.</param>
        /// <param name="days">The number of days to include.</param>
        /// <returns>ServiceResponse&lt;IEnumerable&lt;SourceLogEntry&gt;&gt;.</returns>
        public ServiceResponse<IEnumerable<SourceLogEntry>> GetProjectHistory(string key, int days)
        {
            Func<IEnumerable<SourceLogEntry>> func = () =>
            {
                var project = this.DataContext.AsQueryable<Project>().Single(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
                var history = this.SourceProvider.GetHistory(project.Path, days);
                return history.OrderByDescending(x => x.Time);
            };
            return this.Execute(func);
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
        /// Gets the recent change history for this repository.
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

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>ServiceResponse&lt;Project&gt;.</returns>
        public ServiceResponse<Project> UpdateProject(Project project)
        {
            Func<Project> func = () =>
            {
                return this.DataContext.Save(project);
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Updates the repository.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <returns>ServiceResponse&lt;Repository&gt;.</returns>
        public ServiceResponse<Repository> UpdateRepository(Repository repository)
        {
            Func<Repository> func = () =>
            {
                return this.DataContext.Save(repository);
            };
            return this.Execute(func);
        }

        private readonly ISourceProvider SourceProvider;
    }
}