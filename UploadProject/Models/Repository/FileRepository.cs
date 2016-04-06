using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadProject.Models.Domain;

namespace UploadProject.Models.Repository
{
    public class FileRepository : Repository<File, HttpPostedFileBase>
    {
        public FileRepository(UploadContext db) : base(db) { }

        #region Creation methods
        public override void Add(HttpPostedFileBase item)    // save file path on database
        {
            var file = Create(item);
            db.Files.Add(file);
            db.SaveChanges();
        }

        protected override File Create(HttpPostedFileBase postedFile)  // create file on filesystem returning a file object with his path
        {
            var fileName = System.IO.Path.GetFileName(postedFile.FileName);
            var filePath = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/Files"), fileName);
            postedFile.SaveAs(filePath);

            var newFile = new File() { FileName = fileName, FilePath = filePath, MimeType = postedFile.ContentType };

            return newFile;
        }
        #endregion

        #region Return data methods
        public override List<File> GetAll()  // return all files in database as a list
        {
            return db.Files.ToList();
        }

        public override File GetOne(int id) // return one file by id
        {
            var file = db.Files.Find(id);
            return file;
        }
        #endregion

    }
}