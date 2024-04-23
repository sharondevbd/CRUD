using CodeFirstMasterDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using NuGet.Protocol.Plugins;

namespace CodeFirstMasterDetails.Controllers
{
	public class ApplicantController : Controller
	{
		private readonly ApplicantContext db;
		IWebHostEnvironment environment;
		public ApplicantController(ApplicantContext context, IWebHostEnvironment host)
		{
			db = context;
			environment = host;
		}
		[HttpGet]
		public IActionResult Index()
		{
			var data = db.Applicants.Include(m => m.Exprience).ToList();
			return View(data);
		}
		[HttpGet]
		public IActionResult Create()
		{
			Applicant NewApplicant = new Applicant();
			NewApplicant.Exprience.Add(new ApplicantExprience
			{
				Company = "",
				Designation = "",
				YearOfExp = 0
			});

			return View(NewApplicant);
		}
		[HttpPost]
		public IActionResult Create(Applicant applicant, string btn)
		{
			if (btn == "Add")
			{
				applicant.Exprience.Add(new ApplicantExprience());
			}

			if (btn == "Create")
			{

				if (applicant.Picture != null)
				{
					string ext = Path.GetExtension(applicant.Picture.FileName);
					if (ext == ".jpg" || ext == ".png")
					{
						applicant.TotalExp = applicant.Exprience.Sum(m => m.YearOfExp);

						if (applicant.Picture != null)
						{
							// var ext = Path.GetExtension(faculty.Picture.FileName);
							var rootPath = this.environment.ContentRootPath;
							var fileToSave = Path.Combine(rootPath, "wwwroot/Pictures", applicant.Picture.FileName);
							using (var fileStream = new FileStream(fileToSave, FileMode.Create))
							{
								applicant.Picture.CopyToAsync(fileStream).Wait();
							}
							applicant.PicPath = "~/Pictures/" + applicant.Picture.FileName;
							db.Applicants.Add(applicant);
							if (db.SaveChanges() > 0)
							{
								return RedirectToAction("Index");
							}
						}
						else
						{
							ModelState.AddModelError("", "Please Provide Profile Picture");
							return View(applicant);
						}
					} //if ext jpg

				} //if-pic	
			}
            return View(applicant);
        }
		[HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
			Applicant EditApplicant = db.Applicants.Include(m => m.Exprience).Where(m => m.Id.Equals(id)).FirstOrDefault();

            return View(EditApplicant);
        }

		[HttpPost]
		public async Task<IActionResult> Edit(Applicant ViewModel, string btn)
		{

            var applicant = db.Applicants.Include(m => m.Exprience).Where(m => m.Id.Equals(ViewModel.Id)).FirstOrDefault();
            //var applicant = await db.Applicants.FindAsync(ViewModel.Id)
			if (btn == "Add")
			{
				ViewModel.Exprience.Add(new ApplicantExprience());
			}
			if (btn == "Edit")
			{
				if (applicant != null)
				{
					applicant.Name = ViewModel.Name;
					applicant.Birthday = ViewModel.Birthday;
					applicant.TotalExp = ViewModel.Exprience.Sum(i => i.YearOfExp);
					applicant.IsAvilable = ViewModel.IsAvilable;
					applicant.Exprience = ViewModel.Exprience;

					if (ViewModel.Picture != null)
					{
						string extpic = Path.GetExtension(ViewModel.Picture.FileName);
						if (extpic == ".jpg" || extpic == ".png" || extpic == ".jpeg")
						{

                    var ext = Path.GetExtension(ViewModel.Picture.FileName);
					var rootPath = this.environment.ContentRootPath;
					var fileToSave = Path.Combine(rootPath, "wwwroot/Pictures", ViewModel.Picture.FileName);

					using (var fileStream = new FileStream(fileToSave, FileMode.Create))
					{
					await	ViewModel.Picture.CopyToAsync(fileStream); }
						applicant.PicPath = "~/Pictures/" + ViewModel.Picture.FileName;
					} else
						{
							ModelState.AddModelError("error", "Invalid Picture");
						}

                    } else
					{
						applicant.Picture = applicant.Picture;
					}


			if(	await db.SaveChangesAsync() > 0)
					{
                        return RedirectToAction("index");
                    }
				}
				
			}
			return View(ViewModel);
		}
     //           if (applicant.Picture != null)
     //           {
     //               string ext = Path.GetExtension(applicant.Picture.FileName);
     //               if (ext == ".jpg" || ext == ".png")
     //               {
     //                applicant.TotalExp = applicant.Exprience.Sum(m => m.YearOfExp);

     //                if (applicant.Picture != null)
     //                {
     //                       // var ext = Path.GetExtension(faculty.Picture.FileName);
     //                       var rootPath = this.environment.ContentRootPath;
     //                       var fileToSave = Path.Combine(rootPath, "wwwroot/Pictures", applicant.Picture.FileName);
     //                       using (var fileStream = new FileStream(fileToSave, FileMode.Create))
     //                       {
     //                           applicant.Picture.CopyToAsync(fileStream).Wait();
     //                       }
     //                      applicant.PicPath = "~/Pictures/" + applicant.Picture.FileName;

     //                       db.Applicants.Add(applicant);
     //                   }
     //                   else
     //                   {
     //                       ModelState.AddModelError("", "Please Provide Profile Picture");
     //                       return View(applicant);
     //                   }
     //               } //if ext jpg
					//applicant.PicPath = applicant.PicPath;

					//db.Applicants.RemoveRange(db.Applicants.Where(s => s.Id == applicant.Id));
					//db.SaveChanges();

					//applicant.Exprience = applicant.Exprience;

     //               db.Entry(applicant).State = EntityState.Modified;
     //               if (db.SaveChanges() > 0)
     //               {
     //                   return RedirectToAction("Index");
     //               }

     //           } //if-pic	
     //       }
     //       return View(applicant);
     //   }





        public IActionResult Delete(int id)
		{
			if (id != null)
			{
				var user = db.Applicants.Find(id);
				db.Applicants.Remove(user);
				db.SaveChanges();
			}
            return RedirectToAction("Index");
        }
    }
}

