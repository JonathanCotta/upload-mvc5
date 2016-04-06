using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadProject.Models;
using UploadProject.Models.Repository;

namespace UploadProject.Controllers
{
    public class PicturesController : Controller
    {
        PictureRepository repo = new PictureRepository(new UploadContext());
        // GET: Pictures
        public ActionResult Index()
        {
            ViewBag.Images = repo.GetListItem();
            return View();
        }

        //GET: Pictures/Upload
        public ActionResult Upload()
        {
            return View();
        }

        //POST: Pictures/Upload
        [HttpPost]
        [ActionName("Upload")]
        public ActionResult UploadImage()
        {
            var postedPicture = Request.Files[0];             
            if (postedPicture != null && repo.PictureValidation(postedPicture.ContentType))
            {
                repo.Add(postedPicture);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
           
        }

        //GET: Pictures/Show
        [HttpGet]         
        public ActionResult GetOne(int id)
        {
            var picture = repo.GetOne(id);
            if (picture != null)
            {                   
                return Json(new { OK = true, thumbnailPath = picture.ThumbnailPath},JsonRequestBehavior.AllowGet); 
            }
            else
            {
                return Json(new { OK = false } , JsonRequestBehavior.AllowGet);
            }
        }  
    }
}