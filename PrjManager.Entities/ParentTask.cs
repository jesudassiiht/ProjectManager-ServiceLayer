using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjManager.Entities
{
    [Table("ParentTask")]
    public class ParentTask
    {
        [Key]
        public int ParentTaskId { get; set; }
        public string ParentTaskTitle { get; set; }
    }
}
