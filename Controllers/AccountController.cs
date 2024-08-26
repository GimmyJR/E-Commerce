using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = registerVM.UserName;
                user.PasswordHash = registerVM.Password;
                user.Email = registerVM.Email;
                user.PhoneNumber = registerVM.PhoneNumber;
                IdentityResult result= await userManager.CreateAsync(user,registerVM.Password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,"Customer");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                }

            }
            return View(registerVM);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserVm loginUserVm)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = await userManager.FindByNameAsync(loginUserVm.UserName);
                if (identityUser != null)
                {
                    bool found = await userManager.CheckPasswordAsync(identityUser, loginUserVm.Password);
                    if (found)
                    {
                        
                        await signInManager.SignInAsync(identityUser, loginUserVm.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "User Name or Password is Wrong");
            }
            return View();
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = registerVM.UserName;
                user.PasswordHash = registerVM.Password;
                user.Email = registerVM.Email;
                user.PhoneNumber = registerVM.PhoneNumber;
                IdentityResult result = await userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                }

            }
            return View(registerVM);
        }

        public async Task<IActionResult> ShowProfile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Assuming ApplicationUser has properties like Name, Email, etc.
            var user1 = new UserProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return View(user1);
        }
    }
}


