using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadProject.Models;
using UploadProject.Models.Domain;
using UploadProject.Models.Repository;

namespace UploadProject.Controllers
{
    public class FilesController : Controller
    {
        private FileRepository repo = new FileRepository(new UploadContext());

        // GET: Files
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        //GET: Files/Upload
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        //POST: Files/Upload
        [HttpPost]
        [ActionName("Upload")]
        public ActionResult UploadFile()
        {
            var postedFile = Request.Files[0];
            if (postedFile != null)
            {
                repo.Add(postedFile);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Upload");
            }

        }

        //GET: Files/Download/id
        public ActionResult Download(int id)
        {
            File file = repo.GetOne(id);
            return File(file.FilePath, file.MimeType, file.FileName);
        }

        //GET: Files/Show/id
        public ActionResult Show(int id)
        {
            File file = repo.GetOne(id);
            return File(file.FilePath, file.MimeType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                repo.Dispose();
            base.Dispose(disposing);
        }
    }
}