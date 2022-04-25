using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
      SearchText = new Command<string>(SearchAsync);
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

    async void SearchAsync( string term)
    {
      if (string.IsNullOrEmpty(term))
      {
        term = String.Empty;

      }
      var mainList = await _userService.GetUsers();
      string lowerTerm = term.ToLower();
      var filteredResult = mainList.Where(m => m.LastName.ToLower().Contains(lowerTerm) || m.FirstName.ToLower().Contains(lowerTerm)).ToList();
      foreach (var item in mainList)
      {
        if (!Users.Contains(item))
        {
          Users.Add(item);
        }
        else if(!filteredResult.Contains(item))
        {
          Users.Remove(item);
        }
      }

     
    }


    


   


    public ICommand SearchText { get; }

    private bool isBusy = false;
    private string searchContacts = "Find Contact";

    public string SearchContacts
    {
      get => searchContacts;
      set
      {
        if (value == searchContacts)
        {
          return;
        }
        searchContacts = value;
        RPropertyChanged(nameof(SearchContacts));
      }
    }
    public bool IsBusy
    {
      get => isBusy;
      set
      {
        if (value == isBusy)
        {
          return;
        }
        isBusy = value;
        RPropertyChanged(nameof(IsBusy));
      }


    }

    public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();

   

  }
}
