using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly IUserRepository userRepository;

        public AdminUserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var userViewModel = new UserViewModel();
            userViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                userViewModel.Users.Add(new User
                {
                    Id = Guid.Parse(user.Id),
                    UserName = user.UserName,
                    Email = user.Email
                });
            }

            return View(userViewModel);
        }
    }
}
