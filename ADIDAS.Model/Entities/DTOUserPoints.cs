//------------------------------------------------------------------------------
// <copyright file="DTOUserPoints.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// DtoUserPoints.
    /// </summary>
    public class DtoUserPoints
    {
        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public string User_id { get; set; }

        /// <summary>
        /// Gets or Sets Points_cat_id.
        /// </summary>
        public string Points_cat_id { get; set; }

        /// <summary>
        /// Gets or Sets Obj_id.
        /// </summary>
        public string Obj_id { get; set; }

        /// <summary>
        /// Gets or Sets Obj_type.
        /// </summary>
        public string Obj_type { get; set; }

        /// <summary>
        /// Gets or Sets Activity_ts.
        /// </summary>
        public DateTime Activity_ts { get; set; }
    }
}
