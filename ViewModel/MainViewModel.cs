using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModel
{
  public partial class MainViewModel : ObservableObject
  {
    IConnectivity connectivity;
    IDeviceInfo info;

    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty]
    string text;

    public MainViewModel(IConnectivity connectivity, IDeviceInfo info)
    {
      Items = new ObservableCollection<string>();
      this.connectivity = connectivity;
      this.info = info;
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
    async Task Info()
    {
      await Shell.Current.DisplayAlert("Platform info.", info.Platform.ToString(), "OK");
    }

    [RelayCommand]
    void Clear()
    {
      Items?.Clear();
    }

    [RelayCommand]
    async Task Tap(string s)
    {
      await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }
  }
}
