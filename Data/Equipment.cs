using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildMasterPro.Data
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Equipment name

        [Required]
        [StringLength(255)]
        public string Description { get; set; } // Short description of the equipment

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPerHour { get; set; } = 0; // Rental or operational cost per hour

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Available"; // Available, In Use, Under Maintenance

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ProjectEquipment>? ProjectEquipment { get; set; }
    }
}
