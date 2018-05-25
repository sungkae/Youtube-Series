using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bindings;
namespace Youtube2DMMORPGServer
{
    class ServerHandleData
    {        
        private delegate void Packet_(int Index,byte[] data);
        private static Dictionary<int, Packet_> Packets;

        public void InitializeMessages()
        {
            Console.WriteLine("Initializing Network Packets");
            Packets = new Dictionary<int, Packet_>();
            Packets.Add((int)ClientPackets.CLogin,HandleLogin);
        }

        public void HandleNetworkMessages(int index, byte[] data)
        {
            int packetNum; PacketBuffer buffer;
            buffer = new PacketBuffer();

            buffer.AddBytes(data);
            packetNum = buffer.GetInteger();
            buffer.Dispose();

            if (Packets.TryGetValue(packetNum, out Packet_ Packet))
                Packet.Invoke(index,data);
        }

        private void HandleLogin(int index, byte[] data)
        {
            Console.WriteLine("Got Network Message.");
        }
    }
}
