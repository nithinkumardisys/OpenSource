//------------------------------------------------------------------------------
// <copyright file="HeaderDetailWrapper.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace DepartmentOfAgriculture.Admin.Models.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// HeaderDetailWrapper.
    /// </summary>
    public class HeaderDetailWrapper
    {
        public HeaderDetailWrapper()
        {
            this.Header = new List<DtoPostBametiGridData>();

            this.Detail = new List<DtoPostBametiGridData>();
        }

        public List<DtoPostBametiGridData> Header { get; set; }

        public List<DtoPostBametiGridData> Detail { get; set; }
    }
}
