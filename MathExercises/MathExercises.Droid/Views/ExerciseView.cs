using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidHUD;
using Cirrious.MvvmCross.Droid.Views;

namespace MathExercises.Droid.Views
{
    [Activity(Label = "Rechen Äpp", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ExerciseView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Exercise);
        }
    }
}