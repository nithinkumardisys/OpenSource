//------------------------------------------------------------------------------
// <copyright file="GetHortProduceBHOModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//---------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Get Hort Produce Bho Model.
    /// </summary>
    public class GetHortProduceBhoModel
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Unit_of_measure.
        /// </summary>
        public string Unit_of_measure { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_username.
        /// </summary>
        public string Bho_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_add_edit_flag.
        /// </summary>
        public string Bho_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_add_edit_flag.
        /// </summary>
        public string Adh_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Final_cvrg_flg.
        /// </summary>
        public string Final_cvrg_flg { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Userid.
        /// </summary>
        public int Rec_Created_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Date.
        /// </summary>
        public DateTime Rec_Created_Date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Userid.
        /// </summary>
        public int Rec_Updated_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Date.
        /// </summary>
        public DateTime Rec_Updated_Date { get; set; }

        /// <summary>
        /// Gets or Sets Pending_adh_value.
        /// </summary>
        public decimal Pending_adh_value { get; set; }

        /// <summary>
        /// Gets or Sets Approved_adh_value.
        /// </summary>
        public decimal Approved_adh_value { get; set; }
    }

    /// <summary>
    /// Get Hort Produce Bho last Model.
    /// </summary>
    public class GetHortProduceBholstModel
    {
        /// <summary>
        /// Gets or Sets GetHortProduceBholstModel.
        /// </summary>
        public GetHortProduceBholstModel()
        {
            this.CropList = new List<GetHortProduceBhoCrplstModel>();
        }

        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets CropList.
        /// </summary>
        public List<GetHortProduceBhoCrplstModel> CropList { get; set; }
    }

    /// <summary>
    /// Get Hort Produce Bho Crop last Model.
    /// </summary>
    public class GetHortProduceBhoCrplstModel
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Unit_of_measure.
        /// </summary>
        public string Unit_of_measure { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_username.
        /// </summary>
        public string Bho_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_add_edit_flag.
        /// </summary>
        public string Bho_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_add_edit_flag.
        /// </summary>
        public string Adh_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Final_cvrg_flg.
        /// </summary>
        public string Final_cvrg_flg { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Userid.
        /// </summary>
        public int Rec_Created_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Date.
        /// </summary>
        public DateTime Rec_Created_Date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Userid.
        /// </summary>
        public int Rec_Updated_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Date.
        /// </summary>
        public DateTime Rec_Updated_Date { get; set; }

        /// <summary>
        /// Gets or Sets Pending_adh_value.
        /// </summary>
        public decimal Pending_adh_value { get; set; }

        /// <summary>
        /// Gets or Sets Approved_adh_value.
        /// </summary>
        public decimal Approved_adh_value { get; set; }
    }
}
