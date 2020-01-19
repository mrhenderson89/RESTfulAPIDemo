using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntAlbelliTechnical
{
    public class SimpleInjectorDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="container"/> is a null
        /// reference.</exception>
        public SimpleInjectorDependencyResolver(Container container)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            this.Container = container;
        }

        /// <summary>Gets the container.</summary>
        /// <value>The <see cref="Container"/>.</value>
        public Container Container { get; }

        private IServiceProvider ServiceProvider => this.Container;

        /// <summary>Resolves singly registered services that support arbitrary object creation.</summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>The requested service or object.</returns>
        public object GetService(Type serviceType)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            // By calling GetInstance instead of GetService when resolving a controller, we prevent the
            // container from returning null when the controller isn't registered explicitly and can't be
            // created because of an configuration error. GetInstance will throw a descriptive exception
            // instead. Not doing this will cause MVC to throw a non-descriptive "Make sure that the
            // controller has a parameterless public constructor" exception.
            if (!serviceType.IsAbstract && typeof(IController).IsAssignableFrom(serviceType))
            {
                return this.Container.GetInstance(serviceType);
            }

            return this.ServiceProvider.GetService(serviceType);
        }

        /// <summary>Resolves multiply registered services.</summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>The requested services.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            Type collectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);

            // The IDependencyResolver doesn't state what is expected from the returned enumerable. We,
            // therefore, simply assume it is correct to return a stream.
            var services = (IEnumerable<object>)this.ServiceProvider.GetService(collectionType);

            // NOTE: The contract of IDependencyResolver isn't very clear, but MVC will break when null
            // is returned.
            return services ?? Enumerable.Empty<object>();
        }
    }
}