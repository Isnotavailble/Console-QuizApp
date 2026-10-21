using System;
using System.Collections.Generic;
using System.Text;

namespace QuizConsoleApp
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string QuestionStatement { get; set; }
        public string[] Answers { get; set; }
        public string CorrectAnswer { get; set; }
        
        public QuestionDTO(int id, string questionStatement, string[] answers, string correctAnswer)
        {
            this.Id = id;
            this.QuestionStatement = questionStatement;
            this.Answers = answers;
            this.CorrectAnswer = correctAnswer;
        }

        public QuestionDTO(string questionStatement, string[] answers, string correctAnswer)
        {
            this.QuestionStatement = questionStatement;
            this.Answers = answers;
            this.CorrectAnswer = correctAnswer;
        }
    }
}
