using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReaderUtils_ReadUInt32Test
    {
        [TestMethod]
        public void TestReadUInt32_32()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 32);
            Assert.AreEqual((UInt32)0x12345678, val);
        }

        [TestMethod]
        public void TestReadUInt32_32_r()
        {
            byte[] buffer = new byte[] { 0xef, 0xcd, 0xab, 0x90, };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 32, BitReader.Endianness.Little);
            Assert.AreEqual((UInt32)0x90abcdef, val);
        }

        [TestMethod]
        public void TestReadUInt32_24()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 24);
            Assert.AreEqual((UInt32)0x123456, val);
        }

        [TestMethod]
        public void TestReadUInt32_24_r()
        {
            byte[] buffer = new byte[] { 0xcd, 0xab, 0x90, };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 24, BitReader.Endianness.Little);
            Assert.AreEqual((UInt32)0x90abcd, val);
        }

        [TestMethod]
        public void TestReadUInt32_16()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 16);
            Assert.AreEqual((UInt32)0x1234, val);
        }

        [TestMethod]
        public void TestReadUInt32_16_r()
        {
            byte[] buffer = new byte[] { 0x34, 0x12, };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual((UInt32)0x1234, val);
        }

        [TestMethod]
        public void TestReadUInt32_8()
        {
            byte[] buffer = new byte[] { 0x12 };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 8);
            Assert.AreEqual((UInt32)0x12, val);
        }

        [TestMethod]
        public void TestReadUInt32_8_r()
        {
            byte[] buffer = new byte[] { 0x12 };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual((UInt32)0x12, val);
        }

        [TestMethod]
        public void TestReadUInt32_1()
        {
            byte[] buffer = new byte[] { 0x80 };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 1);
            Assert.AreEqual((UInt32)0x1, val);
        }

        [TestMethod]
        public void TestReadUInt32_1_r()
        {
            byte[] buffer = new byte[] { 0x80 };

            UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, 1, BitReader.Endianness.Little);
            Assert.AreEqual((UInt32)0x1, val);
        }

        [TestMethod]
        public void TestReadUInt32_1to32()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, };
            UInt32 bufferVal = 0x12345678;

            for (int i = 1; i <= 32; ++i) {
                UInt32 val = BitReaderUtils.ReadUInt32(buffer, 0, i);

                UInt32 maskBits = (UInt32)BitReader.GetMaskBits(i);

                maskBits <<= (32 - i);
                UInt32 maskedVal = bufferVal & maskBits;
                maskedVal >>= (32 - i);

                Assert.AreEqual(maskedVal, val);
            }
        }

        [TestMethod]
        public void TestReadUInt32_1to32_r()
        {
            byte[] buffer = new byte[] { 0xef, 0xcd, 0xab, 0x90, };

            UInt32 val;

            val = BitReaderUtils.ReadUInt32(buffer, 0, 1, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00000001, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 2, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00000011, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 3, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00000111, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 4, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00001110, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 5, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00011101, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 6, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00111011, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 7, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01110111, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b11101111, val);

            val = BitReaderUtils.ReadUInt32(buffer, 0, 9, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1101111100000001, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 10, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1011111100000011, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 11, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0111111000000111, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 12, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1111110000001110, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 13, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1111100100011101, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 14, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1111001100111011, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 15, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1110011001110111, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1100110111101111, val);

            val = BitReaderUtils.ReadUInt32(buffer, 0, 17, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b100110111101111100000001, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 18, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b001101101011111100000011, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 19, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b011011010111111000000111, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 20, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b110110101111110000001110, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 21, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b101101011111100100011101, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 22, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b011010101111001100111011, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 23, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b110101011110011001110111, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 24, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b101010111100110111101111, val);

            val = BitReaderUtils.ReadUInt32(buffer, 0, 25, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01010111100110111101111100000001, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 26, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b10101110001101101011111100000011, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 27, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01011100011011010111111000000111, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 28, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b10111001110110101111110000001110, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 29, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01110010101101011111100100011101, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 30, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b11100100011010101111001100111011, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 31, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b11001000110101011110011001110111, val);
            val = BitReaderUtils.ReadUInt32(buffer, 0, 32, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b10010000101010111100110111101111, val);
        }
    }
}
