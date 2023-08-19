using System;

namespace ZodiakNameSpace
{
    public class Zodiak
    {
        public Zodiak(string forecast, string image)
        {

            this.forecast = forecast;
            this.image = image;

        }
        public string forecast;
        public Bitmap image;
    }
}
