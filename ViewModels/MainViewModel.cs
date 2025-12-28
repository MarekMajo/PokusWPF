using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSK_PC_Controller.Models;
using MSK_PC_Controller.Services;
using MSK_PC_Controller.Views;
using System.Collections.ObjectModel;
using System.Data;

namespace MSK_PC_Controller.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    public string UserName { get; }
    private readonly PcService _pcService = new();

    public MainViewModel(string userName)
    {
        UserName = userName;
        Pcs = new ObservableCollection<PcItem>(LoadPcs(userName));
    }

    public ObservableCollection<PcItem> Pcs { get; }

    [ObservableProperty] private PcItem? _selectedPc;

    [RelayCommand]
    private void ToggleTheme()
    {
        ThemeService.ToggleTheme();
    }

    [RelayCommand]
    private void AddPc()
    {
        var dialog = new AddPcView();
        if (dialog.DataContext is AddPcViewModel viewModel)
        {
            viewModel.PcAdded += (_, item) => Pcs.Add(item);
        }

        dialog.Owner = App.Current.MainWindow;
        dialog.ShowDialog();
    }

    private IEnumerable<PcItem> LoadPcs(string userName)
    {
        try
        {
            var table = _pcService.GetUserPcs(userName);
            return FromTable(table);
        }
        catch
        {
            return Enumerable.Range(1, 10)
                .Select(i => new PcItem { Name = $"PC_{i:00}", Ip = $"192.168.0.{i}" });
        }
    }

    private static IEnumerable<PcItem> FromTable(DataTable table)
    {
        foreach (DataRow row in table.Rows)
        {
            yield return new PcItem
            {
                Name = row["PcName"]?.ToString() ?? string.Empty,
                Ip = row["IpAdress"]?.ToString() ?? string.Empty
            };
        }
    }
}
