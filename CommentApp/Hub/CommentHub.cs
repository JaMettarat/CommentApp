using Microsoft.AspNetCore.SignalR;

namespace CommentApp.Hubs
{
    public class CommentHub : Hub
    {
        // ใช้ส่งข้อความหรือการแจ้งเตือนไปยัง Client
        public async Task SendComment(string postId, string username, string text)
        {
            await Clients.All.SendAsync("ReceiveComment", postId, username, text);
        }
    }
}