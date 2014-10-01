using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace TUI.Framework
{
    /// <summary>
    /// dependency injection container
    /// </summary>y
    public static class UnityManager
    {
        /// <summary>
        /// Private variable of UnityContainer object
        /// </summary>
        private static IUnityContainer _container;

        /// <summary>
        /// Gets the UnityContainer object
        /// </summary>
        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    using (IUnityContainer newInstance = new UnityContainer())
                    {
                        UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                        section.Configure(newInstance);
                        _container = newInstance;
                    }
                }
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        /// <summary>
        /// Resolves or Creates a new instance of T
        /// </summary>
        /// <typeparam name="T">Generic type T</typeparam>
        /// <param name="service">service name as string</param>
        /// <returns>Generic typeparam</returns>
        public static T Resolve<T>(string service)
        {
            return Container.Resolve<T>(service);
        }

        /// <summary>
        /// Resolves or Creates a new instance of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}