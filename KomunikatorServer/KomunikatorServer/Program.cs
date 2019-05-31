using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace KomunikatorServer
{
    class Program
    { 
        //Usluga dziala na porcie 1999
        private static string _version = "0.1"; // Tylko w formacie x.x
        private static bool _splash = true;
        private static List<Client> _clientsList;
        private static Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            ServerSetup();
            while (true)
            {
                AcceptClient();
            }
        }


        private static void ServerSetup()
        {
            if (_splash) { AsciiArt(); }
            Console.WriteLine("Server setting up...");
            _socket.Bind(new IPEndPoint(IPAddress.Any,1999));
            _socket.Listen(5);
            Console.WriteLine("Server is listening for clients...");
        }

        private static void AcceptClient()
        {
            Socket socket = _socket.Accept();
            _clientsList.Add(new Client(socket)); 
        }

        class Client
        {
            public static int clientsCount = 0;
            public string name;
            public Socket socket;
            public Thread thread;
            public Client(Socket socket)
            {
                clientsCount++;
                this.socket = socket;
                byte[] nameBuff = new byte[27];
                this.socket.Receive(nameBuff);
                string name = ASCIIEncoding.ASCII.GetString(nameBuff);
                name = name.Remove(name.IndexOf("<EOF>") - 1);
                Console.WriteLine("Client{0}connected!", name);
                this.thread = new Thread(new ThreadStart(Listen));
                this.socket.Send(ASCIIEncoding.ASCII.GetBytes("Hej " + name + "!<EOF>"));
            }
            public void Listen()
            {
                while (true)
                {

                }
            }
            public void Disconnect()
            {
                this.socket.Disconnect(true);
                clientsCount--;
            }

        }
        private static void AsciiArt(){
            int animFrame = 50;
            ConsoleColor initial = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  _");
            Thread.Sleep(animFrame);
            Console.WriteLine(" //\\");
            Thread.Sleep(animFrame);
            Console.WriteLine(" V  \\");
            Thread.Sleep(animFrame);
            Console.WriteLine("  \\  \\_");
            Thread.Sleep(animFrame);
            Console.Write("   \\,'.`-.   ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Banana");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Thread.Sleep(animFrame);
            Console.Write("    |\\ `. `.    ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Chat_Server");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Thread.Sleep(animFrame);
            Console.Write("    ( \\  `. `-.   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("v{0}",_version);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                 _,.-:\\");
            Thread.Sleep(animFrame);
            Console.WriteLine("     \\ \\   `.  `-._             __..--' ,-';/");
            Thread.Sleep(animFrame);
            Console.WriteLine("      \\ `.   `-.   `-..___..---'   _.--' ,'/");
            Thread.Sleep(animFrame);
            Console.WriteLine("       `. `.    `-._        __..--'    ,' /");
            Thread.Sleep(animFrame);
            Console.WriteLine("         `. `-_     ``--..''       _.-' ,'");
            Thread.Sleep(animFrame);
            Console.WriteLine("           `-_ `-.___        __,--'   ,'");
            Thread.Sleep(animFrame);
            Console.WriteLine("              `-.__  `----\"\"\"    __.-'");
            Thread.Sleep(animFrame);
            Console.WriteLine("                   `--..____..--'\n\n\n");
            Console.ForegroundColor = initial;
        }
    }
}
