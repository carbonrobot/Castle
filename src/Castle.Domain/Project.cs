namespace Castle.Domain
{
    /// <summary>
    /// A project that is linked to a location in source control
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the source control path
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }
    }
}