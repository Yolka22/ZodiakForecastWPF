/**
 * Copyright [2023] [Korsun Yehor]
 *
 * Licensed under the Apache License, Version 2.0 (вышеуказанный год).
 * Вы можете использовать этот файл только в соответствии с лицензией Apache License, Version 2.0.
 * Копию лицензии вы можете найти в файле LICENSE.txt в данном дистрибутиве
 * или по адресу http://www.apache.org/licenses/LICENSE-2.0
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using static System.Console;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json;
using ZodiakNameSpace;
using System.Drawing;
namespace Server
{

    internal class Program
    {
        static void Main(string[] args)
        {

            List<Zodiak> zodiacForecastMap = new List<Zodiak>
        {
            {  new Zodiak("aries","Good fortune ahead","aries.png") },
            {  new Zodiak("taurus","Luck is on your side","taurus.png") },
            {  new Zodiak("gemini","Stay cautious today", "gemini.png") },
            {  new Zodiak("cancer","Positive vibes coming your way", "cancer.png") },
            {  new Zodiak("leo","An exciting opportunity awaits", "leo.png") },
            {  new Zodiak("virgo","Things might get challenging", "virgo.png") },
            {  new Zodiak("libra","Balance will be key today", "libra.png") },
            {  new Zodiak("scorpio","Trust your instincts", "scorpio.png") },
            {  new Zodiak("sagittarius","Adventure is calling", "sagittarius.png") },
            {  new Zodiak("capricorn","Hard work will pay off", "capricorn.png") },
            {  new Zodiak("aquarius","Unexpected changes ahead", "aquarius.png") },
            {  new Zodiak("pisces","Creativity will flourish", "pisces.png") }
        };

            int port = 9001;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            try
            {
                listener.Bind(endPoint);
                listener.Listen(100);


                while (true)
                {

                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine("\n> Wait for requests from clients ...");
                    ResetColor();

                    Socket acceptor = listener.Accept();

                    byte[] message_buffer = new byte[256000];
                    int message_bytes_count = acceptor.Receive(message_buffer);
                    string userMessage = Encoding.UTF8.GetString(message_buffer, 0, message_bytes_count);

                    Zodiak foundZodiac = zodiacForecastMap.FirstOrDefault(z => z.sign.ToLower() == userMessage.ToLower());
                    if (foundZodiac != null)
                    {
                        string jsonString = JsonConvert.SerializeObject(foundZodiac);
                        WriteLine(DateTime.Now);
                        WriteLine(jsonString);
                        acceptor.Send(Encoding.UTF8.GetBytes(jsonString));
                    }
                    else
                    {
                        string jsonString = JsonConvert.SerializeObject(new{
                            sign = userMessage,
                            forecast = "we dont have that sign :(",
                        });
                        
                        WriteLine(DateTime.Now);
                        WriteLine(jsonString);
                        acceptor.Send(Encoding.UTF8.GetBytes(jsonString));
                    }



                 }
            }
            catch (Exception ex)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"\n> Server Error {ex.Message}");
                ResetColor();
            }
            finally
            {
                listener.Close();
                ForegroundColor = ConsoleColor.Green;
                WriteLine("\n> Server Stopped");
                ResetColor();
            }
        }
    }
}
