using System.Collections.Generic;

namespace Castle.Domain
{
    /// <summary>
    /// An arbitrary group of projects
    /// </summary>
    public class Repository : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        public Repository()
        {
            this.Projects = new List<Project>();
        }

        /// <summary>
        /// Gets or sets the key for use in url schemas
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new System.ArgumentNullException("value", "Repository name can not be null or blank");

                _name = value;
                this.Key = _name.ToLowerInvariant().Replace(' ', '-');
            }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>The projects.</value>
        public IList<Project> Projects { get; private set; }

        private string _name;
    }
}