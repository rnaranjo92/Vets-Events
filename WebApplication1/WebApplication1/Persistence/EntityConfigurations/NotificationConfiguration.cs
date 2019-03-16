using System.Data.Entity.ModelConfiguration;
using VetsEvents.Models;

namespace VetsEvents.Persistence.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            HasRequired(n => n.Event);
        }
    }
}