//------------------------------------------------------------------------------
// <copyright file="AppMessage.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Application Message.
    /// </summary>
    public partial class AppMessage
    {
        /// <summary>
        /// Gets or Sets Msg_Id.
        /// </summary>
        public int Msg_Id { get; set; }

        /// <summary>
        /// Gets or Sets User_Id.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Gets or Sets Msg_Subject.
        /// </summary>
        public string Msg_Subject { get; set; }

        /// <summary>
        /// Gets or Sets Msg_Desc.
        /// </summary>
        public string Msg_Desc { get; set; }

        /// <summary>
        /// Gets or Sets Msg_Date.
        /// </summary>
        public DateTime? Msg_Date { get; set; }
    }
}