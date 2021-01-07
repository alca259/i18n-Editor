using i18nEditor.DTOs;
using i18nEditor.Services;
using i18nEditor.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace i18nEditor
{
    static class Program
    {
        private static IConfiguration Configuration { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider provider = services.BuildServiceProvider())
            {
                var mainForm = provider.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            services.AddSingleton(Configuration)
                .AddLogging(setup =>
                {
                    setup.AddConsole().SetMinimumLevel(LogLevel.Debug);
                })
                .Configure<EditorConfigDto>(Configuration.GetSection("EditorConfig"))
                .AddSingleton<IFileService, FileService>()
                .AddSingleton<MainForm>();
        }
    }
}
