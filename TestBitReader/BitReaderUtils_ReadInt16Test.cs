using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReaderUtils_ReadInt16Test
    {
        [TestMethod]
        public void TestReadInt16_16()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 16);
            Assert.AreEqual((Int16)0x1234, val);
        }

        [TestMethod]
        public void TestReadInt16_16_r()
        {
            byte[] buffer = new byte[] { 0x34, 0x12, };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual((Int16)0x1234, val);
        }

        [TestMethod]
        public void TestReadInt16_minus_16()
        {
            UInt16 orig = 0x7856;
            Int16 expected = (Int16)(-orig);
            byte[] buffer = new byte[2];
            {
                UInt16 orig_minus_bits = (UInt16)((~orig & 0xffff) + 1);
                buffer[0] = (byte)((orig_minus_bits >> 8) & 0xff);
                buffer[1] = (byte)((orig_minus_bits) & 0xff);
            }

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 16);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt16_minus_16_r()
        {
            UInt16 orig = 0x7856;
            Int16 expected = (Int16)(-orig);
            byte[] buffer = new byte[2];
            {
                UInt16 orig_minus_bits = (UInt16)((~orig & 0xffff) + 1);
                buffer[1] = (byte)((orig_minus_bits >> 8) & 0xff);
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt16_8()
        {
            byte[] buffer = new byte[] { 0x12, };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 8);
            Assert.AreEqual((Int16)0x12, val);
        }

        [TestMethod]
        public void TestReadInt16_8_r()
        {
            byte[] buffer = new byte[] { 0x34, };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual((Int16)0x34, val);
        }

        [TestMethod]
        public void TestReadInt16_minus_8()
        {
            UInt16 orig = 0x0078;
            Int16 expected = (Int16)(-orig);
            byte[] buffer = new byte[1];
            {
                UInt16 orig_minus_bits = (UInt16)((~orig & 0x00ff) + 1);
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 8);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt16_minus_8_r()
        {
            UInt32 orig = 0x0078;
            Int16 expected = (Int16)(-orig);
            byte[] buffer = new byte[1];
            {
                UInt16 orig_minus_bits = (UInt16)((~orig & 0x00ff) + 1);
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt16_2()
        {
            byte[] buffer = new byte[] { 0b01000000 };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 2);
            Assert.AreEqual((Int16)0x1, val);
        }

        [TestMethod]
        public void TestReadInt16_2_r()
        {
            byte[] buffer = new byte[] { 0b01000000 };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 2, BitReader.Endianness.Little);
            Assert.AreEqual((Int16)0x1, val);
        }

        [TestMethod]
        public void TestReadInt16_minus_2()
        {
            byte[] buffer = new byte[] { 0b11000000 };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 2);
            Assert.AreEqual((Int16)(-1), val);
        }

        [TestMethod]
        public void TestReadInt16_minus_2_r()
        {
            byte[] buffer = new byte[] { 0b11000000 };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 2, BitReader.Endianness.Little);
            Assert.AreEqual((Int16)(-1), val);
        }

        [TestMethod]
        public void TestReadInt16_1()
        {
            byte[] buffer = new byte[] { 0x80 };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 1);
            Assert.AreEqual((Int16)0x1, val);
        }

        [TestMethod]
        public void TestReadInt16_1_r()
        {
            byte[] buffer = new byte[] { 0x80 };

            Int16 val = BitReaderUtils.ReadInt16(buffer, 0, 1, BitReader.Endianness.Little);
            Assert.AreEqual((Int16)0x1, val);
        }
    }
}
