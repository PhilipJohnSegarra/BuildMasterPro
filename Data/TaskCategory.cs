using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BuildMasterPro.Data
{
    public class TaskCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string CategoryName { get; set; } = "";
        [AllowNull]
        public string Description { get; set; } = "";

        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }
    }
}
