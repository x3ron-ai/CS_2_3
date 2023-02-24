using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using TagLib;

namespace MusicPlayer
{
    public class Track
    {
        public string name;
        //public string artist;
        public string path;
        public Image image;
        public Track(string path)
        {
            this.path = path;
            this.name = path.Split('\\')[^1].Split('.')[0];
            image = getImage();
        }
        private Image getImage()
        {
            var file = TagLib.File.Create(path);
            Image im = new Image();
            im.Height = 30;
            im.Width = 30;
            im.Stretch = Stretch.Fill;
            try
            {
                IPicture pic = file.Tag.Pictures[0];
                MemoryStream ms = new MemoryStream(pic.Data.Data);
                ms.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                im.Source = bitmap;
                return im;
            }
            catch {
                im.Source = new BitmapImage(new Uri($"pack://application:,,,/csharpicons/musicdefault.png"));
                return im;
            }
        }
    }
    public class Playlist
    {
        public bool isPaused = false;
        public bool isRandom = false;
        public bool isRepeat = false;
        public List<Track> tracks = new List<Track>();
        public Playlist()
        {

        }
        public void LoadTracks(List<string> tracks = null)
        {
            foreach (var track in tracks)
            {
                this.tracks.Add(new Track(track));
            }
        }
    }
}
