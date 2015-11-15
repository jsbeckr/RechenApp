using System;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using MathExercises.Extensions;
using MathExercises.Interfaces;
using MathExercises.Models;

namespace MathExercises.ViewModels
{
    public class ExerciseViewModel
        : MvxViewModel
    {
        private readonly IInformationService informationService;
        private readonly Func<ExerciseModel>[] possibleExercises;

        private readonly Random random = new Random();

        private string answer;

        private MvxCommand checkAnswerCommand;
        private ExerciseModel exercise;

        private int totalAnswers;
        private int correctAnswers;
        private int wrongAnswers;

        public ExerciseViewModel(IInformationService informationService)
        {
            this.informationService = informationService;

            possibleExercises = new Func<ExerciseModel>[]
            {
                CreateAddExercise,
                CreateSubtractExercise,
                CreateMultiplyExercise,
                CreateDivideExercise
            };

            ComputeNewExercise();
        }

        private MvxCommand showStatisticsCommand;

        public ICommand ShowStatisticsCommand
        {
            get
            {
                return this.showStatisticsCommand ?? (this.showStatisticsCommand = new MvxCommand(
                    () =>
                    {
                        informationService.ShowDialog("Stats", $"Richtige Antworten: {correctAnswers}\nFalsche Antworten: {wrongAnswers}\n\n Insgesamt: {totalAnswers}", "OK");
                    }));
            }
        }

        public ExerciseModel Exercise
        {
            get { return exercise; }
            set { SetProperty(ref exercise, value); }
        }

        public string Answer
        {
            get { return answer; }
            set { SetProperty(ref answer, value); }
        }

        public ICommand CheckAnswerCommand
        {
            get
            {
                return checkAnswerCommand ?? (checkAnswerCommand = new MvxCommand(
                    () =>
                    {
                        // sanity check
                        if (string.IsNullOrEmpty(Answer))
                        {
                            informationService.ShowToast("So nicht!", "Bitte etwas eingeben");
                            return;
                        }

                        if (CheckAnswer())
                        {
                            informationService.ShowSuccess("Sehr gut!");
                            this.correctAnswers++;
                        }
                        else
                        {
                            informationService.ShowFailure($"{Exercise.Result} wäre richtiger!");
                            this.wrongAnswers++;
                        }

                        this.totalAnswers++;
                        ComputeNewExercise();
                    }));
            }
        }

        private bool CheckAnswer()
        {
            var result = Exercise.Result == int.Parse(Answer);
            Answer = "";

            return result;
        }

        private void ComputeNewExercise()
        {
            Exercise = possibleExercises.Random().Invoke();
        }

        private ExerciseModel CreateDivideExercise()
        {
            var result = random.Next(1, 30);
            var divider = random.Next(2, 10);
            var firstNumber = result*divider;

            return new ExerciseModel(ExerciseType.Divide, firstNumber, divider, result);
        }

        private ExerciseModel CreateAddExercise()
        {
            var firstNumber = random.Next(0, 499);
            var secondNumber = random.Next(0, 499);
            var result = firstNumber + secondNumber;

            return new ExerciseModel(ExerciseType.Add, firstNumber, secondNumber, result);
        }

        private ExerciseModel CreateMultiplyExercise()
        {
            var firstNumber = random.Next(0, 30);
            var secondNumber = random.Next(0, 30);
            var result = firstNumber*secondNumber;

            return new ExerciseModel(ExerciseType.Multiply, firstNumber, secondNumber, result);
        }

        private ExerciseModel CreateSubtractExercise()
        {
            var firstNumber = random.Next(0, 999);
            var secondNumber = random.Next(0, 999);

            if (firstNumber < secondNumber)
            {
                var temp = firstNumber;
                firstNumber = secondNumber;
                secondNumber = temp;
            }

            var result = firstNumber - secondNumber;

            return new ExerciseModel(ExerciseType.Subtract, firstNumber, secondNumber, result);
        }
    }
}