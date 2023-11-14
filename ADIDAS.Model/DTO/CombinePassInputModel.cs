//------------------------------------------------------------------------------
// <copyright file="CombinePassInputModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// CombinePassInputModel.
    /// </summary>
    public class CombinePassInputModel
    {
        /// <summary>
        /// Gets or Sets Year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or Sets SeletedSeason.
        /// </summary>
        public string Season { get; set; }

        /// <summary>
        /// Gets or Sets SelectedBlocks.
        /// </summary>
        public int Block { get; set; }

        /// <summary>
        /// Gets or Sets SelectedPanchayats.
        /// </summary>
        public int Panchayat { get; set; }

        /// <summary>
        /// Gets or Sets SelectedDistrict.
        /// </summary>
        public int District { get; set; }

        /// <summary>
        /// Gets or Sets SeletedTemplate.
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary>
        public string Title { get; set; }
    }
}
