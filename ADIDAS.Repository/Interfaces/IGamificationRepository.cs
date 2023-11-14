//------------------------------------------------------------------------------
// <copyright file="IGamificationRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using static ADIDAS.Model.Entities.Gamification;

    /// <summary>
    /// IGamificationRepository.
    /// </summary>
    public interface IGamificationRepository
    {
        /// <summary>
        /// GetearnPoints.
        /// </summary>
        /// <returns>AppPoints.</returns>
        List<AppPoints> GetearnPoints();

        /// <summary>
        /// InsertCreativeIdeas.
        /// </summary>
        /// <param name="ideas">ideas.</param>
        /// <returns>Response.</returns>
        long InsertCreativeIdeas(CreativeIdeas ideas);

        /// <summary>
        /// InsertUserPoints.
        /// </summary>
        /// <param name="userPointsList">userPointsList.</param>
        /// <returns>Response.</returns>
        int InsertUserPoints(List<DtoUserPoints> userPointsList);

        /// <summary>
        /// GetLeaderBoard.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>LeaderBoard.</returns>
        LeaderBoard GetLeaderBoard(string userId);

        /// <summary>
        /// GetRewardPoints.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>PointsCalculation.</returns>
        PointsCalculation GetRewardPoints(int userId);

        /// <summary>
        /// GetOverallRank.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>UserRank.</returns>
        UserRank GetOverallRank(int userId);

        /// <summary>
        /// GetIdeasExistsByQuarter.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>Bool Value.</returns>
        bool? GetIdeasExistsByQuarter(int userId);

        /// <summary>
        /// DeleteIdea.
        /// </summary>
        /// <param name="idea">idea.</param>
        /// <returns>Values.</returns>
        int DeleteIdea(Idea idea);

        /// <summary>
        /// GetQuizzes.
        /// </summary>
        /// <returns>QuestionType.</returns>
        List<QuestionType> GetQuizzes();

        /// <summary>
        /// PostAnswer.
        /// </summary>
        /// <param name="quizAnswer">quizAnswer.</param>
        /// <returns>QuizResponse.</returns>
        List<QuizResponse> PostAnswer(List<QuizAnswer> quizAnswer);

        /// <summary>
        /// UpdateCreativeIdeas.
        /// </summary>
        /// <param name="updateIdeasModels">updateIdeasModels.</param>
        /// <returns>UpdateResponse.</returns>
        List<UpdateResponse> UpdateCreativeIdeas(List<UpdateIdeasModel> updateIdeasModels);

        /// <summary>
        /// GetIdeasByUserID.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>IdeasByUserID.</returns>
        List<IdeasByUserID> GetIdeasByUserID(int userId);

        /// <summary>
        /// GetQuizByUserIdOrWeek.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>Bool Value.</returns>
        bool GetQuizByUserIdOrWeek(int userId);

        /// <summary>
        /// GetCreativeIdeasToReview.
        /// </summary>
        /// <param name="user_Id">user_Id.</param>
        /// <param name="status">status.</param>
        /// <param name="start_date">start_date.</param>
        /// <param name="end_date">end_date.</param>
        /// <param name="designation">designation.</param>
        /// <returns>CreativeIdeasToReviewModel.</returns>
        List<CreativeIdeasToReviewModel> GetCreativeIdeasToReview(string user_Id, string status, string start_date, string end_date, string designation);

        /// <summary>
        /// SearchCreativeIdeas.
        /// </summary>
        /// <param name="creativeIdeasAdvanceSearchModel">creativeIdeasAdvanceSearchModel.</param>
        /// <returns>CreativeIdeasToReviewModel.</returns>
        List<CreativeIdeasToReviewModel> SearchCreativeIdeas(CreativeIdeasAdvanceSearchModel creativeIdeasAdvanceSearchModel);

        /// <summary>
        /// GetFeedbackDetails
        /// </summary>
        /// <param name="year">Input param from the user.</param>
        /// <param name="fromDate">Input param from the user.</param>
        /// <param name="todate">Input param from the user.</param>
        /// <param name="districtId">Input param from the user.</param>
        /// <param name="blockId">Input param from the user.</param>
        /// <param name="panchayatId">Input param from the user.</param>
        /// <param name="designation">Input param from the user.</param>
        /// <returns>List[ViewFeedback].</returns>
        List<ViewFeedback> GetFeedbackDetails(int year, string fromDate, string toDate, string districtId, string blockId, string panchayatId, string designation);

        /// <summary>
        /// Get View Feedback User Details.
        /// </summary>
        /// <param name="userid">user_id.</param>
        /// <returns>View Feedback User.</returns>
        List<DisburseEntity> IsViewFeedbackUser(int userid);
    }
}
