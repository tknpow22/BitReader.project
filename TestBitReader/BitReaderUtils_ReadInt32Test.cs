using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReaderUtils_ReadInt32Test
    {
        [TestMethod]
        public void TestReadInt32_32()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 32);
            Assert.AreEqual(0x12345678, val);
        }

        [TestMethod]
        public void TestReadInt32_32_r()
        {
            byte[] buffer = new byte[] { 0xef, 0xcd, 0xab, 0x90, };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 32, BitReader.Endianness.Little);
            unchecked {
                Assert.AreEqual((Int32)0x90abcdef, val);
            }
        }

        [TestMethod]
        public void TestReadInt32_minus_32()
        {
            byte[] buffer = new byte[] { 0xef, 0xcd, 0xab, 0x90, };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 32);
            Assert.AreEqual((UInt32)0xefcdab90, (UInt32)val);
            Assert.AreEqual((Int32)(-271733872), val);
        }

        [TestMethod]
        public void TestReadInt32_minus_32_r()
        {
            byte[] buffer = new byte[] { 0x90, 0xab, 0xcd, 0xef, };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 32, BitReader.Endianness.Little);
            Assert.AreEqual((UInt32)0xefcdab90, (UInt32)val);
            Assert.AreEqual((Int32)(-271733872), val);
        }

        [TestMethod]
        public void TestReadInt32_16()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 16);
            Assert.AreEqual((Int32)0x1234, val);
        }

        [TestMethod]
        public void TestReadInt32_16_r()
        {
            byte[] buffer = new byte[] { 0x34, 0x12, };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual((Int32)0x1234, val);
        }

        [TestMethod]
        public void TestReadInt32_minus_16()
        {
            UInt32 orig = 0x00007856;
            Int32 expected = -(Int32)(orig);
            byte[] buffer = new byte[2];
            {
                UInt32 orig_minus_bits = (~orig & 0x0000ffff) + 1;
                buffer[0] = (byte)((orig_minus_bits >> 8) & 0xff);
                buffer[1] = (byte)((orig_minus_bits) & 0xff);
            }

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 16);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt32_minus_16_r()
        {
            UInt32 orig = 0x00007856;
            Int32 expected = -(Int32)(orig);
            byte[] buffer = new byte[2];
            {
                UInt32 orig_minus_bits = (~orig & 0x0000ffff) + 1;
                buffer[1] = (byte)((orig_minus_bits >> 8) & 0xff);
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt32_8()
        {
            byte[] buffer = new byte[] { 0x12, };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 8);
            Assert.AreEqual((Int32)0x12, val);
        }

        [TestMethod]
        public void TestReadInt32_8_r()
        {
            byte[] buffer = new byte[] { 0x34, };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual((Int32)0x34, val);
        }

        [TestMethod]
        public void TestReadInt32_minus_8()
        {
            UInt32 orig = 0x00000078;
            Int32 expected = -(Int32)(orig);
            byte[] buffer = new byte[1];
            {
                UInt32 orig_minus_bits = (~orig & 0x000000ff) + 1;
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 8);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt32_minus_8_r()
        {
            UInt32 orig = 0x00000078;
            Int32 expected = -(Int32)(orig);
            byte[] buffer = new byte[1];
            {
                UInt64 orig_minus_bits = (~orig & 0x000000ff) + 1;
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt32_2()
        {
            byte[] buffer = new byte[] { 0b01000000 };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 2);
            Assert.AreEqual((Int32)0x1, val);
        }

        [TestMethod]
        public void TestReadInt32_2_r()
        {
            byte[] buffer = new byte[] { 0b01000000 };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 2, BitReader.Endianness.Little);
            Assert.AreEqual((Int32)0x1, val);
        }

        [TestMethod]
        public void TestReadInt32_minus_2()
        {
            byte[] buffer = new byte[] { 0b11000000 };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 2);
            Assert.AreEqual((Int32)(-1), val);
        }

        [TestMethod]
        public void TestReadInt32_minus_2_r()
        {
            byte[] buffer = new byte[] { 0b11000000 };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 2, BitReader.Endianness.Little);
            Assert.AreEqual((Int32)(-1), val);
        }

        [TestMethod]
        public void TestReadInt32_1()
        {
            byte[] buffer = new byte[] { 0x80 };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 1);
            Assert.AreEqual((Int32)0x1, val);
        }

        [TestMethod]
        public void TestReadInt32_1_r()
        {
            byte[] buffer = new byte[] { 0x80 };

            Int32 val = BitReaderUtils.ReadInt32(buffer, 0, 1, BitReader.Endianness.Little);
            Assert.AreEqual((Int32)0x1, val);
        }
    }
}
