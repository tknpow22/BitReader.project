using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReader_ReadTest
    {
        [TestMethod]
        public void TestRead_1()
        {
            byte[] buffer = new byte[] {
                0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef,
                0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef,
            };
            byte val;

            BitReader bitReader = new BitReader(buffer);

            val = bitReader.ReadByte(8);
            Assert.AreEqual(0x12, val);

            val = bitReader.ReadByte(4);
            Assert.AreEqual(0x3, val);
            val = bitReader.ReadByte(4);
            Assert.AreEqual(0x4, val);

            // 0x56: 010 10 1 10
            val = bitReader.ReadByte(3);
            Assert.AreEqual(0b010, val);
            val = bitReader.ReadByte(2);
            Assert.AreEqual(0b10, val);
            val = bitReader.ReadByte(1);
            Assert.AreEqual(0b1, val);
            val = bitReader.ReadByte(2);
            Assert.AreEqual(0b10, val);

            // 0x78, 0x90
            Assert.AreEqual((Int16)30864, bitReader.ReadInt16(16));

            // 0xab
            Assert.AreEqual((UInt16)0xab, bitReader.ReadUInt16(8));

            // 0xcd, 0xef
            Assert.AreEqual((UInt32)0xcdef, bitReader.ReadUInt32(16));

            // 0x12, 0x34, 0x56
            Assert.AreEqual((UInt32)0x123456, bitReader.ReadUInt32(24));

            // 0x78, 0x90
            Assert.AreEqual((UInt16)0x7890, bitReader.ReadUInt16(16));

            // 0xab, 0xcd, 0xef
            Assert.AreEqual((Int32)(-5517841), bitReader.ReadInt32(24));
        }

        [TestMethod]
        public void TestRead_2()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };

            BitReader bitReader = new BitReader(buffer);

            Assert.AreEqual((UInt64)0x1234567890abcd, bitReader.ReadUInt64(56));
            Assert.AreEqual((UInt32)0xef, bitReader.ReadUInt32(8));
        }

        [TestMethod]
        public void TestRead_3()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };

            BitReader bitReader = new BitReader(buffer);

            // 000 100 10001 10100
            Assert.AreEqual((Int64)0, bitReader.ReadInt64(3));
            Assert.AreEqual((Int64)(-4), bitReader.ReadInt64(3));
            Assert.AreEqual((Int64)(-15), bitReader.ReadInt64(5));
            Assert.AreEqual((Int64)(-3), bitReader.ReadInt64(3));
            Assert.AreEqual((Int64)0, bitReader.ReadInt64(2));
        }

        [TestMethod]
        public void TestRead_4()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };

            BitReader bitReader = new BitReader(buffer);

            // 000 100 10001 10100
            Assert.AreEqual((Int32)0, bitReader.ReadInt32(3));
            Assert.AreEqual((Int32)(-4), bitReader.ReadInt32(3));
            Assert.AreEqual((Int32)(-15), bitReader.ReadInt32(5));
            Assert.AreEqual((Int32)(-3), bitReader.ReadInt32(3));
            Assert.AreEqual((Int32)0, bitReader.ReadInt32(2));
        }

        [TestMethod]
        public void TestRead_5()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };

            BitReader bitReader = new BitReader(buffer);

            // 000 100 10001 10100
            Assert.AreEqual((Int16)0, bitReader.ReadInt16(3));
            Assert.AreEqual((Int16)(-4), bitReader.ReadInt16(3));
            Assert.AreEqual((Int16)(-15), bitReader.ReadInt16(5));
            Assert.AreEqual((Int16)(-3), bitReader.ReadInt16(3));
            Assert.AreEqual((Int16)0, bitReader.ReadInt16(2));
        }

        [TestMethod]
        public void TestRead_6()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };

            BitReader bitReader = new BitReader(buffer);

            // 000 100 10001 10100
            Assert.AreEqual((Int16)0, bitReader.ReadInt16(3));
            Assert.AreEqual((Int32)(-4), bitReader.ReadInt32(3));
            Assert.AreEqual((Int64)(-15), bitReader.ReadInt64(5));
            Assert.AreEqual((Int64)(-3), bitReader.ReadInt64(3));
            Assert.AreEqual((Int16)0, bitReader.ReadInt16(2));
        }
    }
}
