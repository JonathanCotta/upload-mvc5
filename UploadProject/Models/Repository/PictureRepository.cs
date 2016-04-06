using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadProject.Models.Domain;

namespace UploadProject.Models.Repository
{
    public class PictureRepository : Repository<Picture, HttpPostedFileBase>
    {
        public PictureRepository(UploadContext db) : base(db) { }

        #region Creation Methods
        public override void Add(HttpPostedFileBase item)
        {
            var picture = Create(item);
            db.Pictures.Add(picture);
            db.SaveChanges();
        }

        protected override Picture Create(HttpPostedFileBase item)
        {
            var picture = SaveImage(item);
            picture.ThumbnailPath = CreateThumbnail(picture);
            return picture;
        }

        private Picture SaveImage(HttpPostedFileBase img)
        {
            var imageName = Path.GetFileNameWithoutExtension(img.FileName);
            var imageExtension = Path.GetExtension(img.FileName);
            var imagePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Images"), imageName + imageExtension);
            img.SaveAs(imagePath);

            var newImg = new Picture() { ImageName = imageName, ImageExtension = imageExtension, ImagePath = imagePath, MimeType = img.ContentType };

            return newImg;
        }

        private string CreateThumbnail(Picture original)
        {
            var srcImage = Image.FromFile(original.ImagePath);

            var thumbName = original.ImageName + ".png";
            var thumbWidth = srcImage.Width / 3 ;
            var thumbHeigth = srcImage.Height / 3 ;

            var newImage = new Bitmap(thumbWidth, thumbHeigth);
            var graphics = Graphics.FromImage(newImage);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.DrawImage(srcImage, new Rectangle(0, 0, thumbWidth, thumbHeigth));


            var path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Thumbnails"), thumbName);
            newImage.Save(path, ImageFormat.Png);

            return "/Upload/Thumbnails/" + thumbName;
        }

        public bool PictureValidation(string MimeType)
        {
            bool validation = false;

            switch (MimeType)
            {
                case "image/jpeg":
                    validation = true;
                    break;

                case "image/pjpeg":
                    validation = true;
                    break;

                case "image/png":
                    validation = true;
                    break;
                default:
                    break;
            }
            return validation;

        }
        #endregion

        #region Return data methods
        public override List<Picture> GetAll()
        {
            return db.Pictures.ToList();
        }

        public override Picture GetOne(int id)
        {
            var picture = db.Pictures.Find(id);
            return picture;
        }

        public List<SelectListItem> GetListItem()
        {
            var pictures = GetAll();
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            selectListItems.Add(new SelectListItem { Text = "None", Value = "0", Selected = true, Disabled = true });

            foreach (var pic in pictures)
            {
                selectListItems.Add(new SelectListItem { Text = pic.ImageName, Value = pic.ImageId.ToString() });
            }

            return selectListItems;
        }
        #endregion

    }
}