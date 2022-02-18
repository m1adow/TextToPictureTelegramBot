using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToPictureTelegramBot.Models
{
    internal class User
    {
        public UserState State { get; set; }
        public long ChatId { get; set; }
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }
        public Color HeartColor { get; set; }
    }

    internal enum UserState
    {
        Basic = 0,
        EnterBackgroundColor = 1,
        EnterTextColor = 2,
        EnterHeartColor = 3
    }
}
