using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BuildMasterPro.Data
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        public string ProjectName { get; set; } = string.Empty;
        [AllowNull]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Startdate { get; set; } = DateTime.Now.Date;
        [DataType(DataType.DateTime)]
        public DateTime Enddate { get; set; } = DateTime.Now.Date;
        public string Status { get; set; } = "Ongoing";
        public string Address {get;set;} = string.Empty;
        public int? ClientId { get; set; }
        public bool isDeleted { get; set; } = false;

        [ForeignKey(nameof(ClientId))]
        public Client? Client { get; set; }

        public ICollection<ProjectUser>? ProjectUsers { get; set; }
        public ICollection<ProjectEquipment>? ProjectEquipments { get; set; }
        public ICollection<ProjectTask>? ProjectTasks { get; set; }
    }
}
