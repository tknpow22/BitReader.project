using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReader_ConstructorTest
    {
        private byte[] _buffer = new byte[] { 0x00, 0x00, 0x00, 0x00 };

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructorBufferNullException()
        {
            new BitReader(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConstructorUnderStartIndexException()
        {
            new BitReader(_buffer, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestConstructorOverStartIndexException()
        {
            new BitReader(_buffer, _buffer.Length);
        }

        [TestMethod]
        public void TestCurrentByteArrayIndex_1()
        {
            BitReader bitReader = new BitReader(_buffer);
            Assert.AreEqual(0, bitReader.CurrentIndex);
        }

        [TestMethod]
        public void TestCurrentByteArrayIndex_2()
        {
            BitReader bitReader = new BitReader(_buffer, 1);
            Assert.AreEqual(1, bitReader.CurrentIndex);
        }
    }
}
