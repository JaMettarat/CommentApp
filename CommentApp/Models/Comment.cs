using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommentApp.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Timestamp { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


    }
}
