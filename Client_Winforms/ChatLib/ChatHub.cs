using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatLib
{
    public class ChatHub
    {
        public static ChatHub? Parse(string json) => JsonSerializer.Deserialize<ChatHub>(json);

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public string Message { get; set; } = string.Empty;

        public string ToJsonString() => JsonSerializer.Serialize(this);
    }
}
