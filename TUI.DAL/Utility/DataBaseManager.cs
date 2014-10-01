/**************************************************************************************
**************************************************************************************/

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using TUI.Utility.Interface;

namespace TUI.Utility
{
    /// <summary>
    ///  Represents  Database methods to do the DB execution.
    /// </summary>
    public class DatabaseManager : IDatabaseManager
    {
        private Database _database;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="database"></param>
        public DatabaseManager(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseManager()
        {
            using (var databaseProviderFactory = new DatabaseProviderFactory())
            {
                _database = databaseProviderFactory.CreateDefault();
            }
        }

        /// <summary>
        /// Constructor with connection name
        /// </summary>
        public DatabaseManager(string connectionName)
        {
            using (var databaseProviderFactory = new DatabaseProviderFactory())
            {
                _database = databaseProviderFactory.Create(connectionName);
            }
        }

        /// <summary>
        /// return database object
        /// </summary>
        public Database DatabaseObject
        {
            get { return _database; }
        }

        /// <summary>
        /// Disposing the database instance
        /// </summary>
        private void Cleanup()
        {
            if (_database != null)
            {
                _database = null;
            }
        }

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            Cleanup();
        }

        /// <summary>
        /// The bulk of the clean-up code is implemented
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Cleanup();
            }
        }

        /// <summary>
        /// Executes query against the connection and returns the number of rows affected
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <param name="isLockSpecified"></param>
        /// <returns>Number of rows affected</returns>
        public int ExecuteSqlNonQuery(DbCommand command, bool isLockSpecified)
        {
            int returnValue = 0;
            returnValue = _database.ExecuteNonQuery(command);
            if (returnValue == 0 && isLockSpecified) { ErrorMessages(""); }
            if (returnValue == 0) { ErrorMessages("No rows Updated"); }
            return returnValue;
        }

        /// <summary>
        /// Executes query and returns the first column of the first row in the result set returned by the query
        /// </summary>
        /// <typeparam name="T">Data type of result value</typeparam>
        /// <param name="command">Command to execute against data source</param>
        /// <returns>First column of the first row in the result set</returns>
        public T GetScalar<T>(DbCommand command)
        {
            T returnValue;
            returnValue = (T)_database.ExecuteScalar(command);
            return returnValue;
        }

        /// <summary>
        /// Provides forward only stream of rows for reading from data source
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <returns></returns>
        public IDataReader GetDataReader(DbCommand command)
        {
            return _database.ExecuteReader(command);
        }

        /// <summary>
        /// Executes the command and returns the results as a DataSet.
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <returns>Results in Dataset</returns>
        public DataSet GetDataSet(DbCommand command)
        {
            return _database.ExecuteDataSet(command);
        }

        /// <summary>
        /// Creates DbParameter for the given DbCommand
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <param name="name">Command parameter name</param>
        /// <param name="value">Command parameter value</param>
        /// <returns>DbParameter</returns>
        public DbParameter GetDatabaseParameter(DbCommand command, string name, object value)
        {
            if (Equals(command, null))
            {
                throw new ArgumentNullException("command", "Argument cannot be null");
            }
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            if (value == null)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = value;
            return parameter;
        }

        /// <summary>
        /// Creates DbParameter for the given DbCommand
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <param name="name">Command parameter name</param>
        /// <param name="value">Command parameter value</param>
        /// <param name="parameterDirection">Command parameter direction</param>
        /// <param name="parameterType">Command parameter type</param>
        /// <returns>DbParameter</returns>
        public DbParameter GetDatabaseParameter(DbCommand command, string name, object value, ParameterDirection parameterDirection, object parameterType)
        {
            if (Equals(command, null))
            {
                throw new ArgumentNullException("command", "Argument cannot be null");
            }
            DbParameter parameter = command.CreateParameter();
            parameter.Direction = parameterDirection;
            parameter.ParameterName = name;
            if (value == null)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = value;
            if (parameterType != null)
            {
                parameter.DbType = (DbType)parameterType;
                if (parameterType.Equals(DbType.String))
                {
                    parameter.Size = 1000;
                }
            }
            return parameter;
        }

        private static void ErrorMessages(string errorMessage)
        {
            Collection<string> errorCodes = new Collection<string>();
            errorCodes.Add(errorMessage);
            throw new Exception(errorMessage);
        }
    }
}