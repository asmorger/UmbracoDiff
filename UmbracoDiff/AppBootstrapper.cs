using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using UmbracoDiff.ViewModels;
using UmbracoDiff.ViewModels.Screens;

namespace UmbracoDiff
{
    public class AppBootstrapper : BootstrapperBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AppBootstrapper" /> class.
        /// </summary>
        public AppBootstrapper()
            : base(true)
        {
            Initialize();
        }

        /// <summary>
        ///     Gets the Autofac container.
        /// </summary>
        /// <value>The Autofac container.</value>
        public static IContainer Container { get; set; }

        /// <summary>
        ///     Override to configure the framework and setup your IoC container.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        ///     CreateWindowManager
        ///     or
        ///     CreateEventAggregator
        /// </exception>
        protected override void Configure()
        {
            if (Container == null)
            {
                var builder = new ContainerBuilder();

                AutofacConfiguration.Register(builder);

                Container = builder.Build();
            }
        }

        /// <summary>
        ///     Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        /// <exception cref="System.Exception">
        ///     This exception is only thrown if the Container could not locate an instance of the
        ///     key.
        /// </exception>
        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                object obj;
                if (Container.TryResolve(service, out obj))
                {
                    return obj;
                }
            }
            else
            {
                object obj;
                if (Container.TryResolveNamed(key, service, out obj))
                {
                    return obj;
                }
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? service.Name));
        }

        /// <summary>
        ///     Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>The located services.</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.Resolve(typeof (IEnumerable<>).MakeGenericType(new[] {service})) as IEnumerable<object>;
        }

        /// <summary>
        ///     Override this to add custom behavior to execute after the application starts.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<AppViewModel>();
        }
    }
}