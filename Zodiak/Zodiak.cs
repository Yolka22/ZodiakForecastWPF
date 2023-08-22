using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace ZodiakNameSpace
{
    public class Zodiak
    {
        public string sign;
        public string forecast;
        public string image64string;
        public Zodiak(string sign, string forecast, string imagePath)
        {
            this.forecast = forecast;
            this.sign = sign;

            if (imagePath != null)
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                this.image64string = Convert.ToBase64String(imageBytes);
            }

        }

        public BitmapImage DecodeImage()
        {
            byte[] imageBytes = Convert.FromBase64String(image64string);

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

    }
}
