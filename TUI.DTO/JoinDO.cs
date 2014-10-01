using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TUI.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class JoinDO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Name is Mandatory")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public int? Age { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        /// <value>
        /// The place.
        /// </value>
        public string Place { get; set; }
        /// <summary>
        /// Gets or sets the fb identifier.
        /// </summary>
        /// <value>
        /// The fb identifier.
        /// </value>
         [Required(ErrorMessage = "Facebook Id is Mandatory")]
        public string fbId { get; set; }
         /// <summary>
         /// Gets or sets the city.
         /// </summary>
         /// <value>
         /// The city.
         /// </value>
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the plan identifier.
        /// </summary>
        /// <value>
        /// The plan identifier.
        /// </value>
            [Required(ErrorMessage = "Mobile is Mandatory")]
        public string Mobile { get; set; }
            /// <summary>
            /// Gets or sets the address.
            /// </summary>
            /// <value>
            /// The address.
            /// </value>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets the plan identifier.
        /// </summary>
        /// <value>
        /// The plan identifier.
        /// </value>
        public int PlanId { get; set; }
    }
}
