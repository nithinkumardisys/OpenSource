//------------------------------------------------------------------------------
// <copyright file="FAQ.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Faq.
    /// </summary>
    public class Faq
    {
        /// <summary>
        /// Gets or Sets Faq_id.
        /// </summary>
        public int Faq_id { get; set; }

        /// <summary>
        /// Gets or Sets Faq_title.
        /// </summary>
        public string Faq_title { get; set; }

        /// <summary>
        /// Gets or Sets Faq_description.
        /// </summary>
        public string Faq_description { get; set; }

        /// <summary>
        /// Gets or Sets Faq_url.
        /// </summary>
        public string Faq_url { get; set; }

        /// <summary>
        /// Gets or Sets Faq_order_no.
        /// </summary>
        public long Faq_order_no { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public string Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public string Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }
    }
}
