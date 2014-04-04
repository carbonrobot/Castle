namespace Castle.Domain
{
    /// <summary>
    /// A project that is linked to a location in source control
    /// </summary>
    public class Project : UriSafeEntity
    {
        /// <summary>
        /// Gets or sets the source control path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the Repository that owns this project.
        /// </summary>
        /// <value>The Repository.</value>
        public Repository Repository { get; set; }

        /// <summary>
        /// Gets or sets the Repository id.
        /// </summary>
        public int RepositoryId { get; set; }

    }
}