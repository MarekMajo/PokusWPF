using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MSK_PC_Controller.Models;
using MSK_PC_Controller.Services;
using System.Net;
using System.Net.Sockets;

namespace MSK_PC_Controller.ViewModels;

public partial class AddPcViewModel : ObservableObject
{
    private readonly PcService _pcService;

    [ObservableProperty] private string _pcName = string.Empty;
    [ObservableProperty] private string _pcIp = string.Empty;
    [ObservableProperty] private string _error = string.Empty;

    public event EventHandler<PcItem>? PcAdded;
    public event EventHandler? RequestClose;

    public AddPcViewModel(PcService pcService)
    {
        _pcService = pcService;
    }

    [RelayCommand]
    private void Save()
    {
        Error = string.Empty;
        if (PcName.Length < 5)
        {
            Error = "Krátky názov Počítača";
            return;
        }

        if (!IPAddress.TryParse(PcIp.Trim(), out var ip) || ip.AddressFamily != AddressFamily.InterNetwork)
        {
            Error = "Nesprávna IP Adresa";
            return;
        }

        try
        {
            _pcService.InsertUserPc(AppState.CurrentUserName ?? string.Empty, PcIp.Trim(), PcName.Trim());
            PcAdded?.Invoke(this, new PcItem { Name = PcName.Trim(), Ip = PcIp.Trim() });
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        RequestClose?.Invoke(this, EventArgs.Empty);
    }
}
