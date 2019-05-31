using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace KomunikatorClient
{
    class Program
    {
        private static string _version = "0.1"; // Tylko w formacie x.x
        private static string name;
        private static bool _splash = true;
        private static bool _correctFormat = false;
        private static bool _correctMaster = false;
        private static byte[] recBuffer = new byte[256];
        private static IPAddress _serverIP;
        private static Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            ClientSetup();
            Console.ReadLine();
        }

        private static void ClientSetup()
        {
            if (_splash) { AsciiArt(); }
            bool correctName = false;
            while (!correctName)
            {
                Console.Write("Wpisz swój nick: ");
                name = Console.ReadLine() + char.MinValue;
                if(String.IsNullOrEmpty(name) || name.Length > 24) {
                    Console.WriteLine("Nick nie może być pusty lub dłuższy niż 24 znaki.");
                }
                else
                {
                    if (name.Contains(" ")) { Console.WriteLine("Nick nie może zawierać spacji."); }
                    else
                    {
                        correctName = true;
                        name +="<EOF>";
                    }
                }

            }
            while (!_correctFormat || !_correctMaster)
            {
                Console.Write("Pozostaw puste, lub wpisz IP serwera w formacie xxx.xxx.xxx.xxx\n>> ");
                string ip = Console.ReadLine();
                if (String.IsNullOrEmpty(ip))
                {
                    setIP("77.45.24.67");
                }
                else
                {
                    setIP(ip);
                }
                if(_correctFormat){ 
                    bool correctMaster = true;
                    try
                    {
                        _socket.Connect(_serverIP, 1999);
                    }
                    catch
                    {
                        correctMaster = false;
                        Write.Er("Połączenie z serwerem");
                    }
                    if (correctMaster) { _correctMaster = true; }
                }
            }
            if (_socket.Connected) { Write.Ok("Połączenie z serwerem"); }
            _socket.Send(ASCIIEncoding.ASCII.GetBytes(name));
        }

        private static void setIP(string ip)
        {
            bool rightFormat = true;
            try
            {
                if (ip == "localhost") { _serverIP = IPAddress.Parse("127.0.0.1");
                } else {
                    _serverIP = IPAddress.Parse(ip);
                }
            }
            catch {
                rightFormat = false;
            }
            if (rightFormat)
            {
                _correctFormat = true;
                Write.Ok("Format IP");
            } else {
                Write.Er("Format IP");
            }
        }

        private static class Write
        {
            public static void Ok(string com)
            {
                Console.Write(com);
                ConsoleColor initial = Console.ForegroundColor;
                Console.Write(" [");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("OK");
                Console.ForegroundColor = initial;
                Console.Write("]\n");
            }
            public static void Er(string com)
            {
                Console.Write(com);
                ConsoleColor initial = Console.ForegroundColor;
                Console.Write(" [");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Błąd");
                Console.ForegroundColor = initial;
                Console.Write("]\n");
            }
        }
        private static void AsciiArt()
        {
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Banana");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Thread.Sleep(animFrame);
            Console.Write("    |\\ `. `.    ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Chat");
            Thread.Sleep(animFrame);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("    ( \\  `. `-.   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("v{0}", _version);
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
