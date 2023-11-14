//------------------------------------------------------------------------------
// <copyright file="GamificationController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using static ADIDAS.Model.Entities.Gamification;

    /// <summary>
    /// GamificationController.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GamificationController : ControllerBase
    {
        private readonly IGamificationService gamificationService;
        private readonly ILogger<GamificationController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamificationController"/> class.
        /// gamificationService.
        /// </summary>
        /// <param name="gamificationService">gamificationService.</param>
        /// <param name="logger">logger.</param>
        public GamificationController(IGamificationService gamificationService, ILogger<GamificationController> logger)
        {
            this.gamificationService = gamificationService;
            this.logger = logger;
        }

        /// <summary>
        /// GetearnPoints.
        /// </summary>
        /// <returns>AppPoints.</returns>
        [HttpGet("GetearnPoints")]
        public IActionResult GetearnPoints()
        {
            try
            {
                List<AppPoints> result = this.gamificationService.GetearnPoints();

                if (!result.Any())
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// GetLeaderBoard.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>LeaderBoard.</returns>
        [HttpGet("GetLeaderBoard/{userId}")]
        public IActionResult GetLeaderBoard(string userId)
        {
            try
            {
                LeaderBoard result = this.gamificationService.GetLeaderBoard(userId);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// GetRewardPoints.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>PointsCalculation.</returns>
        [HttpGet("GetRewardPoints/{userId}")]
        public IActionResult GetRewardPoints(int userId)
        {
            try
            {
                PointsCalculation result = this.gamificationService.GetRewardPoints(userId);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// GetOverallRank.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>UserRank.</returns>
        [HttpGet("GetOverallRank/{userId}")]
        public IActionResult GetOverallRank(int userId)
        {
            try
            {
                UserRank result = this.gamificationService.GetOverallRank(userId);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// GetIdeasExistsByQuarter.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>GetIdeasExistsByQuarter Bool Result.</returns>
        [HttpGet("GetIdeasExistsByQuarter/{userId}")]
        public IActionResult GetIdeasExistsByQuarter(int userId)
        {
            try
            {
                bool? result = this.gamificationService.GetIdeasExistsByQuarter(userId);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetQuizzes.
        /// </summary>
        /// <returns>QuestionType.</returns>
        [HttpGet("GetQuizzes")]
        public IActionResult GetQuizzes()
        {
            try
            {
                List<QuestionType> result = this.gamificationService.GetQuizzes();

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// GetIdeasByUserID.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>IdeasByUserID.</returns>
        [HttpGet("GetIdeasByUserID/{userId}")]
        public IActionResult GetIdeasByUserID(int userId)
        {
            try
            {
                List<IdeasByUserID> result = this.gamificationService.GetIdeasByUserID(userId);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// GetQuizByUserIdOrWeek.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>gamificationService Bool Result.</returns>
        [HttpGet("GetQuizByUserIdOrWeek/{userId}")]
        public IActionResult GetQuizByUserIdOrWeek(int userId)
        {
            try
            {
                bool result = this.gamificationService.GetQuizByUserIdOrWeek(userId);

                if (result == null)
                {
                    return NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// GetCreativeIdeasToReview.
        /// </summary>
        /// <param name="user_Id">user_Id.</param>
        /// <param name="status">status.</param>
        /// <param name="start_date">start_date.</param>
        /// <param name="end_date">end_date.</param>
        /// <param name="designation">designation.</param>
        /// <returns>CreativeIdeasToReviewModel.</returns>
        [HttpGet("GetCreativeIdeasToReview/{userId}/{status}/{start_date}/{end_date}/{designation}")]
        public IActionResult GetCreativeIdeasToReview(string userId, string status, string start_date, string end_date, string designation)
        {
            try
            {
                List<CreativeIdeasToReviewModel> result = this.gamificationService.GetCreativeIdeasToReview(userId, status, start_date, end_date, designation);
                if (!result.Any())
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// SearchCreativeIdeas.
        /// </summary>
        /// <param name="creativeIdeasAdvanceSearchModel">creativeIdeasAdvanceSearchModel.</param>
        /// <returns>SearchCreativeIdeas CreativeIdeasToReviewModel List.</returns>
        [HttpPost("SearchCreativeIdeas")]
        public IActionResult SearchCreativeIdeas([FromBody] CreativeIdeasAdvanceSearchModel creativeIdeasAdvanceSearchModel)
        {
            try
            {
                List<CreativeIdeasToReviewModel> result = this.gamificationService.SearchCreativeIdeas(creativeIdeasAdvanceSearchModel);
                if (!result.Any())
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// PostCreativeIdeas.
        /// </summary>
        /// <param name="creativeIdeas">creativeIdeas.</param>
        /// <returns>Result.</returns>
        [HttpPost("PostCreativeIdeas")]
        public IActionResult PostCreativeIdeas(CreativeIdeas creativeIdeas)
        {
            try
            {
                long result = this.gamificationService.InsertCreativeIdeas(creativeIdeas);

                if (result == 0)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// PostUserPoints.
        /// </summary>
        /// <param name="userPoints">userPoints.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostUserPoints")]
        public IActionResult PostUserPoints(List<DtoUserPoints> userPoints)
        {
            try
            {
                int result = this.gamificationService.InsertUserPoints(userPoints);

                if (result == 0)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// DeleteIdea.
        /// </summary>
        /// <param name="idea">idea.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("DeleteIdea")]
        public IActionResult DeleteIdea([FromBody] Idea idea)
        {
            try
            {
                int result = this.gamificationService.DeleteIdea(idea);
                if (result == 1)
                {
                    return this.Ok("Delete Successful");
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// PostAnswer.
        /// </summary>
        /// <param name="quizAnswer">quizAnswer.</param>
        /// <returns>QuizResponse.</returns>
        [HttpPost("PostAnswer")]
        public IActionResult PostAnswer([FromBody] List<QuizAnswer> quizAnswer)
        {
            try
            {
                List<QuizResponse> result = this.gamificationService.PostAnswer(quizAnswer);
                if (result == null || result.Count == 0)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// UpdateCreativeIdeas.
        /// </summary>
        /// <param name="updateIdeasModels">updateIdeasModels.</param>
        /// <returns>UpdateResponse.</returns>
        [HttpPost("UpdateCreativeIdeas")]
        public IActionResult UpdateCreativeIdeas(List<UpdateIdeasModel> updateIdeasModels)
        {
            try
            {
                List<UpdateResponse> updateModel = this.gamificationService.UpdateCreativeIdeas(updateIdeasModels);
                if (updateModel.Count > 0)
                {
                    return this.Ok(updateModel);
                }

                return this.Ok(updateModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Feedback Details.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="fromDate">fromDate.</param>
        /// <param name="toDate">toDate.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List FeedBackDetails.</returns>
        [HttpGet("GetFeedbackDetails/{year}/{fromDate}/{toDate}/{districtId}/{blockId}/{panchayatId}/{designation}")]
        public IActionResult GetFeedbackDetails(int year, string fromDate, string toDate, string districtId, string blockId, string panchayatId, string designation)
        {
            try
            {
                List<ViewFeedback> result = this.gamificationService.GetFeedbackDetails(year, fromDate, toDate, districtId, blockId, panchayatId, designation);
                if (result == null)
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// IsViewFeedbackUser.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>result.</returns>
        [AllowAnonymous]
        [HttpGet("IsViewFeedbackUser/{userid}")]
        public IActionResult IsViewFeedbackUser(string userid)
        {
            try
            {
                var result = this.gamificationService.IsViewFeedbackUser(Convert.ToInt32(userid));

                if (result.Any())
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }
    }
}
