namespace VetsEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropToEvent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "EventOrganizer_Id", newName: "EventOrganizerId");
            RenameColumn(table: "dbo.Events", name: "EventType_Id", newName: "EventTypeId");
            RenameIndex(table: "dbo.Events", name: "IX_EventOrganizer_Id", newName: "IX_EventOrganizerId");
            RenameIndex(table: "dbo.Events", name: "IX_EventType_Id", newName: "IX_EventTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Events", name: "IX_EventTypeId", newName: "IX_EventType_Id");
            RenameIndex(table: "dbo.Events", name: "IX_EventOrganizerId", newName: "IX_EventOrganizer_Id");
            RenameColumn(table: "dbo.Events", name: "EventTypeId", newName: "EventType_Id");
            RenameColumn(table: "dbo.Events", name: "EventOrganizerId", newName: "EventOrganizer_Id");
        }
    }
}
