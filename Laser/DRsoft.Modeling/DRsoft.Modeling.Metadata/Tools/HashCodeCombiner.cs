using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace DRsoft.Modeling.Metadata.Tools
{
 
/*
 * Class used to combine several hashcodes into a single hashcode
 */
internal class HashCodeCombiner {
 
    private long _combinedHash;
 
    internal HashCodeCombiner() {
       // Start with a seed (obtained from String.GetHashCode implementation)
       _combinedHash = 5381;
    }
 
    internal HashCodeCombiner(long initialCombinedHash)   {
        _combinedHash = initialCombinedHash;
    }
 
    internal static int CombineHashCodes(int h1, int h2) {
        return ((h1 << 5) + h1) ^ h2;
    }
 
    internal static int CombineHashCodes(int h1, int h2, int h3) {
        return CombineHashCodes(CombineHashCodes(h1, h2), h3);
    }
 
    internal static int CombineHashCodes(int h1, int h2, int h3, int h4) {
        return CombineHashCodes(CombineHashCodes(h1, h2), CombineHashCodes(h3, h4));
    }
 
    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5) {
        return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), h5);
    }
 
 
    internal void AddArray(string[] a) {
        if (a != null) {
            int n = a.Length;
            for (int i = 0; i < n; i++) {
                AddObject(a[i]);
            }
        }
    }
 
    internal void AddInt(int n) {
        _combinedHash = ((_combinedHash << 5) + _combinedHash) ^ n;
    }
 
    internal void AddObject(int n) {
        AddInt(n);
    }
 
    internal void AddObject(byte b) {
        AddInt(b.GetHashCode());
    }
 
    internal void AddObject(long l) {
        AddInt(l.GetHashCode());
    }
 
    internal void AddObject(bool b) {
        AddInt(b.GetHashCode());
    }
 
    internal void AddObject(string s) {
        if (s != null)
            AddInt(GetStringHashCode(s));
    }
 
    internal static unsafe int GetStringHashCode(string s)
    {
        fixed (char* chPtr = s)
        {
            int num1 = 352654597;
            int num2 = num1;
            int* numPtr = (int*) chPtr;
            for (int length = s.Length; length > 0; length -= 4)
            {
                num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
                if (length > 2)
                {
                    num2 = (num2 << 5) + num2 + (num2 >> 27) ^ numPtr[1];
                    numPtr += 2;
                }
                else
                    break;
            }
            return num1 + num2 * 1566083941;
        }
    }
  
 
    internal void AddObject(object o) {
        if (o != null)
            AddInt(o.GetHashCode());
    }
 
    internal void AddDateTime(DateTime dt) {
        AddInt(dt.GetHashCode());
    }
 
    private void AddFileSize(long fileSize) {
        AddInt(fileSize.GetHashCode());
    }
 
    [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "This call site is trusted.")]
    private void AddFileVersionInfo(FileVersionInfo fileVersionInfo) {
        AddInt(fileVersionInfo.FileMajorPart.GetHashCode());
        AddInt(fileVersionInfo.FileMinorPart.GetHashCode());
        AddInt(fileVersionInfo.FileBuildPart.GetHashCode());
        AddInt(fileVersionInfo.FilePrivatePart.GetHashCode());
    }
 
    internal long CombinedHash => _combinedHash;
    internal int CombinedHash32 => _combinedHash.GetHashCode();

    internal string CombinedHashString => _combinedHash.ToString("x", CultureInfo.InvariantCulture);
}
 
 
}