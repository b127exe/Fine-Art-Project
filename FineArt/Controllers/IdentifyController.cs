using FineArt.Data;
using FineArt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace FineArt.Controllers
{
	[Authorize]
	public class IdentifyController : Controller
	{
		private readonly FineArtDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public IdentifyController(FineArtDbContext fineArtDbContext, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager , RoleManager<IdentityRole> roleManager)
		{
			_context = fineArtDbContext;
			_webHostEnvironment = webHostEnvironment;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[Authorize(Roles = "Admin")]
		public IActionResult AdminPage()
		{
			var userID = HttpContext.Session.GetString("UserId");
			var admin = _context.AdminManager.FirstOrDefault(m => m.UserId == userID);
			if (admin == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Admin");
			}
		}

		[HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminProfileCreate(AdminManager model)
		{
				if(model != null)
				{
					var userID = HttpContext.Session.GetString("UserId");
					string AvatarName = UploadAdminManagerAvatar(model);
					AdminManager adminManger = new AdminManager()
					{
						AvatarUrl = AvatarName,
						Phone = model.Phone,
						Gender = model.Gender,
						Extras = model.Extras,
						Age = model.Age,
						UserId = userID
					};

					await _context.AdminManager.AddAsync(adminManger);
					await _context.SaveChangesAsync();
					return Json(new { status = "success", message = "Form Data received!" });
				}
			else
			{
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
			}
            
        }
        [Authorize(Roles = "Manager")]
        public IActionResult ManagerPage()
		{
			var userID = HttpContext.Session.GetString("UserId");
			var manager = _context.AdminManager.FirstOrDefault(m => m.UserId == userID);

			if(manager != null)
			{
				if(manager.ApprovedStatus == 0)
				{
					return View("ManagerPagePendingApproval");
				}
				else
				{
					if (string.IsNullOrEmpty(manager.Gender) || string.IsNullOrEmpty(manager.Phone))
					{
						return View();
					}
					else
					{
						return RedirectToAction("Index", "Admin");
					}
				}
			} 

			return NotFound();
		}
		[HttpPost]
        [Authorize(Roles = "Manager")]
        public async  Task<IActionResult> ManagerPage(AdminManager model)
		{
			if(model != null)
			{
                var userID = HttpContext.Session.GetString("UserId");
				var existingManager = await _context.AdminManager.FirstOrDefaultAsync(m => m.UserId == userID);

				if(existingManager != null)
				{
                    string AvatarName = UploadAdminManagerAvatar(model);

					existingManager.AvatarUrl = AvatarName;
					existingManager.Gender = model.Gender;
					existingManager.Phone = model.Phone;
					existingManager.Extras = model.Extras;
					existingManager.Age = model.Age;

					_context.AdminManager.Update(existingManager);
					await _context.SaveChangesAsync();
                }
                 return Json(new { status = "success", message = "Form Data received!" });
            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult TeacherPage()
		{
			var userID = HttpContext.Session.GetString("UserId");
			var teacher = _context.Teachers.FirstOrDefault(t => t.UserId == userID);
			if(teacher != null)
			{
				if(teacher.ApprovedStatus == 0)
				{
					return View("TeacherPagePendingApproval");
				}
				else
				{
					return View();
				}
			}
			return NotFound();
		}
		[HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> TeacherPage(Teacher model)
		{
			if(model != null)
			{
                var userID = HttpContext.Session.GetString("UserId");
				var existingTeacher = await _context.Teachers.FirstOrDefaultAsync(t => t.UserId == userID);

				if(existingTeacher != null)
				{
					var AvatarName = UploadTeacherAvatar(model);

					existingTeacher.Age = model.Age;
					existingTeacher.AvatarUrl = AvatarName;
					existingTeacher.Education = model.Education;
					existingTeacher.Gender = model.Gender;
					existingTeacher.Phone = model.Phone;

					_context.Teachers.Update(existingTeacher);
					await _context.SaveChangesAsync();

                }
                return Json(new { status = "success", message = "Form Data received!" });
            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }
        }
        [Authorize(Roles = "Student")]
        public IActionResult StudentPage()
		{	
            var userID = HttpContext.Session.GetString("UserId");
			var findStudent = _context.Students.FirstOrDefault(s => s.UserId == userID);

			if(findStudent == null)
			{
				return View();
			}
            
			else
			{
				return RedirectToAction("Index","Web");
			}

		}

		[HttpPost]
		[Authorize(Roles = "Student")]
		public async Task<IActionResult> StudentPage(Student model)
		{
			if(model != null)
			{
                var userID = HttpContext.Session.GetString("UserId");
				var AvatarName = UploadStudentAvatar(model);
				var newStudent = new Student()
				{
					DateOfBirth = model.DateOfBirth,
					ArtType = model.ArtType,
					AvatarUrl = AvatarName,
					Gender =  model.Gender,
					UserId = userID,
					Standard = model.Standard
				};

				await _context.Students.AddAsync(newStudent);
				await _context.SaveChangesAsync();

                return Json(new { status = "success", message = "Form Data received!" });
            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }
        }

		public async Task<IActionResult> LoginIdentify()
		{
            var userID = HttpContext.Session.GetString("UserId");
			if(userID != null)
			{
				var user = await _userManager.FindByIdAsync(userID);
				var userRole = await _userManager.GetRolesAsync(user);
				if(userRole != null && userRole.Contains("Admin"))
				{
					var ad = _context.AdminManager.FirstOrDefault(a => a.UserId == userID);
					if(ad == null)
					{
						return RedirectToAction("AdminPage","Identify");
					}
					else
					{
						return RedirectToAction("Index","Admin");
					}
				}
				else if (userRole != null && userRole.Contains("Manager"))
				{
					var man = _context.AdminManager.FirstOrDefault(m => m.UserId == userID);
                    if (man != null)
                    {
                        if (man.ApprovedStatus == 0)
                        {
                            return View("ManagerPagePendingApproval");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(man.Gender) || string.IsNullOrEmpty(man.Phone))
                            {
                                return RedirectToAction("ManagerPage", "Identify");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Admin");
                            }
                        }
                    }
                    return RedirectToAction("Index", "Admin");
                }
                else if (userRole != null && userRole.Contains("Teacher"))
                {
					var tea = _context.Teachers.FirstOrDefault(t => t.UserId == userID);
                    if (tea != null && string.IsNullOrEmpty(tea.Phone) && string.IsNullOrEmpty(tea.Gender))
                    {
                        if (tea.ApprovedStatus == 0)
                        {
                            return View("TeacherPagePendingApproval");
                        }
                        else
                        {
                            return RedirectToAction("TeacherPage", "Identify");
                        }
                    }
                    return RedirectToAction("Index", "Web");
                }
                else if (userRole != null && userRole.Contains("Student"))
                {
					var std = _context.Students.FirstOrDefault(s => s.UserId == userID);
					if(std == null)
					{
						return RedirectToAction("StudentPage", "Identify");
					} 
                    return RedirectToAction("Index", "Web");
                }
                else
				{
                    return Redirect("/Identity/Account/Manage/WhoYouAre");
                }
			}
			else
			{
                return BadRequest("UserId not found in session");
            }
        }

		private string UploadAdminManagerAvatar(AdminManager model)
		{
            string uniqueFileName = string.Empty;
			if(model.AvatarFile != null)
			{
				string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatar-uploads/");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarFile.FileName;
				string filePath = Path.Combine(uploadFolder, uniqueFileName);
				using (var filestream = new FileStream(filePath, FileMode.Create))
				{
					model.AvatarFile.CopyTo(filestream);
				}
			}
			return uniqueFileName;
        }

        private string UploadTeacherAvatar(Teacher model)
        {
            string uniqueFileName = string.Empty;
            if (model.AvatarFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatar-uploads/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.AvatarFile.CopyTo(filestream);
                }
            }
            return uniqueFileName;
        }

        private string UploadStudentAvatar(Student model)
        {
            string uniqueFileName = string.Empty;
            if (model.AvatarFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatar-uploads/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AvatarFile.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.AvatarFile.CopyTo(filestream);
                }
            }
            return uniqueFileName;
        }

    }
}
