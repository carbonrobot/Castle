using System.Collections.Generic;

namespace Castle.Domain
{
    /// <summary>
    /// An arbitrary group of projects
    /// </summary>
    public class Repository : UriSafeEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        public Repository()
        {
            this.Projects = new List<Project>();
        }

        /// <summary>
        /// Gets or sets the source control path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>The projects.</value>
        public IList<Project> Projects { get; private set; }
    }
}