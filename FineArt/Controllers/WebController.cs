using FineArt.Data;
using FineArt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FineArt.Controllers
{
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

            FineArtViewModel fineArtModel = new FineArtViewModel()
			{
				myCompetitions = upcomingCompetition
			};


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

		public IActionResult ParticipateInCompetition()
		{
            var userID = HttpContext.Session.GetString("UserId");
            var findStudent =  _context.Students.FirstOrDefault(s => s.UserId == userID);
			if (findStudent != null)
			{
				var userPost = _context.Postings.Where(p => p.StudentId == findStudent.StudentId).ToList();

				var fineArt = new FineArtViewModel()
				{
					myPostings = userPost
				};
				return View(fineArt);

			} else
			{
				return NotFound();
			}
		}

		public async Task<IActionResult> PostDesigns()
		{
			var DesignPost = await _context.Postings.Include(s => s.Student).ToListAsync();
			return View(DesignPost);
		}

		// TEACHER WEBSITE CONTROLS
		public IActionResult GetStudentsForTeacher()
		{
            var student = _context.Students.Include(u => u.User).ToList();
            return View(student);
		}
		public IActionResult GetStudentsForTeacherById(int? id)
		{
			if(id != null)
			{
				var findStudent = _context.Students.Include(u => u.User).FirstOrDefault(s => s.StudentId == id);
				return View(findStudent);
			}
			return NotFound();
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
