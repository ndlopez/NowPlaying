/*
 * Display notification on the Windows tray
 * Now Playing on FM La Paz
 * Developer: ndzerglink (github.com/ndlopez)
 * Updated on 2021-09-11
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
using System.Windows.Forms;

namespace NowPlaying
{
    public partial class Form1 : Form
    {
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

        private async void Form1_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                try
                {
                    var nowSong = await GetDataAsync();
                    int myUpdate = 280000; //ms
                    //Console.WriteLine(nowSong);
                    notifyIcon1.ShowBalloonTip(myUpdate, DateTime.Now.ToString("%H:%m") + " Now on FM La Paz", nowSong.ToString(), ToolTipIcon.Info);

                }
                catch (Exception){ Console.WriteLine("Async onnection error"); }
                
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
    }
}
