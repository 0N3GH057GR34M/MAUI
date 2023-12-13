using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModel
{
  public partial class MainViewModel : ObservableObject
  {
    IConnectivity connectivity;

    [ObservableProperty]
    bool isPlatformNotMobile;

    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty]
    string text;

    public MainViewModel(IConnectivity connectivity, IDeviceInfo info)
    {
      Items = new ObservableCollection<string>();
      this.connectivity = connectivity;
      isPlatformNotMobile = info.Platform == DevicePlatform.WinUI
                || info.Platform == DevicePlatform.macOS
                || info.Platform == DevicePlatform.MacCatalyst;
    }

    [RelayCommand]
    [Obsolete]
    async Task Add()
    {
      if (string.IsNullOrWhiteSpace(Text)) return;

      if (connectivity.NetworkAccess != NetworkAccess.Internet)
      {
        await Shell.Current.DisplayAlert("Error", "Internet connetction is lost!", "Ok");
        return;
      }

      Items.Add(Text);
      Text = string.Empty;
    }

    [RelayCommand]
    void Delete(string s)
    {
      if (Items.Contains(s))
      {
        Items.Remove(s);
      }
    }

    [RelayCommand]
    void ButtonDelete(string s)
    {
      if (Items.Contains(s))
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
