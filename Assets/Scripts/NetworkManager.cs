using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkManager : MonoBehaviour
{
    private TcpClient server;
	private NetworkStream ns;

	[SerializeField] private string address;
	[SerializeField] private int port;

    void Start()
    {
		Open();
    }

    void Update()
    {
		
    }

	void Open()
	{
		try
        {
            server = new TcpClient(address, port);
        	server.LingerState = new LingerOption(true, 0);
        }
        catch (SocketException)
        {
            Debug.Log("Unable to connect to server");
            return;
        }

		ns = server.GetStream();
    	Debug.Log("connected to server");
	}

	public byte[] Receive()
	{
        byte[] data = new byte[32];
        ns.Read(data, 0, data.Length);

		return data;
	}

	public void Send(byte[] message)
	{
        ns.Write(message, 0, message.Length);
        ns.Flush();
	}

	void Close()
	{
        ns.Close();
        server.Close();
	}
}
