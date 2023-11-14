// <copyright file="MessageEntity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// MessageEntity.
    /// </summary>
    public partial class MessageEntity
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
        /// Gets or Sets Recp_group_id.
        /// </summary>
        public string Recp_group_id { get; set; }

        /// <summary>
        /// Gets or Sets User_Name.
        /// </summary>
        public string User_Name { get; set; }

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

        /// <summary>
        /// Gets or Sets Group_name.
        /// </summary>
        public string Group_name { get; set; }

        /// <summary>
        /// Gets or Sets Group_description.
        /// </summary>
        public string Group_description { get; set; }
    }
}