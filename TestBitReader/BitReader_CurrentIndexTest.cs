using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReader_CurrentIndexTest
    {
        [TestMethod]
        public void TestCurrentIndex()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };
            byte val;
            BitReader bitReader;
            
            bitReader = new BitReader(buffer);
            Assert.AreEqual(0, bitReader.CurrentIndex);

            bitReader.ReadByte(4);
            Assert.AreEqual(1, bitReader.CurrentIndex);
            bitReader.ReadByte(3);
            Assert.AreEqual(1, bitReader.CurrentIndex);
            bitReader.ReadByte(1);
            Assert.AreEqual(1, bitReader.CurrentIndex);

            bitReader.ReadByte(1);
            Assert.AreEqual(2, bitReader.CurrentIndex);
            bitReader.ReadInt16(7);
            Assert.AreEqual(2, bitReader.CurrentIndex);

            bitReader.ReadInt32(8);
            Assert.AreEqual(3, bitReader.CurrentIndex);

            bitReader.ReadUInt64(1);
            Assert.AreEqual(4, bitReader.CurrentIndex);
            bitReader.ReadUInt64(7);
            Assert.AreEqual(4, bitReader.CurrentIndex);

            bitReader.ReadInt16(8);
            Assert.AreEqual(5, bitReader.CurrentIndex);

            val = bitReader.ReadByte(4);
            Assert.AreEqual(6, bitReader.CurrentIndex);
            Assert.AreEqual(0xa, val);
            val = bitReader.ReadByte(3);
            Assert.AreEqual(6, bitReader.CurrentIndex);
            Assert.AreEqual(0x5, val);
            val = bitReader.ReadByte(1);
            Assert.AreEqual(6, bitReader.CurrentIndex);
            Assert.AreEqual(0x1, val);

            bitReader.CurrentIndex = 1;

            val = bitReader.ReadByte(3);
            Assert.AreEqual(2, bitReader.CurrentIndex);
            Assert.AreEqual(0x1, val);
            val = bitReader.ReadByte(2);
            Assert.AreEqual(2, bitReader.CurrentIndex);
            Assert.AreEqual(0x2, val);
            val = bitReader.ReadByte(3);
            Assert.AreEqual(2, bitReader.CurrentIndex);
            Assert.AreEqual(0x4, val);

            val = bitReader.ReadByte(8);
            Assert.AreEqual(3, bitReader.CurrentIndex);
            Assert.AreEqual(0x56, val);

            bitReader = new BitReader(buffer, 3);
            Assert.AreEqual(3, bitReader.CurrentIndex);
            val = bitReader.ReadByte(8);
            Assert.AreEqual(4, bitReader.CurrentIndex);
            Assert.AreEqual(0x78, val);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCurrentIndexUnderStartIndexException()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };
            BitReader bitReader = new BitReader(buffer);
            bitReader.CurrentIndex = -1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCurrentIndexOverStartIndexException()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };
            BitReader bitReader = new BitReader(buffer);
            bitReader.CurrentIndex = buffer.Length;
        }
    }
}
