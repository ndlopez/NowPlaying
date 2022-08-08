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


namespace NowPlaying
{
    public partial class Form1 : Form
    {
        static System.Timers.Timer songTimer;
        public string gotStation;
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
                int myUpdate = 3000; //duration time in ms
                var myTime = DateTime.Now.ToString("HH:mm  ");
                string thisStation = "Third Rock";
                var auxVar = nowSong.ToString().Split("-");
                //Console.WriteLine(nowSong);
                notifyIcon1.ShowBalloonTip(myUpdate, DateTime.Now.ToString("HH:mm") + " Now on " + thisStation, nowSong.ToString(), ToolTipIcon.Info);
                
                nowArtist.Text = myTime + "\n" + auxVar[1].Trim();
                nowPlayingLabel.Text = auxVar[0];//GetImgAsync(); //

                notifyIcon1.Text = myTime + "Now: " + nowSong.ToString();
                nowPlayingAlbum.Text = GetImgPath(nowSong.ToString());

                if (GetImgPath(nowSong.ToString()) != "0")
                {
                    nowArtwork.ImageLocation = GetImgPath(nowSong.ToString());
                }
                else
                {
                    nowArtwork.ImageLocation = "https://lastfm.freetls.fastly.net/i/u/174s/c1322f3a5c3fcf4810078a14c4caae11.png";
                }
            }
            catch (Exception)
            {
                notifyIcon1.ShowBalloonTip(5000, "Cannot connect to URL", "Error", ToolTipIcon.Error);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        public async Task<string> GetDataAsync()
        {
            //string thirdRockURL = this is default
            string gotURL = "https://feed.tunein.com/profiles/s151799/nowPlaying?token=eyJwIjpmYWxzZSwidCI6IjIwMjItMDgtMDZUMTM6NDg6NTIuMzY1MDAwNVoifQ&itemToken=BgUFAAEAAQABAAEAb28B91ACAAEFAAA&formats=mp3,aac,ogg,flash,html,hls,wma&serial=9275b839-1f68-4ff5-8c9b-18162687b82a&partnerId=RadioTime&version=5.1904&itemUrlScheme=secure&reqAttempt=1";
            string firstProp = "Header";
            string currSong = "";

            if (gotStation == "fmlapaz")
            {
                firstProp = "data";
                gotURL = "https://icecasthd.net:2199/rpc/lapazfm/streaminfo.get";
            }

            var httpClient = new HttpClient();
            var releasesResponse = await JsonDocument.ParseAsync(await httpClient.GetStreamAsync(
                gotURL));            

            var root = releasesResponse.RootElement.GetProperty(firstProp);

            if (gotStation == "fmlapaz") { 
                var elems = root.EnumerateArray();
                
                //string currArtist = "";
                //string[] dat= {"",""}; //=new String[2];

                while (elems.MoveNext())
                {
                    var node = elems.Current;
                    var props = node.EnumerateObject();

                    while (props.MoveNext())
                    {
                        var prop = props.Current;
                        if (prop.Name == "song")//song
                        {
                            currSong = prop.Value.ToString();
                        }
                        else { currSong = "No song found"; }
                    }

                }
            
            }
            
            var thirdElm = root.EnumerateObject();
            while (thirdElm.MoveNext())
            {
                var myProp = thirdElm.Current;
                if (myProp.Name == "Subtitle")
                {
                    currSong = myProp.Value.ToString();
                }
            }
            
            return currSong;
        }

        private String GetImgPath(string currentSong)
        {
            string apiKey = "";
            var auxVar = currentSong.Split("-");
            string thisArtist = auxVar[0].Trim();
            string thisSong = auxVar[1].Trim();
            string xmlString = "https://ws.audioscrobbler.com/2.0/?method=track.getInfo&api_key=" +
                apiKey + "&artist=" + thisArtist + "&track=" + thisSong;
            string text = "";
            //return xmlString; To test if sth was returned.
            try {
                /*XmlDocument artistDoc = new XmlDocument();artistDoc.Load(xmlString);                
                // Album title from XML
                XmlNode node = artistDoc.DocumentElement.SelectSingleNode("track/album/title");
                text = (node.InnerText != null) ? node.InnerText:""; //Works fine if node has no attrib*/
                /* Imgs List
                XmlNodeList allImg = artistDoc.GetElementsByTagName("track/album/image.size");
                foreach (XmlElement imgSize in allImg)
                {
                    foreach (XmlElement imgs in imgSize.ChildNodes)
                    {
                        text = (imgs.InnerText != null)? imgs.InnerText : "Sorry, no such thing";
                    }
                }*/
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlString);
                XmlNode node = doc.DocumentElement.SelectSingleNode("track/album");
                if (node != null)
                {
                    int jdx = 0;
                    foreach (XmlNode nodes in node.SelectNodes("image"))
                    {
                        if (nodes.Attributes != null && nodes.HasChildNodes)
                        {
                            if (jdx == 2)
                                //jdx = 2 is img size 174x174
                                text = nodes.FirstChild.Value;
                        }
                        else { text = "No values here."; }
                        jdx += 1;
                    }
                }
                else { text = "0"; }
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

        private void fMLaPazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gotURL = "https://icecasthd.net:2199/rpc/lapazfm/streaminfo.get";
            gotStation = "fmlapaz";
        }

        private void thirdRockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gotURL = "https://feed.tunein.com/profiles/s151799/nowPlaying?token=eyJwIjpmYWxzZSwidCI6IjIwMjItMDgtMDZUMTM6NDg6NTIuMzY1MDAwNVoifQ&itemToken=BgUFAAEAAQABAAEAb28B91ACAAEFAAA&formats=mp3,aac,ogg,flash,html,hls,wma&serial=9275b839-1f68-4ff5-8c9b-18162687b82a&partnerId=RadioTime&version=5.1904&itemUrlScheme=secure&reqAttempt=1";
            gotStation = "thirdrock";
        }
    }

}
