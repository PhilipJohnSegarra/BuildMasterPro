using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BuildMasterPro.Data
{
    public class ProjectEquipment
    {
        [Key]
        public int ProjectEquipmentId { get; set; }

        [Required]
        public int ProjectId { get; set; } // Foreign Key to Project

        [Required]
        public int EquipmentId { get; set; } // Foreign Key to Equipment

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UsageHours { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPerHour { get; set; } = 0;

        [NotMapped] // This is a computed field, not stored in the DB
        public decimal TotalCost => UsageHours * CostPerHour;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Assigned"; // Assigned, In Use, Returned, Maintenance

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public Equipment? Equipment { get; set; }
    }
}
