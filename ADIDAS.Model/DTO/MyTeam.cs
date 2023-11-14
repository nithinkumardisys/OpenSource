//------------------------------------------------------------------------------
// <copyright file="MyTeam.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// My Team.
    /// </summary>
    public class MyTeam
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public long Rank { get; set; }

        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public long User_id { get; set; }

        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Profile_Image.
        /// </summary>
        public string Profile_Image { get; set; }

        /// <summary>
        /// Gets or Sets Points.
        /// </summary>
        public long Points { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public string Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Badge.
        /// </summary>
        public string Badge { get; set; }
    }

    /// <summary>
    /// Overall Leader Board.
    /// </summary>
    public class OverallLeaderBoard
    {
        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public int User_id { get; set; }

        /// <summary>
        /// Gets or Sets NAME.
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public string Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Points.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public long Rank { get; set; }

        /// <summary>
        /// Gets or Sets Profile_Image.
        /// </summary>
        public string Profile_Image { get; set; }

        /// <summary>
        /// Gets or Sets Badge.
        /// </summary>
        public string Badge { get; set; }
    }
}
