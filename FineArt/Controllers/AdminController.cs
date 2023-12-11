using FineArt.Data;
using FineArt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;

namespace FineArt.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AdminController : Controller
    {
        private readonly FineArtDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(FineArtDbContext context, RoleManager<IdentityRole> roleManager, ILogger<AdminController> logger, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;

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
        [HttpPost]
        public IActionResult ManagerApproval(string? id)
        {
            var findManager = _context.AdminManager.Where(m => m.UserId == id).FirstOrDefault();

            if(findManager != null)
            {
                findManager.ApprovedStatus = 1;
                _context.AdminManager.Update(findManager);
                _context.SaveChanges();
                return Json(new { status = "success", message = "Manager approved received!" });
            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }

        }

        public IActionResult TeacherApproval()
        {
            var approvalTeacher = _context.Teachers.Include(t => t.User).Where(a => a.ApprovedStatus == 0).ToList();
            return View(approvalTeacher);
        }
        [HttpPost]
        public IActionResult TeacherApproval(string? id)
        {
            var findTeacher = _context.Teachers.Where(t => t.UserId == id).FirstOrDefault();
            if(findTeacher != null)
            {
                findTeacher.ApprovedStatus = 1;
                _context.Teachers.Update(findTeacher);
                _context.SaveChanges();
                return Json(new { status = "success", message = "Manager approved received!" });
            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }
        }

        public IActionResult AwardsList()
        {
            var awards = _context.Awards.ToList();
            return View(awards);
        }
        public IActionResult AddAward()
        {        
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAward(Award model)
        {
            if(model != null)
            {
                Award award = new Award()
                {
                    AwardTitle = model.AwardTitle,
                    AwardAmount = model.AwardAmount,
                    Type = model.Type,
                    Terms = model.Terms
                };
                await _context.Awards.AddAsync(award);
                await _context.SaveChangesAsync();
                return Json(new { status = "success", message = "Award added successfully!" });
            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAward(int? id)
        {
            if(_context.Awards == null)
            {
                return Json(new { status = "error", message = "An error occurred because competition set is empty." });
            }
            var award = await _context.Awards.FindAsync(id);
            if(award != null)
            {
                _context.Remove(award);
                await _context.SaveChangesAsync();
                return Json(new { status = "success", message = "Award deleted successfully!" });
            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }
        }

        public IActionResult CompetitionsList()
        {
            var competitions = _context.Competitions.Where(c => c.Status == 0).ToList();
            return View(competitions);
        }
        public IActionResult AddCompetition()
        {
            ViewData["AwardId"] = new SelectList(_context.Awards, "AwardId", "AwardTitle");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCompetition(Competition model)
        {
           if(model != null)
            {
                Competition competition = new Competition()
                {
                    CompetitionTitle = model.CompetitionTitle,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Status = 0,
                    Conditions = model.Conditions,
                    AwardId = model.AwardId
                };

                await _context.Competitions.AddAsync(competition);
                await _context.SaveChangesAsync();
                return Json(new { status = "success", message = "Competition added successfully!" });
            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }
        }

        public IActionResult EditCompetition(int? id)
        {
            var competition = _context.Competitions.Find(id);
            ViewData["AwardId"] = new SelectList(_context.Awards, "AwardId", "AwardTitle");
            return View(competition);
        }
        [HttpPost]
        public async Task<IActionResult> EditCompetition(int? id, Competition model)
        {
            if(model != null)
            {
                 _context.Update(model);
                await _context.SaveChangesAsync();
                return Json(new { status = "success", message = "Competition edited successfully!" });

            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCompetition(int? id)
        {
            if(_context.Competitions == null)
            {
                return Json(new { status = "error", message = "An error occurred because competition set is empty." });
            }
            var competition = await _context.Competitions.FindAsync(id);
            if(competition != null)
            {
                _context.Remove(competition);
                await _context.SaveChangesAsync();
                return Json(new { status = "success", message = "Competition deleted successfully!" });
            }
            else
            {
                return Json(new { status = "error", message = "An error occurred while processing the form data." });
            }
        }

        public IActionResult DesignsForAdmin()
        {
            var desgin = _context.Postings.Include(s => s.Student.User).ToList();
            return View(desgin);
        }

        public async Task<IActionResult> CompetitionSubmission()
        {
            DateTime currentDate = DateTime.Now;
            var competitions = await _context.Competitions.ToListAsync();

            var ongoingCompetition = competitions
        .Where(c => DateTime.ParseExact(c.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) <= currentDate)
        .Where(c => DateTime.ParseExact(c.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) >= currentDate)
        .Where(c => c.Status == 0)
        .ToList();

            //var competitionsWithEqualEndDate = competitions
            //    .Where(c => DateTime.ParseExact(c.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) == currentDate)
            //    .ToList();

            var competitionsWithEqualOrOneDayAfterEndDate = competitions
        .Where(c => DateTime.ParseExact(c.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) >= currentDate.AddDays(-1) &&
                    DateTime.ParseExact(c.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) <= currentDate)
        .Where(c => c.Status == 0)
        .ToList();

            ongoingCompetition.AddRange(competitionsWithEqualOrOneDayAfterEndDate);

            return View(ongoingCompetition);
        }

        public async Task<IActionResult> SubmissionDesignByStudent(int? id)
        {
            var submission = await _context.PostingSubmissions.Include(p => p.Posting).Include(c => c.Competition).Include(s => s.Student).ThenInclude(s => s.User).Where(c => c.CompetitionId == id).ToListAsync();
            return View(submission);
        }

        public async Task<IActionResult> GetSubmissionRemarks(int? id)
        {
            if(id != null)
            {
                var remarks = await _context.SubmissionRemarks.Where(s => s.SubmissionId == id).ToListAsync();
                var marks = await _context.SubmissionRemarks.Where(sr => sr.SubmissionId == id)
                .GroupBy(sr => sr.Marks)
                .Select(group => new
                {
                    Mark = group.Key,
                    Count = group.Count(),
                }).ToListAsync();

                return Json(new { status = "success", message = "remarks get successfully...", remarks = remarks, marks = marks, submissionId = id });
            }
            else
            {
                return Json(new { status = "error", message = "an error occure while getting the remarks..." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AnnounceCompetitionWinner(int? id)
        {
            if (id != null)
            {
                var findSubmission = await _context.PostingSubmissions.FindAsync(id);
                if(findSubmission != null)
                {
                    //Update the one status to 1 means (winner)
                    findSubmission.SubmissionStatus = 1;
                    _context.Update(findSubmission);
                    await _context.SaveChangesAsync();

                    //Update those who have 0 status to 2 means (failed)
                    var submissionWithZeroStatus = await _context.PostingSubmissions.Where(s => s.SubmissionStatus == 0).ToListAsync();
                    foreach (var submissions in submissionWithZeroStatus)
                    {
                        submissions.SubmissionStatus = 2;
                        _context.Update(submissions);
                    }
                    await _context.SaveChangesAsync();

                    //Award the Student
                    string remarks = "First Congratulations and As an administrator, I want to express my appreciation for your hard work and commitment to excellence. Your success in this competition reflects positively on both your individual abilities and the overall artistic community within our institution. We are proud to have such talented individuals contributing to the vibrant creative atmosphere here.";
                    AwardedStudent awardedStudent = new AwardedStudent()
                    {
                        AwardRemarks = remarks,
                        StudentId = findSubmission.StudentId,
                        PostingId = findSubmission.PostingId,
                        CompetitionId = findSubmission.CompetitionId
                    };
                    await _context.AwardedStudents.AddAsync(awardedStudent);
                    await _context.SaveChangesAsync();

                    // reward the amount of award to student 
                    var awardAmount = await _context.Competitions.Include(a => a.Award).FirstOrDefaultAsync(c => c.CompetitionId == findSubmission.CompetitionId);
                    var findStudent = await _context.Students.FindAsync(findSubmission.StudentId);
                    if(findStudent.WalletAmount != null)
                    {
                        findStudent.WalletAmount += awardAmount.Award.AwardAmount;
                        _context.Students.Update(findStudent);
                    }
                    else
                    {
                        findStudent.WalletAmount = awardAmount.Award.AwardAmount;
                        _context.Students.Update(findStudent);
                    }
                    await _context.SaveChangesAsync();


                    // End the competition
                    var endCompetition = await _context.Competitions.FindAsync(findSubmission.CompetitionId);
                    if(endCompetition != null)
                    {
                        endCompetition.Status = 1;
                        _context.Competitions.Update(endCompetition);
                        await _context.SaveChangesAsync();
                    }

                }
                return Json(new { status = "success", message = "The winner of the competition is successfully annnounce!" });

            }
            else
            {
                return Json(new { status = "error", message = "an error occure while announce the winner..." });
            }
        }

        // Exhibition work start here 
        public IActionResult AddExhibition()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddExhibition(Exhibition model)
        {
            if(model != null)
            {
                var userID = HttpContext.Session.GetString("UserId");
                var findAdminManager = await _context.AdminManager.FirstOrDefaultAsync(u => u.UserId == userID);
                if(findAdminManager != null)
                { 
                    Exhibition exhibition = new Exhibition()
                    {
                        ExhibitionTitle = model.ExhibitionTitle,
                        ExhibitionDate = model.ExhibitionDate,
                        Details = model.Details,
                        Conditions = model.Conditions,
                        Country = model.Country,
                        AdminManagerId = findAdminManager.AdminManagerId,
                        ExStatus = 0
                    };

                    await _context.Exhibitions.AddAsync(exhibition);
                    await _context.SaveChangesAsync();

                }
                return Json(new { status = "success", message = "The Exhibition is added successfully!" });
            }
            else
            {
                return Json(new { status = "error", message = "an error occure while submitting the form..." });
            }
        }
        
        public IActionResult ExhibitionsList()
        {
            return View();
        }

        // Dashboard home ajax call functions
        public async Task<IActionResult> GetUserCount()
        {
            var userCount = await _userManager.Users.CountAsync();

            var studentCount = await _userManager.GetUsersInRoleAsync("Student");
            var teacherCount = await _userManager.GetUsersInRoleAsync("Teacher");
            var managerCount = await _userManager.GetUsersInRoleAsync("Manager");

            return Json(new { status = "success", totalUser = userCount, totalStudent = studentCount.Count, totalTeacher = teacherCount.Count, totalManager = managerCount.Count });
        }
    }
}
