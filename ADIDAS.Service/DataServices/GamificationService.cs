//------------------------------------------------------------------------------
// <copyright file="GamificationService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;
    using static ADIDAS.Model.Entities.Gamification;

    public class GamificationService : IGamificationService
    {
        private readonly IGamificationRepository gamificationRepo;

        public GamificationService(IGamificationRepository gamificationRepo)
        {
            this.gamificationRepo = gamificationRepo;
        }

        public List<AppPoints> GetearnPoints()
        {
            return this.gamificationRepo.GetearnPoints();
        }

        public LeaderBoard GetLeaderBoard(string userId)
        {
            return this.gamificationRepo.GetLeaderBoard(userId);
        }

        public UserRank GetOverallRank(int userId)
        {
            return this.gamificationRepo.GetOverallRank(userId);
        }

        public PointsCalculation GetRewardPoints(int userId)
        {
            return this.gamificationRepo.GetRewardPoints(userId);
        }

        public long InsertCreativeIdeas(CreativeIdeas ideas)
        {
            return this.gamificationRepo.InsertCreativeIdeas(ideas);
        }

        public int InsertUserPoints(List<DtoUserPoints> userPoints)
        {
            return this.gamificationRepo.InsertUserPoints(userPoints);
        }

        public bool? GetIdeasExistsByQuarter(int userId)
        {
            return this.gamificationRepo.GetIdeasExistsByQuarter(userId);
        }

        public int DeleteIdea(Idea idea)
        {
            return this.gamificationRepo.DeleteIdea(idea);
        }

        public List<QuestionType> GetQuizzes()
        {
            return this.gamificationRepo.GetQuizzes();
        }

        public List<QuizResponse> PostAnswer(List<QuizAnswer> quizAnswer)
        {
            return this.gamificationRepo.PostAnswer(quizAnswer);
        }

        public List<UpdateResponse> UpdateCreativeIdeas(List<UpdateIdeasModel> updateIdeasModels)
        {
            return this.gamificationRepo.UpdateCreativeIdeas(updateIdeasModels);
        }

        public List<IdeasByUserID> GetIdeasByUserID(int userId)
        {
            return this.gamificationRepo.GetIdeasByUserID(userId);
        }

        public bool GetQuizByUserIdOrWeek(int UserId)
        {
            return this.gamificationRepo.GetQuizByUserIdOrWeek(UserId);
        }

        public List<CreativeIdeasToReviewModel> GetCreativeIdeasToReview(string user_Id, string status, string start_date, string end_date, string designation)
        {
            return this.gamificationRepo.GetCreativeIdeasToReview(user_Id, status, start_date, end_date, designation);
        }

        public List<CreativeIdeasToReviewModel> SearchCreativeIdeas(CreativeIdeasAdvanceSearchModel creativeIdeasAdvanceSearchModel)
        {
            return this.gamificationRepo.SearchCreativeIdeas(creativeIdeasAdvanceSearchModel);
        }

        /// <summary>
        /// GetFeedbackDetails.
        /// </summary>
        /// <param name="year">Input param from the user.</param>
        /// <param name="fromDate">fromDate.</param>
        /// <param name="toDate">toDate.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List[ViewFeedback].</returns>
        public List<ViewFeedback> GetFeedbackDetails(int year, string fromDate, string toDate, string districtId, string blockId, string panchayatId, string designation)
        {
            return this.gamificationRepo.GetFeedbackDetails(year, fromDate, toDate, districtId, blockId, panchayatId, designation);
        }

        /// <summary>
        /// Getting Pgs User.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>PgsUser.</returns>
        public List<DisburseEntity> IsViewFeedbackUser(int userid)
        {
            return this.gamificationRepo.IsViewFeedbackUser(userid);
        }
    }
}
