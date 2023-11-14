using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Data.OleDb;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace StockReceipts
{
    [Serializable]  // Add this attribute
    public class AppConfig
    {
        public int PriceUpdateMode { get; set; }
        public int SalesPriceUpdateMode { get; set; }

        public void SaveConfiguration(AppConfig config, string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, config);
            }
        }

        public AppConfig LoadConfiguration(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                return (AppConfig)formatter.Deserialize(stream);
            }
        }
    }


    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static AppConfig AppConfigInstance { get; private set; } // Static instance of AppConfig
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoadConfiguration();
            using (LoginForm loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    // Exit the application if the login wasn't successful
                    return;
                }
            }

            // Proceed to main application
            
            MAINMENUform mainMenuForm = new MAINMENUform();
            Application.Run(mainMenuForm);

        }
        private static void LoadConfiguration()
        {
            string configFilePath = Path.Combine(Application.StartupPath, "appConfig.bin");

            AppConfigInstance = File.Exists(configFilePath)
                                ? new AppConfig().LoadConfiguration(configFilePath)
                                : new AppConfig { PriceUpdateMode = 2, SalesPriceUpdateMode = 2 };
        }
        public static string LoggedInPermissions { get; set; }
        public static string LoggedInName { get; set; }
        public static string LoggedInLastName1 { get; set; }
        public static string LoggedInLastName2 { get; set; }
        public static string LoggedInBranchNumber { get; set; }
        public static string LoggedInUsername { get; set; }
        public static decimal LoggedInTaxRate { get; set; }
        public static string DatabaseConnectionString { get; set; }
        public static string PermissionsString { get; set; }


        public static string LoadEncryptedConnectionString()
        {
            try
            {
                byte[] encryptedData = File.ReadAllBytes("config.dat");
                byte[] decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (CryptographicException)
            {
                throw new Exception("No se puede leer el archivo de configuración para acceso a la base de datos, por favor reconfigure.");
            }
        }

        public static class SimpleLogger
        {
            private static readonly string logFilePath = @"C:\MyLogs\appLog.txt";

            public static void Log(string message)
            {
                try
                {
                    var dir = Path.GetDirectoryName(logFilePath);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}\r\n");
                }
                catch (Exception ex)
                {
                    LogException(ex);                }
            }

            public static void LogException(Exception ex)
            {
                Log($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
            }
        }

        public static void StoreBranchNumberAndTaxRate()
        {
            LoggedInBranchNumber = GetBranchNumber();
            LoggedInTaxRate = GetTaxRate();
        }

        public static string GetBranchNumber()
        {
            // Conexión a la base de datos para obtener el valor de Aguila.
            using (var connection = new OleDbConnection(DatabaseConnectionString))
            {
                connection.Open();
                using (var cmd = new OleDbCommand("SELECT Aguila FROM CatAguilas", connection))
                {
                    LoggedInBranchNumber = cmd.ExecuteScalar()?.ToString() ?? "N/A";
                    return LoggedInBranchNumber;
                }
            }
        }

        public static decimal GetTaxRate()
        {

            // Conexión a la base de datos para obtener el valor del impuesto.
            string tableName = ConstructTableName("Param");
            using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(DatabaseConnectionString))
            {
                connection.Open();
                using (var cmd = new OleDbCommand($"SELECT TOP 1 IVA FROM {tableName}", connection))
                {
                    var result = cmd.ExecuteScalar();
                    if (decimal.TryParse(result?.ToString(), out decimal taxRate))
                    {
                        LoggedInTaxRate = taxRate / 100M;
                        return LoggedInTaxRate;
                    }
                }
            }
            // Retornar tasa de impuesto predeterminada si no se encuentra en la base de datos.
            return 0.16M;
        }
        public static bool TestDatabaseConnection(string connectionString)
        {
            try
            {
                using (var connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        // Construir el nombre de la tabla.
        public static string ConstructTableName(string baseTableName)
        {
            return $"{LoggedInBranchNumber}_{baseTableName}";
        }

        // Define the mapping between buttons and their indices
        public static readonly Dictionary<string, int> ButtonPermissionMap = new Dictionary<string, int>
    {
        { "MAINMENU_button_salesconfig", 0 },
        { "MAINMENU_button_userconfig", 1 },
        { "MAINMENU_salescancel", 2 },
        { "MAINMENU_BUTTON_salesregister", 3 },
        { "MAINMENU_salesreprint", 4 }
    };


        public static List<bool> GetPermissionsList()
        {
            if (string.IsNullOrEmpty(LoggedInPermissions))
                return new List<bool>();

            return LoggedInPermissions.Split(',').Select(p => p.Trim() == "1").ToList();
        }

        public static bool HasPermission(string permissionName)
        {
            if (string.IsNullOrEmpty(LoggedInPermissions))
                return false;

            var permissions = GetPermissionsList();
            if (ButtonPermissionMap.TryGetValue(permissionName, out int index))
            {
                return index < permissions.Count && permissions[index];
            }
            return false;
        }




    }
    public static class DataGridViewExtensions
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }


}