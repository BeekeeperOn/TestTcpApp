using System;
using System.Text;
using System.Net.Sockets;
using UnityEngine;

public class TcpReactiveClient
{
    private const int Port = 8888;
    private const string Server = "127.0.0.1";

    public void Connection(string command)
    {
        try
        {
            using (var client = new TcpClient())
            {
                client.Connect(Server, Port);

                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(command);
                    stream.Write(data, 0, data.Length);
                }
            }
        }
        catch (SocketException exc)
        {
            Debug.Log($"{exc}");
        }
        catch (Exception exc)
        {
            Debug.Log($"{exc}");
        }
    }
}