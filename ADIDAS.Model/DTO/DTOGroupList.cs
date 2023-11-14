//------------------------------------------------------------------------------
// <copyright file="DTOGroupList.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// GroupList.
    /// </summary>
    public class DtoGroupList
    {
        /// <summary>
        /// Gets or Sets Recp_group_id.
        /// </summary>
        public int Recp_group_id { get; set; }

        /// <summary>
        /// Gets or Sets Group_name.
        /// </summary>
        public string Group_name { get; set; }

        /// <summary>
        /// Gets or Sets Group_description.
        /// </summary>
        public string Group_description { get; set; }

        /// <summary>
        /// Gets or Sets Edit_flg.
        /// </summary>
        public string Edit_flg { get; set; }

        /// <summary>
        /// Gets or Sets Created_date.
        /// </summary>
        public DateTime Created_date { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Created_by.
        /// </summary>
        public string Created_by { get; set; }

        /// <summary>
        /// Gets or Sets Recipient_cnt.
        /// </summary>
        public int Recipient_cnt { get; set; }

        /// <summary>
        /// Gets or Sets Sender_cnt.
        /// </summary>
        public int Sender_cnt { get; set; }
    }
}
