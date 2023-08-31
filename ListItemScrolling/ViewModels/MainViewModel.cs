using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ListItemScrolling.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    [ObservableProperty]
    private ObservableCollection<string> items;

    public MainViewModel()
    {
        Items = new ObservableCollection<string>();
        for (int i = 0;i<40;i++)
        {
            Items.Add($"test {i}");
        }
    }
}
