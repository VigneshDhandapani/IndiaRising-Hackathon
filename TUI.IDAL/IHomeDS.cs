using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TUI.DTO;
using System.Collections.ObjectModel;

namespace TUI.IDAL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHomeDS
    {
        /// <summary>
        /// Plans the register.
        /// </summary>
        /// <param name="planning">The planning.</param>
        void PlanRegister(Planning planning);
        /// <summary>
        /// Searches the planned.
        /// </summary>
        /// <param name="joinDO">The join do.</param>
        /// <returns></returns>
        Collection<Planning> SearchPlanned(JoinDO joinDO);
        /// <summary>
        /// Gets the state list.
        /// </summary>
        /// <returns></returns>
        Collection<DataSource> GetStateList();
        /// <summary>
        /// Gets the city list.
        /// </summary>
        /// <param name="StateId">The state identifier.</param>
        /// <returns></returns>
        Collection<DataSource> GetCityList(int? StateId);
        /// <summary>
        /// Joins the tui.
        /// </summary>
        /// <param name="joinDO">The join do.</param>
        void JoinTUI(JoinDO joinDO);
    }
}
