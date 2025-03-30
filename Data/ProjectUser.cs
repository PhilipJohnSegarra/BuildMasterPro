using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BuildMasterPro.Data
{
    public class ProjectUser
    {
        [Key]
        public int ProjectUserId { get; set; }

        public int? ProjectId { get; set; }

        public string? UserId { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        [ForeignKey(nameof(ProjectId))]
        [JsonIgnore]
        public Project? Project { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}
