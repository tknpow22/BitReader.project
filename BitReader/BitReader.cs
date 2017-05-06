using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tknpow22.IO
{
    /// <summary>
    /// バイト配列からビット単位で読み込む
    /// References: BitStreamReader: http://referencesource.microsoft.com/#PresentationCore/Shared/MS/Internal/Ink/BitStream.cs
    /// </summary>
    public class BitReader
    {
        /// <summary>
        /// 1 バイトのバイト数
        /// </summary>
        public const int BitsPerByte = 8;

        /// <summary>
        /// 読み込み対象バイト配列への参照
        /// </summary>
        private byte[] _byteArray = null;

        /// <summary>
        /// 読み込み対象バイト配列へのインデックス
        /// </summary>
        private int _byteArrayIndex = 0;

        /// <summary>
        /// 読み込み対象バイト配列から読み込める最大ビット数
        /// </summary>
        private uint _bufferLengthInBits = 0;

        /// <summary>
        /// 読み込み途中のビットデータのキャッシュ
        /// </summary>
        private byte _partialByte = 0;

        /// <summary>
        /// ビットデータのキャッシュに残るビット数
        /// </summary>
        private int _cbitsInPartialByte = 0;

        /// <summary>
        /// 読み込み対象データのエンディアンネス
        /// </summary>
        private Endianness _endianness = Endianness.Big;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="buffer">読み込み対象データ</param>
        /// <param name="startIndex">読み込みを開始するバイトインデックス</param>
        /// <param name="endianness">データのエンディアンネス</param>
        public BitReader(byte[] buffer, int startIndex = 0, Endianness endianness = Endianness.Big)
        {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }

            if (startIndex < 0 || buffer.Length <= startIndex) {
                throw new ArgumentOutOfRangeException("startIndex");
            }

            this._byteArray = buffer;
            this._byteArrayIndex = startIndex;
            this._bufferLengthInBits = (uint)(buffer.Length - startIndex) * BitReader.BitsPerByte;
            this._partialByte = 0;
            this._cbitsInPartialByte = 0;
            this._endianness = endianness;
        }

        /// <summary>
        /// 読み込み対象データのエンディアンネスを表す
        /// </summary>
        public enum Endianness
        {
            Little,
            Big,
        }

        /// <summary>
        /// 現在の読み込み対象バイト配列へのインデックスを返す
        /// NOTE: MS.Internal.Ink.BitStreamReader とは動作が違うので注意
        /// </summary>
        public int CurrentIndex
        {
            get {
                return this._byteArrayIndex;
            }
            set {
                int startIndex = value;

                if (startIndex < 0 || this._byteArray.Length <= startIndex) {
                    throw new ArgumentOutOfRangeException("CurrentIndex");
                }

                this._byteArrayIndex = startIndex;
                this._bufferLengthInBits = (uint)(this._byteArray.Length - startIndex) * BitReader.BitsPerByte;
                this._partialByte = 0;
                this._cbitsInPartialByte = 0;
            }
        }

        /// <summary>
        /// ビットデータを読み UInt64 として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <returns>読み込んだ値</returns>
        public UInt64 ReadUInt64(int countOfBits)
        {
            int bytesOfType = sizeof(UInt64);
            int bitsOfType = this.GetBitsOfType(bytesOfType);

            this.CheckNumeric(countOfBits, bitsOfType);

            List<byte> buffer = this.FetchNumeric(countOfBits, bytesOfType);
            return BitConverter.ToUInt64(buffer.ToArray(), 0);
        }

        /// <summary>
        /// ビットデータを読み Int64 として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <returns>読み込んだ値</returns>
        public Int64 ReadInt64(int countOfBits)
        {
            int bytesOfType = sizeof(Int64);
            int bitsOfType = this.GetBitsOfType(bytesOfType);

            this.CheckNumeric(countOfBits, bitsOfType);

            ReadInt64Converter converter = new ReadInt64Converter(this);
            return converter.Convert(countOfBits, bytesOfType, bitsOfType);
        }

        /// <summary>
        /// ビットデータを読み UInt32 として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <returns>読み込んだ値</returns>
        public UInt32 ReadUInt32(int countOfBits)
        {
            int bytesOfType = sizeof(UInt32);
            int bitsOfType = this.GetBitsOfType(bytesOfType);

            this.CheckNumeric(countOfBits, bitsOfType);

            List<byte> buffer = this.FetchNumeric(countOfBits, bytesOfType);
            return BitConverter.ToUInt32(buffer.ToArray(), 0);
        }

        /// <summary>
        /// ビットデータを読み Int32 として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <returns>読み込んだ値</returns>
        public Int32 ReadInt32(int countOfBits)
        {
            int bytesOfType = sizeof(Int32);
            int bitsOfType = this.GetBitsOfType(bytesOfType);

            this.CheckNumeric(countOfBits, bitsOfType);

            ReadInt32Converter converter = new ReadInt32Converter(this);
            return converter.Convert(countOfBits, bytesOfType, bitsOfType);
        }

        /// <summary>
        /// ビットデータを読み UInt16 として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <returns>読み込んだ値</returns>
        public UInt16 ReadUInt16(int countOfBits)
        {
            int bytesOfType = sizeof(UInt16);
            int bitsOfType = this.GetBitsOfType(bytesOfType);

            this.CheckNumeric(countOfBits, bitsOfType);

            List<byte> buffer = this.FetchNumeric(countOfBits, bytesOfType);
            return BitConverter.ToUInt16(buffer.ToArray(), 0);
        }

        /// <summary>
        /// ビットデータを読み Int16 として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <returns>読み込んだ値</returns>
        public Int16 ReadInt16(int countOfBits)
        {
            int bytesOfType = sizeof(Int16);
            int bitsOfType = this.GetBitsOfType(bytesOfType);

            this.CheckNumeric(countOfBits, bitsOfType);

            ReadInt16Converter converter = new ReadInt16Converter(this);
            return converter.Convert(countOfBits, bytesOfType, bitsOfType);
        }

        /// <summary>
        /// ビットデータを読み byte として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <returns>読み込んだ値</returns>
        public byte ReadByte(int countOfBits)
        {
            return this.FetchByte(countOfBits);
        }

        /// <summary>
        /// ビットデータを読み byte の List として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <returns>読み込んだ byte の List</returns>
        public List<byte> ReadBytes(int countOfBits)
        {
            return this.FetchBytes(countOfBits, false);
        }

        /// <summary>
        /// MSB のみビットの立ったマスクビットを返す
        /// </summary>
        /// <param name="countOfBits">マスクビットのビット数</param>
        /// <returns>マスクビット</returns>
        public static UInt64 GetMsbMaskBits(int countOfBits)
        {
            if (countOfBits <= 0) {
                throw new ArgumentOutOfRangeException("countOfBits");
            }

            UInt64 mask = 0x01;
            if (1 < countOfBits) {
                mask <<= countOfBits - 1;
            }

            return mask;
        }

        /// <summary>
        /// すべてビットの立ったマスクビットを返す
        /// </summary>
        /// <param name="countOfBits">マスクビットのビット数</param>
        /// <returns>マスクビット</returns>
        public static UInt64 GetMaskBits(int countOfBits)
        {
            UInt64 mask = 0x00;
            mask = ~mask;
            if (countOfBits < sizeof(UInt64) * BitReader.BitsPerByte) {
                mask <<= countOfBits;
                mask = ~mask;
            }

            return mask;
        }

        /// <summary>
        /// ビットデータを読み Int に変換する
        /// </summary>
        /// <typeparam name="T">Int の型</typeparam>
        private abstract class ReadIntConverter<T>
        {
            /// <summary>
            /// 変換用インスタンス
            /// </summary>
            BitReader _bitReader;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="bitReader">変換用インスタンス</param>
            public ReadIntConverter(BitReader bitReader)
            {
                this._bitReader = bitReader;
            }

            /// <summary>
            /// Int に変換する
            /// </summary>
            /// <param name="countOfBits">ビット数</param>
            /// <param name="bytesOfType">Int の型のバイト数</param>
            /// <param name="bitsOfType">Int の型のビット数</param>
            /// <returns>変換した値</returns>
            public T Convert(int countOfBits, int bytesOfType, int bitsOfType) {

                List<byte> buffer = this._bitReader.FetchNumeric(countOfBits, bytesOfType);

                T result;
                if (countOfBits == 1 || countOfBits == bitsOfType) {
                    // 1 ビットのみ、または、フルビットを読み込む際は、BitConverter で処理する
                    result = this.ToInt(buffer.ToArray());
                } else {
                    // 2 ビット以上、かつ、フルビット未満を読み込む際は、符号なし整数として取得し、
                    // 取得したビット群の最上位ビットがセットされていれば、負数とみなし、
                    // セットされていなければ、正数とみなす。
                    // ・正数の場合は、そのまま符号付き整数としてキャストする
                    // ・負数の場合は、2 の補数の逆算を行い正数を得て、その正数を負数にしたものを結果値とする
                    UInt64 temp = this.ToUInt64(buffer.ToArray());

                    UInt64 msbMaskBits = BitReader.GetMsbMaskBits(countOfBits);
                    if ((temp & msbMaskBits) == 0) {
                        // 正数
                        // 値は「フルビットの幅 - 1」に収まるため、オーバーフローはしない
                        result = this.Cast(temp);
                    } else {
                        // 負数
                        temp -= 1;
                        UInt64 maskBits = BitReader.GetMaskBits(countOfBits);
                        unchecked {
                            result = this.Cast(~temp & maskBits);
                        }

                        result = ToNegative(result);
                    }
                }

                return result;
            }

            /// <summary>
            /// Int への変換を行う
            /// </summary>
            /// <param name="byteArray">バイト配列</param>
            /// <returns>変換した値</returns>
            protected abstract T ToInt(byte[] byteArray);

            /// <summary>
            /// UInt への変換を行う
            /// </summary>
            /// <param name="byteArray">バイト配列</param>
            /// <returns>変換した値</returns>
            protected abstract UInt64 ToUInt64(byte[] byteArray);

            /// <summary>
            /// マイナスへの反転を行う
            /// </summary>
            /// <param name="value">元の値</param>
            /// <returns>反転した値</returns>
            protected abstract T ToNegative(T value);

            /// <summary>
            /// Int へのキャストを行う
            /// </summary>
            /// <param name="value">元の値</param>
            /// <returns>キャストした値</returns>
            protected abstract T Cast(UInt64 value);
        }

        /// <summary>
        /// ビットデータを読み Int64 に変換する
        /// </summary>
        private class ReadInt64Converter : ReadIntConverter<Int64>
        {
            public ReadInt64Converter(BitReader bitReader) : base(bitReader)
            {
            }

            protected override Int64 ToInt(byte[] byteArray)
            {
                return BitConverter.ToInt64(byteArray, 0);
            }

            protected override UInt64 ToUInt64(byte[] byteArray)
            {
                return BitConverter.ToUInt64(byteArray, 0);
            }

            protected override Int64 ToNegative(Int64 value)
            {
                return -value;
            }

            protected override Int64 Cast(UInt64 value)
            {
                return (Int64)value;
            }
        }

        /// <summary>
        /// ビットデータを読み Int32 に変換する
        /// </summary>
        private class ReadInt32Converter : ReadIntConverter<Int32>
        {
            public ReadInt32Converter(BitReader bitReader) : base(bitReader)
            {
            }

            protected override Int32 ToInt(byte[] byteArray)
            {
                return BitConverter.ToInt32(byteArray, 0);
            }

            protected override UInt64 ToUInt64(byte[] byteArray)
            {
                return BitConverter.ToUInt32(byteArray, 0);
            }

            protected override Int32 ToNegative(Int32 value)
            {
                return -value;
            }

            protected override Int32 Cast(UInt64 value)
            {
                return (Int32)value;
            }
        }

        /// <summary>
        /// ビットデータを読み Int16 に変換する
        /// </summary>
        private class ReadInt16Converter : ReadIntConverter<Int16>
        {
            public ReadInt16Converter(BitReader bitReader) : base(bitReader)
            {
            }

            protected override Int16 ToInt(byte[] byteArray)
            {
                return BitConverter.ToInt16(byteArray, 0);
            }

            protected override UInt64 ToUInt64(byte[] byteArray)
            {
                return BitConverter.ToUInt16(byteArray, 0);
            }

            protected override Int16 ToNegative(Int16 value)
            {
                return (Int16)(-value);
            }

            protected override Int16 Cast(UInt64 value)
            {
                return (Int16)value;
            }
        }

        /// <summary>
        /// ビットデータを読み byte として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <returns>読み込んだ値</returns>
        private byte FetchByte(int countOfBits)
        {
            if (countOfBits <= 0 || BitReader.BitsPerByte < countOfBits) {
                throw new ArgumentOutOfRangeException("countOfBits");
            }

            if (this._bufferLengthInBits < countOfBits) {
                throw new ArgumentOutOfRangeException("countOfBits");
            }

            byte result = 0;

            if (countOfBits <= this._cbitsInPartialByte) {
                // キャッシュデータからビットデータを取得する
                result = (byte)(this._partialByte >> (BitReader.BitsPerByte - countOfBits));

                // 残りをキャッシュの左側に寄せておく
                unchecked {
                    this._partialByte <<= countOfBits;
                }

                // キャッシュのビット数を更新する
                this._cbitsInPartialByte -= countOfBits;

            } else {
                // 次のバイトデータから読み込むビット数
                int readBitsFromNextByte = countOfBits - this._cbitsInPartialByte;

                // 次のバイトデータに施す右シフト回数
                int rightShiftNextByte = BitReader.BitsPerByte - readBitsFromNextByte;

                // 次のバイトデータを取り出す
                byte nextByte = _byteArray[_byteArrayIndex];
                ++_byteArrayIndex;

                // キャッシュからビットを取り出す
                // NOTE: 右シフト数 = 8 - bitsCountInCache - (readBits - bitsCountInCache) 
                //                  = 8 - bitsCountInCache - readBits + bitsCountInCache
                //                  = 8 - readBits
                result = (byte)(this._partialByte >> (BitReader.BitsPerByte - countOfBits));

                // キャッシュからのビットと、読み込んだバイトデータからのビットを論理和する
                result |= (byte)(nextByte >> rightShiftNextByte);

                unchecked {
                    // 残りをキャッシュの左側に寄せておく
                    this._partialByte = (byte)(nextByte << readBitsFromNextByte);
                }

                // キャッシュのビット数を更新する
                this._cbitsInPartialByte = BitReader.BitsPerByte - readBitsFromNextByte;
            }

            // 読み込み対象バイト配列から読み込める最大ビット数を更新する
            this._bufferLengthInBits -= (uint)countOfBits;

            return result;
        }

        /// <summary>
        /// ビットデータを読み byte の List として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <param name="useNumericRead">数値を読み込む際は true を指定</param>
        /// <returns>読み込んだ値の List</returns>
        private List<byte> FetchBytes(int countOfBits, bool useNumericRead)
        {
            int byteCount = 0;

            List<byte> result = new List<byte>();
            while (0 < countOfBits) {
                int readBitsAtByte = BitReader.BitsPerByte;
                if (countOfBits < BitReader.BitsPerByte) {
                    readBitsAtByte = countOfBits;
                }

                byte byteValue = this.FetchByte(readBitsAtByte);

                if (useNumericRead && 0 < byteCount && readBitsAtByte < BitReader.BitsPerByte) {
                    // 最終の読み込みデータが 8 ビットに足らない場合の処理

                    // NOTE: useNumericRead:
                    //       バイト配列として読み込む際は、本処理は必要無いため、
                    //       処理の有無を指定する。
                    // NOTE: 0 < byteCount:
                    //       最後の読み込みデータが 8 ビットに足らない場合でも、
                    //       1 バイトしか読み取らない際はシフトの必要はないので、
                    //       本処理は行わない。

                    // 下の処理で左にシフトすることで、
                    // ずれたビットデータを格納する領域を先頭に追加する。
                    result.Insert(0, 0x00);

                    // 最後の読み込みデータのビット数分、左にシフトする。
                    // ただし、最後の 1 つ前(以下、直近)の読み込みデータは下で処理するためシフトしない
                    for (int i = 0; i < result.Count - 1; ++i) {
                        ushort bits = (ushort)(result[i] << BitReader.BitsPerByte | result[i + 1]);
                        bits <<= readBitsAtByte;
                        result[i] = (byte)(bits >> BitReader.BitsPerByte);
                    }

                    // 直近の読み込みデータと最後の読み込みデータを論理和し、
                    // 最後の読み込みデータとする。
                    ushort lastData = (ushort)(result[result.Count - 1] << readBitsAtByte);
                    lastData |= byteValue;
                    byteValue = (byte)(lastData & 0xff);

                    // 直近の読み込みデータを除き、
                    // 下で最後の読み込みデータが追加されるのに備える。
                    result.RemoveAt(result.Count - 1);
                }

                result.Add(byteValue);
                ++byteCount;

                countOfBits -= readBitsAtByte;
            }

            return result;
        }

        /// <summary>
        /// ビット数を得る
        /// </summary>
        /// <param name="bytesOfType">バイト数</param>
        /// <returns>ビット数</returns>
        private int GetBitsOfType(int bytesOfType)
        {
            Debug.Assert(0 < bytesOfType);
            return bytesOfType * BitReader.BitsPerByte;
        }

        /// <summary>
        /// 読み込むビット数と型のビット数とのチェックを行う
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <param name="bitsOfType">型を構成するビット数</param>
        private void CheckNumeric(int countOfBits, int bitsOfType)
        {
            if (countOfBits <= 0 || bitsOfType < countOfBits) {
                throw new ArgumentOutOfRangeException("countOfBits");
            }
        }

        /// <summary>
        /// 数値としてビットデータを読み byte の List として返す
        /// </summary>
        /// <param name="countOfBits">読み込むビット数</param>
        /// <param name="bytesOfType">型を構成するバイト数</param>
        /// <returns>読み込んだ byte の List</returns>
        private List<byte> FetchNumeric(int countOfBits, int bytesOfType)
        {
            List<byte> buffer = this.FetchBytes(countOfBits, true);
            this.AdjustNumeric(buffer, bytesOfType);

            return buffer;
        }

        /// <summary>
        /// データのエンディアンネスにあわせ、型を構成するバイトを整形する
        /// </summary>
        /// <param name="buffer">読み込んだ byte の List</param>
        /// <param name="bytesOfType">型を構成するバイト数</param>
        private void AdjustNumeric(List<byte> buffer, int bytesOfType)
        {
            while (buffer.Count < bytesOfType) {
                if (this._endianness == Endianness.Big) {
                    buffer.Insert(0, 0x00);
                } else {
                    buffer.Add(0x00);
                }
            }

            if (BitConverter.IsLittleEndian) {
                if (this._endianness == Endianness.Big) {
                    buffer.Reverse();
                }
            } else {
                if (this._endianness == Endianness.Little) {
                    buffer.Reverse();
                }
            }
        }
    }
}
