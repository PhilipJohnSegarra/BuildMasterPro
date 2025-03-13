using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildMasterPro.Data
{
    public class TaskUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        [ForeignKey(nameof(TaskId))]
        public ProjectTask? ProjectTask { get; set; }
    }
}
