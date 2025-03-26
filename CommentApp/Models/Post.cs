using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommentApp.Models
{
    public class Post
    {        
        [Key]
        public Guid Id { get; set; }        
        public string Content { get; set; }
        public string ImgageUrl { get; set; }
        public Guid UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Timestamp { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
