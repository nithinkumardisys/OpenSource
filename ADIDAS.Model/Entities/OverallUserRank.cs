// <copyright file="OverallUserRank.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// OverallUserRank.
    /// </summary>
    public class OverallUserRank
    {
        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public int User_id { get; set; }

        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public int? Total_points { get; set; }

        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public long? Overall_rank { get; set; }
    }
}
