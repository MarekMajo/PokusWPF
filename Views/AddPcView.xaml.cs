using System.Windows;
using MSK_PC_Controller.Services;
using MSK_PC_Controller.ViewModels;

namespace MSK_PC_Controller.Views;

public partial class AddPcView : Window
{
    public AddPcView()
    {
        InitializeComponent();
        var viewModel = new AddPcViewModel(new PcService());
        viewModel.RequestClose += (_, _) => Close();
        DataContext = viewModel;
    }
}
