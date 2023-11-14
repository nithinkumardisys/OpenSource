//------------------------------------------------------------------------------
// <copyright file="IErrorService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.Interfaces
{
    using ADIDAS.Model.DTO;

    /// <summary>
    /// IErrorService.
    /// </summary>
    public interface IErrorService
    {
        /// <summary>
        /// AuditErrorLog.
        /// </summary>
        /// <param name="errorModel">errorModel.</param>
        /// <returns>boolean.</returns>
        bool AuditErrorLog(ErrorModel errorModel);
    }
}
