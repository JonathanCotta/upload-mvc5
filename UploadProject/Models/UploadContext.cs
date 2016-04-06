using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UploadProject.Models.Domain; 

namespace UploadProject.Models
{
    public class UploadContext : DbContext
    {
        public UploadContext() : base("name=UploadContext") { }

        public DbSet<File> Files { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}