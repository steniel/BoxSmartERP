using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

namespace BoxSmart_ERP
{
    // Add in Program.cs or similar startup location

    internal static class Program
    {
        
        public static IConfiguration Configuration { get; private set; }
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            // Build configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Use Directory.GetCurrentDirectory() instead of AppContext.BaseDirectory
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                .Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);   
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(Configuration));
            
        }
    }
        
}