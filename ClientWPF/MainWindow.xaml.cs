/**
 * Copyright [2023] [Korsun Yehor]
 *
 * Licensed under the Apache License, Version 2.0 (вышеуказанный год).
 * Вы можете использовать этот файл только в соответствии с лицензией Apache License, Version 2.0.
 * Копию лицензии вы можете найти в файле LICENSE.txt в данном дистрибутиве
 * или по адресу http://www.apache.org/licenses/LICENSE-2.0
 */


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZodiakNameSpace;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {

        public MainWindow()
        {

            InitializeComponent();
        }

        public static int port = 9001;
        public static IPAddress ip = IPAddress.Parse("127.0.0.1");
        public static IPEndPoint endPoint = new IPEndPoint(ip, port);

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            string message = input_textBox.Text;
            byte[] message_buffer = Encoding.UTF8.GetBytes(message);

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(endPoint);
            client.Send(message_buffer);


            byte[] server_message_buffer = new byte[4096];
            int server_message_bytes_count = client.Receive(server_message_buffer);
            string received_json = Encoding.UTF8.GetString(server_message_buffer, 0, server_message_bytes_count);
            Zodiak receivedZodiak = JsonConvert.DeserializeObject<Zodiak>(received_json);

            Forecast_label.Content = receivedZodiak.forecast;
        }
    }
}
