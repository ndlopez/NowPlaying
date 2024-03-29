﻿/*
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
        // static System.Timers.Timer songTimer;
        public string gotStation;
        public bool isPlaying = true;
        // public object playStream = new WMPLib.WindowsMediaPlayer();
        //get User screen size: does not recognize
        /*public double height = SystemParameters.FullPrimaryScreenHeight;
        public double width = SystemParameters.FullPrimaryScreenWidth;
        public double resolution = height * width*/
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
                var auxVar = nowSong.ToString().Split(",");
                var aux2 = auxVar[0].ToString().Split("-");
                //Console.WriteLine(nowSong);
                notifyIcon1.ShowBalloonTip(myUpdate, DateTime.Now.ToString("HH:mm") + " Now on " + thisStation, auxVar[0].ToString(), ToolTipIcon.Info);

                nowTime.Text = myTime;
                nowArtist.Text = aux2[1].Trim();
                nowPlayingLabel.Text = aux2[0];//GetImgAsync(); //

                notifyIcon1.Text = myTime + "Now: " + auxVar[0].ToString();
                
                if (GetImgPath(auxVar[0].ToString()) != "")
                {
                    nowArtwork.ImageLocation = GetImgPath(auxVar[0].ToString());
                    nowPlayingAlbum.Text = "Status: OK";//GetImgPath(nowSong.ToString());
                }
                else
                {
                    nowArtwork.ImageLocation = auxVar[1].ToString();
                    //"http://cdn-profiles.tunein.com/s151799/images/logoq.jpg?t=636355620405200000"; 
                    //"https://lastfm.freetls.fastly.net/i/u/174s/c1322f3a5c3fcf4810078a14c4caae11.png";
                    nowPlayingAlbum.Text = "Status: AudioScrobbler error";
                }
            }
            catch (Exception)
            {
                notifyIcon1.ShowBalloonTip(3000, "Cannot connect to URL", "Error", ToolTipIcon.Error);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        public async Task<string> GetDataAsync()
        {
            //string thirdRockURL = default
            string gotURL = "https://feed.tunein.com/profiles/s151799/nowPlaying";
            //"?token=eyJwIjpmYWxzZSwidCI6IjIwMjItMDgtMDZUMTM6NDg6NTIuMzY1MDAwNVoifQ&itemToken=BgUFAAEAAQABAAEAb28B91ACAAEFAAA&formats=mp3,aac,ogg,flash,html,hls,wma&serial=9275b839-1f68-4ff5-8c9b-18162687b82a&partnerId=RadioTime&version=5.1904&itemUrlScheme=secure&reqAttempt=1";
            string firstProp = "Header";
            string secProp = "Secondary";
            string currSong = "";
            string gotImg = "";

            if (gotStation == "fmlapaz")
            {
                firstProp = "data";
                gotURL = "https://stream.consultoradas.com/cp/get_info.php?p=8042";
                // the above is JSON formatted but not accessible
            }

            var httpClient = new HttpClient();
            var releasesResponse = await JsonDocument.ParseAsync(await httpClient.GetStreamAsync(
                gotURL));

            var root = releasesResponse.RootElement.GetProperty(firstProp);
            var sec_root = releasesResponse.RootElement.GetProperty(secProp);

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
                        if (prop.Name == "title")//song
                        {
                            currSong = prop.Value.ToString();
                        }
                        else { currSong = "No song found"; }
                    }

                }            
            }
            //default case Search on ThirdRock
            var thirdElm = root.EnumerateObject();
            while (thirdElm.MoveNext())
            {
                var myProp = thirdElm.Current;
                if (myProp.Name == "Subtitle")
                {
                    currSong = myProp.Value.ToString();
                }
            }
            var imgElm = sec_root.EnumerateObject();
            while (imgElm.MoveNext())
            {
                var myProp = imgElm.Current;
                if (myProp.Name == "Image")
                {
                    gotImg = myProp.Value.ToString();
                }
            }
            //nowPlayingAlbum.Text = gotImg;
            return currSong + "," + gotImg;
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
                            if (jdx == 3)
                                //jdx = 2 is img size 174x174 too large for smaller screens
                                text = nodes.FirstChild.Value;
                            /*if (jdx == 1)
                                // Img is small for 1080 screen
                                text = nodes.FirstChild.Value;*/
                        }
                        else { text = "assets/cd_case_274px.png"; }
                        jdx += 1;
                    }
                }
                else { text = ""; }
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
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("### Timer Stopped ### \n");
            NotifyLoader();
        }

        private void fMLaPazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gotStation = "fmlapaz";
        }

        private void thirdRockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gotStation = "thirdrock";
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            var playStream = new WMPLib.WindowsMediaPlayer();
            //playStream.URL = "https://rfcmedia3.streamguys1.com/thirdrock.mp3";
            playStream.URL = "https://rfcmedia3.streamguys1.com/thirdrock.aac";


            if (isPlaying)
            {
                isPlaying = false;
                // gifImg.ImageLocation = "https://raw.githubusercontent.com/ndlopez/fmLaPazNow/main/assets/equalizer.gif";
                // playBtn.Enabled = false;
                // stopBtn.Enabled = true;
                try 
                {
                    playStream.controls.play();
                    nowPlayingAlbum.Text = "Now Streaming...";
                }
                catch
                {
                    nowPlayingAlbum.Text = "Buffering...";
                }
            }
            else
            {
                isPlaying = true;
                playStream.controls.stop(); //does NOT work :(
                nowPlayingAlbum.Text = "Stream stopped.";
            }
            
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            playBtn.Enabled = true;
            stopBtn.Enabled = false;
        }
    }

}
