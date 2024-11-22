using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BuildMasterPro.Data
{
    public class ProjectTask
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string TaskName { get; set; } = string.Empty;
        [Required]
        public string TaskDescription { get; set; } = string.Empty;

        //AssignedTo property later
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        [AllowNull]
        public string Status { get; set; } = "Not Started";
        [AllowNull]
        public string Priority { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; } = default!;
    }
}
