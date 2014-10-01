using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace TUI.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class Planning
    {

        /// <summary>
        /// Gets or sets the plan identifier.
        /// </summary>
        /// <value>
        /// The plan identifier.
        /// </value>
        public int PlanId { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage="Name is Mandatory")]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        /// <value>
        /// The place.
        /// </value>
        [Required(ErrorMessage = "Place is Mandatory")]
        public string Place { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Required(ErrorMessage = "City is Mandatory")]
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Required(ErrorMessage = "State is Mandatory")]
        public string State { get; set; }
        /// <summary>
        /// Gets or sets the phone no.
        /// </summary>
        /// <value>
        /// The phone no.
        /// </value>
        public string PhoneNo { get; set; }
        /// <summary>
        /// Gets or sets the photos.
        /// </summary>
        /// <value>
        /// The photos.
        /// </value>
        public Collection<byte[]> Photos { get; set; }
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the join count.
        /// </summary>
        /// <value>
        /// The join count.
        /// </value>
        public int JoinCount { get; set; }
    }
}
