using Telegram.Bot;
using System.Drawing.Imaging;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;

namespace TextToPictureTelegramBot
{
    public partial class Form1 : Form
    {
        public string Token { get; private set; } = "1898319362:AAGbrOnEPt7coIUk_sg7txKBqGBaR4cFl28";
        private string _path = "D://picture.jpg";
        private TelegramBotClient? _client;

        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap GetScreenShot()
        {
            Rectangle bounds = new(this.Bounds.X + 7, this.Bounds.Y + 30, 400, 400);
            Bitmap bitmap = new(bounds.Width, bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            return bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            _client = new TelegramBotClient(Token);
            _client.StartReceiving();
            _client.OnMessage += OnMessageHandler;
        }

        private async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            try
            {
                var message = e.Message;

                if (message.Text != null)
                {
                    labelText.Invoke((MethodInvoker)delegate
                    {
                        labelText.Text = message.Text.ToUpper();
                    });

                    GetScreenShot().Save(_path, ImageFormat.Jpeg);

                    using (var stream = File.Open(_path, FileMode.Open))
                    {
                        InputOnlineFile file = new(stream);
                        file.FileName = "picture.jpg";
                        var send = await _client.SendDocumentAsync(message.Chat.Id, file);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }            
        }
    }
}