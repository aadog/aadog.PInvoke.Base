using System.Globalization;
using System.Text;

namespace aadog.PInvoke.Base
{
    public class HexDumpOption()
    {
        public int? offset;
        public int? length;
        public bool? header;
        public bool? ansi;
    }
    public unsafe class HexDump
    {
        public static string fromPtr(IntPtr ptr, HexDumpOption? option=null)
        {
            option ??= new HexDumpOption { };
            option.offset ??= 0;
            option.length ??= 64;
            option.header ??= true;
            var bytes = ptr.readByteArray(option.length.Value);
            return FormatAsHex(bytes, option.header.Value);
        }

        public static string fromAddress(void* addr, HexDumpOption? option = default)
        {
            return fromPtr(IntPtrExtension.ptr(addr), option);
        }
        public static string FormatAsHex(ReadOnlySpan<byte> data, bool header)
        {
            byte ReplaceControlCharacterWithDot(byte character)
                => character < 31 || character >= 127 ? (byte)46 /* dot */ : character;
            byte[] ReplaceControlCharactersWithDots(byte[] characters)
                => characters.Select(ReplaceControlCharacterWithDot).ToArray();

            var result = new StringBuilder();
            const int lineWidth = 16;
            if (header)
                result.AppendLine("      0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F");
            for (var pos = 0; pos < data.Length;)
            {
                var line = data.Slice(pos, Math.Min(lineWidth, data.Length - pos)).ToArray();
                var asHex = string.Join(" ", line.Select(v => v.ToString("X2", CultureInfo.InvariantCulture)));
                asHex += new string(' ', lineWidth * 3 - 1 - asHex.Length);
                var asCharacters = Encoding.ASCII.GetString(ReplaceControlCharactersWithDots(line));
                result.Append(FormattableString.Invariant($"{pos:X4} {asHex} {asCharacters}\n"));
                pos += line.Length;
            }
            return result.ToString();
        }
    }
}
