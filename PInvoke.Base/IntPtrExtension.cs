using System.Runtime.InteropServices;
using System.Text;

namespace aadog.PInvoke.Base
{
    public static unsafe class IntPtrExtension
    {
        public static IntPtr ptr(Int64 p)
        {
            return ptr((void*)p);
        }
        public static IntPtr ptr(int p)
        {
            return ptr((void*)p);
        }

        public static IntPtr ptr(void* p)
        {
            var ptr = new IntPtr(p);
            return ptr;
        }
        public static bool isNull(this IntPtr ptr)
        {
            return ptr == IntPtr.Zero;
        }
        public static IntPtr Add(this IntPtr ptr, void* offset)
        {
            ptr = IntPtr.Add(ptr, (int)offset);
            return ptr;
        }
        public static IntPtr Add(this IntPtr ptr, Int64 offset)
        {
            ptr = IntPtr.Add(ptr, (int)offset);
            return ptr;
        }
        public static IntPtr Add(IntPtr ptr, int offset)
        {
            ptr = IntPtr.Add(ptr, offset);
            return ptr;
        }
        public static IntPtr Add(IntPtr ptr, byte offset)
        {
            ptr = IntPtr.Add(ptr, offset);
            return ptr;
        }
        public static IntPtr Sub(this IntPtr ptr, Int64 offset)
        {
            ptr = IntPtr.Add(ptr, -(int)offset);
            return ptr;
        }
        public static IntPtr Sub(this IntPtr ptr, int offset)
        {
            ptr = IntPtr.Add(ptr, -offset);
            return ptr;
        }
        public static IntPtr Sub(this IntPtr ptr, byte offset)
        {
            ptr = IntPtr.Add(ptr, -offset);
            return ptr;
        }
        public static IntPtr readPointer(this IntPtr ptr)
        {
            ptr = Marshal.ReadIntPtr(ptr);
            return ptr;
        }
        public static IntPtr writePointer(this IntPtr ptr, IntPtr w)
        {
            Marshal.WriteIntPtr(ptr, w);
            return ptr;
        }
        public static IntPtr writePointer(this IntPtr ptr, void* w)
        {
            Marshal.WriteIntPtr(ptr, IntPtrExtension.ptr(w));
            return ptr;
        }

        public static byte readByte(this IntPtr ptr)
        {
            return Marshal.ReadByte(ptr);
        }
        public static IntPtr writeByte(this IntPtr ptr, byte w)
        {
            Marshal.WriteByte(ptr, w);
            return ptr;
        }
        public static Int16 readInt16(this IntPtr ptr)
        {
            return Marshal.ReadInt16(ptr);
        }
        public static IntPtr writeInt16(this IntPtr ptr, Int16 w)
        {
            Marshal.WriteInt16(ptr, w);
            return ptr;
        }
        public static Int32 readInt32(this IntPtr ptr)
        {
            return Marshal.ReadInt32(ptr);
        }
        public static IntPtr writeInt32(this IntPtr ptr, Int32 w)
        {
            Marshal.WriteInt32(ptr, w);
            return ptr;
        }
        public static Int64 readInt64(this IntPtr ptr)
        {
            return Marshal.ReadInt64(ptr);
        }
        public static IntPtr writeInt64(this IntPtr ptr, Int64 w)
        {
            Marshal.WriteInt64(ptr, w);
            return ptr;
        }
        public static string? readUtf8String(this IntPtr ptr, int? size = null)
        {
            if (size == null)
            {
                return Marshal.PtrToStringUTF8(ptr);
            }
            return Marshal.PtrToStringUTF8(ptr, size.Value);
        }
        public static IntPtr writeUtf8String(this IntPtr ptr, string w)
        {
            var bf = Encoding.UTF8.GetBytes(w);
            var pBuf = stackalloc byte[bf.Length];
            for (int i = 0; i < bf.Length; i++)
            {
                pBuf[i] = bf[i];
            }
            NativeMemory.Copy(&pBuf[0], ptr.ToPointer(), (nuint)bf.Length);
            return ptr;
        }
        public static string? readCString(this IntPtr ptr, int? size = null)
        {
            if (size == null)
            {
                return Marshal.PtrToStringAnsi(ptr);
            }
            return Marshal.PtrToStringAnsi(ptr, size.Value);
        }
        public static IntPtr writeAnsiString(this IntPtr ptr, string w)
        {
            var bf = Encoding.ASCII.GetBytes(w);
            var pBuf = stackalloc byte[bf.Length];
            for (int i = 0; i < bf.Length; i++)
            {
                pBuf[i] = bf[i];
            }
            NativeMemory.Copy(&pBuf[0], ptr.ToPointer(), (nuint)bf.Length);
            return ptr;
        }
        public static string? readUtf16String(this IntPtr ptr, int? size = null)
        {
            if (size == null)
            {
                return Marshal.PtrToStringUni(ptr);
            }
            return Marshal.PtrToStringUni(ptr, size.Value);
        }
        public static IntPtr writeUtf16String(this IntPtr ptr, string w)
        {
            var bf = Encoding.Unicode.GetBytes(w);
            var pBuf = stackalloc byte[bf.Length];
            for (int i = 0; i < bf.Length; i++)
            {
                pBuf[i] = bf[i];
            }
            NativeMemory.Copy(&pBuf[0], ptr.ToPointer(), (nuint)bf.Length);
            return ptr;
        }
        public static byte[]? readByteArray(this IntPtr ptr, int size)
        {
            var bf = new byte[size];
            fixed (byte* pbuf = &bf[0])
                NativeMemory.Copy(ptr.ToPointer(), pbuf, (UIntPtr)size);
            return bf;
        }
        public static IntPtr writeByteArray(this IntPtr ptr, byte[] w)
        {
            var pBuf = stackalloc byte[w.Length];
            for (int i = 0; i < w.Length; i++)
            {
                pBuf[i] = w[i];
            }
            NativeMemory.Copy(&pBuf[0], ptr.ToPointer(), (nuint)w.Length);
            return ptr;
        }

    }
}
