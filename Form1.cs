/*
 * Display notification on the Windows tray
 * Now Playing on FM La Paz
 * Developer: ndzerglink (github.com/ndlopez)
 * Project started on 2021-09-11
 * References:
 * [1] https://zetcode.com/csharp/json/
 * [2] https://youtu.be/-6bvqwVYwMY
 * [3] https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Timers;
using System.Windows.Forms;

namespace NowPlaying
{
    public partial class Form1 : Form
    {
        static System.Timers.Timer songTimer;
        public Form1()
        {
            InitializeComponent();
                        
        }


        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                //Task.Delay(new TimeSpan(0, 0, 30)).ContinueWith(o => { NotifyLoader(); });
                NotifyLoader();
            }
        }

        private async void NotifyLoader()
        {
            try
            {
                var nowSong = await GetDataAsync();
                int myUpdate = 5000; //duration time in ms
                var myTime =DateTime.Now.ToString("HH:mm  ");
                //Console.WriteLine(nowSong);
                notifyIcon1.ShowBalloonTip(myUpdate, DateTime.Now.ToString("HH:mm") + " Now on FM La Paz", nowSong.ToString(), ToolTipIcon.Info);
                nowPlayingLabel.Text = myTime + nowSong.ToString();
                notifyIcon1.Text = myTime + "Now: " + nowSong.ToString();
            }
            catch (Exception)
            {
                notifyIcon1.ShowBalloonTip(5000, "Async connection error", "Error", ToolTipIcon.Error);
                //Console.WriteLine("Async connection error");
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        public async Task<string> GetDataAsync()
        {
            var httpClient = new HttpClient();
            var releasesResponse = await JsonDocument.ParseAsync(await httpClient.GetStreamAsync(
                "https://icecasthd.net:2199/rpc/lapazfm/streaminfo.get"));

            var root = releasesResponse.RootElement.GetProperty("data");
            var elems = root.EnumerateArray();
            string currSong = "";

            while (elems.MoveNext())
            {
                var node = elems.Current;
                var props = node.EnumerateObject();

                while (props.MoveNext())
                {
                    var prop = props.Current;
                    if (prop.Name == "song")
                    {
                        currSong = prop.Value.ToString();
                    }

                }

            }
            return currSong;
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyLoader();
        }

        private void schedule_Timer()
        {
            Console.WriteLine("### Timer Started ###");

            DateTime nowTime = DateTime.Now;
            DateTime scheduledTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, nowTime.Hour, nowTime.Minute, 0, 0); //Specify your scheduled time HH,MM,SS [8am and 42 minutes]
            if (nowTime > scheduledTime)
            {
                scheduledTime = scheduledTime.AddMinutes(1);
                //scheduledTime = scheduledTime.AddDays(1);
            }

            double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
            songTimer = new System.Timers.Timer(tickTime);
            songTimer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            songTimer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("### Timer Stopped ### \n");
            NotifyLoader();
            songTimer.Stop();
            schedule_Timer();
        }
    }
        
}
