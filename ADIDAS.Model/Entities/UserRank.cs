// <copyright file="UserRank.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// UserRank.
    /// </summary>
    public class UserRank
    {
        /// <summary>
        /// UserRank.
        /// </summary>
        public UserRank()
        {
            this.OverallRank = new OverallUserRank();
        }

        /// <summary>
        /// Gets or Sets Today_Points.
        /// </summary>
        public int? Today_Points { get; set; }

        /// <summary>
        /// Gets or Sets Yesterday_Points.
        /// </summary>
        public int? Yesterday_Points { get; set; }

        /// <summary>
        /// Gets or Sets This_Week_Points.
        /// </summary>
        public int? This_Week_Points { get; set; }

        /// <summary>
        /// Gets or Sets OverallRank.
        /// </summary>
        public OverallUserRank OverallRank { get; set; }
    }
}
