using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRsoft.Engine.Model.DataBaseTable
{
    [Table("AlmListTable")]
    public class AlmListTable
    {
        [Key] public DateTime? DateTime { get; set; }
        [Required] public int? number { get; set; }
        [Required] [MaxLength(500)] public string? Message { get; set; }
    }
}