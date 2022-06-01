using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkTest : MonoBehaviour
{
    private Socket _socket;
    private EndPoint _serverEp;

    private bool _isConnected = false;

    void Start()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _serverEp = new IPEndPoint(IPAddress.Loopback, 10200);
        _isConnected = true;
    }

    void Update()
    {
        if (!_isConnected)
        {
            return;
        }

        byte[] recvBytes = new byte[1024];
        int numReceived = _socket.ReceiveFrom(recvBytes, ref _serverEp);
        Debug.Log(Encoding.UTF8.GetString(recvBytes, 0, numReceived));
    }
}