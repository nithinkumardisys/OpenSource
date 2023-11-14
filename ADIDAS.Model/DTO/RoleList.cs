//------------------------------------------------------------------------------
// <copyright file="RoleList.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Role List.
    /// </summary>
    public class RoleList
    {
        /// <summary>
        /// Gets or Sets Role_id.
        /// </summary>
        public int Role_id { get; set; }

        /// <summary>
        /// Gets or Sets Role_name.
        /// </summary>
        public string Role_name { get; set; }

        /// <summary>
        /// Gets or Sets Role_description.
        /// </summary>
        public string Role_description { get; set; }

        /// <summary>
        /// Gets or Sets Usr_cnt.
        /// </summary>
        public int Usr_cnt { get; set; }

        /// <summary>
        /// Gets or Sets Created_by.
        /// </summary>
        public string Created_by { get; set; }

        /// <summary>
        /// Gets or Sets Created_date.
        /// </summary>
        public DateTime Created_date { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Edit_flg.
        /// </summary>
        public string Edit_flg { get; set; }
    }
}
