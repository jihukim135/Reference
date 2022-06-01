using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkManager : MonoBehaviour
{
	UdpClient udpClient;
	[SerializeField] private string ip = "127.0.0.1";
	[SerializeField] private int port = 7777;

	IPEndPoint endPoint;

    void Start()
    {
		Open();

		IPAddress ipAddr;
		IPAddress.TryParse(ip, out ipAddr);
		endPoint = new IPEndPoint(ipAddr, port);
    }

    void Update()
    {
        Debug.Log(Receive());
    }

	void Open()
	{
        UdpClient udpClient = new UdpClient();
	}

	string Receive()
	{
		Debug.Log(endPoint);
        byte[] bytes = udpClient.Receive(ref endPoint);
        
  		return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
	}

	void Send(string message)
	{
		byte[] buf = Encoding.UTF8.GetBytes(message);
        udpClient.Send(buf, buf.Length, ip, port);
	}

	void Close()
	{
        udpClient.Close();
	}
}
