//------------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.DataRepository
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// BaseRepository.
    /// </summary>
    public class BaseRepository
    {
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// BaseRepository.
        /// </summary>
        /// <param name="config">config.</param>
        public BaseRepository(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// This method is used to retrieve connection string from the configuration.
        /// </summary>
        /// <returns>Connection String.</returns>
        public string GetConnectionString()
        {
            string cs = this.config["DBSettings:ConnectionString"];
            return cs;
        }
    }
}
