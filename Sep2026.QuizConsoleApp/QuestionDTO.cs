using System;
using System.Collections.Generic;
using System.Text;

namespace QuizConsoleApp
{
    public class QuestionDTO
    {
        private int id;
        private string questionStatement;
        private string[] answers;
        private string correctAnswer;
        public QuestionDTO(int id, string questionStatement, string[] answers, string correctAnswer)
        {
            this.id = id;
            this.questionStatement = questionStatement;
            this.answers = answers;
            this.correctAnswer = correctAnswer;
        }

        public QuestionDTO(string questionStatement, string[] answers, string correctAnswer)
        {
            this.questionStatement = questionStatement;
            this.answers = answers;
            this.correctAnswer = correctAnswer;
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string QuestionStatement { get { return questionStatement; } set { questionStatement = value; } }
        public string[] Answers { get { return answers; } set { answers = value; } }
        public string CorrectAnswer { get { return correctAnswer; } set { correctAnswer = value; } }



    }
}
