//------------------------------------------------------------------------------
// <copyright file="ExceptionHandlerAttribute.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.ActionFilters
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using ADIDAS.Model.DTO;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// ExceptionHandlerAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Readonly error service property for inject.
        /// </summary>
        private IErrorService _errorService { get; }

        private readonly ILogger<ExceptionHandlerAttribute> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerAttribute"/> class.
        /// </summary>
        /// <param name="errorService">Inject Error service.</param>
        /// <param name="logger">logger.</param>
        public ExceptionHandlerAttribute(IErrorService errorService, ILogger<ExceptionHandlerAttribute> logger)
        {
            this._errorService = errorService;
            this._logger = logger;
        }

        /// <summary>
        /// Overrided this method to capture the exception from entire.
        /// controller of this API application and stored the details in SQL log table.
        /// </summary>
        /// <param name="context">Contains the details of Exception.</param>
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            // Populate the error details in model class and pass this info into DB.
            ErrorModel errorModel = new ErrorModel()
            {
                UserId = "0",
                ActivityDesription = context.Exception.Message,
                ActivityStatus = "failed",
                ActivityTF = DateTime.Now,
                ActivitySource = context.RouteData.Values["controller"].ToString(),
                ApiSource = context.RouteData.Values["action"].ToString(),
                ActivityType = "Error",
                RetValue = 0,
            };
            this.HandleExceptionDetails(context);
        }

        /// <summary>
        /// Handle exception message based on type info.
        /// </summary>
        /// <param name="context">context.</param>
        private void HandleExceptionDetails(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            Type type = context.Exception.GetType();
            string message = string.Empty;
            if (type == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (type == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            else if (type == typeof(System.Data.SqlClient.SqlException))
            {
                status = HttpStatusCode.InternalServerError;
            }
            else if (type == typeof(System.FormatException) || type == typeof(System.InvalidCastException))
            {
                status = HttpStatusCode.BadRequest;
            }
            else
            {
                status = HttpStatusCode.NotFound;
            }

            context.ExceptionHandled = true;

            string stackTraceFileDetails = string.Empty;

            // Get a StackTrace object for the exception
            StackTrace st = new StackTrace(context.Exception, true);
            foreach (StackFrame stackFrame in st.GetFrames())
            {
                if (!string.IsNullOrEmpty(stackFrame.GetFileName()))
                {
                    stackTraceFileDetails += " at the line Number of " + stackFrame.GetFileLineNumber().ToString() + " from the " + stackFrame.GetMethod().Name + string.Empty +
                        " method of " + System.IO.Path.GetFileName(stackFrame.GetFileName()) + ";" + Environment.NewLine;
                }
            }

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            string err = Environment.NewLine + "*****************************" + Environment.NewLine;
            err += stackTraceFileDetails + Environment.NewLine;
            err += "Error Message : " + Environment.NewLine;
            err += message + " " + context.Exception.Message + Environment.NewLine;
            err += "*****************************" + Environment.NewLine;
            response.WriteAsync(err);
            this._logger.LogDebug(err);
        }
    }
}