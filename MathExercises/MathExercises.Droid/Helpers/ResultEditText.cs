using System.Windows.Input;
using Android.Content;
using Android.Util;
using Android.Views.InputMethods;
using Android.Widget;

namespace MathExercises.Droid.Helpers
{
    public class ResultEditText : EditText
    {
        public ResultEditText(Context c, IAttributeSet a) : base(c, a)
        {
            EditorAction += EventHandlerContrato;
        }

        public ICommand KeyCommand { get; set; }

        public void EventHandlerContrato(object myObject, EditorActionEventArgs e)
        {
            e.Handled = false;
            if (e.ActionId == ImeAction.ImeNull ||
                e.ActionId == ImeAction.Next ||
                e.ActionId == ImeAction.Unspecified ||
                e.ActionId == ImeAction.None ||
                e.ActionId == ImeAction.Send ||
                e.ActionId == ImeAction.Go ||
                e.ActionId == ImeAction.Done)
            {
                if (KeyCommand != null)
                {
                    KeyCommand.Execute(null);
                    e.Handled = true;
                }
            }
        }
    }
}