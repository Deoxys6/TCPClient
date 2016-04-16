using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean isClientConnected = false;
            string ip = "192.168.0.58";
            while (true)
            {
                try
                {
                    TcpClient tcpClient = new TcpClient();
                    //Tries to connect to home server then prompts the user for a valid IP address
                    do {
                        Console.WriteLine("Connecting...");
                        try
                        {
                            tcpClient.Connect(ip, 8000);
                            isClientConnected = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Enter an IP");
                            ip = Console.ReadLine();
                        }
                    }
                    while (!isClientConnected);


                    Console.WriteLine("Connected...");

                    Stream stm = tcpClient.GetStream();
                    ASCIIEncoding asen = new ASCIIEncoding();
                    //byte[] ba = asen.GetBytes(str);
                    Console.WriteLine("Sending...");
                    stm.Write(ba, 0, ba.Length); byte[]
                    bb = new byte[4096];
                    int k = stm.Read(bb, 0, 100);
                    for (int i = 0; i < k; i++)
                    {
                        Console.Write(Convert.ToChar(bb[i]));
                    }
                    tcpClient.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
