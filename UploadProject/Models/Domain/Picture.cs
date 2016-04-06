using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UploadProject.Models.Domain
{
    public class Picture
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string ImageName { get; set; }
        [Required]
        public string ImageExtension { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public string MimeType { get; set; }
        [Required]
        public string ThumbnailPath { get; set; }
    }
}