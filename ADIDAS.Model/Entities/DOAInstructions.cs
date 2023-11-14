//------------------------------------------------------------------------------
// <copyright file="DOAInstructions.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// DoaInstructions.
    /// </summary>
    public class DoaInstructions
    {
        /// <summary>
        /// Gets or Sets DOA_letter_ID.
        /// </summary>
        public int DOA_letter_ID { get; set; }

        /// <summary>
        /// Gets or Sets Letter_Type_Code.
        /// </summary>
        public string Letter_Type_Code { get; set; }

        /// <summary>
        /// Gets or Sets Letter_Type.
        /// </summary>
        public string Letter_Type { get; set; }

        /// <summary>
        /// Gets or Sets Blob_Filename.
        /// </summary>
        public string Blob_Filename { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_user_id.
        /// </summary>
        public int? Rec_created_user_id { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated__user_id.
        /// </summary>
        public int? Rec_updated__user_id { get; set; }
    }
}
