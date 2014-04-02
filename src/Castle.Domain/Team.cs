using System.Collections.Generic;

namespace Castle.Domain
{
    /// <summary>
    /// An arbitrary group of projects
    /// </summary>
    public class Team : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Team"/> class.
        /// </summary>
        public Team()
        {
            this.Projects = new List<Project>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>The projects.</value>
        public IList<Project> Projects { get; private set; }
    }
}