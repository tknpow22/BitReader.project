using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tknpow22.IO
{
    public class BitReaderUtils
    {
        public static UInt64 ReadUInt64(byte[] buffer, int startIndex, int countOfBits, BitReader.Endianness endianness = BitReader.Endianness.Big)
        {
            BitReader reader = new BitReader(buffer, startIndex, endianness);
            return reader.ReadUInt64(countOfBits);
        }

        public static Int64 ReadInt64(byte[] buffer, int startIndex, int countOfBits, BitReader.Endianness endianness = BitReader.Endianness.Big)
        {
            BitReader reader = new BitReader(buffer, startIndex, endianness);
            return reader.ReadInt64(countOfBits);
        }

        public static UInt32 ReadUInt32(byte[] buffer, int startIndex, int countOfBits, BitReader.Endianness endianness = BitReader.Endianness.Big)
        {
            BitReader reader = new BitReader(buffer, startIndex, endianness);
            return reader.ReadUInt32(countOfBits);
        }

        public static Int32 ReadInt32(byte[] buffer, int startIndex, int countOfBits, BitReader.Endianness endianness = BitReader.Endianness.Big)
        {
            BitReader reader = new BitReader(buffer, startIndex, endianness);
            return reader.ReadInt32(countOfBits);
        }

        public static UInt16 ReadUInt16(byte[] buffer, int startIndex, int countOfBits, BitReader.Endianness endianness = BitReader.Endianness.Big)
        {
            BitReader reader = new BitReader(buffer, startIndex, endianness);
            return reader.ReadUInt16(countOfBits);
        }

        public static Int16 ReadInt16(byte[] buffer, int startIndex, int countOfBits, BitReader.Endianness endianness = BitReader.Endianness.Big)
        {
            BitReader reader = new BitReader(buffer, startIndex, endianness);
            return reader.ReadInt16(countOfBits);
        }

        public static byte ReadByte(byte[] buffer, int startIndex, int countOfBits)
        {
            BitReader reader = new BitReader(buffer, startIndex);
            return reader.ReadByte(countOfBits);
        }

        public static List<byte> ReadBytes(byte[] buffer, int startIndex, int countOfBits)
        {
            BitReader reader = new BitReader(buffer, startIndex);
            return reader.ReadBytes(countOfBits);
        }
    }
}
