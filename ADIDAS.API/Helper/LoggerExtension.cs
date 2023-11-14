//------------------------------------------------------------------------------
// <copyright file="LoggerExtension.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace Microsoft.Extensions.Logging
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// LoggerExtensions.
    /// </summary>
    public static partial class LoggerExtensions
    {
        /// <summary>
        /// LogError.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="exception">exception.</param>
        public static void LogError(this ILogger logger, Exception exception)
        {
            string stackTraceFileDetails = string.Empty;

            // Get a StackTrace object for the exception
            StackTrace st = new StackTrace(exception, true);
            foreach (StackFrame stackFrame in st.GetFrames())
            {
                if (!string.IsNullOrEmpty(stackFrame.GetFileName()))
                {
                    stackTraceFileDetails += " at the line Number of " + stackFrame.GetFileLineNumber().ToString() + " from the " + stackFrame.GetMethod().Name + string.Empty +
                        " method of " + System.IO.Path.GetFileName(stackFrame.GetFileName()) + ";" + Environment.NewLine;
                }
            }

            string err = Environment.NewLine + "*****************************" + Environment.NewLine;
            err += stackTraceFileDetails + Environment.NewLine;
            err += "Error Message :" + string.Join("      ", exception.Message) + Environment.NewLine;
            err += "*****************************" + Environment.NewLine;

            logger.LogError(err);
        }
    }
}