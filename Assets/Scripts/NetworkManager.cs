using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkManager : MonoBehaviour
{
	Socket clientSocket;
	EndPoint serverEP;

    void Start()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		serverEP = new IPEndPoint(IPAddress.Loopback, 10200);
    }

    void Update()
    {
        
    }

	string Recv()
	{
		byte[] recvBytes = new byte[1024];
  		int nRecv = clientSocket.ReceiveFrom(recvBytes, ref serverEP);
  		return Encoding.UTF8.GetString(recvBytes, 0, nRecv);
	}

	void Send(string message)
	{
		byte[] buf = Encoding.UTF8.GetBytes(message);
  		clientSocket.SendTo(buf, serverEP);
	}
}
