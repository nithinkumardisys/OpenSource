//------------------------------------------------------------------------------
// <copyright file="Gamification.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Gamification.
    /// </summary>
    public static class Gamification
    {
        /// <summary>
        /// idea.
        /// </summary>
        public class Idea
        {
            /// <summary>
            /// Gets or Sets User_Id.
            /// </summary>
            public int User_Id { get; set; }

            /// <summary>
            /// Gets or Sets Idea_Id.
            /// </summary>
            public int Idea_Id { get; set; }

            /// <summary>
            /// Gets or Sets Idea_Category.
            /// </summary>
            public string Idea_Category { get; set; }
        }

        /// <summary>
        /// Quiz.
        /// </summary>
        public class Quiz
        {
            /// <summary>
            /// Gets or Sets Quiz_id.
            /// </summary>
            public int Quiz_id { get; set; }

            /// <summary>
            /// Gets or Sets Question.
            /// </summary>
            public string Question { get; set; }

            /// <summary>
            /// Gets or Sets Option1.
            /// </summary>
            public string Option1 { get; set; }

            /// <summary>
            /// Gets or Sets Option2.
            /// </summary>
            public string Option2 { get; set; }

            /// <summary>
            /// Gets or Sets Option3.
            /// </summary>
            public string Option3 { get; set; }

            /// <summary>
            /// Gets or Sets Option4.
            /// </summary>
            public string Option4 { get; set; }

            /// <summary>
            /// Gets or Sets Answer.
            /// </summary>
            public string Answer { get; set; }

            /// <summary>
            /// Gets or Sets Rec_created_date.
            /// </summary>
            public DateTime Rec_created_date { get; set; }

            /// <summary>
            /// Gets or Sets Rec_created_user_id.
            /// </summary>
            public int Rec_created_user_id { get; set; }

            /// <summary>
            /// Gets or Sets Week_nm.
            /// </summary>
            public int Week_nm { get; set; }
        }

        /// <summary>
        /// Option.
        /// </summary>
        public class Option
        {
            /// <summary>
            /// Gets or Sets AnswerId.
            /// </summary>
            public int AnswerId { get; set; }

            /// <summary>
            /// Gets or Sets Answer.
            /// </summary>
            public string Answer { get; set; }
        }

        /// <summary>
        /// QuestionType.
        /// </summary>
        public class QuestionType
        {
            /// <summary>
            /// Gets or Sets QuestionId.
            /// </summary>
            public int QuestionId { get; set; }

            /// <summary>
            /// Gets or Sets Question.
            /// </summary>
            public string Question { get; set; }

            /// <summary>
            /// Gets or Sets CorrectAnswerId.
            /// </summary>
            public string CorrectAnswerId { get; set; }

            /// <summary>
            /// Gets or Sets CorrectAnswer.
            /// </summary>
            public string CorrectAnswer { get; set; }

            /// <summary>
            /// Gets or Sets AnswerOption.
            /// </summary>
            public List<Option> AnswerOption { get; set; }
        }

        /// <summary>
        /// QuizAnswer.
        /// </summary>
        public class QuizAnswer
        {
            /// <summary>
            /// Gets or Sets User_Id.
            /// </summary>
            public int User_Id { get; set; }

            /// <summary>
            /// Gets or Sets Quiz_Id.
            /// </summary>
            public int Quiz_Id { get; set; }

            /// <summary>
            /// Gets or Sets Answer_Id.
            /// </summary>
            public int Answer_Id { get; set; }

            /// <summary>
            /// Gets or Sets Rec_created_date.
            /// </summary>
            public DateTime Rec_created_date { get; set; }
        }

        /// <summary>
        /// QuizResponse.
        /// </summary>
        public class QuizResponse
        {
            /// <summary>
            /// Gets or Sets Status.
            /// </summary>
            public string Status { get; set; }

            /// <summary>
            /// Gets or Sets Reason.
            /// </summary>
            public string Reason { get; set; }

            /// <summary>
            /// Gets or Sets QuizId.
            /// </summary>
            public int QuizId { get; set; }

            /// <summary>
            /// Gets or Sets UserId.
            /// </summary>
            public int UserId { get; set; }
        }

        /// <summary>
        /// IdeasByUserID.
        /// </summary>
        public class IdeasByUserID
        {
            /// <summary>
            /// Gets or Sets Idea_id.
            /// </summary>
            public int Idea_id { get; set; }

            /// <summary>
            /// Gets or Sets User_Id.
            /// </summary>
            public int User_Id { get; set; }

            /// <summary>
            /// Gets or Sets Idea_Type.
            /// </summary>
            public string Idea_Type { get; set; }

            /// <summary>
            /// Gets or Sets Idea_Category.
            /// </summary>
            public string Idea_Category { get; set; }

            /// <summary>
            /// Gets or Sets Idea_Description.
            /// </summary>
            public string Idea_Description { get; set; }

            /// <summary>
            /// Gets or Sets Rec_created_date.
            /// </summary>
            public DateTime Rec_created_date { get; set; }

            /// <summary>
            /// Gets or Sets Quarter.
            /// </summary>
            public string Quarter { get; set; }

            /// <summary>
            /// Gets or Sets Shortlisted_flg.
            /// </summary>
            public string Shortlisted_flg { get; set; }

            /// <summary>
            /// Gets or Sets Status.
            /// </summary>
            public string Status { get; set; }
        }
    }

    /// <summary>
    /// UserBadge.
    /// </summary>
    public class UserBadge
    {
        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public int User_id { get; set; }

        /// <summary>
        /// Gets or Sets Points.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or Sets Badge.
        /// </summary>
        public string Badge { get; set; }
    }
}
