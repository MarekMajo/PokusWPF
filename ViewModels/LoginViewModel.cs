using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSK_PC_Controller.Services;

namespace MSK_PC_Controller.ViewModels;

internal partial class LoginViewModel : ObservableObject
{
    private readonly AuthService _auth;

    [ObservableProperty] private string _userName = "";
    [ObservableProperty] private string _password = "";
    [ObservableProperty] private string _error = "";

    public event EventHandler<string>? LoggedIn;

    public LoginViewModel(AuthService auth)
    {
        _auth = auth;
        if (AppState.IsDarkMode)
        {
            ThemeService.ApplyTheme(true);
        }
    }

    [RelayCommand]
    private void Login()
    {
        Error = "";

        if (_auth.CheckLogin(UserName, Password))
        {
            AppState.CurrentUserName = UserName;
            LoggedIn?.Invoke(this, UserName);
            return;
        }

        Error = "Nesprávne údaje";
    }

    [RelayCommand]
    private void ToggleTheme()
    {
        ThemeService.ToggleTheme();
    }
}
