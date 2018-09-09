﻿using System;
using System.Net.Sockets;
using Bindings;

namespace Youtube2DMMORPGClient
{
    class ClientTCP
    {
        public TcpClient PlayerSocket;
        private static NetworkStream myStream;
        private ClientHandleData chd;
        private byte[] asyncBuff;
        private bool connecting;
        private bool connected;

        public void ConnectToServer()
        {
            if(PlayerSocket != null)
            {
                if (PlayerSocket.Connected || connected)
                    return;
                PlayerSocket.Close();
                PlayerSocket = null;
            }
            
            PlayerSocket = new TcpClient();
            chd = new ClientHandleData();
            PlayerSocket.ReceiveBufferSize = 4096;
            PlayerSocket.SendBufferSize = 4096;
            PlayerSocket.NoDelay = false;
            Array.Resize(ref asyncBuff, 8192);
            PlayerSocket.BeginConnect("127.0.0.1", 5555, new AsyncCallback(ConnectCallback), PlayerSocket);
            connecting = true;
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            PlayerSocket.EndConnect(ar);
            if(PlayerSocket.Connected == false)
            {
                connecting = false;
                connected = false;
                return;
            }
            else
            {
                PlayerSocket.NoDelay = true;
                myStream = PlayerSocket.GetStream();
                myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
                connected = true;
                connecting = false;
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            int byteAmt = myStream.EndRead(ar);
            byte[] myBytes = null;
            Array.Resize(ref myBytes, byteAmt);
            Buffer.BlockCopy(asyncBuff, 0, myBytes, 0, byteAmt);
            
            if(byteAmt == 0)
            {
                //Destroy The Game
                return;
            }

            //Handle Network Packets.
            chd.HandleNetworkMessages(myBytes);
            myStream.BeginRead(asyncBuff, 0, 8192, OnReceive, null);
        }

        public void SendData(byte[] data)
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddBytes(data);
            myStream.Write(buffer.ToArray(), 0, buffer.ToArray().Length);
            buffer.Dispose();
        }

        public void SendLogin()
        {
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddInteger((int)ClientPackets.CLogin);
            buffer.AddString("Sungkae");
            buffer.AddString("D3chu");
            SendData(buffer.ToArray());
            buffer.Dispose();
        }
    }
}
