using System.Data.Entity.ModelConfiguration;
using VetsEvents.Models;

namespace VetsEvents.Persistence.EntityConfigurations
{
    public class EventTypeConfiguration : EntityTypeConfiguration<EventType>
    {
        public EventTypeConfiguration()
        {
            Property(et => et.Name)
                .IsRequired()
                .HasMaxLength(255);
                
        }
    }
}