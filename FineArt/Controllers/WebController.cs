using FineArt.Data;
using FineArt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FineArt.Controllers
{
	[Authorize]
	public class WebController : Controller
	{
		private readonly FineArtDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly ILogger<WebController> _logger;
        public WebController(FineArtDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<WebController> logger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
			_logger = logger;

        }
        public async Task<IActionResult> Index()
		{
			var userID = HttpContext.Session.GetString("UserId");
			// get cuurent date
            DateTime currentDate = DateTime.Now;
            //var upcomingCompetition = await _context.Competitions.Where(c => DateTime.Parse(c.StartDate) > currentDate).ToListAsync();

            var allCompetitions = await _context.Competitions.ToListAsync();
            //var upcomingCompetition = allCompetitions
            //    .Where(c => DateTime.ParseExact(c.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) > currentDate)
            //    .ToList();
            var upcomingCompetition = allCompetitions
			.Where(c => DateTime.ParseExact(c.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) > currentDate)
			.Take(3)
			.ToList();
            var ongoingCompetition = allCompetitions
            .Where(c => DateTime.ParseExact(c.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) < currentDate)
            .ToList();

			var submissions = _context.PostingSubmissions.Include(c => c.Competition).ToList();

            FineArtViewModel fineArtModel = new FineArtViewModel()
			{
				myCompetitions = upcomingCompetition,
				ongoingCompetitions = ongoingCompetition,
				myPostingSubmissions = submissions
			};
			if(userID != null)
			{
				var findStudent = _context.Students.FirstOrDefault(s => s.UserId == userID);
				if(findStudent != null)
				{
					ViewData["UserID"] = findStudent.StudentId; 
				}
			}
            return View(fineArtModel);
		}
		[HttpPost]
		public async Task<IActionResult> AddPosting(FineArtViewModel posting)
		{
			if(HttpContext.Session.GetString("UserId") != null && posting != null)
			{
                var userID = HttpContext.Session.GetString("UserId");
				var findStudent = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userID);

				if(findStudent != null)
				{
					var DesignImage = PostFileUpload(posting.inputPosting);
					Posting post = new Posting()
					{
						Title = posting.inputPosting.Title,
						Description = posting.inputPosting.Description,
						Quotation = posting.inputPosting.Quotation,
						DesignImageUrl = DesignImage,
						PostedDate = DateTime.Now.ToString(),
						PaidStatus = 0,
						StudentId = findStudent.StudentId
					};
					await _context.Postings.AddAsync(post);
					await _context.SaveChangesAsync();
				}
                return Json(new { status = "success", message = "Award added successfully!" });
            }
            return Json(new { status = "error", message = "An error occured while processing the form..." });
        }

		public IActionResult ParticipateInCompetition(int? id)
		{
			var findCompetition = _context.Competitions.Include(a => a.Award).FirstOrDefault(c => c.CompetitionId == id);		

            var userID = HttpContext.Session.GetString("UserId");
            var findStudent =  _context.Students.FirstOrDefault(s => s.UserId == userID);
			if (findStudent != null)
			{
				var userPost = _context.Postings.Where(p => p.StudentId == findStudent.StudentId).ToList();

				var fineArt = new FineArtViewModel()
				{
					myPostings = userPost,
					inputCompetition = findCompetition
				};
				return View(fineArt);

			} else
			{
				return NotFound();
			}
		}
		[HttpPost]
		public async Task<IActionResult> ParticipateInCompetition(FineArtViewModel model)
		{
            var userID = HttpContext.Session.GetString("UserId");
            var findStudent = _context.Students.FirstOrDefault(s => s.UserId == userID);
            if (model.inputPostingSubmission != null && findStudent != null)
			{
                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("yyyy-MM-d");

                PostingSubmission submission = new PostingSubmission()
				{
					SubmissionQuote = model.inputPostingSubmission.SubmissionQuote,
					CompetitionId = model.inputPostingSubmission.CompetitionId,
					StudentId = findStudent.StudentId,
					PostingId = model.inputPostingSubmission.PostingId,
					SubmissionDate = formattedDate,
					SubmissionStatus = 0
				};
				await _context.PostingSubmissions.AddAsync(submission);
				await _context.SaveChangesAsync();
                return Json(new { status = "success", message = "Your Design for Competition is submit successfully!" });
            }
			else
			{
                return Json(new { status = "error", message = "An error occured while processing the form..." });
            }
            
        }


        public async Task<IActionResult> PostDesigns()
		{
			var DesignPost = await _context.Postings.Include(s => s.Student).ToListAsync();
			return View(DesignPost);
		}

		public IActionResult GiveRemarksForDesign(int? id)
		{
			var submission = _context.PostingSubmissions.Include(c => c.Competition).Include(c => c.Posting).Include(c => c.Student).ThenInclude(s => s.User).Where(c => c.CompetitionId == id).ToList();
			return View(submission);
		}
		[HttpPost]
		public async Task<IActionResult> GiveRemarksForDesign(SubmissionRemarks model)
		{
			if(model != null)
			{
				MarkStatus markStatus = model.Marks;
				SubmissionRemarks submissionRemarks = new SubmissionRemarks()
				{
					Marks = markStatus,
					Comments = model.Comments,
					SubmissionId = model.SubmissionId
				};

				await _context.SubmissionRemarks.AddAsync(submissionRemarks);
				await _context.SaveChangesAsync();

				return Json(new { status = "success", message = "Your Remarks is submit successfully!" });
			}
			else
			{
                return Json(new { status = "error", message = "An error occured while processing the form..." });
            }

        }

        // Ajax Calls for post by id
        public async Task<IActionResult> GetPostById(int? id)
		{
			var data = await _context.Postings.FindAsync(id);
			return new JsonResult(data);
		}

		public async Task<IActionResult> GetUserNotifications()
		{
            var userID = HttpContext.Session.GetString("UserId");
			var findStudent = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userID);
			if(userID != null && findStudent != null)
			{

				var options = new JsonSerializerOptions { 
					ReferenceHandler = ReferenceHandler.Preserve,
				};


				var notification = await _context.Notifications.Include(c => c.Competition).Include(p => p.Posting).Where(s => s.StudentId == findStudent.StudentId && s.NotShowHide == 0).ToListAsync();
				//var notification = await _context.Notifications.ToListAsync();

				if(notification != null && notification.Any())
				{
					return Json(new { status = "success", message = "notification fetch successfully!", data = notification }, options);
				}
				else
				{
                    return Json(new { status = "warning", message = "notification set is empty!", });
                }
            }
			else
			{
                return Json(new { status = "error", message = "An error occured while fetching the user notification..." });
            }
        }

        // TEACHER WEBSITE CONTROLS
        [Authorize(Roles = "Teacher")]
        public IActionResult GetStudentsForTeacher()
		{
            var student = _context.Students.Include(u => u.User).ToList();
            return View(student);
		}
        [Authorize(Roles = "Teacher")]
        public IActionResult GetStudentsForTeacherById(int? id)
		{
			if(id != null)
			{
				var findStudent = _context.Students.Include(u => u.User).FirstOrDefault(s => s.StudentId == id);
                return View(findStudent);
            }
			return NotFound();
		}
        [Authorize(Roles = "Teacher")]
        public IActionResult GetCompetitionForTeacher()
		{
			var competitions = _context.Competitions.Include(a => a.Award).ToList();
			return View(competitions);
		}


        private string PostFileUpload(Posting posting)
		{
			string uniqueFileName = string.Empty;
			if(posting.DesignImageFile != null)
			{
				string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "posting-uploads/");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + posting.DesignImageFile.FileName;
				string uploadPath = Path.Combine(uploadFolder, uniqueFileName);
				using (var filestream = new FileStream(uploadPath, FileMode.Create))
				{
					posting.DesignImageFile.CopyTo(filestream);
				}
			}
			return uniqueFileName;
		}
	}
}
