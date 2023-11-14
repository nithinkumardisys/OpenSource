//------------------------------------------------------------------------------
// <copyright file="InsAgriAssetModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Ins Agri Asset Model.
    /// </summary>
    public class InsAgriAssetModel
    {
        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }

        /// <summary>
        /// Gets or Sets Type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public string Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Structure_desc.
        /// </summary>
        public string Structure_desc { get; set; }

        /// <summary>
        /// Gets or Sets Capacitymandatoryflag.
        /// </summary>
        public string Capacitymandatoryflag { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string UnitofMeasure { get; set; }
    }
}
