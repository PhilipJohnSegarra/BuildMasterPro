using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BuildMasterPro.Data
{
    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = ResourceType.Others.ToString();
        [Required]
        public string Status { get; set; } = ResourceStatus.Available.ToString();
        [AllowNull]
        public double Quantity { get; set; } = 0;



        public enum ResourceType
        {
            Equipment,
            Materials,
            Personnel,
            Others
        }

        public enum ResourceStatus
        {
            Available,
            Unavailable,
            Disposed
        }
    }
}
