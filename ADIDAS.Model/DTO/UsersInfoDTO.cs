//------------------------------------------------------------------------------
// <copyright file="UsersInfoDTO.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Users Info Dto.
    /// </summary>
    public class UsersInfoDto
    {
        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public int User_id { get; set; }

        /// <summary>
        /// Gets or Sets Fullname.
        /// </summary>
        public string Fullname { get; set; }

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

        /// <summary>
        /// Gets or Sets CheckedRecord.
        /// </summary>
        public bool CheckedRecord { get; set; }
    }
}
