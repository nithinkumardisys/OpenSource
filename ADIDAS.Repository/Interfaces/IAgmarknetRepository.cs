//------------------------------------------------------------------------------
// <copyright file="IAgmarknetRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System;
    using ADIDAS.Model.DTO;

    /// <summary>
    /// Interface Agmarknet Repository.
    /// </summary>
    public interface IAgmarknetRepository
    {
        /// <summary>
        /// GetAgmarknetArrivalData.
        /// </summary>
        /// <param name="reportedDate">reportedDate.</param>
        /// <returns>Response.</returns>
        AgmarknetArrivalData GetAgmarknetArrivalData(DateTime reportedDate);

        /// <summary>
        /// GetAgmarknetPriceData.
        /// </summary>
        /// <param name="reportedDate">reportedDate.</param>
        /// <returns>Response.</returns>
        AgmarknetPriceData GetAgmarknetPriceData(DateTime reportedDate);
    }
}
