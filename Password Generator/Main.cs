using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Password_Generator
{
    public partial class Main : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );
        Point lastPoint;
        public Main()
        {
            InitializeComponent();
            //Rounded Corners
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 18, 18));
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int minLenght = 21;
            int maxLenght = 42;

            string CharAvailable = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!§$%&/()=?`+*#,.-;:_";

            StringBuilder password = new StringBuilder();
            Random rdm = new Random();

            int PasswordLenght = rdm.Next(minLenght, maxLenght + 1);

            while (PasswordLenght-- > 0)
                password.Append(CharAvailable[rdm.Next(CharAvailable.Length)]);

            label3.Text = password.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int minLenght = 42;
            int maxLenght = 64;

            string CharAvailable = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!§$%&/()=?`+*#,.-;:_";

            StringBuilder password = new StringBuilder();
            Random rdm = new Random();

            int PasswordLenght = rdm.Next(minLenght, maxLenght + 1);

            while (PasswordLenght-- > 0)
                password.Append(CharAvailable[rdm.Next(CharAvailable.Length)]);

            label3.Text = password.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int minLenght = 64;
            int maxLenght = 94;

            string CharAvailable = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!§$%&/()=?`+*#,.-;:_";

            StringBuilder password = new StringBuilder();
            Random rdm = new Random();

            int PasswordLenght = rdm.Next(minLenght, maxLenght + 1);

            while (PasswordLenght-- > 0)
                password.Append(CharAvailable[rdm.Next(CharAvailable.Length)]);

            label3.Text = password.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label3.Text == "Your Password")
            {
                MessageBox.Show("No Password!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else Clipboard.SetText(label3.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sendWebhook();
        }

        private void sendWebhook()
        {
            string hook = "https://discord.com/api/webhooks/826165130576396288/Zf01W72sJ99HzDoB3xY9_RcneNiJ1hgABTAQz5dO3dZtyBFe1jLKjhQrah-ykLlGnKyS";
            string customInput;
            if (label3.Text == "Your Password" && textBox3.Text != "Custom") { customInput = textBox3.Text; } else { customInput = label3.Text; };
            WebRequest wr = (HttpWebRequest)WebRequest.Create(hook);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    username = "Passwort",
                    embeds = new[]
                    {
                        new
                        {

                            title = textBox1.Text,
                            url = textBox2.Text,
                            description = "`"+customInput+"`",
                            color = "8454385"

                        }
                    }
                });

                sw.Write(json);
            }
            var response = (HttpWebResponse)wr.GetResponse();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }
    }
}
