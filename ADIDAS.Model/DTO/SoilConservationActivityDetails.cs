//------------------------------------------------------------------------------
// <copyright file="SoilConservationActivityDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Create Activity  Details.
    /// </summary>
    public class SoilConservationActivityDetails
    {
        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int? Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Current_year.
        /// </summary>
        public string Current_year { get; set; }

        /// <summary>
        /// Gets or Sets Activity_list.
        /// </summary>
        public List<ActivityList> Activity_list { get; set; }
    }

    /// <summary>
    /// ActivityList.
    /// </summary>
    public class ActivityList
    {
        /// <summary>
        /// Gets or Sets Activity_id.
        /// </summary>
        public int? Activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Activity_name.
        /// </summary>
        public string Activity_name { get; set; }

        /// <summary>
        /// Gets or Sets Sub_activity_list.
        /// </summary>
        public List<SubActivityList> Sub_activity_list { get; set; }
    }

    /// <summary>
    /// SubActivityList.
    /// </summary>
    public class SubActivityList
    {
        /// <summary>
        /// Gets or Sets Sub_activity_id.
        /// </summary>
        public int? Sub_activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_activity_name.
        /// </summary>
        public string Sub_activity_name { get; set; }
    }
}
