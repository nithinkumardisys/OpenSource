//------------------------------------------------------------------------------
// <copyright file="IAgmarknetService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.Interfaces
{
    using System;
    using ADIDAS.Model.DTO;

    /// <summary>
    /// Interface AgmarknetService.
    /// </summary>
    public interface IAgmarknetService
    {
        /// <summary>
        /// GetAgmarknetArrivalData.
        ///  </summary>
        /// <param name="reportedDate">reportedDate.</param>
        /// <returns><see cref="GetAgmarknetArrivalData"/>.</returns>
        AgmarknetArrivalData GetAgmarknetArrivalData(DateTime reportedDate);

        /// <summary>
        /// GetAgmarknetPriceData.
        ///  </summary>
        /// <param name="reportedDate">reportedDate.</param>
        /// <returns><see cref="GetAgmarknetPriceData"/>.</returns>
        AgmarknetPriceData GetAgmarknetPriceData(DateTime reportedDate);
    }
}
