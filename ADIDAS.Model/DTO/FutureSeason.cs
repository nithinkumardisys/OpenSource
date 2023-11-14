//------------------------------------------------------------------------------
// <copyright file="FutureSeason.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// FutureSeason.
    /// </summary>
    public class FutureSeason
    {
        /// <summary>
        /// Gets or Sets Season_Id.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets Season_Name.
        /// </summary>
        public string Season_Name { get; set; }

        /// <summary>
        /// Gets or Sets Season_year.
        /// </summary>
        public string Season_year { get; set; }

        /// <summary>
        /// Gets or Sets Start_Date.
        /// </summary>
        public DateTime? Start_Date { get; set; }

        /// <summary>
        /// Gets or Sets End_Date.
        /// </summary>
        public DateTime? End_Date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Userid.
        /// </summary>
        public string Rec_Created_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Date.
        /// </summary>
        public DateTime? Rec_Created_Date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Userid.
        /// </summary>
        public string Rec_Updated_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Date.
        /// </summary>
        public DateTime? Rec_Updated_Date { get; set; }

        /// <summary>
        /// Gets or Sets Agri_Cvrg_Lck_Flg.
        /// </summary>
        public string Agri_Cvrg_Lck_Flg { get; set; }

        /// <summary>
        /// Gets or Sets Agri_Tgt_Lck_Flg.
        /// </summary>
        public string Agri_Tgt_Lck_Flg { get; set; }

        /// <summary>
        /// Gets or Sets Horti_Cvrg_Lck_Flg.
        /// </summary>
        public string Horti_Cvrg_Lck_Flg { get; set; }

        /// <summary>
        /// Gets or Sets Horti_Tgt_Lck_Flg.
        /// </summary>
        public string Horti_Tgt_Lck_Flg { get; set; }

        /// <summary>
        /// Gets or Sets Seed_Used_Input_flg.
        /// </summary>
        public string Seed_Used_Input_flg { get; set; }
    }
}
