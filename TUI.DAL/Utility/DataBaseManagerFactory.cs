using TUI.Utility.Interface;
namespace TUI.Utility
{
    /// <summary>
    /// Encapsulates function  required for creating concrete implementation fo IDatabaseManager
    /// </summary>
    public static class DatabaseManagerFactory
    {
        /// <summary>
        /// Creates and returns concrete implementation of IDatabaseManager
        /// </summary>
        /// <returns></returns>
        public static IDatabaseManager GetDatabaseManager(string connectionName)
        {
            IDatabaseManager dataBaseManager = new DatabaseManager(connectionName);
            return dataBaseManager;
        }
    }
}