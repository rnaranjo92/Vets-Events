using System.Data.Entity.ModelConfiguration;
using VetsEvents.Models;

namespace VetsEvents.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => new { a.EventId , a.AttendeeId});
        }
    }
}