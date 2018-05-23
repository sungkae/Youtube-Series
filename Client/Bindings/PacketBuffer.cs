using System;
using System.Collections.Generic;
using System.Text;

namespace Bindings
{
    class PacketBuffer : IDisposable
    {
        List<byte> Buff;
        byte[] readBuff;
        int readpos;
        bool buffUpdate = false;

        public PacketBuffer()
        {
            Buff = new List<byte>();
            readpos = 0;
        }

        public int GetReadPos()
        {
            return readpos;
        }

        public byte[] ToArray()
        {
            return Buff.ToArray();
        }

        public int Count()
        {
            return Buff.Count;
        }

        public int Length()
        {
            return Count() - readpos;
        }

        public void Clear()
        {
            Buff.Clear();
            readpos = 0;
        }

        //Write Data
        public void AddInteger(int Input)
        {
            Buff.AddRange(BitConverter.GetBytes(Input));
            buffUpdate = true;
        }

        public void AddFloat(float Input)
        {
           Buff.AddRange(BitConverter.GetBytes(Input));
           buffUpdate = true;
        }

        public void AddString(string Input)
        {
            Buff.AddRange(BitConverter.GetBytes(Input.Length));
            Buff.AddRange(Encoding.ASCII.GetBytes(Input));
            buffUpdate = true;
        }

        public void AddByte(byte Input)
        {
            Buff.Add(Input);
            buffUpdate = true;
        }

        public void AddBytes(byte[] Input)
        {
            Buff.AddRange(Input);
            buffUpdate = true;
        }

        public void AddShort(short Input)
        {
            Buff.AddRange(BitConverter.GetBytes(Input));
            buffUpdate = true;
        }

        //Read Data
        public int GetInteger(bool Peek = true)
        {
            if(Buff.Count> readpos)
            {
                if (buffUpdate)
                {
                    readBuff = Buff.ToArray();
                    buffUpdate = false;
                }

                int result = BitConverter.ToInt32(readBuff, readpos);

                if(Peek & Buff.Count > readpos)
                {
                    readpos += 4;
                }

                return result;
            }
            else
            {
                throw new Exception("Packet Buffer is past It's limit.");
            }
        }

        public float GetFloat(bool Peek = true)
        {
            if (Buff.Count > readpos)
            {
                if (buffUpdate)
                {
                    readBuff = Buff.ToArray();
                    buffUpdate = false;
                }

                float result = BitConverter.ToSingle(readBuff, readpos);

                if (Peek & Buff.Count > readpos)
                {
                    readpos += 4;
                }

                return result;
            }
            else
            {
                throw new Exception("Packet Buffer is past It's limit.");
            }
        }

        public string GetString(bool Peek = true)
        {

            int length = GetInteger(true);

            if (buffUpdate)
            {
                readBuff = Buff.ToArray();
                buffUpdate = false;
            }

            string result = Encoding.ASCII.GetString(readBuff, readpos, length);

            if (Peek & Buff.Count > readpos)
            {
                if(result.Length > 0)
                {
                    readpos += length;
                }
            }

            return result;
        }

        public byte GetByte(bool Peek = true)
        {
            if (Buff.Count > readpos)
            {
                if (buffUpdate)
                {
                    readBuff = Buff.ToArray();
                    buffUpdate = false;
                }

                byte result = readBuff[readpos];

                if (Peek & Buff.Count > readpos)
                {
                    readpos += 1;
                }

                return result;
            }
            else
            {
                throw new Exception("Packet Buffer is past It's limit.");
            }
        }

        public byte[] GetBytes(int Length, bool Peek = true)
        {
            if (buffUpdate)
            {
                readBuff = Buff.ToArray();
                buffUpdate = false;
            }

            byte[] result = Buff.GetRange(readpos, Length).ToArray();
            if (Peek)
            {
                readpos += Length;
            }

            return result;
        }

        private bool disposedValue = false;

        //IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    Buff.Clear();
                }

                readpos = 0;
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
