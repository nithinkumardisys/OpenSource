//------------------------------------------------------------------------------
// <copyright file="DistrictLeaderBoard.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// District LeaderBoard.
    /// </summary>
    public class DistrictLeaderBoard
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Points.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public long Rank { get; set; }
    }
}
