using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;
using Steer73.FormsApp.ViewModels;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace Steer73.FormsApp.Views
{
  public partial class UsersView : ContentPage
  {
    public UsersView()
    {
      InitializeComponent();

      ViewModel = new UsersViewModel(
          new UserService(),
          new MessageService());
    }

    protected override async void OnAppearing()
    {
      base.OnAppearing();
      LoadingFrame.IsVisible = true;
      await ViewModel.Initialize();
      LoadingFrame.IsVisible = false;
    }

    protected UsersViewModel ViewModel
    {
      get => BindingContext as UsersViewModel;
      set => BindingContext = value;
    }






    //Showing a Code behind Service
    private void MainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
      string searchTerm = e.NewTextValue;
    }

    private void DataListView_Refreshing(object sender, System.EventArgs e)
    {
      if (IsBusy)
      {
        DataListView.IsRefreshing = true;

      }
      else
      {
        DataListView.IsRefreshing= false;
      }
    }
  }
}
