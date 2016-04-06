namespace UploadProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        FilePath = c.String(nullable: false),
                        MimeType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(nullable: false),
                        ImageExtension = c.String(nullable: false),
                        ImagePath = c.String(nullable: false),
                        MimeType = c.String(nullable: false),
                        ThumbnailPath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pictures");
            DropTable("dbo.Files");
        }
    }
}
