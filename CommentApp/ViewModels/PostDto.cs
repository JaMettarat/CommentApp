using CommentApp.Models;

namespace CommentApp.ViewModels
{
    public class PostDto
    {
        public IEnumerable<Post> Post { get; set; } = new List<Post>();
        public User UserLogin { get; set; }
    }   

}
