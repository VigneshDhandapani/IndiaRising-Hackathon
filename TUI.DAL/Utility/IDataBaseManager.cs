/**************************************************************************************
**************************************************************************************/

using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace TUI.Utility.Interface
{
    /// <summary>
    /// Encapsulates available functionality for Data Service Layer for communicating with any give database
    /// </summary>
    public interface IDatabaseManager : IDisposable
    {
        Database DatabaseObject { get; }

        /// <summary>
        /// Executes query against the connection and returns the number of rows affected
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <param name="isLockSpecified"></param>
        /// <returns>Number of rows affected</returns>
        int ExecuteSqlNonQuery(DbCommand command, bool isLockSpecified);

        /// <summary>
        /// Executes query and returns the first column of the first row in the result set returned by the query
        /// </summary>
        /// <typeparam name="T">Data type of result value</typeparam>
        /// <param name="command">Command to execute against data source</param>
        /// <returns>First column of the first row in the result set</returns>
        T GetScalar<T>(DbCommand command);

        /// <summary>
        /// Provides forward only stream of rows for reading from data source
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <returns></returns>
        IDataReader GetDataReader(DbCommand command);

        /// <summary>
        /// Executes the command and returns the results as a DataSet.
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <returns>Results in Dataset</returns>
        DataSet GetDataSet(DbCommand command);

        /// <summary>
        /// Creates DbParameter for the given DbCommand
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <param name="name">Command parameter name</param>
        /// <param name="value">Command parameter value</param>
        /// <returns>DbParameter</returns>
        DbParameter GetDatabaseParameter(DbCommand command, string name, object value);

        /// <summary>
        /// Creates DbParameter for the given DbCommand
        /// </summary>
        /// <param name="command">Command to execute against data source</param>
        /// <param name="name">Command parameter name</param>
        /// <param name="value">Command parameter value</param>
        /// <param name="parameterDirection">Command parameter direction</param>
        /// <returns>DbParameter</returns>
        DbParameter GetDatabaseParameter(DbCommand command, string name, object value, ParameterDirection parameterDirection, object parameterType);
    }
}