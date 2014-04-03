using Castle.Services.Providers;

namespace Castle.Services
{
    public abstract class DataService : DomainService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataService"/> class.
        /// </summary>
        /// <param name="context">The repository data context.</param>
        protected DataService(IDataContext context)
        {
            this.DataContext = context;
        }

        protected IDataContext DataContext;
    }
}