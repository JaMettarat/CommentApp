using System.ComponentModel.DataAnnotations.Schema;

namespace CommentApp.ViewModels
{
    public class CommentDto
    {
        public string Comment { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}
