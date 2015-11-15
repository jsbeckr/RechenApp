using System;

namespace MathExercises.Models
{
    public enum ExerciseType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public class ExerciseModel
    {
        private readonly string exercisePattern = "{0} {1} {2} = ?";

        public ExerciseModel(ExerciseType exerciseType, int firstNumber, int secondNumber, int result)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
            Result = result;
            ExerciseType = exerciseType;
        }

        public string Text
        {
            get
            {
                var symbol = "";

                switch (ExerciseType)
                {
                    case ExerciseType.Add:
                        symbol = "+";
                        break;
                    case ExerciseType.Subtract:
                        symbol = "−";
                        break;
                    case ExerciseType.Multiply:
                        symbol = "×";
                        break;
                    case ExerciseType.Divide:
                        symbol = "÷";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(ExerciseType), ExerciseType, null);
                }

                return string.Format(exercisePattern, FirstNumber, symbol, SecondNumber);
            }
        }

        public int FirstNumber { get; }

        public int SecondNumber { get; }

        public int Result { get; }

        public ExerciseType ExerciseType { get; set; }
    }
}