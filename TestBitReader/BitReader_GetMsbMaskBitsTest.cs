using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReader_GetMsbMaskBitsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetMsbMaskBitsException_0()
        {
            BitReader.GetMsbMaskBits(0);
        }

        [TestMethod]
        public void TestGetMsbMaskBits_1()
        {
            UInt64 mask = BitReader.GetMsbMaskBits(1);
            Assert.AreEqual((UInt64)0b00000001, mask);
        }

        [TestMethod]
        public void TestGetMsbMaskBits_4()
        {
            UInt64 mask = BitReader.GetMsbMaskBits(4);
            Assert.AreEqual((UInt64)0b00001000, mask);
        }

        [TestMethod]
        public void TestGetMsbMaskBits_5()
        {
            UInt64 mask = BitReader.GetMsbMaskBits(5);
            Assert.AreEqual((UInt64)0b00010000, mask);
        }

        [TestMethod]
        public void TestGetMsbMaskBits_8()
        {
            UInt64 mask = BitReader.GetMsbMaskBits(8);
            Assert.AreEqual((UInt64)0b10000000, mask);
        }

        [TestMethod]
        public void TestGetMsbMaskBits_9()
        {
            UInt64 mask = BitReader.GetMsbMaskBits(9);
            Assert.AreEqual((UInt64)0b0000000100000000, mask);
        }

        [TestMethod]
        public void TestGetMsbMaskBits_16()
        {
            UInt64 mask = BitReader.GetMsbMaskBits(16);
            Assert.AreEqual((UInt64)0x8000, mask);
        }

        [TestMethod]
        public void TestGetMsbMaskBits_32()
        {
            UInt64 mask = BitReader.GetMsbMaskBits(32);
            Assert.AreEqual((UInt64)0x80000000, mask);
        }

        [TestMethod]
        public void TestGetMsbMaskBits_64()
        {
            UInt64 mask = BitReader.GetMsbMaskBits(64);
            Assert.AreEqual((UInt64)0x8000000000000000, mask);
        }
    }
}
