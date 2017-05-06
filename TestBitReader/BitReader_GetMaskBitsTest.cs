using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReader_GetMaskBitsTest
    {
        [TestMethod]
        public void TestGetMaskBits_0()
        {
            UInt64 mask = BitReader.GetMaskBits(0);
            Assert.AreEqual((UInt64)0x00, mask);
        }

        [TestMethod]
        public void TestGetMaskBits_1()
        {
            UInt64 mask = BitReader.GetMaskBits(1);
            Assert.AreEqual((UInt64)0b00000001, mask);
        }

        [TestMethod]
        public void TestGetMaskBits_4()
        {
            UInt64 mask = BitReader.GetMaskBits(4);
            Assert.AreEqual((UInt64)0b00001111, mask);
        }

        [TestMethod]
        public void TestGetMaskBits_5()
        {
            UInt64 mask = BitReader.GetMaskBits(5);
            Assert.AreEqual((UInt64)0b00011111, mask);
        }

        [TestMethod]
        public void TestGetMaskBits_8()
        {
            UInt64 mask = BitReader.GetMaskBits(8);
            Assert.AreEqual((UInt64)0b11111111, mask);
        }

        [TestMethod]
        public void TestGetMaskBits_9()
        {
            UInt64 mask = BitReader.GetMaskBits(9);
            Assert.AreEqual((UInt64)0b0000000111111111, mask);
        }

        [TestMethod]
        public void TestGetMaskBits_16()
        {
            UInt64 mask = BitReader.GetMaskBits(16);
            Assert.AreEqual((UInt64)0xffff, mask);
        }

        [TestMethod]
        public void TestGetMaskBits_32()
        {
            UInt64 mask = BitReader.GetMaskBits(32);
            Assert.AreEqual((UInt64)0xffffffff, mask);
        }

        [TestMethod]
        public void TestGetMaskBits_63()
        {
            UInt64 mask = BitReader.GetMaskBits(63);
            Assert.AreEqual((UInt64)0x7fffffffffffffff, mask);
        }

        [TestMethod]
        public void TestGetMaskBits_64()
        {
            UInt64 mask = BitReader.GetMaskBits(64);
            Assert.AreEqual((UInt64)0xffffffffffffffff, mask);
        }
    }
}
