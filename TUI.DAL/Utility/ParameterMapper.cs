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
    /// Implementation of Parametermapper
    /// </summary>
    public class ParameterMapper : IParameterMapper
    {
        private Collection<SelectParameter> parameters = new Collection<SelectParameter>();
        private IDatabaseManager dbManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbManager"></param>
        /// <param name="parameters"></param>
        public ParameterMapper(IDatabaseManager dbManager, Collection<SelectParameter> parameters)
        {
            this.parameters = parameters;
            this.dbManager = dbManager;
        }

        /// <summary>
        /// Assign the passed in parameters
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterValues"></param>
        public void AssignParameters(DbCommand command, object[] parameterValues)
        {
            if (Equals(command, null))
            {
                throw new ArgumentNullException("command","Argument cannot be null");
            }
            foreach (SelectParameter selectParameter in parameters)
            {
                command.Parameters.Add(dbManager.GetDatabaseParameter(command,
                selectParameter.ParameterName, selectParameter.ParameterValue,
                selectParameter.ParameterDirection, selectParameter.ParameterType));
            }
        }
    }

    /// <summary>
    /// Class to hold the parameter values
    /// </summary>
    public class SelectParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Parameter value
        /// </summary>
        public object ParameterValue { get; set; }

        /// <summary>
        ///  Specifies the type of a parameter within a query relative to the System.Data.DataSet
        /// </summary>
        public ParameterDirection ParameterDirection { get; set; }

        /// <summary>
        /// Parameter type
        /// </summary>
        public object ParameterType { get; set; }
    }
}