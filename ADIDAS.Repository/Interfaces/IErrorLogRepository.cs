//------------------------------------------------------------------------------
// <copyright file="IErrorLogRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using ADIDAS.Model.DTO;

    /// <summary>
    /// IErrorLogRepository.
    /// </summary>
    public interface IErrorLogRepository
    {
        /// <summary>
        /// AuditErrorLog.
        /// </summary>
        /// <param name="errorModel">errorModel.</param>
        /// <returns>Bool Value.</returns>
        bool AuditErrorLog(ErrorModel errorModel);
    }
}
