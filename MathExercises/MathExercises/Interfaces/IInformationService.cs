using System;

namespace MathExercises.Interfaces
{
    public interface IInformationService
    {
        void ShowDialog(string title, string message, string okButtonText);

        void ShowToast(string title, string message);

        void ShowSuccess(string message);

        void ShowFailure(string message);
    }
}