namespace Castle.Domain
{
    /// <summary>
    /// A project that is linked to a location in source control
    /// </summary>
    public class Project : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the source control path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the team that owns this project.
        /// </summary>
        /// <value>The team.</value>
        public Team Team { get; set; }

        /// <summary>
        /// Gets or sets the team id.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets the full group name plus project name
        /// </summary>
        public string FullName
        {
            get
            {
                if (this.Team != null)
                {
                    return string.Format("{0} / {1}", this.Team.Name, this.Name);
                }
                return this.Name;
            }
        }
    }
}