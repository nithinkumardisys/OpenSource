//------------------------------------------------------------------------------
// <copyright file="SoilConservationCreateRequest.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Create Soil Conservation Create Request Details.
    /// </summary>
    public class SoilConservationCreateRequest
    {
        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int? Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Activity_id.
        /// </summary>
        public int? Activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_activity_id.
        /// </summary>
        public int? Sub_activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Is_final_submission.
        /// </summary>
        public bool? Is_final_submission { get; set; }

        /// <summary>
        /// Gets or Sets Is_activity_submitted.
        /// </summary>
        public bool? Is_activity_submitted { get; set; }

        /// <summary>
        /// Gets or Sets Is_subActivity_submitted.
        /// </summary>
        public bool? Is_subActivity_submitted { get; set; }

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
        public int? Physical_target { get; set; }

        /// <summary>
        /// Gets or Sets Financial_target.
        /// </summary>
        public double? Financial_target { get; set; }

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
        public string Date_of_starting { get; set; }

        /// <summary>
        /// Gets or Sets Due_date_of_completion.
        /// </summary>
        public string Due_date_of_completion { get; set; }

        /// <summary>
        /// Gets or Sets Estimated_budget.
        /// </summary>
        public double? Estimated_budget { get; set; }

        /// <summary>
        /// Gets or Sets Project_irrigated_area.
        /// </summary>
        public double? Project_irrigated_area { get; set; }

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
        public string Actual_date_of_completion { get; set; }

        /// <summary>
        /// Gets or Sets Actual_expenditure.
        /// </summary>
        public double? Actual_expenditure { get; set; }

        /// <summary>
        /// Gets or Sets Actual_irrigated_area.
        /// </summary>
        public double? Actual_irrigated_area { get; set; }

        /// <summary>
        /// Gets or Sets Youtube_story_link.
        /// </summary>
        public string Youtube_story_link { get; set; }

        /// <summary>
        /// Gets or Sets Date_Of_Activity.
        /// </summary>
        public string Date_Of_Activity { get; set; }

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
        public double? TotalCoveredArea { get; set; }

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
        public double? Mushroom_productivity { get; set; }

        /// <summary>
        /// Gets or Sets Crop_productivity.
        /// </summary>
        public double? Crop_productivity { get; set; }

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
        public int? Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int? Rec_updated_userid { get; set; }

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
    /// Post Caste details.
    /// </summary>
    public class Caste
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
        /// Gets or Sets Ebc_male.
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

    public class PostMale : Caste
    {
    }

    /// <summary>
    /// Post Female details.
    /// </summary>
    public class PostFemale : Caste
    {
    }

    /// <summary>
    /// Post others details.
    /// </summary>
    public class PostOthers : Caste
    {
    }
}
