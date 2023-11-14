//------------------------------------------------------------------------------
// <copyright file="BiaNotificationAudits.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// BiaNotificationAudits.
    /// </summary>
    public class BiaNotificationAudits
    {
        /// <summary>
        /// Gets or Sets AoUserId.
        /// </summary>
        public int AoUserId { get; set; }

        /// <summary>
        /// Gets or Sets Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets DueDate.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or Sets AssignedByName.
        /// </summary>
        public string AssignedByName { get; set; }

        /// <summary>
        /// Gets or Sets AssignedByDesignation.
        /// </summary>
        public string AssignedByDesignation { get; set; }
    }
}
