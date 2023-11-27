using FineArt.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FineArt.Controllers
{
    public class AdminController : Controller
    {
        private readonly FineArtDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(FineArtDbContext context, RoleManager<IdentityRole> roleManager, ILogger<AdminController> logger)
        {
            _context = context;
            _roleManager = roleManager;
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Roles()
        {
            var role = _roleManager.Roles;
            return View(role);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole model)
        {
            // Aoid Duplication Roles
            if(!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
                TempData["RoleStatus"] = "Add";
            }
            return RedirectToAction("Roles");
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string? id)
        {

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                // Handle the case where the role is not found
                return NotFound();
            }

            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(string? id, IdentityRole model)
        {
            var existingRole = await _roleManager.FindByIdAsync(model.Id);
            if (existingRole == null)
            {
                // Handle the case where the role is not found
                return NotFound();
            }

            existingRole.Name = model.Name;
            existingRole.NormalizedName = model.Name.ToUpper();

            var result = await _roleManager.UpdateAsync(existingRole);
            if (result.Succeeded)
            {
                TempData["RoleStatus"] = "Edit";
                return RedirectToAction("Roles");
            }

            return View(model);
        }
        public IActionResult ManagerApproval()
        {
            var approvalManagers = _context.AdminManager.Include(m => m.User).Where(a => a.ApprovedStatus == 0).ToList();
            Console.WriteLine("Hello Manager Approval Page");
            return View(approvalManagers);
        }
    }
}
