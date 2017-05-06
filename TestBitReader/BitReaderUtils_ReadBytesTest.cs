using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReaderUtils_ReadBytesTest
    {
        byte[] _buffer = new byte[] { 0b10111101, 0b01000010, 0b01101001, 0b11100100, 0b00001111 };

        [TestMethod]
        public void TestReadBytes_3()
        {
            List<byte> list;

            list = BitReaderUtils.ReadBytes(_buffer, 0, 3);
            Assert.AreEqual(0b101, list[0]);
        }

        [TestMethod]
        public void TestReadBytes_6()
        {
            List<byte> list;

            list = BitReaderUtils.ReadBytes(_buffer, 0, 6);
            Assert.AreEqual(0b101111, list[0]);
        }

        [TestMethod]
        public void TestReadBytes_9()
        {
            List<byte> list;

            list = BitReaderUtils.ReadBytes(_buffer, 0, 9);
            Assert.AreEqual(0b10111101, list[0]);
            Assert.AreEqual(0b0, list[1]);
        }

        [TestMethod]
        public void TestReadBytes_8()
        {
            List<byte> list = BitReaderUtils.ReadBytes(_buffer, 0, 8);
            Assert.AreEqual(0b10111101, list[0]);
        }

        [TestMethod]
        public void TestReadBytes_16()
        {
            List<byte> list = BitReaderUtils.ReadBytes(_buffer, 0, 16);
            Assert.AreEqual(0b10111101, list[0]);
            Assert.AreEqual(0b01000010, list[1]);
        }

        [TestMethod]
        public void TestReadBytes_18()
        {
            List<byte> list;

            list = BitReaderUtils.ReadBytes(_buffer, 0, 18);
            Assert.AreEqual(0b10111101, list[0]);
            Assert.AreEqual(0b01000010, list[1]);
            Assert.AreEqual(0b01, list[2]);
        }

        [TestMethod]
        public void TestReadBytes_40()
        {
            List<byte> list = BitReaderUtils.ReadBytes(_buffer, 0, 40);
            Assert.AreEqual(0b10111101, list[0]);
            Assert.AreEqual(0b01000010, list[1]);
            Assert.AreEqual(0b01101001, list[2]);
            Assert.AreEqual(0b11100100, list[3]);
            Assert.AreEqual(0b00001111, list[4]);
        }
    }
}
