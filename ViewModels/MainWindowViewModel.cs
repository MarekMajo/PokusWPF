using CommunityToolkit.Mvvm.ComponentModel;
using MSK_PC_Controller.Services;

namespace MSK_PC_Controller.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private object _currentViewModel;

    public MainWindowViewModel()
    {
        var auth = new AuthService();
        var login = new LoginViewModel(auth);
        login.LoggedIn += (_, user) => CurrentViewModel = new MainViewModel(user);

        CurrentViewModel = login;
    }
}
