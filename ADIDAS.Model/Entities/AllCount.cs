//------------------------------------------------------------------------------
// <copyright file="AllCount.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// All Count.
    /// </summary>
    public class AllCount
    {
        /// <summary>
        /// Gets or Sets Districts.
        /// </summary>
        public string Districts { get; set; }

        /// <summary>
        /// Gets or Sets Reg_UsrCount.
        /// </summary>
        public int Reg_UsrCount { get; set; }

        /// <summary>
        /// Gets or Sets Login_UsrCount.
        /// </summary>
        public int Login_UsrCount { get; set; }

        /// <summary>
        /// Gets or Sets NoLogin_UsrCount.
        /// </summary>
        public int NoLogin_UsrCount { get; set; }

        /// <summary>
        /// Gets or Sets TgtSub_ACCount.
        /// </summary>
        public int TgtSub_ACCount { get; set; }

        /// <summary>
        /// Gets or Sets CvrgSub_ACCount.
        /// </summary>
        public int CvrgSub_ACCount { get; set; }
    }
}
