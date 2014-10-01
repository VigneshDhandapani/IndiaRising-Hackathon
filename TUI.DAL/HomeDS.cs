using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TUI.IDAL;
using TUI.DTO;
using System.Data.Common;
using TUI.Utility.Interface;
using TUI.Utility;
using System.Data;
using System.Collections.ObjectModel;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace TUI.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeDS : IHomeDS
    {

        /// <summary>
        /// Plans the register.
        /// </summary>
        /// <param name="planning">The planning.</param>
        public void PlanRegister(Planning planning)
        {
            string planQuery = "sp_InsertPlanning";
            using (IDatabaseManager dbmanager = DatabaseManagerFactory.GetDatabaseManager("TUI"))
            {
                DbCommand command = dbmanager.DatabaseObject.GetStoredProcCommand(planQuery);
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iName", planning.Name, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iPlace", planning.Place, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iCity", planning.City, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iState", planning.State, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iPhone", planning.PhoneNo, ParameterDirection.Input, DbType.String));
                //command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iPhotos", planning.Photos, ParameterDirection.Input, null));
                dbmanager.ExecuteSqlNonQuery(command, false);
            }
        }
        /// <summary>
        /// Joins the tui.
        /// </summary>
        /// <param name="joinDO"></param>
        public void JoinTUI(JoinDO joinDO)
        {
            string planQuery = "sp_InsertJoinees";
            using (IDatabaseManager dbmanager = DatabaseManagerFactory.GetDatabaseManager("TUI"))
            {
                DbCommand command = dbmanager.DatabaseObject.GetStoredProcCommand(planQuery);
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iName", joinDO.Name, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iCity", joinDO.City, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iState", joinDO.State, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iPhone", joinDO.Mobile, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@ifbId", joinDO.fbId, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iAddress", joinDO.Address, ParameterDirection.Input, DbType.String));
                command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iAge", joinDO.Age, ParameterDirection.Input, DbType.Int32));
                //command.Parameters.Add(dbmanager.GetDatabaseParameter(command, "@iPhotos", planning.Photos, ParameterDirection.Input, null));
                dbmanager.ExecuteSqlNonQuery(command, false);
            }
        }
        /// <summary>
        /// Searches the planned.
        /// </summary>
        /// <param name="joinDO">The join do.</param>
        /// <returns></returns>
        public Collection<Planning> SearchPlanned(JoinDO joinDO)
        {
            Collection<Planning> planningList = new Collection<Planning>();
            var planQuery = "sp_SearchPlanned";
            using (IDatabaseManager dbmanager = DatabaseManagerFactory.GetDatabaseManager("TUI"))
            {
                Collection<SelectParameter> selectParameter = new Collection<SelectParameter>();
                selectParameter.Add(new SelectParameter { ParameterName = "@iCity", ParameterValue = joinDO.City, ParameterDirection = ParameterDirection.Input, ParameterType = null });
                selectParameter.Add(new SelectParameter { ParameterName = "@iState", ParameterValue = joinDO.State, ParameterDirection = ParameterDirection.Input, ParameterType = null });
                var rowMapper = MapBuilder<Planning>.MapNoProperties().Map(b => b.PlanId).ToColumn("PlanId").Map(b => b.Name).ToColumn("Name").Map(b => b.Place).ToColumn("Place").Map(b => b.City).ToColumn("City").Map(b => b.CreatedOn).ToColumn("CreatedOn").Map(b => b.State).ToColumn("State").Build();
                planningList = new Collection<Planning>(dbmanager.DatabaseObject.CreateSprocAccessor(planQuery,new ParameterMapper(dbmanager, selectParameter), rowMapper).Execute().ToList());
            }
            return planningList;
        }


        /// <summary>
        /// Gets the state list.
        /// </summary>
        /// <returns></returns>
        public Collection<DataSource> GetStateList()
        {
            Collection<DataSource> datasourceList = new Collection<DataSource>();
            var planQuery = "sp_GetStateList";
            using (IDatabaseManager dbmanager = DatabaseManagerFactory.GetDatabaseManager("TUI"))
            {
                var rowMapper = MapBuilder<DataSource>.MapNoProperties().Map(b => b.Key).ToColumn("StateId").Map(b => b.Value).ToColumn("StateName").Build();
                datasourceList = new Collection<DataSource>(dbmanager.DatabaseObject.CreateSprocAccessor(planQuery, rowMapper).Execute().ToList());
            }
            return datasourceList;
        }

        /// <summary>
        /// Gets the city list.
        /// </summary>
        /// <param name="StateId">The state identifier.</param>
        /// <returns></returns>
        public Collection<DataSource> GetCityList(int? StateId)
        {
            Collection<DataSource> datasourceList = new Collection<DataSource>();
            var planQuery = "sp_GetCityList";
            using (IDatabaseManager dbmanager = DatabaseManagerFactory.GetDatabaseManager("TUI"))
            {
                Collection<SelectParameter> selectParameter = new Collection<SelectParameter>();
                selectParameter.Add(new SelectParameter { ParameterName = "@iStateId", ParameterValue = StateId, ParameterDirection = ParameterDirection.Input, ParameterType = null });
                var rowMapper = MapBuilder<DataSource>.MapNoProperties().Map(b => b.Key).ToColumn("CityId").Map(b => b.Value).ToColumn("CityName").Build();
                datasourceList = new Collection<DataSource>(dbmanager.DatabaseObject.CreateSprocAccessor(planQuery,new ParameterMapper(dbmanager, selectParameter), rowMapper).Execute().ToList());
            }
            return datasourceList;
        }
    }
}
