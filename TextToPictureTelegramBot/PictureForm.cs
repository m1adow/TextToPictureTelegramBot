using Telegram.Bot;
using System.Drawing.Imaging;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using TextToPictureTelegramBot.Models;

namespace TextToPictureTelegramBot
{
    public partial class PictureForm : Form
    {
        public string Token { get; private set; } = "1898319362:AAGbrOnEPt7coIUk_sg7txKBqGBaR4cFl28";
        private string _path = $@"{Environment.CurrentDirectory}/picture.jpg";
        private TelegramBotClient? _client;
        private List<User> _users = new();

        public PictureForm()
        {
            InitializeComponent();
        }

        private Bitmap GetScreenShot()
        {
            Rectangle bounds = new(this.Bounds.X + 20, this.Bounds.Y + 50, this.Bounds.Width - 40, this.Bounds.Height - 100);
            Bitmap bitmap = new(bounds.Width, bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            return bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelText.BringToFront();
            _client = new TelegramBotClient(Token);
            _client.StartReceiving();
            _client.OnMessage += OnMessageHandler;
        }

        private async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            try
            {
                User? currentUser = _users.FirstOrDefault(u => u.ChatId == e.Message.Chat.Id);

                if (currentUser is null)
                {
                    currentUser = new User
                    {
                        ChatId = e.Message.Chat.Id,
                        State = UserState.Basic,
                        BackgroundColor = Color.White,
                        TextColor = Color.Black,
                        HeartColor = "NONE"
                    };

                    _users.Add(currentUser);
                }

                var message = e.Message;

                if (currentUser.State == UserState.Basic)
                {
                    if (message.Text != null && message.Text[0] != '/')
                    {
                        this.BackColor = currentUser.BackgroundColor;
                        labelText.ForeColor = currentUser.TextColor;
                        SetHeart(currentUser.HeartColor);

                        labelText.Invoke((MethodInvoker)delegate
                        {
                            labelText.Text = message.Text.ToUpper();
                            labelText.Left = (this.ClientSize.Width - labelText.Width) / 2;
                            labelText.Top = (this.ClientSize.Height - labelText.Height) / 2;
                        });

                        pictureBox.Invoke((MethodInvoker)delegate
                        {
                            pictureBox.Left = (this.ClientSize.Width - pictureBox.Width) / 2;
                            pictureBox.Top = (this.ClientSize.Height - pictureBox.Height) / 2;
                        });

                        GetScreenShot().Save(_path, ImageFormat.Jpeg);

                        using (var stream = File.Open(_path, FileMode.Open))
                        {
                            InputOnlineFile file = new(stream);
                            file.FileName = "picture.jpg";
                            await _client.SendDocumentAsync(message.Chat.Id, file);
                        }
                    }
                    else if (message.Text != null && message.Text[0] == '/')
                    {
                        switch (message.Text)
                        {
                            case "/background":
                                currentUser.State = UserState.EnterBackgroundColor;
                                await _client.SendTextMessageAsync(currentUser.ChatId, "Choose color of background: Red, Green, Blue, White, Black");
                                return;
                            case "/textcolor":
                                currentUser.State = UserState.EnterTextColor;
                                await _client.SendTextMessageAsync(currentUser.ChatId, "Choose color of background: Red, Green, Blue, White, Black");
                                return;
                            case "/heart":
                                currentUser.State = UserState.EnterHeartColor;
                                await _client.SendTextMessageAsync(currentUser.ChatId, "Choose color of background: Red, White, Rose, Black, None");
                                return;
                            default:
                                await _client.SendTextMessageAsync(currentUser.ChatId, "Bot doesn't exist this command");
                                return;
                        }
                    }
                }

                if (currentUser.State == UserState.EnterBackgroundColor)
                {
                    ChangeBackgroundColor(_client, currentUser, e);
                    await _client.SendTextMessageAsync(currentUser.ChatId, "Your color was changed");
                    currentUser.State = UserState.Basic;
                }

                if (currentUser.State == UserState.EnterTextColor)
                {
                    ChangeTextColor(_client, currentUser, e);
                    await _client.SendTextMessageAsync(currentUser.ChatId, "Your color was changed");
                    currentUser.State = UserState.Basic;
                }

                if(currentUser.State == UserState.EnterHeartColor)
                {
                    ChangeHeartColor(_client, currentUser, e);
                    await _client.SendTextMessageAsync(currentUser.ChatId, "Your heart was changed");
                    currentUser.State = UserState.Basic;
                }
            }
            catch (Exception ex) { }
        }

        private async void ChangeBackgroundColor(TelegramBotClient client, User user, MessageEventArgs e)
        {
            switch (e.Message.Text.ToUpper())
            {
                case "RED":
                    user.BackgroundColor = Color.Red;
                    return;
                case "GREEN":
                    user.BackgroundColor = Color.Green;
                    return;
                case "BLUE":
                    user.BackgroundColor = Color.Blue;
                    return;
                case "WHITE":
                    user.BackgroundColor = Color.White;
                    return;
                case "BLACK":
                    user.BackgroundColor = Color.Black;
                    return;
                default:
                    await client.SendTextMessageAsync(user.ChatId, "You can't choose this color");
                    return;
            }
        }

        private async void ChangeTextColor(TelegramBotClient client, User user, MessageEventArgs e)
        {
            switch (e.Message.Text.ToUpper())
            {
                case "RED":
                    user.TextColor = Color.Red;
                    return;
                case "GREEN":
                    user.TextColor = Color.Green;
                    return;
                case "BLUE":
                    user.TextColor = Color.Blue;
                    return;
                case "WHITE":
                    user.TextColor = Color.White;
                    return;
                case "BLACK":
                    user.TextColor = Color.Black;
                    return;
                default:
                    await client.SendTextMessageAsync(user.ChatId, "You can't choose this color.");
                    return;
            }
        }

        private async void ChangeHeartColor(TelegramBotClient client, User user, MessageEventArgs e)
        {
            switch (e.Message.Text.ToUpper())
            {
                case "RED":
                    user.HeartColor = "RED";
                    return;
                case "ROSE":
                    user.HeartColor = "ROSE";
                    return;
                case "WHITE":
                    user.HeartColor = "WHITE";
                    return;
                case "BLACK":
                    user.HeartColor = "BLACK";
                    return;
                case "NONE":
                    user.HeartColor = "NONE";
                    return;
                default:
                    await client.SendTextMessageAsync(user.ChatId, "You can't choose this color");
                    return;
            }
        }

        private void SetHeart(string color)
        {
            switch (color)
            {
                case "RED":
                    pictureBox.Image = Resources.Images.redHeart;
                    return;
                case "ROSE":
                    pictureBox.Image = Resources.Images.roseHeart;
                    return;
                case "WHITE":
                    pictureBox.Image = Resources.Images.whiteHeart;
                    return;
                case "BLACK":
                    pictureBox.Image = Resources.Images.blackHeart;
                    return;
                case "NONE":
                    pictureBox.Image = null;
                    return;
            }
        }
    }
}