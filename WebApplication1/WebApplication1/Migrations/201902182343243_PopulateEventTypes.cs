namespace VetsEvents.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateEventTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO EventTypes (Id,Name) VALUES(1,'Job Fair')");
            Sql("INSERT INTO EventTypes (Id,Name) VALUES(2,'Free Medical Consultation')");
            Sql("INSERT INTO EventTypes (Id,Name) VALUES(3,'Free Training')");
            Sql("INSERT INTO EventTypes (Id,Name) VALUES(4,'Job Network')");
            Sql("INSERT INTO EventTypes (Id,Name) VALUES(5,'Conference')");
            Sql("INSERT INTO EventTypes (Id,Name) VALUES(6,'Fundraiser')");

        }

        public override void Down()
        {
            Sql("DELETE FROM EventTypes Where Id IN (1,2,3,4,5,6)");
        }
    }
}
