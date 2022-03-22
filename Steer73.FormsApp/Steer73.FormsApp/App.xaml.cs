using Steer73.FormsApp.Views;
using Xamarin.Forms;

namespace Steer73.FormsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new UsersView();
        }
    }
}
