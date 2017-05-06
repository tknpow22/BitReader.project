using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tknpow22.IO;

namespace Tknpow22.Test
{
    [TestClass]
    public class BitReaderUtils_ReadUInt64Test
    {
        [TestMethod]
        public void TestReadUInt64_64()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 64);
            Assert.AreEqual((UInt64)0x1234567890abcdef, val);
        }

        [TestMethod]
        public void TestReadUInt64_64_r()
        {
            byte[] buffer = new byte[] { 0xef, 0xcd, 0xab, 0x90, 0x78, 0x56, 0x34, 0x12, };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 64, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0x1234567890abcdef, val);
        }

        [TestMethod]
        public void TestReadUInt64_56()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 56);
            Assert.AreEqual((UInt64)0x1234567890abcd, val);
        }

        [TestMethod]
        public void TestReadUInt64_56_r()
        {
            byte[] buffer = new byte[] { 0xcd, 0xab, 0x90, 0x78, 0x56, 0x34, 0x12, };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 56, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0x1234567890abcd, val);
        }

        [TestMethod]
        public void TestReadUInt64_16()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 16);
            Assert.AreEqual((UInt64)0x1234, val);
        }

        [TestMethod]
        public void TestReadUInt64_16_r()
        {
            byte[] buffer = new byte[] { 0x34, 0x12, };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0x1234, val);
        }

        [TestMethod]
        public void TestReadUInt64_8()
        {
            byte[] buffer = new byte[] { 0x12 };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 8);
            Assert.AreEqual((UInt64)0x12, val);
        }

        [TestMethod]
        public void TestReadUInt64_8_r()
        {
            byte[] buffer = new byte[] { 0x12 };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0x12, val);
        }

        [TestMethod]
        public void TestReadUInt64_1()
        {
            byte[] buffer = new byte[] { 0x80 };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 1);
            Assert.AreEqual((UInt64)0x1, val);
        }

        [TestMethod]
        public void TestReadUInt64_1_r()
        {
            byte[] buffer = new byte[] { 0x80 };

            UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, 1, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0x1, val);
        }

        [TestMethod]
        public void TestReadUInt64_1to64()
        {
            byte[] buffer = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef, };
            UInt64 bufferVal = 0x1234567890abcdef;

            for (int i = 1; i <= 64; ++i) {
                UInt64 val = BitReaderUtils.ReadUInt64(buffer, 0, i);

                UInt64 maskBits = BitReader.GetMaskBits(i);

                maskBits <<= (64 - i);
                UInt64 maskedVal = bufferVal & maskBits;
                maskedVal >>= (64 - i);

                Assert.AreEqual(maskedVal, val);
            }
        }

        [TestMethod]
        public void TestReadUInt64_1to64_r()
        {
            byte[] buffer = new byte[] { 0xef, 0xcd, 0xab, 0x90, 0x78, 0x56, 0x34, 0x12, };

            UInt64 val;

            val = BitReaderUtils.ReadUInt64(buffer, 0, 1, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00000001, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 2, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00000011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 3, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00000111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 4, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00001110, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 5, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00011101, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 6, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00111011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 7, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01110111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 8, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b11101111, val);

            val = BitReaderUtils.ReadUInt64(buffer, 0, 9, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1101111100000001, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 10, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1011111100000011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 11, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0111111000000111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 12, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1111110000001110, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 13, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1111100100011101, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 14, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1111001100111011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 15, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1110011001110111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 16, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1100110111101111, val);

            val = BitReaderUtils.ReadUInt64(buffer, 0, 17, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b100110111101111100000001, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 18, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b001101101011111100000011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 19, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b011011010111111000000111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 20, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b110110101111110000001110, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 21, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b101101011111100100011101, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 22, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b011010101111001100111011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 23, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b110101011110011001110111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 24, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b101010111100110111101111, val);

            val = BitReaderUtils.ReadUInt64(buffer, 0, 25, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01010111100110111101111100000001, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 26, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b10101110001101101011111100000011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 27, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01011100011011010111111000000111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 28, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b10111001110110101111110000001110, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 29, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01110010101101011111100100011101, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 30, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b11100100011010101111001100111011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 31, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b11001000110101011110011001110111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 32, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b10010000101010111100110111101111, val);

            val = BitReaderUtils.ReadUInt64(buffer, 0, 33, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0010000001010111100110111101111100000001, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 34, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0100000110101110001101101011111100000011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 35, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1000001101011100011011010111111000000111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 36, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0000011110111001110110101111110000001110, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 37, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0000111101110010101101011111100100011101, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 38, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0001111011100100011010101111001100111011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 39, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0011110011001000110101011110011001110111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 40, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0111100010010000101010111100110111101111, val);

            val = BitReaderUtils.ReadUInt64(buffer, 0, 41, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b111100000010000001010111100110111101111100000001, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 42, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b111000010100000110101110001101101011111100000011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 43, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b110000101000001101011100011011010111111000000111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 44, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b100001010000011110111001110110101111110000001110, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 45, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b000010100000111101110010101101011111100100011101, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 46, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b000101010001111011100100011010101111001100111011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 47, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b001010110011110011001000110101011110011001110111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 48, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b010101100111100010010000101010111100110111101111, val);

            val = BitReaderUtils.ReadUInt64(buffer, 0, 49, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b10101100111100000010000001010111100110111101111100000001, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 50, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01011000111000010100000110101110001101101011111100000011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 51, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b10110001110000101000001101011100011011010111111000000111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 52, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b01100011100001010000011110111001110110101111110000001110, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 53, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b11000110000010100000111101110010101101011111100100011101, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 54, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b10001101000101010001111011100100011010101111001100111011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 55, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00011010001010110011110011001000110101011110011001110111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 56, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b00110100010101100111100010010000101010111100110111101111, val);

            val = BitReaderUtils.ReadUInt64(buffer, 0, 57, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0110100010101100111100000010000001010111100110111101111100000001, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 58, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1101000001011000111000010100000110101110001101101011111100000011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 59, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1010000010110001110000101000001101011100011011010111111000000111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 60, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0100000101100011100001010000011110111001110110101111110000001110, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 61, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b1000001011000110000010100000111101110010101101011111100100011101, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 62, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0000010010001101000101010001111011100100011010101111001100111011, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 63, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0000100100011010001010110011110011001000110101011110011001110111, val);
            val = BitReaderUtils.ReadUInt64(buffer, 0, 64, BitReader.Endianness.Little);
            Assert.AreEqual((UInt64)0b0001001000110100010101100111100010010000101010111100110111101111, val);
        }
    }
}
