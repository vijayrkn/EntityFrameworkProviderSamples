using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkProviderSamples.Models
{
    public class Order
    {
        [Key]
        public Guid TrackingId { get; set; } = Guid.NewGuid();

        [Required]
        public string? CustomerName { get; set; }

        [Required]
        public string? Street { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        [StringLength(2)]
        public string? State { get; set; }

        [Required]
        [Range(0, 99999)]
        public int? ZipCode { get; set; }
    }
}
