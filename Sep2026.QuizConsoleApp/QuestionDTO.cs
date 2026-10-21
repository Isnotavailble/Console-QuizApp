using System;
using System.Collections.Generic;
using System.Text;

namespace QuizConsoleApp
{
    public class QuestionDTO
    {
        public int Id { get; set; } = 0;
        public string QuestionStatement { get; set; } = "";
        public string[] Answers { get; set; } = { };
        public string CorrectAnswer { get; set; } = "";
        
    }
}
