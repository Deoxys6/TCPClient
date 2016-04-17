using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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
                    String str = Console.ReadLine();
                    byte[] ba = asen.GetBytes(str);
                    Console.WriteLine("Sending...");

                    stm.Write(ba, 0, ba.Length);

                    byte[] receivedInfo = new byte[4096];
                    int bytesReceived = stm.Read(receivedInfo, 0, 4096);
                    var binaryFormatter = new BinaryFormatter();
                    
                    using (var ms = new MemoryStream(receivedInfo))
                    {
                        int[,] musicInfo = (int[,])binaryFormatter.Deserialize(ms);
                        Console.WriteLine(musicInfo);
                        for(int x = 0; x < musicInfo.Length; x++)
                        {
                            Console.WriteLine(string.Format("Note {0} Duration {1}",musicInfo[0, x], musicInfo[1, x]));
                            Console.Beep(musicInfo[0 , x] , musicInfo[1, x]);
                        }
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
