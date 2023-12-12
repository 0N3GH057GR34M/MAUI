using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModel
{
  public partial class MainViewModel: ObservableObject
  {
    IConnectivity connectivity;

    [ObservableProperty]
    bool isPlatformWindows;

    public MainViewModel(IConnectivity connectivity, IDeviceInfo info)
    {
      Items = new ObservableCollection<string>();
      this.connectivity = connectivity;
      isPlatformWindows = info.Platform == DevicePlatform.WinUI;
    }

    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty]
    string text;

    [RelayCommand]
    async Task Add()
    {
      if (string.IsNullOrWhiteSpace(text)) return;

      if (connectivity.NetworkAccess != NetworkAccess.Internet)
      {
        await Shell.Current.DisplayAlert("Error", "Internet connetction is lost!", "Ok");
        return;
      }

      Items.Add(text);
      Text = string.Empty;
    }

    [RelayCommand]
    void Delete(string s)
    {
      if (items.Contains(s))
      {
        Items.Remove(s);
      }
    }

    [RelayCommand]
    async Task Tap(string s)
    {
      await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }
  }
}
