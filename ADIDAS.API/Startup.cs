//------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API
{
    using System;
    using System.Text;
    using ADIDAS.API.ActionFilters;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.DataRepository;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.DataServices;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// Startup.
        /// </summary>
        /// <param name="configuration">configuration</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ExternalApi>(this.Configuration.GetSection("ExternalApi"));
            services.Configure<BlobConfig>(this.Configuration.GetSection("BlobConfig"));
            services.Configure<NotificationConfig>(this.Configuration.GetSection("NotificationConfig"));
            services.Configure<Notifications>(this.Configuration.GetSection("PushNotification"));
            services.Configure<DBSettings>(this.Configuration.GetSection("DBSettings"));
            services.Configure<Jwt>(this.Configuration.GetSection("Jwt"));
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionHandlerAttribute));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.Configuration["Jwt:Key"])),
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  ClockSkew = TimeSpan.Zero,
                  ValidateLifetime = true,
              };
          });
            ConfigureSwagger(services);
            ConfigureHealthCheck(services);
            RegisterDependencies(services);
            services.AddCors();
        }

        /// <summary>
        /// Configure Health Check.
        /// </summary>
        /// <param name="services">services.</param>
        public static void ConfigureHealthCheck(IServiceCollection services)
        {
            services.AddHealthChecks();
        }

        /// <summary>
        /// Configure Swagger.
        /// </summary>
        /// <param name="services">services.</param>
        public static void ConfigureSwagger(IServiceCollection services)
        {
            // Swagger Setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ADIDAAS", Version = "v1" });
            });
        }

        /// <summary>
        /// Register Dependencies.
        /// </summary>
        /// <param name="services">services.</param>
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICropService, CropService>();
            services.AddScoped<ICropRepository, CropRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMiscService, MiscService>();
            services.AddScoped<IMiscRepository, MiscRepository>();
            services.AddScoped<ILgDirRepository, LgDirRepository>();
            services.AddScoped<ILgDirService, LgDirService>();
            services.AddScoped<ICSReportRepository, CSReportDataRepository>();
            services.AddScoped<ICSReportData, CSReportService>();
            services.AddScoped<IGamificationService, GamificationService>();
            services.AddScoped<IGamificationRepository, GamificationRepository>();
            services.AddScoped<IPlantProtectionService, PlantProtectionService>();
            services.AddScoped<IPlantProtectionRepository, PlantProtectionRepository>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<IHorticultureService, HoricultureService>();
            services.AddScoped<IHoricultureRepository, HoricultureRepository>();
            services.AddScoped<IBavasService, BavasService>();
            services.AddScoped<IBavasRepository, BavasRepository>();
            services.AddScoped<IBametiService, BametiService>();
            services.AddScoped<IBametiRepository, BametiRepository>();
            services.AddScoped<IAnnualPlanOutlayService, AnnualPlanOutlayService>();
            services.AddScoped<IAnnualPlanOutlayRepository, AnnualPlanOutlayRepository>();
            services.AddScoped<IAssetManagementService, AssetManagementService>();
            services.AddScoped<IAssetManagementRepository, AssetManagementRepository>();
            services.AddScoped<IOrganicFarmingService, OrganicFarmingService>();
            services.AddScoped<ICombinePassHarvesterService, CombinePassHarvesterService>();
            services.AddScoped<IOrganicFarmingRepository, OrganicFarmingRepository>();
            services.AddScoped<ICombinePassHarvesterRepository, CombinePassHarvesterRepository>();
            services.AddScoped<IPaisService, PaisService>();
            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IAgmarknetService, AgmarknetService>();
            services.AddScoped<IAgmarknetRepository, AgmarknetRepository>();
            services.AddScoped<ISoilConservationService, SoilConservationService>();
            services.AddScoped<ISoilConservationRepository, SoilConservationRepository>();
            services.AddScoped<IFarmersOutreachService, FarmersOutreachService>();
            services.AddScoped<IFarmersOutreachServiceRepository, FarmersOutreachServiceRepository>();
            services.AddScoped<IBeneficiaryService, BeneficiaryService>();
            services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="env">env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "ADIDAS REST APIs");
            });

            app.UseHealthChecks("/health");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
