using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRsoft.Engine.Model.DataBaseTable
{
    [Table("UserLoginTable")]
    public class UserLogin
    {
        [Key] public string? UserName { get; set; }
        [Required] public string? Password { get; set; }
        [Required] public bool DebugLimit { get; set; }
        [Required] public bool ParamSetLimit { get; set; }
        [Required] public bool MarkingLimit { get; set; }
        [Required] public bool PhotoLimit { get; set; }
    }
}
