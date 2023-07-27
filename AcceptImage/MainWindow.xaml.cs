using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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

namespace AcceptImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ipAddress = IPAddress.Parse("192.168.31.90");
            var port = 80;

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var ep = new IPEndPoint(ipAddress, port);
                socket.Bind(ep);
                socket.Listen(10);


                while (true)
                {
                    var client = socket.Accept();
                    Task.Run(() =>
                    {
                        userName.Content = client.RemoteEndPoint;

                        var length = 0;
                        var bytes = new byte[1024];

                        do
                        {
                            length = client.Receive(bytes);
                            var msg = Encoding.UTF8.GetString(bytes, 0, length);
                        } while (true);

                    });
                }
            }
        }
    }
}
