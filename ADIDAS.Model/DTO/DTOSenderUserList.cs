//------------------------------------------------------------------------------
// <copyright file="DTOSenderUserList.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Sender User List.
    /// </summary>
    public class DtoSenderUserList
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
        /// Gets or Sets User_name.
        /// </summary>
        public string User_name { get; set; }

        /// <summary>
        /// Gets or Sets Department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Division_name.
        /// </summary>
        public string Division_name { get; set; }
    }
}
