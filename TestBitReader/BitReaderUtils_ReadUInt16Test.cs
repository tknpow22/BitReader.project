using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReaderUtils_ReadUInt16Test
    {
        [TestMethod]
        public void TestReadUInt16_16()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, };

            UInt16 val = BitReaderUtils.ReadUInt16(buffer, 0, 16);
            Assert.AreEqual((UInt16)0x1234, val);
        }

        [TestMethod]
        public void TestReadUInt16_16_r()
        {
            byte[] buffer = new byte[] { 0xab, 0x90, };

            UInt16 val = BitReaderUtils.ReadUInt16(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual((UInt16)0x90ab, val);
        }

        [TestMethod]
        public void TestReadUInt16_8()
        {
            byte[] buffer = new byte[] { 0x12 };

            UInt16 val = BitReaderUtils.ReadUInt16(buffer, 0, 8);
            Assert.AreEqual((UInt16)0x12, val);
        }

        [TestMethod]
        public void TestReadUInt16_8_r()
        {
            byte[] buffer = new byte[] { 0x12 };

            UInt16 val = BitReaderUtils.ReadUInt16(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual((UInt16)0x12, val);
        }

        [TestMethod]
        public void TestReadUInt16_1()
        {
            byte[] buffer = new byte[] { 0x80 };

            UInt16 val = BitReaderUtils.ReadUInt16(buffer, 0, 1);
            Assert.AreEqual((UInt16)0x1, val);
        }

        [TestMethod]
        public void TestReadUInt16_1_r()
        {
            byte[] buffer = new byte[] { 0x80 };

            UInt16 val = BitReaderUtils.ReadUInt16(buffer, 0, 1, BitReader.Endianness.Little);
            Assert.AreEqual((UInt16)0x1, val);
        }

        [TestMethod]
        public void TestReadUInt16_1to16()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, };
            UInt16 bufferVal = 0x1234;

            for (int i = 1; i <= 16; ++i) {
                UInt16 val = BitReaderUtils.ReadUInt16(buffer, 0, i);

                UInt16 maskBits = (UInt16)BitReader.GetMaskBits(i);

                maskBits <<= (16 - i);
                UInt16 maskedVal = (UInt16)(bufferVal & maskBits);
                maskedVal >>= (16 - i);

                Assert.AreEqual(maskedVal, val);
            }
        }

        [TestMethod]
        public void TestReadUInt16_1to16_r()
        {
            byte[] buffer = new byte[] { 0xef, 0xcd, };

            UInt16 val;

            val = BitReaderUtils.ReadUInt16(buffer, 0, 1, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00000001, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 2, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00000011, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 3, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00000111, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 4, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00001110, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 5, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00011101, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 6, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00111011, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 7, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01110111, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b11101111, val);

            val = BitReaderUtils.ReadUInt16(buffer, 0, 9, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1101111100000001, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 10, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1011111100000011, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 11, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0111111000000111, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 12, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1111110000001110, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 13, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1111100100011101, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 14, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1111001100111011, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 15, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1110011001110111, val);
            val = BitReaderUtils.ReadUInt16(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1100110111101111, val);
        }
    }
}
