using Telegram.Bot;
using System.Drawing.Imaging;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;

namespace TextToPictureTelegramBot
{
    public partial class PictureForm : Form
    {
        public string Token { get; private set; } = "1898319362:AAGbrOnEPt7coIUk_sg7txKBqGBaR4cFl28";
        private string _path = $@"{Environment.CurrentDirectory}/picture.jpg";
        private TelegramBotClient? _client;

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
                var message = e.Message;

                if (message.Text != null && message.Text[0] != '/')
                {
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
                        var send = await _client.SendDocumentAsync(message.Chat.Id, file);
                    }

                }
                else if (message.Text != null && message.Text[0] == '/')
                {
                    if (message.Text == "/background")
                    {
                        _client.OnMessage -= OnMessageHandler;
                        await _client.SendTextMessageAsync(message.Chat.Id, "Choose color of background: Red, Green, Blue, White, Black");
                        _client.OnMessage += OnBackGroundHandler;
                    }
                    else if (message.Text == "/textcolor")
                    {
                        _client.OnMessage -= OnMessageHandler;
                        await _client.SendTextMessageAsync(message.Chat.Id, "Choose color of text: Red, Green, Blue, White, Black");
                        _client.OnMessage += OnTextColorHandler;
                    }
                    else if (message.Text == "/heart")
                    {
                        _client.OnMessage -= OnMessageHandler;
                        await _client.SendTextMessageAsync(message.Chat.Id, "Choose color of heart: Red, Rose, White, Black\nFor delete: None");
                        _client.OnMessage += OnHeartHandler;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private async void OnBackGroundHandler(object sender, MessageEventArgs e)
        {
            try
            {
                var message = e.Message;

                if (message.Text != null)
                {
                    switch (message.Text.ToUpper())
                    {
                        case "RED":
                            this.BackColor = Color.Red;
                            break;
                        case "GREEN":
                            this.BackColor = Color.Green;
                            break;
                        case "BLUE":
                            this.BackColor = Color.Blue;
                            break;
                        case "WHITE":
                            this.BackColor = Color.White;
                            break;
                        case "BLACK":
                            this.BackColor = Color.Black;
                            break;
                        default:
                            await _client.SendTextMessageAsync(message.Chat.Id, "You can't choose this color.");
                            break;
                    }
                }
            }
            catch (Exception exc) { }

            _client.OnMessage -= OnBackGroundHandler;
            _client.OnMessage += OnMessageHandler;
        }

        private async void OnTextColorHandler(object sender, MessageEventArgs e)
        {
            try
            {
                var message = e.Message;

                if (message.Text != null)
                {
                    switch (message.Text.ToUpper())
                    {
                        case "RED":
                            labelText.ForeColor = Color.Red;
                            break;
                        case "GREEN":
                            labelText.ForeColor = Color.Green;
                            break;
                        case "BLUE":
                            labelText.ForeColor = Color.Blue;
                            break;
                        case "WHITE":
                            labelText.ForeColor = Color.White;
                            break;
                        case "BLACK":
                            this.BackColor = Color.Black;
                            break;
                        default:
                            await _client.SendTextMessageAsync(message.Chat.Id, "You can't choose this color.");
                            break;
                    }
                }
            }
            catch (Exception exc) { }

            _client.OnMessage -= OnTextColorHandler;
            _client.OnMessage += OnMessageHandler;
        }

        private async void OnHeartHandler(object sender, MessageEventArgs e)
        {
            try
            {
                var message = e.Message;

                if (message.Text != null)
                {
                    switch (message.Text.ToUpper())
                    {
                        case "RED":
                            pictureBox.Image = Resources.Images.redHeart;
                            break;
                        case "ROSE":
                            pictureBox.Image = Resources.Images.roseHeart;
                            break;
                        case "WHITE":
                            pictureBox.Image = Resources.Images.whiteHeart;
                            break;
                        case "BLACK":
                            pictureBox.Image = Resources.Images.blackHeart;
                            break;
                        case "NONE":
                            pictureBox.Image = null;
                            break;
                        default:
                            await _client.SendTextMessageAsync(message.Chat.Id, "You can't choose this color.");
                            break;
                    }
                }
            }
            catch (Exception exc) { }

            _client.OnMessage -= OnHeartHandler;
            _client.OnMessage += OnMessageHandler;
        }
    }
}