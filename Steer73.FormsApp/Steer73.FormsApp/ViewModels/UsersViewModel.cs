using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;

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

        public bool IsBusy { get; set; }

        public ICollection<User> Users { get; } = new List<User>();
    }
}
