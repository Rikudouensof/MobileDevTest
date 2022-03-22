using System.Threading.Tasks;
using Xamarin.Forms;

namespace Steer73.FormsApp.Framework
{
    public class MessageService : IMessageService
    {
        public Task ShowError(string message)
        {
            return Application.Current.MainPage.DisplayAlert("FormsApp", message, "OK");
        }
    }

    public interface IMessageService
    {
        Task ShowError(string message); 
    }
}
