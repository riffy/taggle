namespace Taggle.ViewModels;

[RegisterSingleton]
public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
}
