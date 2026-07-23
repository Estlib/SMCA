using Microsoft.Extensions.Configuration;
using SMCA.Database;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SMCA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            #if DEBUG
            Controllers.ConsoleController.Open();
            #endif

            Console.WriteLine("SMCA starting...");
            try
            {
                using DBConfiguration database = new DBConfiguration();

                database.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                string message = $"Cannot create DB, see exact error below:\n\n{ex.Message}";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButton.OK);
            }


        }
    }

}
