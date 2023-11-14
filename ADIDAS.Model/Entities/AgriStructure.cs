//------------------------------------------------------------------------------
// <copyright file="AgriStructure.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Agriculture Structure.
    /// </summary>
    public class AgriStructure
    {
        /// <summary>
        /// Gets or Sets Structure_ID.
        /// </summary>
        public int Structure_ID { get; set; }

        /// <summary>
        /// Gets or Sets Structure_name.
        /// </summary>
        public string Structure_name { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int? Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Structure_desc.
        /// </summary>
        public string Structure_desc { get; set; }

        /// <summary>
        /// Gets or Sets Is_Capacity_Mandatory_flg.
        /// </summary>
        public string Is_Capacity_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Unit_of_measure.
        /// </summary>
        public string Unit_of_measure { get; set; }
    }
}
