using System.ComponentModel.DataAnnotations;

namespace VetsEvents.Models
{
    public class EventType
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}