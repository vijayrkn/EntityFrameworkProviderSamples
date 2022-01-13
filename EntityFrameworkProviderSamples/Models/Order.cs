using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkProviderSamples.Models
{
    public enum TShirtSize { XS, S, M, L, XL, XXL }
    public class Order
    {
        [Key]
        public Guid TrackingId { get; set; } = Guid.NewGuid();

        [Required]
        public string? EmployeeName { get; set; }

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

        [Display(Name = "T-Shirt Size")]
        public TShirtSize? ShirtSize { get; set; }
    }
}
