//------------------------------------------------------------------------------
// <copyright file="LeaderBoard.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Leader Board.
    /// </summary>
    public class LeaderBoard
    {
        /// <summary>
        /// Gets or Sets LeaderBoard.
        /// </summary>
        public LeaderBoard()
        {
            this.OverAll = new List<OverallLeaderBoard>();

            this.DistrictLeaderBoard = new List<DistrictLeaderBoard>();

            this.BlockLeaderBoard = new List<BlockLeaderBoard>();
        }

        /// <summary>
        /// Gets or Sets OverAll.
        /// </summary>
        public List<OverallLeaderBoard> OverAll { get; set; }

        /// <summary>
        /// Gets or Sets DistrictLeaderBoard.
        /// </summary>
        public List<DistrictLeaderBoard> DistrictLeaderBoard { get; set; }

        /// <summary>
        /// Gets or Sets BlockLeaderBoard.
        /// </summary>
        public List<BlockLeaderBoard> BlockLeaderBoard { get; set; }

        /// <summary>
        /// Gets or Sets MyTeams.
        /// </summary>
        public List<MyTeam> MyTeams { get; set; }
    }
}
