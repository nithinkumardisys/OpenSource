//------------------------------------------------------------------------------
// <copyright file="DTOViewProgramRequest.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// View Program Request.
    /// </summary>
    public class DtoViewProgramRequest
    {
        /// <summary>
        /// Gets or Sets SchemeId.
        /// </summary>
        public string SchemeId { get; set; }

        /// <summary>
        /// Gets or Sets ActivityId.
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Userdesignation.
        /// </summary>
        public string Userdesignation { get; set; }

        /// <summary>
        /// Gets or Sets Topic.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Gets or Sets Fromdate.
        /// </summary>
        public string Fromdate { get; set; }

        /// <summary>
        /// Gets or Sets ToDate.
        /// </summary>
        public string ToDate { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }
    }
}
