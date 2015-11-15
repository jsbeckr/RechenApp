using System;
using System.Threading;
using Acr.UserDialogs;
using Android.App;
using AndroidHUD;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using MathExercises.Interfaces;

namespace MathExercises.Droid.Infrastructure
{
    public class InformationService : IInformationService
    {
        private IMvxMainThreadDispatcher dispatcher;

        public InformationService(IMvxMainThreadDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public void ShowDialog(string title, string message, string okButtonText)
        {
            UserDialogs.Instance.Alert(message, title, okButtonText);
        }

        public void ShowToast(string title, string message)
        {
            this.dispatcher.RequestMainThreadAction(() =>
            {
                UserDialogs.Instance.InfoToast(title, message, 2000);
            });
        }

        public void ShowSuccess(string message)
        {
            this.dispatcher.RequestMainThreadAction(() =>
            {
                UserDialogs.Instance.ShowSuccess(message, 1500);
            });
        }

        public void ShowFailure(string message)
        {
            this.dispatcher.RequestMainThreadAction(() =>
            {
                UserDialogs.Instance.ShowError(message, 1500);
            });
        }
    }
}