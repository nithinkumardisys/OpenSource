// <copyright file="SeasonInfo.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// SeasonInfo.
    /// </summary>
    public class SeasonInfo
    {
        public string Season_name { get; set; }

        public string Season_year { get; set; }

        public DateTime Start_date { get; set; }

        public DateTime End_date { get; set; }

        public string Season_Status { get; set; }
    }
}
