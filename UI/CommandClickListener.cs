using Android.Views;
using System.Windows.Input;

namespace ShortDev.Android.UI;

public sealed class CommandClickListener(ICommand command) : Java.Lang.Object, View.IOnClickListener
{
    public ICommand Command { get; } = command;

    public void OnClick(View? v)
    {
        if (!Command.CanExecute(parameter: null))
            return;

        Command.Execute(parameter: null);
    }
}
