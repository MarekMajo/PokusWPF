using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using MSK_PC_Controller.Models;

namespace MSK_PC_Controller.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    public string UserName { get; }

    public MainViewModel(string userName)
    {
        UserName = userName;
        Pcs = Enumerable.Range(1, 10)
            .Select(i => new PcItem { Name = $"PC_{i:00}", Ip = $"192.168.0.{i}" })
            .ToList();
    }

    public List<PcItem> Pcs { get; }

    [ObservableProperty] private PcItem? _selectedPc;

    [RelayCommand]
    private void ToggleTheme()
    {
        var helper = new PaletteHelper();
        var theme = helper.GetTheme();

        theme.SetBaseTheme(theme.GetBaseTheme() == BaseTheme.Dark ? BaseTheme.Light : BaseTheme.Dark);
        helper.SetTheme(theme);
    }
}