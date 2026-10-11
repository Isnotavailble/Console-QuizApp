using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QuizConsoleApp
{
    public class QuestionService
    {
        public List<QuestionDTO> QuestionDTOList = new List<QuestionDTO>();
        public int GenerateId()
        {
            int id = Math.Max(0, QuestionDTOList.Count);
            return id;
        }
        public bool CreateQuestion(QuestionDTO questionDTO)
        {

            if (questionDTO == null || questionDTO.QuestionStatement == null ||
                questionDTO.CorrectAnswer == null || questionDTO.Answers.Length < 1)
            {
                Console.WriteLine("Questions should not contain null");
                return false;
            }

            int id = GenerateId();
            questionDTO.Id = id;
            QuestionDTOList.Add(questionDTO);

            return true;
        }
        public bool UpdateQuestion(QuestionDTO questionDTO)
        {
            if (questionDTO == null)
            {
                Console.WriteLine("Question not found");
                return false;
            }

            int id = questionDTO.Id;
            QuestionDTO result = QuestionDTOList.Where(q => q.Id == id).FirstOrDefault()!;

            if (result == null) { Console.WriteLine("Invalid Id"); return false; }

            result.QuestionStatement = string.IsNullOrEmpty(questionDTO.QuestionStatement) ? result.QuestionStatement : questionDTO.QuestionStatement;
            result.CorrectAnswer = string.IsNullOrEmpty(questionDTO.CorrectAnswer) ? result.CorrectAnswer : questionDTO.CorrectAnswer;
            result.Answers = questionDTO.Answers.Length > 0 ? questionDTO.Answers : result.Answers;

            Console.WriteLine("Question updated successfully");

            return true;

        }
        public bool DeleteQuestion(int id)
        {
            QuestionDTOList.RemoveAt(id);
            Console.WriteLine($" Question with ID {id} has been deleted successfully");
            return true;
        }
        public List<QuestionDTO> Shuffle()
        {
            List<QuestionDTO> result = new List<QuestionDTO>(QuestionDTOList);
            // Replace "MyList" with the actual name of your list
            for (int i = QuestionDTOList.Count - 1; i > 0; i--)
            {
                int j = Random.Shared.Next(i + 1);

                // Swap the items
                var temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }
            return result;
        }

        public bool CheckAnswer(int questionId, int answerIndex)
        {

            QuestionDTO questionDTO = QuestionDTOList.Where(q => q.Id == questionId).FirstOrDefault()!;

            if (questionDTO == null)
            {
                Console.WriteLine("Invalid Quesiton");
                return false;
            }
            else if (questionDTO.Answers[answerIndex].Trim() != questionDTO.CorrectAnswer.Trim())
            {
                Console.WriteLine($"Your answer is wrong\nCorrect Answer : {questionDTO.CorrectAnswer}\nYour Answer : {questionDTO.Answers[answerIndex]}");
                return false;
            }

            return true;
        }
        public void CalculateMark(int correctCount)
        {
            double totalQuestions = Convert.ToDouble(QuestionDTOList.Count);
            double correctRate = (correctCount / totalQuestions) * 100;
            Console.WriteLine($"Correct {correctRate}% ( {correctCount} out of  {totalQuestions} questions )");

        }

    }
}
