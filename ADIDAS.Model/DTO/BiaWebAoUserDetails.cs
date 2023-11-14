//------------------------------------------------------------------------------
// <copyright file="BiaWebAoUserDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// BiaWebAoUserDetails.
    /// </summary>
    public class BiaWebAoUserDetails
    {
        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets PendingTasks.
        /// </summary>
        public List<BiaWebAoPendingTasks> PendingTasks { get; set; }
    }
}