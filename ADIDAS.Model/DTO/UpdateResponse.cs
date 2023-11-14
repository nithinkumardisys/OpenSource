//------------------------------------------------------------------------------
// <copyright file="UpdateResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Update Response.
    /// </summary>
    public class UpdateResponse
    {
        /// <summary>
        /// Gets or Sets IdeaId.
        /// </summary>
        public string IdeaId { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }
    }
}
