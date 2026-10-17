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
            if (QuestionDTOList.Count == 0)
                return 1;
            //get the largest id from list + 1
            int id = QuestionDTOList.Max(q => q.Id) + 1;

            return id;
        }

        public void CreateQuestion(QuestionDTO questionDTO)
        {
            //check null values first
            if (questionDTO == null || questionDTO.QuestionStatement == null ||
                questionDTO.CorrectAnswer == null || questionDTO.Answers.Length < 1)
            {
                Console.WriteLine("Unable to create a question.Please try again");
                return;
            }

            int id = GenerateId();

            //trim the comma separated values ( answers ) and remove the empty value.
            List<string> cleanAnswers = new List<string>();
            foreach (string ans in questionDTO.Answers)
            {
                string temp = ans.Trim();
                if (string.IsNullOrEmpty(temp))
                    continue;

                cleanAnswers.Add(ans.Trim());
            }

            if (!cleanAnswers.Contains(questionDTO.CorrectAnswer))
            {
                Console.WriteLine("Correct answer is not found");
                return;
            }

            questionDTO.Id = id;
            questionDTO.QuestionStatement = questionDTO.QuestionStatement.Trim();
            questionDTO.CorrectAnswer = questionDTO.CorrectAnswer.Trim();
            questionDTO.Answers = cleanAnswers.ToArray();

            QuestionDTOList.Add(questionDTO);
            Console.WriteLine("Question created successfully");

        }

        public void UpdateQuestion(QuestionDTO questionDTO)
        {
            if (questionDTO == null)
            {
                Console.WriteLine("Must define a question");
                return;
            }

            int id = questionDTO.Id;
            QuestionDTO result = QuestionDTOList.Where(q => q.Id == id).FirstOrDefault()!;

            if (result == null) { Console.WriteLine("Invalid Id"); return; }

            result.QuestionStatement = string.IsNullOrEmpty(questionDTO.QuestionStatement) ? result.QuestionStatement : questionDTO.QuestionStatement.Trim();
            result.CorrectAnswer = string.IsNullOrEmpty(questionDTO.CorrectAnswer) ? result.CorrectAnswer : questionDTO.CorrectAnswer.Trim();
            
            //result.Answers = questionDTO.Answers.Length > 0 ? questionDTO.Answers : result.Answers;
            //trim the comma separated values and remove the empty string values
            if (questionDTO.Answers.Length > 0)
            {
                List<string> cleanAnswers = new List<string>();
                foreach (string ans in questionDTO.Answers)
                {

                    if (string.IsNullOrEmpty(ans.Trim()))
                        continue;

                    cleanAnswers.Add(ans.Trim());
                }
                if (!cleanAnswers.Contains(result.CorrectAnswer)) {
                    Console.WriteLine("Correct answer is not found");
                    return;
                }

                result.Answers = cleanAnswers.ToArray();
            }

            Console.WriteLine("Question updated successfully");

        }

        public void DeleteQuestion(int id)
        {
            QuestionDTO targetQuestion = QuestionDTOList.Where(q => q.Id == id).FirstOrDefault()!;
            if (targetQuestion == null)
            {
                Console.WriteLine($"Question {id} not found");
                return;
            }
            QuestionDTOList.Remove(targetQuestion);

            Console.WriteLine($" Question with ID {id} has been deleted successfully");

        }

        public List<QuestionDTO> Shuffle()
        {
            List<QuestionDTO> result = new List<QuestionDTO>(QuestionDTOList);
            // shuffle logic
            for (int i = QuestionDTOList.Count - 1; i > 0; i--)
            {
                //generate random number with upper bound ( i + 1 )
                int j = Random.Shared.Next(i + 1);

                // Swap the items
                var temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }
            return result;
        }

        public void ViewAllQuestions()
        {
            if (QuestionDTOList.Count == 0)
            {
                Console.WriteLine("There is no question in the list.Please create some questions");
                return;
            }
            foreach (QuestionDTO questionDTO in QuestionDTOList)
            {
                Console.WriteLine($"\nID ({questionDTO.Id}) Question : {questionDTO.QuestionStatement}");
                for (int i = 0; i < questionDTO.Answers.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {questionDTO.Answers[i]}");
                }

            }

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
                Console.WriteLine($"\n\nYour answer is wrong\nCorrect Answer : {questionDTO.CorrectAnswer}\nYour Answer : {questionDTO.Answers[answerIndex]}");
                return false;
            }

            return true;
        }

        public void CalculateMark(int correctCount)
        {
            double totalQuestions = Convert.ToDouble(QuestionDTOList.Count);
            double correctRate = Math.Round(((correctCount / totalQuestions) * 100),2);
            
            Console.WriteLine($"Correct {correctRate}% ( {correctCount} out of  {totalQuestions} questions )");

        }

    }
}
