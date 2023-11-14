//------------------------------------------------------------------------------
// <copyright file="DTOStructure.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Structure.
    /// </summary>
    public class DtoStructure
    {
        /// <summary>
        /// Gets or Sets Structure_id.
        /// </summary>
        public int Structure_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Structure_Name.
        /// </summary>
        public string Structure_Name { get; set; }

        /// <summary>
        /// Gets or Sets Is_Name_Mandatory_flg.
        /// </summary>
        public string Is_Name_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Is_Addr_Mandatory_flg.
        /// </summary>
        public string Is_Addr_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Is_Capacity_Mandatory_flg.
        /// </summary>
        public string Is_Capacity_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Unit_of_measure.
        /// </summary>
        public string Unit_of_measure { get; set; }

        /// <summary>
        /// Gets or Sets Structure_desc.
        /// </summary>
        public string Structure_desc { get; set; }
    }
}
