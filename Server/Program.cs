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

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, string> zodiacForecastMap = new Dictionary<string, string>
        {
            { "aries", "Good fortune ahead" },
            { "taurus", "Luck is on your side" },
            { "gemini", "Stay cautious today" },
            { "cancer", "Positive vibes coming your way" },
            { "leo", "An exciting opportunity awaits" },
            { "virgo", "Things might get challenging" },
            { "libra", "Balance will be key today" },
            { "scorpio", "Trust your instincts" },
            { "sagittarius", "Adventure is calling" },
            { "capricorn", "Hard work will pay off" },
            { "aquarius", "Unexpected changes ahead" },
            { "pisces", "Creativity will flourish" }
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

                    if (message_bytes_count > 0)
                    {
                        string user_message = Encoding.UTF8.GetString(message_buffer, 0, message_bytes_count);
                    WriteLine($"{user_message} {DateTime.Now}");

                    string response = "default";
                        user_message = user_message.ToLower();
                    if (zodiacForecastMap.ContainsKey(user_message))
                    {
                        response = zodiacForecastMap[user_message];
                    }
                    else
                    {
                        response = "Sign not found :(";
                    }


                    byte[] response_buffer = Encoding.UTF8.GetBytes(response);
                    acceptor.Send(response_buffer);


                    acceptor.Shutdown(SocketShutdown.Both);
                    acceptor.Close();


                    if (user_message == "server/stop")
                    {
                        break;
                    }
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
