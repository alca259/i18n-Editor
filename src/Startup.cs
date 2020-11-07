using i18nEditor.Views;
using Microsoft.Extensions.DependencyInjection;

namespace i18nEditor
{
    public static class Startup
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection RegisterFactories(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection RegisterNavigation(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection RegisterWindowAndPages(this IServiceCollection services)
        {
            services.AddScoped<MainWindow>();
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
