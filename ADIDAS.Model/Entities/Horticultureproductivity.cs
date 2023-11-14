//------------------------------------------------------------------------------
// <copyright file="Horticultureproductivity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Horticultureproductivity.
    /// </summary>
    public class Horticultureproductivity
    {
        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Season_Id.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets CropValues.
        /// </summary>
        public List<CropValue> CropValues { get; set; }
    }

    /// <summary>
    /// CropValue.
    /// </summary>
    public class CropValue
    {
        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets CropID.
        /// </summary>
        public int CropID { get; set; }

        /// <summary>
        /// Gets or Sets Productivity.
        /// </summary>
        public decimal Productivity { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_date.
        /// </summary>
        public DateTime Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_userid.
        /// </summary>
        public int Submitted_userid { get; set; }
    }
}
