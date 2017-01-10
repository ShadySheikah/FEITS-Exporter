using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;

namespace FEITS.Model
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class FontCharacterData
    {
        public readonly ushort Value;
        public readonly ushort ImageIndex;
        public readonly ushort XOffset;
        public readonly ushort YOffset;
        public readonly byte Width;
        public readonly byte Height;
        public readonly byte Padding1;
        public readonly int CropHeight;
        public readonly int CropWidth;
        public readonly byte[] Padding2; // Size = 3;
        public readonly char Character;

        public FontCharacterData(byte[] data, int ofs = 0)
        {
            Value = BitConverter.ToUInt16(data, ofs + 0);
            ImageIndex = BitConverter.ToUInt16(data, ofs + 2);
            XOffset = BitConverter.ToUInt16(data, ofs + 4);
            YOffset = BitConverter.ToUInt16(data, ofs + 6);
            Width = data[ofs + 8];
            Height = data[ofs + 9];
            Padding1 = data[ofs + 0xA];
            CropHeight = data[ofs + 0xB];
            if (CropHeight > 0x7F)
                CropHeight -= 256;
            CropWidth = data[ofs + 0xC];
            if (CropWidth > 0x7F)
                CropWidth -= 256;
            Padding2 = new byte[0x3];
            Array.Copy(data, ofs + 0xD, Padding2, 0, 3);

            Character = Encoding.Unicode.GetString(data, ofs + 0, 2)[0];
        }
    }
}