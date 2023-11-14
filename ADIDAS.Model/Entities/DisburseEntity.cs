//------------------------------------------------------------------------------
// <copyright file="DisburseEntity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// DisburseEntity.
    /// </summary>
    public class DisburseEntity
    {
        /// <summary>
        /// Gets or Sets User_Id.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Gets or Sets Division_id.
        /// </summary>
        public int Division_id { get; set; }

        /// <summary>
        /// Gets or Sets Division_name.
        /// </summary>
        public string Division_name { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Sub_division_id.
        /// </summary>
        public int Sub_division_id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_division_name.
        /// </summary>
        public string Sub_division_name { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or Sets Drawing_Officer_ID.
        /// </summary>
        public int Drawing_Officer_ID { get; set; }
    }
}
