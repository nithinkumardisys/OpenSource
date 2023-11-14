// <copyright file="UserEntity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// UserEntity.
    /// </summary>
    public partial class UserEntity
    {
        /// <summary>
        /// UserEntity.
        /// </summary>
        public UserEntity()
        {
            this.Permissions = new List<PermissionName>();

            this.Groups = new GroupDetails();
        }

        /// <summary>
        /// Gets or Sets User_Id.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Gets or Sets App_Reg_Id.
        /// </summary>
        public string App_Reg_Id { get; set; }

        /// <summary>
        /// Gets or Sets User_Name.
        /// </summary>
        public string User_Name { get; set; }

        /// <summary>
        /// Gets or Sets User_Password.
        /// </summary>
        public string User_Password { get; set; }

        /// <summary>
        /// Gets or Sets First_name.
        /// </summary>
        public string First_name { get; set; }

        /// <summary>
        /// Gets or Sets Last_name.
        /// </summary>
        public string Last_name { get; set; }

        /// <summary>
        /// Gets or Sets Email_id.
        /// </summary>
        public string Email_id { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or Sets Data_entry_flg.
        /// </summary>
        public string Data_entry_flg { get; set; }

        /// <summary>
        /// Gets or Sets Approval_flg.
        /// </summary>
        public string Approval_flg { get; set; }

        /// <summary>
        /// Gets or Sets Hq_flg.
        /// </summary>
        public string Hq_flg { get; set; }

        /// <summary>
        /// Gets or Sets LGDirLst.
        /// </summary>
        public List<LgDir> LGDirLst { get; set; }

        /// <summary>
        /// Gets or Sets Permissions.
        /// </summary>
        public List<PermissionName> Permissions { get; set; }

        /// <summary>
        /// Gets or Sets Groups.
        /// </summary>
        public GroupDetails Groups { get; set; }

        /// <summary>
        /// Gets or Sets City_of_residence.
        /// </summary>
        public string City_of_residence { get; set; }

        /// <summary>
        /// Gets or Sets Phone_num.
        /// </summary>
        public string Phone_num { get; set; }

        /// <summary>
        /// Gets or Sets Preferred_lang.
        /// </summary>
        public string Preferred_lang { get; set; }

        /// <summary>
        /// Gets or Sets Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or Sets Image_file_name.
        /// </summary>
        public string Image_file_name { get; set; }

        /// <summary>
        /// Gets or Sets Image_file_location.
        /// </summary>
        public string Image_file_location { get; set; }

        /// <summary>
        /// Gets or Sets Badge.
        /// </summary>
        public string Badge { get; set; }

        /// <summary>
        /// Gets or Sets Mkt_flg.
        /// </summary>
        public string Mkt_flg { get; set; }

        /// <summary>
        /// Gets or Sets Is_Add_Crop.
        /// </summary>
        public string Is_Add_Crop { get; set; }

        /// <summary>
        /// Gets or Sets IsWatershed.
        /// </summary>
        public string IsWatershed { get; set; }

        /// <summary>
        /// Gets or Sets Points.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or Sets IsFosFlag.
        /// </summary>
        public string IsFosFlag { get; set; }
    }

    /// <summary>
    /// LgDir.
    /// </summary>
    public class LgDir
    {
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
    }
}
