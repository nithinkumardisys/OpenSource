//------------------------------------------------------------------------------
// <copyright file="Bihan1Stats.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Bihan Stats.
    /// </summary>
    public class Bihan1Stats
    {
        /// <summary>
        /// Gets or Sets Districts.
        /// </summary>
        public string Districts { get; set; }

        /// <summary>
        /// Gets or Sets User_Count_using_v1.
        /// </summary>
        public int User_Count_using_v1 { get; set; }

        /// <summary>
        /// Gets or Sets Count_of_users_submitted_target.
        /// </summary>
        public int Count_of_users_submitted_target { get; set; }

        /// <summary>
        /// Gets or Sets Count_of_users_submitted_actl.
        /// </summary>
        public int Count_of_users_submitted_actl { get; set; }
    }
}
