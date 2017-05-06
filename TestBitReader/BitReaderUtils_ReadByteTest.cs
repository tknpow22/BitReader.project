using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReaderUtils_ReadByteTest
    {
        byte[] _buffer = new byte[] { 0b10111101, 0b01000010, 0b01101001, 0b11100100, 0b00001111 };

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestReadByte_minus_1()
        {
            BitReaderUtils.ReadByte(_buffer, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestReadByte_0()
        {
            BitReaderUtils.ReadByte(_buffer, 0, 0);
        }

        [TestMethod]
        public void TestReadByte_1()
        {
            byte val = BitReaderUtils.ReadByte(_buffer, 0, 1);
            Assert.AreEqual(0b1, val);
        }

        [TestMethod]
        public void TestReadByte_5()
        {
            byte val = BitReaderUtils.ReadByte(_buffer, 0, 5);
            Assert.AreEqual(0b10111, val);
        }

        [TestMethod]
        public void TestReadByte_8()
        {
            byte val = BitReaderUtils.ReadByte(_buffer, 0, 8);
            Assert.AreEqual(0b10111101, val);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestReadByte_9()
        {
            BitReaderUtils.ReadByte(_buffer, 0, 9);
        }
    }
}
