using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;
using Xamarin.Forms;

namespace Steer73.FormsApp.ViewModels
{
  public class UsersViewModel : ViewModel
  {
    private readonly IUserService _userService;
    private readonly IMessageService _messageService;

    public UsersViewModel(
        IUserService userService,
        IMessageService messageService)
    {
      _userService = userService;
      _messageService = messageService;
      SearchText = new Xamarin.Forms.Command<string>(Search);
    }

    public async Task Initialize()
    {
      try
      {
        
        IsBusy = true;
      

        var users = await _userService.GetUsers();

        foreach (var user in users)
        {
          Users.Add(user);
        }
      }
      catch (Exception ex)
      {
        await _messageService.ShowError(ex.Message);
      }
      finally
      {
        IsBusy = false;
      }
    }

    void Search( string term)
    {
      string lowerTerm = term.ToLower();
      foreach (var item in Users)
      {
        if (item.FirstName.ToLower().Contains(lowerTerm) || item.LastName.ToLower().Contains(lowerTerm))
        {
          Users.Add(item);
        }
        else
        {
          Users.Remove(item);
        }
      }
    }


    public async void Refresh()
    {
      if (IsBusy)
      {
        await _messageService.ShowError("Contacts loading, please wait!");
      }
      else
      {

      }
    }


   


    public ICommand SearchText { get; }

    public bool IsBusy { get; set; } = true;

    public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

   

  }
}
