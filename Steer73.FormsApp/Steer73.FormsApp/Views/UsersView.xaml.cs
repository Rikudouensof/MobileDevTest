using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;
using Steer73.FormsApp.ViewModels;
using Xamarin.Forms;

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

            await ViewModel.Initialize();
        }

        protected UsersViewModel ViewModel
        {
            get => BindingContext as UsersViewModel;
            set => BindingContext = value;
        }
    }
}
