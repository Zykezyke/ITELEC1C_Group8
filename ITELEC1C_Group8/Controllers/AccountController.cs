using ITELEC1C_Group8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ITELEC1C_Group8.Data;
using ITELEC1C_Group8.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ITELEC1C_Group8.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbData;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _environment;
        public AccountController(AppDbContext dbData, SignInManager<User> signInManager, UserManager<User> userManager, IWebHostEnvironment environment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbData = dbData;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(loginInfo.UserName, loginInfo.Password, loginInfo.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Console.WriteLine($"Login failed for user: {loginInfo.UserName}");
                Console.WriteLine($"Result: {result}");
                ModelState.AddModelError("", "Failed to Login");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userEnteredData)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User();
                newUser.UserName = userEnteredData.UserName;
                newUser.FirstName = userEnteredData.FirstName;
                newUser.LastName = userEnteredData.LastName;
                newUser.Email = userEnteredData.Email;
                newUser.PhoneNumber = userEnteredData.Phone;

                var result = await _userManager.CreateAsync(newUser, userEnteredData.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "User");

                    this.TempData["messages"] = "Account created, you may now login";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            return View(userEnteredData);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel userEnteredData)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;

                if (userEnteredData.Role == "Doctor")
                {
                    // Create a Doctor object
                    Doctor newDoctor = new Doctor
                    {
                        UserName = userEnteredData.UserName,
                        FirstName = userEnteredData.FirstName,
                        LastName = userEnteredData.LastName,
                        Email = userEnteredData.Email,
                        PhoneNumber = userEnteredData.Phone,
                        Branch = userEnteredData.Branch.Value
                    };
                    if (userEnteredData.DoctorPfp != null)
                    {
                        string folder = "doctors/images/";
                        string servFolder = Path.Combine(_environment.WebRootPath, folder);
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + userEnteredData.DoctorPfp.FileName;
                        string filePath = Path.Combine(servFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await userEnteredData.DoctorPfp.CopyToAsync(fileStream);
                        }

                        newDoctor.imagePath = folder + uniqueFileName; // Assuming you have an ImagePath property in your Doctor model
                    }

                    result = await _userManager.CreateAsync(newDoctor, userEnteredData.Password);
                }
                else
                {
                    // Create a regular User object
                    User newUser = new User
                    {
                        UserName = userEnteredData.UserName,
                        FirstName = userEnteredData.FirstName,
                        LastName = userEnteredData.LastName,
                        Email = userEnteredData.Email,
                        PhoneNumber = userEnteredData.Phone
                    };

                    result = await _userManager.CreateAsync(newUser, userEnteredData.Password);
                }

                if (result.Succeeded)
                {
                    var createdUser = await _userManager.FindByNameAsync(userEnteredData.UserName);

                    // Assign the selected role to the new user
                    await _userManager.AddToRoleAsync(createdUser, userEnteredData.Role);

                    TempData["messages"] = "Account created successfully.";
                    return RedirectToAction("Index", "ShowUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            return View(userEnteredData);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(string id)
        {
            // Retrieve the user from the database using the provided id
            var user = _userManager.FindByIdAsync(id).Result;

            // Map the user data to the UpdateUserViewModel
            var updateUserModel = new UpdateViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };

            return View(updateUserModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user from the database using the model's UserId
                var user = await _userManager.FindByIdAsync(model.UserId);

                // Update the user properties
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;

                // Update the user in the database
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["messages"] = "User details updated successfully.";
                    return RedirectToAction("Index", "ShowUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            // If ModelState is not valid, redisplay the form with the current model
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            var deleteUserModel = new UpdateViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };

            return View(deleteUserModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    TempData["messages"] = "User deleted successfully.";
                    return RedirectToAction("Index", "ShowUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            // If user is not found or deletion fails, redirect to ShowUsers
            return RedirectToAction("Index", "ShowUsers");
        }
    }
}
