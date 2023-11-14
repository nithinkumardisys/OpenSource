//------------------------------------------------------------------------------
// <copyright file="AuthController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// AuthController.
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenService tokenService;
        private readonly IUserService userService;
        private readonly IOptions<Jwt> jwtOptions;
        private readonly ILogger<AuthController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// AuthController.
        /// </summary>
        /// <param name="tokenService">tokenService.</param>
        /// <param name="userService">userService.</param>
        /// <param name="jwtOptions">jwtOptions.</param>
        /// <param name="logger">logger.</param>
        public AuthController(ITokenService tokenService, IUserService userService, IOptions<Jwt> jwtOptions, ILogger<AuthController> logger)
        {
            this.tokenService = tokenService;
            this.userService = userService;
            this.jwtOptions = jwtOptions;
            this.logger = logger;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="loginModel">loginModel.</param>
        /// <returns>Login Response.</returns>
        [HttpPost]

        // [ValidateAntiForgeryToken]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (loginModel == null)
                {
                    return this.BadRequest("Invalid client request");
                }

                UserEntity userEntity = new UserEntity();
                userEntity.User_Name = loginModel.UserName;
                userEntity.User_Password = loginModel.Password;
                userEntity.App_Reg_Id = string.Empty;
                UserEntity user = this.userService.PostUserDetails(userEntity);
                if (user == null || string.IsNullOrEmpty(user.User_Name))
                {
                    return this.Unauthorized();
                }

                var claims = new List<Claim>();
                string userName = user.First_name + " " + user.Last_name;
                string imageUrl = user.Image_file_location + string.Empty + user.Image_file_name;
                claims.Add(new Claim(ClaimTypes.Name, userName));
                claims.Add(new Claim("loginId", loginModel.UserName));
                claims.Add(new Claim(ClaimTypes.Role, user.Designation));
                claims.Add(new Claim(ClaimTypes.Role, string.IsNullOrEmpty(user.Hq_flg) ? string.Empty : user.Hq_flg));
                claims.Add(new Claim(ClaimTypes.Role, user.User_Name));
                claims.Add(new Claim(ClaimTypes.Role, user.Designation.ToUpper().Trim() + user.Department.ToUpper().Trim()));
                if (user.Designation.Equals("District Agriculture Officer"))
                {
                    var districtId = user.LGDirLst.Select(x => x.District_id).FirstOrDefault();
                    claims.Add(new Claim("DistrictId", districtId.ToString()));
                }

                claims.Add(new Claim("imageURL", imageUrl));
                claims.Add(new Claim("AnnualOutlay", user.Department.ToUpper().Trim() + ", " + user.Designation.ToUpper().Trim() + "," + " " + (user.LGDirLst.Any() ? user.LGDirLst.Select(a => a.District_name?.ToUpper().Trim()).FirstOrDefault() : string.Empty)));
                claims.Add(new Claim("Designation", user.Designation));
                claims.Add(new Claim("DeptDesignation", user.Designation.ToUpper().Trim() + user.Department.ToUpper().Trim()));
                claims.Add(new Claim("userID", user.User_Id.ToString()));
                var accessToken = this.tokenService.GenerateAccessToken(claims);
                var refreshToken = this.tokenService.GenerateRefreshToken();
                this.userService.SaveRefreshToken(loginModel.UserName, refreshToken, DateTime.UtcNow.AddHours(this.jwtOptions.Value.RefershTimeOut));
                return this.Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Unauthorized();
            }
        }

        /// <summary>
        /// Refresh.
        /// </summary>
        /// <param name="tokenApiModel">tokenApiModel.</param>
        /// <returns>Refresh Response.</returns>
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            try
            {
                if (tokenApiModel is null)
                {
                    return this.BadRequest("Invalid client request");
                }

                string accessToken = tokenApiModel.AccessToken;
                string refreshToken = tokenApiModel.RefreshToken;
                var principal = this.tokenService.GetPrincipalFromExpiredToken(accessToken);
                var username = principal.Claims.Where(x => x.Type == "loginId").Select(x => x.Value).FirstOrDefault();
                TokenData user = this.userService.GetToken(username);
                if (user == null || user.Refresh_token != refreshToken || user.Token_expiry_ts <= DateTime.UtcNow)
                {
                    return this.Unauthorized("Token Expired");
                }

                var newAccessToken = this.tokenService.GenerateAccessToken(principal.Claims);
                var newRefreshToken = this.tokenService.GenerateRefreshToken();
                this.userService.SaveRefreshToken(username, newRefreshToken, DateTime.UtcNow.AddHours(this.jwtOptions.Value.RefershTimeOut));
                return this.Ok(new
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.BadRequest("Invalid client request");
            }
        }
    }
}
