//------------------------------------------------------------------------------
// <copyright file="GamificationRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.DataRepository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using static ADIDAS.Model.Entities.Gamification;

    /// <summary>
    /// GamificationRepository.
    /// </summary>
    public class GamificationRepository : BaseRepository, IGamificationRepository
    {
        private static string spInsertCreativeIdeas = "usp_insert_Creative_Ideas";
        private static string spInsertUserPoints = "usp_insert_user_points";
        private static string qnGetIdeasExistsByQuarter = "GetIdeasExistsByQuarter";
        private static string spGetIdeasExistsByQuarter = "usp_getdata_creative_ideas";
        private static string qnDeleteIdea = "DeleteIdea";
        private static string qnGetQuizzes = "GetQuizzes";
        private static string spGetQuizzes = "usp_getdata_app_quiz";
        private static string qnPostAnswer = "PostAnswer";
        private static string qnGetIdeasByUserID = "GetIdeasByUserID";
        private static string spUpdateCreativeIdeas = "usp_update_creative_ideas";
        private static string spSearchCreativeIdeas = "usp_getdata_creative_ideas_web";
        private static string qnGetQuizByUserIdOrWeek = "GetCntQuiz_ByUserIdOrWeek";
        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamificationRepository"/> class.
        /// GamificationRepository.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="options">options.</param>
        public GamificationRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// GetearnPoints.
        /// </summary>
        /// <returns>list.</returns>
        public List<AppPoints> GetearnPoints()
        {
            List<AppPoints> resultset = new List<AppPoints>();
            List<DbParameter> parameters = new List<DbParameter>();
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_earnpoints", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt?.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<AppPoints>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// InsertCreativeIdeas.
        /// </summary>
        /// <param name="ideas">ideas.</param>
        /// <returns>Success Response.</returns>
        public long InsertCreativeIdeas(CreativeIdeas ideas)
        {
            long insertRowsCount = 0;

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = string.IsNullOrEmpty(ideas.User_Id) ? DBNull.Value : (object)ideas.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Idea_Type", Value = string.IsNullOrEmpty(ideas.Idea_Type) ? DBNull.Value : (object)ideas.Idea_Type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Idea_Category", Value = string.IsNullOrEmpty(ideas.Idea_Category) ? DBNull.Value : (object)ideas.Idea_Category, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Idea_Description", Value = string.IsNullOrEmpty(ideas.Idea_Description) ? DBNull.Value : (object)ideas.Idea_Description, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Inserted_Idea_id", Value = DBNull.Value, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Output });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertCreativeIdeas, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            insertRowsCount = Convert.ToInt64(result["@Inserted_Idea_id"]);

            return insertRowsCount;
        }

        /// <summary>
        /// InsertUserPoints.
        /// </summary>
        /// <param name="userPointsList">userPointsList.</param>
        /// <returns>Success Result.</returns>
        public int InsertUserPoints(List<DtoUserPoints> userPointsList)
        {
            foreach (var userPoints in userPointsList)
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = string.IsNullOrEmpty(userPoints.User_id) ? DBNull.Value : (object)userPoints.User_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@points_cat_id", Value = string.IsNullOrEmpty(userPoints.Points_cat_id) ? DBNull.Value : (object)userPoints.Points_cat_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@activity_ts", Value = (object)userPoints.Activity_ts, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@obj_id", Value = string.IsNullOrEmpty(userPoints.Obj_id) ? DBNull.Value : (object)userPoints.Obj_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@obj_type", Value = string.IsNullOrEmpty(userPoints.Obj_type) ? DBNull.Value : (object)userPoints.Obj_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertUserPoints, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            }

            return 1;
        }

        /// <summary>
        /// GetLeaderBoard.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>DB Data Set Values.</returns>
        public LeaderBoard GetLeaderBoard(string userId)
        {
            LeaderBoard leaderBoard = new LeaderBoard();

            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataSet dt = SqlHelper.ExecuteDataSet<SqlConnection>("usp_getdata_leaderboard", parameters, SqlHelper.ExecutionType.Procedure);

            if (dt?.Tables.Count > 0)
            {
                leaderBoard.OverAll = SqlHelper.ConvertDataTableToList<OverallLeaderBoard>(dt.Tables[0]);

                leaderBoard.BlockLeaderBoard = SqlHelper.ConvertDataTableToList<BlockLeaderBoard>(dt.Tables[1]);

                leaderBoard.DistrictLeaderBoard = SqlHelper.ConvertDataTableToList<DistrictLeaderBoard>(dt.Tables[2]);
                leaderBoard.MyTeams = new List<MyTeam>();
                leaderBoard.MyTeams = SqlHelper.ConvertDataTableToList<MyTeam>(dt.Tables[3]);
            }

            return leaderBoard;
        }

        /// <summary>
        /// GetRewardPoints.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>Dataset Results.</returns>
        public PointsCalculation GetRewardPoints(int userId)
        {
            PointsCalculation pointsCalculation = new PointsCalculation();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataSet dt = SqlHelper.ExecuteDataSet<SqlConnection>("usp_getdata_rewardpoints", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt?.Tables.Count > 0)
            {
                pointsCalculation.Total_Points = dt.Tables[0].Rows[0]["Total_Points"] != DBNull.Value ? Convert.ToInt32(dt.Tables[0].Rows[0]["Total_Points"]) : 0;

                pointsCalculation.Today_Points = dt.Tables[1].Rows[0]["Today_Points"] != DBNull.Value ? Convert.ToInt32(dt.Tables[1].Rows[0]["Today_Points"]) : 0;

                pointsCalculation.Yesterday_Points = dt.Tables[2].Rows[0]["Yesterday_Points"] != DBNull.Value ? Convert.ToInt32(dt.Tables[2].Rows[0]["Yesterday_Points"]) : 0;

                pointsCalculation.This_Week_Points = dt.Tables[3].Rows[0]["This_Week_Points"] != DBNull.Value ? Convert.ToInt32(dt.Tables[3].Rows[0]["This_Week_Points"]) : 0;

                pointsCalculation.This_Month_Points = dt.Tables[4].Rows[0]["This_Month_Points"] != DBNull.Value ? Convert.ToInt32(dt.Tables[4].Rows[0]["This_Month_Points"]) : 0;

                pointsCalculation.Previous_Year_Points = dt.Tables[10].Rows[0]["Previous_Year_Points"] != DBNull.Value ? Convert.ToInt32(dt.Tables[10].Rows[0]["Previous_Year_Points"]) : 0;

                pointsCalculation.Previous_Year = dt.Tables[10].Rows[0]["Previous_Year"] != DBNull.Value ? dt.Tables[10].Rows[0]["Previous_Year"].ToString() : string.Empty;

                pointsCalculation.TotalActivityBasedCalculations = SqlHelper.ConvertDataTableToList<TotalActivityBasedCalculation>(dt.Tables[5]);

                pointsCalculation.TotalActivityBasedCalculationsToday = SqlHelper.ConvertDataTableToList<TotalActivityBasedCalculation>(dt.Tables[6]);

                pointsCalculation.TotalActivityBasedCalculationsYesterday = SqlHelper.ConvertDataTableToList<TotalActivityBasedCalculation>(dt.Tables[7]);

                pointsCalculation.TotalActivityBasedCalculationsWeek = SqlHelper.ConvertDataTableToList<TotalActivityBasedCalculation>(dt.Tables[8]);

                pointsCalculation.TotalActivityBasedCalculationsMonth = SqlHelper.ConvertDataTableToList<TotalActivityBasedCalculation>(dt.Tables[9]);
            }

            return pointsCalculation;
        }

        /// <summary>
        /// GetOverallRank.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>UserRank Details.</returns>
        public UserRank GetOverallRank(int userId)
        {
            UserRank userRank = new UserRank();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataSet dt = SqlHelper.ExecuteDataSet<SqlConnection>("usp_getdata_overallrank ", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt?.Tables.Count > 0)
            {
                userRank.OverallRank = SqlHelper.ConvertDataTableToList<OverallUserRank>(dt.Tables[0]).FirstOrDefault();

                userRank.Today_Points = dt.Tables[1].Rows[0]["Today_Points"] != DBNull.Value ? Convert.ToInt32(dt.Tables[1].Rows[0]["Today_Points"]) : 0;

                userRank.Yesterday_Points = dt.Tables[2].Rows[0]["Yesterday_Points"] != DBNull.Value ? Convert.ToInt32(dt.Tables[2].Rows[0]["Yesterday_Points"]) : 0;

                userRank.This_Week_Points = dt.Tables[3].Rows[0]["This_Week_Points"] != DBNull.Value ? Convert.ToInt32(dt.Tables[3].Rows[0]["This_Week_Points"]) : 0;
            }

            return userRank;
        }

        /// <summary>
        /// GetIdeasExistsByQuarter.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>Bool Value.</returns>
        public bool? GetIdeasExistsByQuarter(int userId)
        {
            bool? result = false;
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetIdeasExistsByQuarter, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Idea_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Idea_Type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Idea_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetIdeasExistsByQuarter, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "1")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// DeleteIdea.
        /// </summary>
        /// <param name="idea">idea.</param>
        /// <returns>Success Failure Status values.</returns>
        public int DeleteIdea(Idea idea)
        {
            int insertRowsCount = 0;
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnDeleteIdea, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = idea.User_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Idea_Id", Value = idea.Idea_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Idea_Type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Idea_Category", Value = idea.Idea_Category, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetIdeasExistsByQuarter, dbparams, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result["RowsAffected"];

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetQuizzes.
        /// </summary>
        /// <returns>Lists.</returns>
        public List<QuestionType> GetQuizzes()
        {
            List<QuestionType> questions = new List<QuestionType>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetQuizzes, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@quiz_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@answer_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetQuizzes, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt?.Rows.Count > 0)
            {
                List<Quiz> quizzes = SqlHelper.ConvertDataTableToList<Quiz>(dt);
                foreach (Quiz quiz in quizzes)
                {
                    List<Option> lstoptions = new List<Option>();
                    for (int i = 0; i <= 3; i++)
                    {
                        Option option = new Option();
                        option.AnswerId = i + 1;
                        option.Answer = (i + 1) == 1 ? quiz.Option1 : ((i + 1) == 2 ? quiz.Option2 : ((i + 1) == 3 ? quiz.Option3 : ((i + 1) == 4 ? quiz.Option4 : string.Empty)));
                        lstoptions.Add(option);
                    }

                    QuestionType questionType = new QuestionType();
                    questionType.QuestionId = quiz.Quiz_id;
                    questionType.Question = quiz.Question;
                    questionType.CorrectAnswerId = quiz.Answer;
                    questionType.CorrectAnswer = quiz.Answer == "1" ? quiz.Option1 : (quiz.Answer == "2" ? quiz.Option2 : (quiz.Answer == "3" ? quiz.Option3 : (quiz.Answer == "4" ? quiz.Option4 : string.Empty)));
                    questionType.AnswerOption = lstoptions;
                    questions.Add(questionType);
                }
            }

            return questions;
        }

        /// <summary>
        /// PostAnswer.
        /// </summary>
        /// <param name="quizAnswer">quizAnswer.</param>
        /// <returns>QuizResponse Results.</returns>
        public List<QuizResponse> PostAnswer(List<QuizAnswer> quizAnswer)
        {
            int insertRowsCount = 0;
            List<QuizResponse> res = new List<QuizResponse>();

            foreach (QuizAnswer item in quizAnswer)
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPostAnswer, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = item.User_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@quiz_Id", Value = item.Quiz_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@answer_Id", Value = item.Answer_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Rec_created_date", Value = item.Rec_created_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetQuizzes, dbparams, SqlHelper.ExecutionType.Procedure);
                insertRowsCount += result["RowsAffected"];

                string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                if (!string.IsNullOrEmpty(spOut))
                {
                    QuizResponse quizResponse = new QuizResponse();
                    foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                        if (splitteddata[0].Trim().Equals("Status"))
                        {
                            quizResponse.Status = splitteddata[1].ToString();
                        }
                        else if (splitteddata[0].Trim().Equals("Reason"))
                        {
                            quizResponse.Reason = splitteddata[1].ToString();
                        }
                        else if (splitteddata[0].Trim().Equals("Quiz Id"))
                        {
                            quizResponse.QuizId = Convert.ToInt32(splitteddata[1]);
                        }
                        else if (splitteddata[0].Trim().Equals("UserId"))
                        {
                            quizResponse.UserId = Convert.ToInt32(splitteddata[1]);
                        }
                    }

                    res.Add(quizResponse);
                }
            }

            return res;
        }

        /// <summary>
        /// GetIdeasByUserID.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>List Vaues.</returns>
        public List<IdeasByUserID> GetIdeasByUserID(int userId)
        {
            List<IdeasByUserID> result = new List<IdeasByUserID>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetIdeasByUserID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Idea_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Idea_Type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Idea_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetIdeasExistsByQuarter, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<IdeasByUserID>(dt);
                foreach (var item in result)
                {
                    item.Shortlisted_flg = string.IsNullOrEmpty(item.Shortlisted_flg) ? item.Shortlisted_flg = "N" : item.Shortlisted_flg;
                }
            }

            return result;
        }

        /// <summary>
        /// UpdateCreativeIdeas.
        /// </summary>
        /// <param name="updateIdeasModels">updateIdeasModels.</param>
        /// <returns>Response Values.</returns>
        public List<UpdateResponse> UpdateCreativeIdeas(List<UpdateIdeasModel> updateIdeasModels)
        {
            List<UpdateResponse> result = new List<UpdateResponse>();

            foreach (var idea in updateIdeasModels)
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Idea_id", Value = idea.Idea_id == 0 ? DBNull.Value : (object)idea.Idea_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = idea.User_Id == 0 ? DBNull.Value : (object)idea.User_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@start_date", Value = idea.Start_date == DateTime.MinValue ? DBNull.Value : (object)idea.Start_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@end_date", Value = idea.End_date == DateTime.MinValue ? DBNull.Value : (object)idea.End_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@shortlisted_flg", Value = string.IsNullOrEmpty(idea.Shortlisted_flg) ? DBNull.Value : (object)idea.Shortlisted_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = idea.District_Id == 0 ? DBNull.Value : (object)idea.District_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@comment", Value = string.IsNullOrEmpty(idea.Comment) ? DBNull.Value : (object)idea.Comment, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spUpdateCreativeIdeas, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string gdata = dt.Rows[0]["Column1"].ToString();
                    UpdateResponse updateResponse = new UpdateResponse();
                    updateResponse.Status = gdata;
                    updateResponse.IdeaId = idea.Idea_id.ToString();
                    updateResponse.UserId = idea.User_Id.ToString();
                    result.Add(updateResponse);
                }
            }

            return result;
        }

        /// <summary>
        /// designation.
        /// </summary>
        /// <param name="creativeIdeasAdvanceSearchModel">creativeIdeasAdvanceSearchModel.</param>
        /// <returns>User Details List.</returns>
        public List<CreativeIdeasToReviewModel> SearchCreativeIdeas(CreativeIdeasAdvanceSearchModel creativeIdeasAdvanceSearchModel)
        {
            List<CreativeIdeasToReviewModel> usersDetails = new List<CreativeIdeasToReviewModel>();

            List<DbParameter> paramsUserInfo = new List<DbParameter>();
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = creativeIdeasAdvanceSearchModel.User_Id == 0 ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.User_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@status ", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Status) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Status, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@start_date", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Start_date) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Start_date, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@end_date", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.End_date) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.End_date, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@dao_designation", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Dao_designation) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Dao_designation, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@first_name", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.First_name) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.First_name, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@last_name", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Last_name) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Last_name, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@usr_designation", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Usr_designation) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Usr_designation, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@Idea_Category", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.IdeaCategory) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.IdeaCategory, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@department", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Department) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Department, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@gender", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Gender) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Gender, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@block_name", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Block_name) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Block_name, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@panchayat_name", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Panchayat_name) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Panchayat_name, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@Date_applied_from", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Date_applied_from) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Date_applied_from, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@Date_applied_to", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Date_applied_to) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Date_applied_to, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            paramsUserInfo.Add(new SqlParameter { ParameterName = "@email_id", Value = string.IsNullOrEmpty(creativeIdeasAdvanceSearchModel.Email_id) ? DBNull.Value : (object)creativeIdeasAdvanceSearchModel.Email_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_creative_ideas_web", paramsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt?.Rows.Count > 0)
            {
                {
                    List<CreativeIdeasToReviewModel> list = SqlHelper.ConvertDataTableToList<CreativeIdeasToReviewModel>(dt);

                    usersDetails = list.Select(x => new CreativeIdeasToReviewModel
                    {
                        Idea_id = x.Idea_id,
                        Name = x.Name,
                        District_name = x.District_name,
                        Designation = x.Designation,
                        Department = x.Department,
                        Block_Names = x.Block_Names,
                        Panchayat_Names = x.Panchayat_Names,
                        Idea_Category = x.Idea_Category,
                        Idea_Type = x.Idea_Type,
                        Quarter = x.Quarter,
                        Idea_Description = x.Idea_Description,
                        Rec_created_date = x.Rec_created_date,
                        Shortlisted_flg = x.Shortlisted_flg,
                    }).GroupBy(x => x.Idea_id).Select(x => x.First()).ToList();
                }
            }

            return usersDetails;
        }

        /// <summary>
        /// GetCreativeIdeasToReview.
        /// </summary>
        /// <param name="user_Id">user_Id.</param>
        /// <param name="status">status.</param>
        /// <param name="start_date">start_date.</param>
        /// <param name="end_date">end_date.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List.</returns>
        public List<CreativeIdeasToReviewModel> GetCreativeIdeasToReview(string user_Id, string status, string start_date, string end_date, string designation)
        {
            List<CreativeIdeasToReviewModel> result = new List<CreativeIdeasToReviewModel>();
            List<DbParameter> dbparams = new List<DbParameter>();

            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = user_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@status", Value = string.IsNullOrEmpty(status) ? DBNull.Value : (object)status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            if (status == "Y")
            {
                dbparams.Add(new SqlParameter { ParameterName = "@hq_flag", Value = string.IsNullOrEmpty(status) ? DBNull.Value : (object)status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }
            else if (status == "W")
            {
                dbparams.Add(new SqlParameter { ParameterName = "@hq_flag", Value = "Y", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }

            dbparams.Add(new SqlParameter { ParameterName = "@start_date", Value = string.IsNullOrEmpty(start_date) ? DBNull.Value : (object)start_date, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@end_date", Value = string.IsNullOrEmpty(end_date) ? DBNull.Value : (object)end_date, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@dao_designation", Value = string.IsNullOrEmpty(designation) ? DBNull.Value : (object)designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spSearchCreativeIdeas, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<CreativeIdeasToReviewModel>(dt);
            }

            return result;
        }

        /// <summary>
        /// GetQuizByUserIdOrWeek.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>Result.</returns>
        public bool GetQuizByUserIdOrWeek(int userId)
        {
            bool res = false;
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetQuizByUserIdOrWeek, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@quiz_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@answer_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetQuizzes, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                res = Convert.ToInt32(dt.Rows[0][0]) != 0;
            }

            return res;
        }

        /// <summary>
        /// GetFeedbackDetails.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="fromDate">fromDate.</param>
        /// <param name="toDate">toDate.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>ViewFeedback.</returns>
        public List<ViewFeedback> GetFeedbackDetails(int year, string fromDate, string toDate, string districtId, string blockId, string panchayatId, string designation)
        {
            List<ViewFeedback> list = new List<ViewFeedback>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetViewFeedbackDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@from_date", Value = DateTime.Parse(fromDate), SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@to_date", Value = DateTime.Parse(toDate), SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_view_feed_back", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = (from DataRow dr in dt.Rows
                        select new ViewFeedback()
                        {
                            DistrictName = dr["DistrictName"].ToString(),
                            BlockName = dr["BlockName"].ToString(),
                            PanchayatName = dr["PanchayatName"].ToString(),
                            Name = dr["Name"].ToString(),
                            Designation = dr["designation"].ToString(),
                            Date = Convert.ToDateTime(dr["Date"]),
                            Feedback = dr["feedback"].ToString(),
                            SerialNo = dr["Sno"].ToString(),
                            DivisionName = dr["DivisionName"].ToString(),
                            SubDivisionName = dr["SubDivisionName"].ToString(),
                        }).ToList();
            }

            return list;
        }

        /// <summary>
        /// Getting View Feedback User Details.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>User Details.</returns>
        public List<DisburseEntity> IsViewFeedbackUser(int userid)
        {
            var enitity = new List<DisburseEntity>();
            var parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetViewFeedbackDesignation", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_view_feed_back", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                enitity = SqlHelper.ConvertDataTableToList<DisburseEntity>(dt);
            }

            return enitity;
        }
    }
}