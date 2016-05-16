namespace MapleDev83.Network.PacketHandling
{
    using System;

    class PacketReader
    {
        byte[] buffer;
        int position;
        public int available { private set; get; }

        public PacketReader(byte[] data)
        {
            int length = data.Length;
            buffer = new byte[length];
            System.Buffer.BlockCopy(data, 0, buffer, 0, length);
            available = length;
        }

        public int Read(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("Length", "Length cannot be equals or lower then zero.");
            int pos = position;
            if (available <= 0)
                throw new Exception("Reached data capacity.");
            position += length;
            available -= length;
            return pos;
        }

        public byte[] ReadBytes(int bytes)
        {
            byte[] result = new byte[bytes];
            System.Buffer.BlockCopy(buffer, Read(bytes), result, 0, bytes);
            return result;
        }

        public byte ReadByte()
        {
            return buffer[Read(1)];
        }

        public sbyte ReadSByte()
        {
            return (sbyte)buffer[Read(1)];
        }

        public short ReadShort()
        {
            return BitConverter.ToInt16(buffer, Read(2));
        }

        public ushort ReadUShort()
        {
            return BitConverter.ToUInt16(buffer, Read(2));
        }

        public int ReadInt()
        {
            return BitConverter.ToInt32(buffer, Read(4));
        }

        public uint ReadUInt()
        {
            return BitConverter.ToUInt32(buffer, Read(4));
        }

        public long ReadLong()
        {
            return BitConverter.ToInt64(buffer, Read(8));
        }

        public ulong ReadULong()
        {
            return BitConverter.ToUInt64(buffer, Read(8));
        }

        public string ReadString(int length, char nullchar = '.')
        {
            if (length == 0) return String.Empty;

            byte[] bytes = ReadBytes(length);

            char[] ret = new char[bytes.Length];
            for (int x = 0; x < bytes.Length; x++)
            {
                if (bytes[x] < 32 && bytes[x] >= 0)
                    ret[x] = nullchar;
                else
                {
                    int chr = ((short)bytes[x]) & 0xFF;
                    ret[x] = (char)chr;
                }
            }
            if (nullchar != '.')
            {
                return new String(ret).Replace(nullchar.ToString(), "");
            }
            else
            {
                return new String(ret);
            }
        }

        public string ReadMapleString()
        {
            return ReadString(ReadShort());
        }

        public bool ReadBool()
        {
            return BitConverter.ToBoolean(buffer, Read(1));
        }

    }
}
