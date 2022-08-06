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
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

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
                var myTime = DateTime.Now.ToString("HH:mm  ");
                //Console.WriteLine(nowSong);
                notifyIcon1.ShowBalloonTip(myUpdate, DateTime.Now.ToString("HH:mm") + " Now on FM La Paz", nowSong.ToString(), ToolTipIcon.Info);
                var auxVar = nowSong.ToString().Split("-");

                nowArtist.Text = myTime + "\n" + auxVar[1].Trim();
                nowPlayingLabel.Text = auxVar[0];//GetImgAsync(); //

                notifyIcon1.Text = myTime + "Now: " + nowSong.ToString();
                nowPlayingAlbum.Text = GetImgPath(nowSong.ToString());
                nowArtwork.ImageLocation = "https://lastfm.freetls.fastly.net/i/u/174s/c1322f3a5c3fcf4810078a14c4caae11.png";

            }
            catch (Exception)
            {
                notifyIcon1.ShowBalloonTip(5000, "Cannot connect to URL", "Error", ToolTipIcon.Error);
                //Console.WriteLine("Async connection error");
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        public async Task<string> GetDataAsync()
        {
            string fmLaPazURL = "https://icecasthd.net:2199/rpc/lapazfm/streaminfo.get";
            var httpClient = new HttpClient();
            var releasesResponse = await JsonDocument.ParseAsync(await httpClient.GetStreamAsync(
                fmLaPazURL));

            var root = releasesResponse.RootElement.GetProperty("data");
            var elems = root.EnumerateArray();
            string currSong = "";
            //string currArtist = "";
            //string[] dat= {"",""}; //=new String[2];

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

        private String GetImgPath(string currentSong)
        {
            //var httpClient = new HttpClient();
            string apiKey = "";
            //currentSong = await GetDataAsync();
            var auxVar = currentSong.Split("-");
            string thisArtist = auxVar[1].Trim();
            string thisSong = auxVar[0];
            String xmlString = "https://ws.audioscrobbler.com/2.0/?method=track.getInfo&api_key=" +
                apiKey + "&artist=" + thisArtist + "&track=" + thisSong; //+"solar%20power";
            string text = "";
            Console.WriteLine("thisString: " + xmlString);

            try {
                //XmlDocument artistDoc = new XmlDocument();artistDoc.Load(xmlString);                
                // Album title from XML
                //XmlNode node = artistDoc.DocumentElement.SelectSingleNode("track/album/title");
                //text = (node.InnerText != null) ? node.InnerText:""; //Works fine if node has no attrib
                var artistDoc = XElement.Parse(xmlString);
                int i;
                var q = from node in artistDoc.Descendants("track")
                        let name = node.Attribute("album")
                        let length = node.Attribute("title")
                        select new { Name = (name != null) ? name.Value : "", Length = (length != null && Int32.TryParse(length.Value, out i)) ? i : 0 };

                foreach (var node in q)
                {
                    Console.WriteLine("Name={0}, Length={1}", node.Name, node.Length);
                }
                /* Imgs List
                XmlNodeList allImg = artistDoc.GetElementsByTagName("track/album/image.size");
                foreach (XmlElement imgSize in allImg)
                {
                    foreach (XmlElement imgs in imgSize.ChildNodes)
                    {
                        text = (imgs.InnerText != null)? imgs.InnerText : "Sorry, no such thing";
                    }
                }*/
            }
            catch
            {
                text = "No internet connection or path error";
            }
            
            return text;
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
