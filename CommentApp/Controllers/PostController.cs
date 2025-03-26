using CommentApp.Data;
using CommentApp.Hubs;
using CommentApp.Models;
using CommentApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CommentApp.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments.OrderByDescending(c => c.Timestamp))
                .ThenInclude(c => c.User)
                .ToListAsync();

            if (posts == null)
            {
                return View(new PostDto()); // Return an empty PostDto if no posts are available
            }

            var userLogin = new User
            {
                Id = Guid.Parse("2f69d9bb-5e65-46ce-a292-420e9a137e6a"),
                Username = "Blend 285",
                Email = "jane@example.com",
                PasswordHash = "hashed_password2"
            };

            var postDto = new PostDto
            {
                Post = posts,
                UserLogin = userLogin
            };

            return View(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                var post = new Post { Content = content };
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDto commentObj, [FromServices] IHubContext<CommentHub> commentHub)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment { PostId = commentObj.PostId, Text = commentObj.Comment, UserId = commentObj.UserId };
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                // ส่งข้อความไปยังทุก Client ผ่าน SignalR
                await commentHub.Clients.All.SendAsync("ReceiveComment", commentObj.PostId, commentObj.Username, commentObj.Comment);
            }
            else
                ModelState.AddModelError("Text", "Comment is required");

            return RedirectToAction("Index");
        }

    }
}
