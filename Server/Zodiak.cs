using System;
using System.Drawing;
using System.Drawing.Imaging;
namespace ZodiakNameSpace
{
    public class Zodiak
    {
        public string forecast;
        public Bitmap image = null;
        public Zodiak(string forecast, string imagePath)
        {

            this.forecast = forecast;
            image = new Bitmap(imagePath);

        }

    }
}
