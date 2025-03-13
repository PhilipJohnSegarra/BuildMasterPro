using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildMasterPro.Data
{
    public class TaskActivity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Content {  get; set; }
        public string? ImagesMongoID { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int? TaskId { get; set; }
        public string? ActivityUserId { get; set; }

        [ForeignKey(nameof(ActivityUserId))]
        public ApplicationUser? User { get; set; }

        [ForeignKey(nameof(TaskId))]
        public ProjectTask? Task { get; set; }
    }
}
