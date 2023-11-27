// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FineArt.Data;
using FineArt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FineArt.Areas.Identity.Pages.Account.Manage
{
    public class WhoYouAreModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly FineArtDbContext _context;

        public WhoYouAreModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            FineArtDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

            //[Phone]
            //[Display(Name = "Phone number")]
            //public string PhoneNumber { get; set; }

            [Required]
            public string? Role { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            var isAdmin = await _userManager.GetUsersInRoleAsync("Admin");

            var availableRoles = isAdmin.Any() ? new List<string> { "Teacher", "Student", "Manager" } : _roleManager.Roles.Select(x => x.Name).ToList();

            Input = new InputModel
            {
                //PhoneNumber = phoneNumber
                RoleList = availableRoles.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if(HttpContext.Session.GetString("UserId") != null)
            {
                var userID = HttpContext.Session.GetString("UserId");
                var userEmail = HttpContext.Session.GetString("UserEmail");

                var findUser = await _userManager.FindByEmailAsync(userEmail);
                var userRole = await _userManager.GetRolesAsync(findUser);

                if (userRole.Any())
                {
                    return Redirect("/");
                }

            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userRole = await _userManager.GetRolesAsync(user);
            if(userRole == null || !userRole.Any())
            {
                var addRole = await _userManager.AddToRoleAsync(user, Input.Role);

                if (!addRole.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to add user to role.";
                    return RedirectToPage();
                }

                await _signInManager.RefreshSignInAsync(user);

                if (addRole.Succeeded)
                {
                    userRole = await _userManager.GetRolesAsync(user);
                    var userID = HttpContext.Session.GetString("UserId");
                    if (userRole.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPage", "Identify");
                    }
                    else if (userRole.Contains("Manager"))
                    {
                        AdminManager adminManager = new AdminManager()
                        {
                            ApprovedStatus = 0,
                            UserId = userID
                        };

						try
						{
							await _context.AdminManager.AddAsync(adminManager);
							await _context.SaveChangesAsync();
							return RedirectToAction("ManagerPage", "Identify");
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
							throw;
						}

					}
                    else if (userRole.Contains("Teacher"))
                    {
                        Teacher teacher = new Teacher()
                        {
                            ApprovedStatus = 0,
                            UserId = userID
                        };
                        await _context.Teachers.AddAsync(teacher);
                        await _context.SaveChangesAsync();
						return RedirectToAction("TeacherPage", "Identify");
					}
                    else
                    {
						return RedirectToAction("StudentPage", "Identify");
					}
                }
            }
            else if (!userRole.Contains(Input.Role))
            {
                // Remove the user from existing roles
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                {
                    var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeRolesResult.Succeeded)
                    {
                        StatusMessage = "Unexpected error when trying to remove user from roles.";
                        return RedirectToPage();
                    }
                }

                // Add the user to the new role
                var addToRoleResult = await _userManager.AddToRoleAsync(user, Input.Role);
                if (!addToRoleResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to add user to role.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
