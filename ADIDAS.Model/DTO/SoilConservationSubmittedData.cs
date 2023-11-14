//------------------------------------------------------------------------------
// <copyright file="SoilConservationSubmittedData.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Create Soil Conservation Submitted Data Response.
    /// </summary>
    public class SoilConservationSubmittedDataResponse
    {
        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int? Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Activity_list.
        /// </summary>
        public List<SubmittedActivityList> Activity_list { get; set; }
    }

    /// <summary>
    /// Submitted Activity List.
    /// </summary>
    public class SubmittedActivityList
    {
        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int? Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Pctivity_id.
        /// </summary>
        public int? Activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Activity_name.
        /// </summary>
        public string Activity_name { get; set; }

        /// <summary>
        /// Gets or Sets Is_activity_submitted.
        /// </summary>
        public bool? Is_activity_submitted { get; set; }

        /// <summary>
        /// Gets or Sets Over_all_count.
        /// </summary>
        public int? Over_all_count { get; set; }

        /// <summary>
        /// Gets or Sets Sub_activity_list.
        /// </summary>
        public List<SubmittedSubActivityList> Sub_activity_list { get; set; }
    }

    /// <summary>
    /// Male.
    /// </summary>
    public class Male
    {
        /// <summary>
        /// Gets or Sets Sc_male.
        /// </summary>
        public int? Sc_male { get; set; }

        /// <summary>
        /// Gets or Sets St_male.
        /// </summary>
        public int? St_male { get; set; }

        /// <summary>
        /// Gets or Sets General_male.
        /// </summary>
        public int? General_male { get; set; }

        /// <summary>
        /// Gets or Sets Bc_male.
        /// </summary>
        public int? Bc_male { get; set; }

        /// <summary>
        /// Gets or Sets Ebc_male.
        /// </summary>
        public int? Ebc_male { get; set; }

        /// <summary>
        /// Gets or Sets Minority_male.
        /// </summary>
        public int? Minority_male { get; set; }

        /// <summary>
        /// Gets or Sets GenderWiseTotal_male.
        /// </summary>
        public int? GenderWiseTotal_male { get; set; }
    }

    /// <summary>
    /// Female
    /// </summary>
    public class Female
    {
        /// <summary>
        /// Gets or Sets Sc_female.
        /// </summary>
        public int? Sc_female { get; set; }

        /// <summary>
        /// Gets or Sets St_female.
        /// </summary>
        public int? St_female { get; set; }

        /// <summary>
        /// Gets or Sets General_female.
        /// </summary>
        public int? General_female { get; set; }

        /// <summary>
        /// Gets or Sets Bc_female.
        /// </summary>
        public int? Bc_female { get; set; }

        /// <summary>
        /// Gets or Sets Ebc_female.
        /// </summary>
        public int? Ebc_female { get; set; }

        /// <summary>
        /// Gets or Sets Minority_female.
        /// </summary>
        public int? Minority_female { get; set; }

        /// <summary>
        /// Gets or Sets GenderWiseTotal_female.
        /// </summary>
        public int? GenderWiseTotal_female { get; set; }
    }

    /// <summary>
    /// Others.
    /// </summary>
    public class Others
    {
        /// <summary>
        /// Gets or Sets Sc_others.
        /// </summary>
        public int? Sc_others { get; set; }

        /// <summary>
        /// Gets or Sets St_others.
        /// </summary>
        public int? St_others { get; set; }

        /// <summary>
        /// Gets or Sets General_others.
        /// </summary>
        public int? General_others { get; set; }

        /// <summary>
        /// Gets or Sets Bc_others.
        /// </summary>
        public int? Bc_others { get; set; }

        /// <summary>
        /// Gets or Sets Ebc_others.
        /// </summary>
        public int? Ebc_others { get; set; }

        /// <summary>
        /// Gets or Sets Minority_others.
        /// </summary>
        public int? Minority_others { get; set; }

        /// <summary>
        /// Gets or Sets GenderWiseTotal_others.
        /// </summary>
        public int? GenderWiseTotal_others { get; set; }
    }

    /// <summary>
    /// CastType.
    /// </summary>
    public class CastType
    {
        /// <summary>
        /// Gets or Sets Sc.
        /// </summary>
        public int? Sc { get; set; }

        /// <summary>
        /// Gets or Sets St.
        /// </summary>
        public int? St { get; set; }

        /// <summary>
        /// Gets or Sets General.
        /// </summary>
        public int? General { get; set; }

        /// <summary>
        /// Gets or Sets Bc.
        /// </summary>
        public int? Bc { get; set; }

        /// <summary>
        /// Gets or Sets Ebc.
        /// </summary>
        public int? Ebc { get; set; }

        /// <summary>
        /// Gets or Sets Minority.
        /// </summary>
        public int? Minority { get; set; }

        /// <summary>
        /// Gets or Sets Total.
        /// </summary>
        public int? Total { get; set; }
    }

    /// <summary>
    /// SubActivity Detail.
    /// </summary>
    public class SubActivityDetail
    {
        /// <summary>
        /// Gets or Sets Is_final_submission.
        /// </summary>
        public bool? Is_final_submission { get; set; }

        /// <summary>
        /// Gets or Sets Structure_id.
        /// </summary>
        public int? Structure_id { get; set; }

        /// <summary>
        /// Gets or Sets Structure_name.
        /// </summary>
        public string Structure_name { get; set; }

        /// <summary>
        /// Gets or Sets Yojana_number.
        /// </summary>
        public string Yojana_number { get; set; }

        /// <summary>
        /// Gets or Sets Yojana_name.
        /// </summary>
        public string Yojana_name { get; set; }

        /// <summary>
        /// Gets or Sets Physical_target.
        /// </summary>
        public decimal? Physical_target { get; set; }

        /// <summary>
        /// Gets or Sets Financial_target.
        /// </summary>
        public decimal? Financial_target { get; set; }

        /// <summary>
        /// Gets or Sets Current_year.
        /// </summary>
        public string Current_year { get; set; }

        /// <summary>
        /// Gets or Sets Registration_no.
        /// </summary>
        public string Registration_no { get; set; }

        /// <summary>
        /// Gets or Sets IsRegisterNumberVerified.
        /// </summary>
        public bool? IsRegisterNumberVerified { get; set; }

        /// <summary>
        /// Gets or Sets IsRegisterNumberValid.
        /// </summary>
        public bool? IsRegisterNumberValid { get; set; }

        /// <summary>
        /// Gets or Sets Dbt_number.
        /// </summary>
        public string Dbt_number { get; set; }

        /// <summary>
        /// Gets or Sets Mobile_number.
        /// </summary>
        public string Mobile_number { get; set; }

        /// <summary>
        /// Gets or Sets Beneficiary_name.
        /// </summary>
        public string Beneficiary_name { get; set; }

        /// <summary>
        /// Gets or Sets Name_Of_Village.
        /// </summary>
        public string Name_Of_Village { get; set; }

        /// <summary>
        /// Gets or Sets Name_Of_district.
        /// </summary>
        public string Name_Of_district { get; set; }

        /// <summary>
        /// Gets or Sets Name_Of_panchayat.
        /// </summary>
        public string Name_Of_panchayat { get; set; }

        /// <summary>
        /// Gets or Sets Name_Of_block.
        /// </summary>
        public string Name_Of_block { get; set; }

        /// <summary>
        /// Gets or Sets Total_no_of_beneficiaries.
        /// </summary>
        public int? Total_no_of_beneficiaries { get; set; }

        /// <summary>
        /// Gets or Sets Male.
        /// </summary>
        public PostMale Male { get; set; }

        /// <summary>
        /// Gets or Sets Female.
        /// </summary>
        public PostFemale Female { get; set; }

        /// <summary>
        /// Gets or Sets Others.
        /// </summary>
        public PostOthers Others { get; set; }

        /// <summary>
        /// Gets or Sets Sc_total.
        /// </summary>
        public int? Sc_total { get; set; }

        /// <summary>
        /// Gets or Sets St_total.
        /// </summary>
        public int? St_total { get; set; }

        /// <summary>
        /// Gets or Sets General_total.
        /// </summary>
        public int? General_total { get; set; }

        /// <summary>
        /// Gets or Sets Bc_total.
        /// </summary>
        public int? Bc_total { get; set; }

        /// <summary>
        /// Gets or Sets Ebc_total.
        /// </summary>
        public int? Ebc_total { get; set; }

        /// <summary>
        /// Gets or Sets Minority_total.
        /// </summary>
        public int? Minority_total { get; set; }

        /// <summary>
        /// Gets or Sets Date_of_starting.
        /// </summary>
        public DateTime? Date_of_starting { get; set; }

        /// <summary>
        /// Gets or Sets Due_date_of_completion.
        /// </summary>
        public DateTime? Due_date_of_completion { get; set; }

        /// <summary>
        /// Gets or Sets Estimated_budget.
        /// </summary>
        public decimal? Estimated_budget { get; set; }

        /// <summary>
        /// Gets or Sets Project_irrigated_area.
        /// </summary>
        public decimal? Project_irrigated_area { get; set; }

        /// <summary>
        /// Gets or Sets Pre_construction_image_data.
        /// </summary>
        public string Pre_construction_image_data { get; set; }

        /// <summary>
        /// Gets or Sets Pre_construction_image_location.
        /// </summary>
        public string Pre_construction_image_location { get; set; }

        /// <summary>
        /// Gets or Sets Pre_construction_image_name.
        /// </summary>
        public string Pre_construction_image_name { get; set; }

        /// <summary>
        /// Gets or Sets Pre_construction_latitude.
        /// </summary>
        public string Pre_construction_latitude { get; set; }

        /// <summary>
        /// Gets or Sets Pre_construction_longtitude.
        /// </summary>
        public string Pre_construction_longtitude { get; set; }

        /// <summary>
        /// Gets or Sets During_construction_image_data.
        /// </summary>
        public string During_construction_image_data { get; set; }

        /// <summary>
        /// Gets or Sets During_construction_image_location.
        /// </summary>
        public string During_construction_image_location { get; set; }

        /// <summary>
        /// Gets or Sets During_construction_image_name.
        /// </summary>
        public string During_construction_image_name { get; set; }

        /// <summary>
        /// Gets or Sets During_construction_latitude.
        /// </summary>
        public string During_construction_latitude { get; set; }

        /// <summary>
        /// Gets or Sets During_construction_longtitude.
        /// </summary>
        public string During_construction_longtitude { get; set; }

        /// <summary>
        /// Gets or Sets Post_construction_image_data.
        /// </summary>
        public string Post_construction_image_data { get; set; }

        /// <summary>
        /// Gets or Sets Post_construction_image_location.
        /// </summary>
        public string Post_construction_image_location { get; set; }

        /// <summary>
        /// Gets or Sets Post_construction_image_name.
        /// </summary>
        public string Post_construction_image_name { get; set; }

        /// <summary>
        /// Gets or Sets Pos_construction_latitude.
        /// </summary>
        public string Pos_construction_latitude { get; set; }

        /// <summary>
        /// Gets or Sets Post_construction_longtitude.
        /// </summary>
        public string Post_construction_longtitude { get; set; }

        /// <summary>
        /// Gets or Sets Actual_date_of_completion.
        /// </summary>
        public DateTime? Actual_date_of_completion { get; set; }

        /// <summary>
        /// Gets or Sets Actual_expenditure.
        /// </summary>
        public decimal? Actual_expenditure { get; set; }

        /// <summary>
        /// Gets or Sets Actual_irrigated_area.
        /// </summary>
        public decimal? Actual_irrigated_area { get; set; }

        /// <summary>
        /// Gets or Sets Youtube_story_link.
        /// </summary>
        public string Youtube_story_link { get; set; }

        /// <summary>
        /// Gets or Sets Date_Of_Activity.
        /// </summary>
        public DateTime? Date_Of_Activity { get; set; }

        /// <summary>
        /// Gets or Sets Image_data.
        /// </summary>
        public string Image_data { get; set; }

        /// <summary>
        /// Gets or Sets Image_location.
        /// </summary>
        public string Image_location { get; set; }

        /// <summary>
        /// Gets or Sets Image_name.
        /// </summary>
        public string Image_name { get; set; }

        /// <summary>
        /// Gets or Sets Latitude.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or Sets Longitutde.
        /// </summary>
        public string Longitutde { get; set; }

        /// <summary>
        /// Gets or Sets Topic_id.
        /// </summary>
        public int? Topic_id { get; set; }

        /// <summary>
        /// Gets or Sets Topic_name.
        /// </summary>
        public string Topic_name { get; set; }

        /// <summary>
        /// Gets or Sets TotalCoveredArea.
        /// </summary>
        public decimal? TotalCoveredArea { get; set; }

        /// <summary>
        /// Gets or Sets Plant_id.
        /// </summary>
        public int? Plant_id { get; set; }

        /// <summary>
        /// Gets or Sets PlantType.
        /// </summary>
        public string PlantType { get; set; }

        /// <summary>
        /// Gets or Sets Plant_survived_one_year.
        /// </summary>
        public int? Plant_survived_one_year { get; set; }

        /// <summary>
        /// Gets or Sets Plant_survived_two_year.
        /// </summary>
        public int? Plant_survived_two_year { get; set; }

        /// <summary>
        /// Gets or Sets No_Of_Plants.
        /// </summary>
        public int? No_Of_Plants { get; set; }

        /// <summary>
        /// Gets or Sets Mushroom_productivity.
        /// </summary>
        public decimal? Mushroom_productivity { get; set; }

        /// <summary>
        /// Gets or Sets Crop_productivity.
        /// </summary>
        public decimal? Crop_productivity { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int? Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_type.
        /// </summary>
        public string Crop_type { get; set; }

        /// <summary>
        /// Gets or Sets Input_type_id.
        /// </summary>
        public int? Input_type_id { get; set; }

        /// <summary>
        /// Gets or Sets Input_type_name.
        /// </summary>
        public string Input_type_name { get; set; }

        /// <summary>
        /// Gets or Sets Input_type_value.
        /// </summary>
        public string Input_type_value { get; set; }

        /// <summary>
        /// Gets or Sets Input_quantity.
        /// </summary>
        public string Input_quantity { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public string Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public string Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets RecCreatedDate.
        /// </summary>
        public string RecCreatedDate { get; set; }

        /// <summary>
        /// Gets or Sets RecUpdatedDate.
        /// </summary>
        public string RecUpdatedDate { get; set; }
    }

    /// <summary>
    /// Submitted SubActivity List.
    /// </summary>
    public class SubmittedSubActivityList
    {
        /// <summary>
        /// Gets or Sets Sub_activity_id.
        /// </summary>
        public int? Sub_activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_activity_name.
        /// </summary>
        public string Sub_activity_name { get; set; }

        /// <summary>
        /// Gets or Sets Is_subActivity_submitted.
        /// </summary>
        public bool? Is_subActivity_submitted { get; set; }

        /// <summary>
        /// Gets or Sets Sub_activity_count.
        /// </summary>
        public int? Sub_activity_count { get; set; }

        /// <summary>
        /// Gets or Sets Sub_activity_details.
        /// </summary>
        public List<SubActivityDetail> Sub_activity_details { get; set; }
    }
}