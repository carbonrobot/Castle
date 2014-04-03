using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Castle.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.LoadConfiguration();

            // register svn settings, tmp
            container.RegisterInstance<Castle.Services.Providers.SvnSourceProviderOptions>(new Services.Providers.SvnSourceProviderOptions()
            {
                UserName = "castle",
                Password = "xcYCtqXn9Blo"
            });
        }
    }
}
