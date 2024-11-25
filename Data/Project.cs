﻿using System.ComponentModel.DataAnnotations;
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
        public DateTime Startdate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Enddate { get; set; }

        public string Status { get; set; } = "Ongoing";
    }
}
