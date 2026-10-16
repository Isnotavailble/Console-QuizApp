using System;
using System.Collections.Generic;
using QuizConsoleApp;

QuestionService questionService = new QuestionService();
bool run = true;

while (run)
{
    Console.WriteLine("\nQuiz App");
    Console.WriteLine("-------------");
    Console.WriteLine("Menu : ");
    Console.WriteLine("1. Manage questions");
    Console.WriteLine("2. Answer questions");
    Console.WriteLine("3. Exit");
    Console.Write("Choose an option >>> ");

    string inputOption = Console.ReadLine()!;
    int option = Convert.ToInt32(inputOption);

    switch (option)
    {
        case 1:
            Console.WriteLine("\nQuestion Management Menu");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.Write("Choose an Option >>> ");

            string inputNextOption = Console.ReadLine()!;
            int opt = Convert.ToInt32(inputNextOption);

            switch (opt)
            {
                case 1:
                    Console.Write("Enter Question Statement >>> ");
                    string inputQuestionStatement = Console.ReadLine()!;

                    Console.Write("Enter Question's answers (comma separated) >>> ");
                    string inputAnswers = Console.ReadLine()!;
                    string[] answers = inputAnswers.Split(',');

                    Console.Write("Enter correct answer >>> ");
                    string inputCorrectAnswer = Console.ReadLine()!;


                    QuestionDTO questionDTO = new QuestionDTO(inputQuestionStatement.Trim(), answers, inputCorrectAnswer.Trim());
                    questionService.CreateQuestion(questionDTO);
                    break;

                case 2:
                    Console.WriteLine("Hit Enter or Leave empty if you don't want to update a field");
                    Console.Write("Enter Question ID to update >>> ");
                    int updateId = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter new Question Statement >>> ");
                    string updateStatement = Console.ReadLine()!;

                    Console.Write("Enter new Answers (comma separated) >>> ");
                    string updateAnswersInput = Console.ReadLine()!;
                    string[] updateAnswers = updateAnswersInput.Split(',');
                  
                    Console.Write("Enter new correct answer >>> ");
                    string updateCorrect = Console.ReadLine()!;

                    // Using the constructor WITH ID for updates
                    QuestionDTO updateDTO = new QuestionDTO(updateId, updateStatement, updateAnswers, updateCorrect);
                    questionService.UpdateQuestion(updateDTO);
                    break;

                case 3:
                    Console.Write("Enter Question ID to delete >>> ");
                    int deleteId = Convert.ToInt32(Console.ReadLine());
                    questionService.DeleteQuestion(deleteId);
                    break;
            }
            break;

        case 2:
            Console.WriteLine("\n--- Answer Question ---");

            if (questionService.QuestionDTOList.Count == 0)
            {
                Console.WriteLine("No questions available yet!");
                break;
            }

            List<QuestionDTO> shuffledList = questionService.Shuffle();
            int score = 0;

            foreach (QuestionDTO q in shuffledList)
            {
                Console.WriteLine($"\nQuestion: {q.QuestionStatement}");

                for (int i = 0; i < q.Answers.Length; i++)
                {
                    int optNum = i + 1;
                    Console.WriteLine($"{optNum}. {q.Answers[i]}");
                }

                Console.Write("Choose answer number >>> ");
                int ansIndex = Convert.ToInt32(Console.ReadLine());

                bool isCorrect = questionService.CheckAnswer(q.Id, ansIndex - 1);
                if (isCorrect)
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
            }

            questionService.CalculateMark(score);
            break;

        case 3:
            run = false;
            Console.WriteLine("Program Exited");
            break;

        default:
            Console.WriteLine("Invalid option, try again.");
            break;
    }
}