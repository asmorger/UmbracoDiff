using System.ComponentModel;
using System.Linq;
using Autofac;
using Caliburn.Micro;

namespace UmbracoDiff
{
    public static class AutofacConfiguration
    {
        /// <summary>
        ///     Registers the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void Register(ContainerBuilder builder)
        {
            RegisterModules(builder);
            RegisterViewModels(builder);
            RegisterViews(builder);
            RegisterServices(builder);
        }

        /// <summary>
        ///     Registers the view models.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(type => type.Name.EndsWith("ViewModel"))
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace) && type.Namespace.Contains("ViewModels"))
                .Where(type => type.GetInterface(typeof (INotifyPropertyChanged).Name) != null)
                .AsSelf()
                .InstancePerDependency();
        }

        /// <summary>
        ///     Registers the views.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(type => type.Name.EndsWith("View"))
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace) && type.Namespace.Contains("Views"))
                .AsSelf()
                .InstancePerDependency();
        }

        /// <summary>
        ///     Registers the modules.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void RegisterModules(ContainerBuilder builder)
        {
        }

        /// <summary>
        ///     Registers the services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
        }
    }
}