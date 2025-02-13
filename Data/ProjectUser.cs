using System.ComponentModel.DataAnnotations;

namespace BuildMasterPro.Data
{
    public class ProjectUser
    {
        [Key]
        public int ProjectUserId { get; set; }

        public int? ProjectId { get; set; }

        public int? UserId { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }
}
