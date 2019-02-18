namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverrideConventionsForEventsAndGenres : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Events", "Organizer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "Genre_Id" });
            DropIndex("dbo.Events", new[] { "Organizer_Id" });
            AlterColumn("dbo.Events", "Venue", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Events", "Genre_Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Events", "Organizer_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Genres", "Name", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Events", "Genre_Id");
            CreateIndex("dbo.Events", "Organizer_Id");
            AddForeignKey("dbo.Events", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Organizer_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Organizer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Events", new[] { "Organizer_Id" });
            DropIndex("dbo.Events", new[] { "Genre_Id" });
            AlterColumn("dbo.Genres", "Name", c => c.String());
            AlterColumn("dbo.Events", "Organizer_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Events", "Genre_Id", c => c.Byte());
            AlterColumn("dbo.Events", "Venue", c => c.String());
            CreateIndex("dbo.Events", "Organizer_Id");
            CreateIndex("dbo.Events", "Genre_Id");
            AddForeignKey("dbo.Events", "Organizer_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Events", "Genre_Id", "dbo.Genres", "Id");
        }
    }
}
