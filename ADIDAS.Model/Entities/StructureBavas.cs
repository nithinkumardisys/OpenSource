// <copyright file="StructureBavas.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// StructureBavas.
    /// </summary>
    public class StructureBavas
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Structure_name.
        /// </summary>
        public string Structure_name { get; set; }

        /// <summary>
        /// Gets or Sets Namemandatoryflag.
        /// </summary>
        public string Namemandatoryflag { get; set; }

        /// <summary>
        /// Gets or Sets Addressmandatoryflag.
        /// </summary>
        public string Addressmandatoryflag { get; set; }

        /// <summary>
        /// Gets or Sets Capacitymandatoryflag.
        /// </summary>
        public string Capacitymandatoryflag { get; set; }

        /// <summary>
        /// Gets or Sets UnitofMeasure.
        /// </summary>
        public string UnitofMeasure { get; set; }

        /// <summary>
        /// Gets or Sets ReccreateduserId.
        /// </summary>
        public int ReccreateduserId { get; set; }

        /// <summary>
        /// Gets or Sets Structure_description.
        /// </summary>
        public string Structure_description { get; set; }
    }
}
