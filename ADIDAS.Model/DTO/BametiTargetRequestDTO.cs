//------------------------------------------------------------------------------
// <copyright file="BametiTargetRequestDTO.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Bameti Target Request.
    /// </summary>
    public class BametiTargetRequestDto
    {
        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Activity_id.
        /// </summary>
        public int Activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Period.
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Gets or Sets Uom.
        /// </summary>
        public string Uom { get; set; }
    }
}
