//------------------------------------------------------------------------------
// <copyright file="BametiTargetGet.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Bameti Target.
    /// </summary>
    public class BametiTargetGet
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Activity_id.
        /// </summary>
        public int Activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Target_qty.
        /// </summary>
        public int? Target_qty { get; set; }

        /// <summary>
        /// Gets or Sets UOM.
        /// </summary>
        public string UOM { get; set; }

        /// <summary>
        /// Gets or Sets Period.
        /// </summary>
        public string Period { get; set; }
    }
}
