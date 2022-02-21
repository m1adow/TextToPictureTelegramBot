using Telegram.Bot.Types.ReplyMarkups;

namespace TextToPictureTelegramBot.Models
{
    internal class Buttons
    {
        public static IReplyMarkup Colors()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Red" }, new KeyboardButton { Text = "Green" }, new KeyboardButton { Text = "Blue" }, new KeyboardButton { Text = "White" }, new KeyboardButton { Text = "Black" } },
                }
            };
        }

        public static IReplyMarkup ColorsOfHeart()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Red" }, new KeyboardButton { Text = "White" }, new KeyboardButton { Text = "Rose" }, new KeyboardButton { Text = "Black" }, new KeyboardButton { Text = "None" } },
                }
            };
        }
    }
}
