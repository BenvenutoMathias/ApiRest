using System.ComponentModel.DataAnnotations;

namespace RestApi.Entities.DataEntities
{
    public class Chapter : BaseEntity
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = new Course();
        [Required]
        public string List = string.Empty;
    }
}
