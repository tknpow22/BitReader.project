using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReaderUtils_ReadInt64Test
    {
        [TestMethod]
        public void TestReadInt64_64()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 64);
            Assert.AreEqual(0x1234567890abcdef, val);
        }

        [TestMethod]
        public void TestReadInt64_64_r()
        {
            byte[] buffer = new byte[] { 0xef, 0xcd, 0xab, 0x90, 0x78, 0x56, 0x34, 0x12, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 64, BitReader.Endianness.Little);
            Assert.AreEqual(0x1234567890abcdef, val);
        }

        [TestMethod]
        public void TestReadInt64_minus_64()
        {
            byte[] buffer = new byte[] { 0xef, 0xcd, 0xab, 0x90, 0x78, 0x56, 0x34, 0x12, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 64);
            Assert.AreEqual((UInt64)0xefcdab9078563412, (UInt64)val);
            Assert.AreEqual(-1167088091436534766L, val);
        }

        [TestMethod]
        public void TestReadInt64_minus_64_r()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 64, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0xefcdab9078563412, (UInt64)val);
            Assert.AreEqual(-1167088091436534766L, val);
        }

        [TestMethod]
        public void TestReadInt64_56()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 56);
            Assert.AreEqual(0x1234567890abcd, val);
        }

        public void TestReadInt64_56_r()
        {
            byte[] buffer = new byte[] { 0xcd, 0xab, 0x90, 0x78, 0x56, 0x34, 0x12, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 56, BitReader.Endianness.Little);
            Assert.AreEqual(0x1234567890abcd, val);
        }

        [TestMethod]
        public void TestReadInt64_minus_56()
        {
            UInt64 orig = 0x00785634efcdab90;
            Int64 expected = -(Int64)(orig);
            byte[] buffer = new byte[7];
            {
                UInt64 orig_minus_bits = (~orig & 0x00ffffffffffffff) + 1;
                buffer[0] = (byte)((orig_minus_bits >> 48) & 0xff);
                buffer[1] = (byte)((orig_minus_bits >> 40) & 0xff);
                buffer[2] = (byte)((orig_minus_bits >> 32) & 0xff);
                buffer[3] = (byte)((orig_minus_bits >> 24) & 0xff);
                buffer[4] = (byte)((orig_minus_bits >> 16) & 0xff);
                buffer[5] = (byte)((orig_minus_bits >> 8) & 0xff);
                buffer[6] = (byte)((orig_minus_bits) & 0xff);
            }

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 56);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt64_minus_56_r()
        {
            UInt64 orig = 0x00785634efcdab90;
            Int64 expected = -(Int64)(orig);
            byte[] buffer = new byte[7];
            {
                UInt64 orig_minus_bits = (~orig & 0x00ffffffffffffff) + 1;
                buffer[6] = (byte)((orig_minus_bits >> 48) & 0xff);
                buffer[5] = (byte)((orig_minus_bits >> 40) & 0xff);
                buffer[4] = (byte)((orig_minus_bits >> 32) & 0xff);
                buffer[3] = (byte)((orig_minus_bits >> 24) & 0xff);
                buffer[2] = (byte)((orig_minus_bits >> 16) & 0xff);
                buffer[1] = (byte)((orig_minus_bits >> 8) & 0xff);
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 56, BitReader.Endianness.Little);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt64_16()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 16);
            Assert.AreEqual(0x1234, val);
        }

        [TestMethod]
        public void TestReadInt64_16_r()
        {
            byte[] buffer = new byte[] { 0x34, 0x12, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual(0x1234, val);
        }

        [TestMethod]
        public void TestReadInt64_minus_16()
        {
            UInt64 orig = 0x0000000000007856;
            Int64 expected = -(Int64)(orig);
            byte[] buffer = new byte[2];
            {
                UInt64 orig_minus_bits = (~orig & 0x000000000000ffff) + 1;
                buffer[0] = (byte)((orig_minus_bits >> 8) & 0xff);
                buffer[1] = (byte)((orig_minus_bits) & 0xff);
            }

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 16);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt64_minus_16_r()
        {
            UInt64 orig = 0x0000000000007856;
            Int64 expected = -(Int64)(orig);
            byte[] buffer = new byte[2];
            {
                UInt64 orig_minus_bits = (~orig & 0x000000000000ffff) + 1;
                buffer[1] = (byte)((orig_minus_bits >> 8) & 0xff);
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt64_8()
        {
            byte[] buffer = new byte[] { 0x12, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 8);
            Assert.AreEqual(0x12, val);
        }

        [TestMethod]
        public void TestReadInt64_8_r()
        {
            byte[] buffer = new byte[] { 0x34, };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual(0x34, val);
        }

        [TestMethod]
        public void TestReadInt64_minus_8()
        {
            UInt64 orig = 0x0000000000000078;
            Int64 expected = -(Int64)(orig);
            byte[] buffer = new byte[1];
            {
                UInt64 orig_minus_bits = (~orig & 0x00000000000000ff) + 1;
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 8);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt64_minus_8_r()
        {
            UInt64 orig = 0x0000000000000078;
            Int64 expected = -(Int64)(orig);
            byte[] buffer = new byte[1];
            {
                UInt64 orig_minus_bits = (~orig & 0x00000000000000ff) + 1;
                buffer[0] = (byte)((orig_minus_bits) & 0xff);
            }

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual(expected, val);
        }

        [TestMethod]
        public void TestReadInt64_2()
        {
            byte[] buffer = new byte[] { 0b01000000 };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 2);
            Assert.AreEqual((Int64)0x1, val);
        }

        [TestMethod]
        public void TestReadInt64_2_r()
        {
            byte[] buffer = new byte[] { 0b01000000 };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 2, BitReader.Endianness.Little);
            Assert.AreEqual((Int64)0x1, val);
        }

        [TestMethod]
        public void TestReadInt64_minus_2()
        {
            byte[] buffer = new byte[] { 0b11000000 };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 2);
            Assert.AreEqual((Int64)(-1), val);
        }

        [TestMethod]
        public void TestReadInt64_minus_2_r()
        {
            byte[] buffer = new byte[] { 0b11000000 };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 2, BitReader.Endianness.Little);
            Assert.AreEqual((Int64)(-1), val);
        }

        [TestMethod]
        public void TestReadInt64_1()
        {
            byte[] buffer = new byte[] { 0x80 };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 1);
            Assert.AreEqual((Int64)0x1, val);
        }

        [TestMethod]
        public void TestReadInt64_1_r()
        {
            byte[] buffer = new byte[] { 0x80 };

            Int64 val = BitReaderUtils.ReadInt64(buffer, 0, 1, BitReader.Endianness.Little);
            Assert.AreEqual((Int64)0x1, val);
        }
    }
}
