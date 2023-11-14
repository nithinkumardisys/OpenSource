//------------------------------------------------------------------------------
// <copyright file="ErrorService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.DataServices
{
    using ADIDAS.Model.DTO;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    public class ErrorService : IErrorService
    {
        private readonly IErrorLogRepository errorLogRepository;

        public ErrorService(IErrorLogRepository errorLogRepository)
        {
            this.errorLogRepository = errorLogRepository;
        }

        public bool AuditErrorLog(ErrorModel errorModel)
        {
            return this.errorLogRepository.AuditErrorLog(errorModel);
        }
    }
}
