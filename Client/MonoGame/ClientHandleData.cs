using System;
using Bindings;
using System.Collections.Generic;
namespace Youtube2DMMORPGClient
{
    class ClientHandleData
    {
        public PacketBuffer buffer = new PacketBuffer();
        private delegate void Packet_(byte[] data);
        private Dictionary<int, Packet_> Packets;

        public void InitializeMessages()
        {
            Packets = new Dictionary<int, Packet_>();             
        }

        public void HandleNetworkMessages(byte[] data)
        {
            int packetNum; PacketBuffer buffer;
            buffer = new PacketBuffer();

            buffer.AddBytes(data);
            packetNum = buffer.GetInteger();
            buffer.Dispose();

            if (Packets.TryGetValue(packetNum, out Packet_ Packet))
                Packet.Invoke(data);
        }
    }
}
