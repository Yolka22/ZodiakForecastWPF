﻿/**
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

            Dictionary<string, Zodiak> zodiacForecastMap = new Dictionary<string, Zodiak>
        {
            { "aries", new Zodiak("Good fortune ahead","image") },
            { "taurus", new Zodiak("Luck is on your side","image") },
            { "gemini", new Zodiak("Stay cautious today","image") },
            { "cancer", new Zodiak("Positive vibes coming your way", "image") },
            { "leo", new Zodiak("An exciting opportunity awaits", "image") },
            { "virgo", new Zodiak("Things might get challenging", "image") },
            { "libra", new Zodiak("Balance will be key today", "image") },
            { "scorpio", new Zodiak("Trust your instincts", "image") },
            { "sagittarius", new Zodiak("Adventure is calling", "image") },
            { "capricorn", new Zodiak("Hard work will pay off", "image") },
            { "aquarius", new Zodiak("Unexpected changes ahead", "image") },
            { "pisces", new Zodiak("Creativity will flourish", "image") }
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

                    byte[] message_buffer = new byte[4096];
                    int message_bytes_count = acceptor.Receive(message_buffer);
                    string userMessage = Encoding.UTF8.GetString(message_buffer, 0, message_bytes_count);

                    string jsonString = JsonConvert.SerializeObject(zodiacForecastMap[userMessage]);
                    WriteLine(jsonString);


                    acceptor.Send(Encoding.UTF8.GetBytes(jsonString));


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
