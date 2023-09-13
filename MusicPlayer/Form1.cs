using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        int minut;
        int second;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private RenPlayer Mp3Player = new RenPlayer();

        Bitmap playImage = new Bitmap(MusicPlayer.Properties.Resources.play);
        Bitmap pauseImage = new Bitmap(MusicPlayer.Properties.Resources.pause);

        public Form1()
        {
            InitializeComponent();
        }

        public string GetTimeMinutsAndSeconds(int miliseconds)
        {
            second = miliseconds / 1000;
            minut = second / 60;
            return String.Format("{0:00}", (float)minut) + ":" + String.Format("{0:00}", (float)(second % 60));
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "Mp3 Files|*.mp3";

                if(file.ShowDialog() == DialogResult.OK)
                {
                    Mp3Player.Open(file.FileName);

                    Mp3Player.Play();
                    button1.Image = pauseImage;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Mp3Player.IsPlaying == true)
            {
                Mp3Player.Stop();
                button1.Image = playImage;
            }
            else
            {
                Mp3Player.Play();
                button1.Image = pauseImage;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
