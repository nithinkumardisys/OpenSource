//------------------------------------------------------------------------------
// <copyright file="AgmarknetService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.DataServices
{
    using System;
    using ADIDAS.Model.DTO;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    /// <summary>
    /// Agmarknet Service.
    /// </summary>
    public class AgmarknetService : IAgmarknetService
    {
        private readonly IAgmarknetRepository agmarknetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgmarknetService"/> class.
        /// AgmarknetService.
        /// </summary>
        /// <param name="agmarknetRepository">agmarknetRepository.</param>
        public AgmarknetService(IAgmarknetRepository agmarknetRepository)
        {
            this.agmarknetRepository = agmarknetRepository;
        }

        /// <summary>
        /// Get Agmarknet Arrival Data
        /// </summary>
        /// <param name="reportedDate">Reporting Date</param>
        /// <returns>List Of Arrival Data</returns>
        public AgmarknetArrivalData GetAgmarknetArrivalData(DateTime reportedDate)
        {
            return this.agmarknetRepository.GetAgmarknetArrivalData(reportedDate);
        }

        /// <summary>
        /// Get Agmarknet Price Data
        /// </summary>
        /// <param name="reportedDate">Reporting Date</param>
        /// <returns>List Of Price Data</returns>
        public AgmarknetPriceData GetAgmarknetPriceData(DateTime reportedDate)
        {
            return this.agmarknetRepository.GetAgmarknetPriceData(reportedDate);
        }
    }
}
