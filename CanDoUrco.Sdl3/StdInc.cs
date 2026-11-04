// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    public const uint InvalidUnicodeCodepoint = 0xFFFD;

    public static bool UseDotnetEpsilon { get; set; }

    public static bool UseDotnetPi { get; set; }

    public static float FltEpsilon => UseDotnetEpsilon ? float.Epsilon : 1.1920928955078125e-07F;

    public static double PiD => UseDotnetPi ? double.Pi : 3.141592653589793238462643383279502884D;

    public static float PiF => UseDotnetPi ? float.Pi : 3.141592653589793238462643383279502884F;

    public static nuint IconVError => unchecked((nuint)(-1));

    public static nuint IconVE2Big => unchecked((nuint)(-2));

    public static nuint IconVEILSeq => unchecked((nuint)(-3));

    public static nuint IconVEInval => unchecked((nuint)(-4));

    [LibraryImport(DllName, EntryPoint = "SDL_malloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _MAlloc(nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_calloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _CAlloc(nuint nmemb, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_realloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _Realloc(void* mem, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void _Free(void* mem);

    [LibraryImport(DllName, EntryPoint = "SDL_GetOriginalMemoryFunctions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void _GetOriginalMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetMemoryFunctions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void _GetMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetMemoryFunctions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _SetMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    );

    [LibraryImport(DllName, EntryPoint = "SDL_aligned_alloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _AlignedAlloc(nuint alignment, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_aligned_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void _AlignedFree(void* mem);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumAllocations")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumAllocations();

    [LibraryImport(DllName, EntryPoint = "SDL_GetEnvironment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Environment _GetEnvironment();

    [LibraryImport(DllName, EntryPoint = "SDL_CreateEnvironment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Environment _CreateEnvironment(
        [MarshalAs(UnmanagedType.I1)] bool populated
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetEnvironmentVariable",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial string? _GetEnvironmentVariable(Environment env, string name);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetEnvironmentVariables",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte** _GetEnvironmentVariables(Environment env);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetEnvironmentVariable",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetEnvironmentVariable(
        Environment env,
        string name,
        string? value,
        [MarshalAs(UnmanagedType.I1)] bool overwrite
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_UnsetEnvironmentVariable",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _UnsetEnvironmentVariable(Environment env, string name);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyEnvironment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyEnvironment(Environment env);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_getenv",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial string? _GetEnv(string name);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_getenv_unsafe",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial string? _GetEnvUnsafe(string name);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_setenv_unsafe",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetEnvUnsafe(
        Environment env,
        string name,
        string? value,
        [MarshalAs(UnmanagedType.I1)] bool overwrite
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_unsetenv_unsafe",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _UnsetEnvUnsafe(Environment env, string name);

    [LibraryImport(DllName, EntryPoint = "SDL_qsort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void _QSort(
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, int> compare
    );

    [LibraryImport(DllName, EntryPoint = "SDL_bsearch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _BSearch(
        void* key,
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, int> compare
    );

    [LibraryImport(DllName, EntryPoint = "SDL_qsort_r")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void _QSortR(
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, void*, int> compare,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_bsearch_r")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _BSearchR(
        void* key,
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, void*, int> compare,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_abs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _Abs(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_isalpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsAlpha(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_isalnum")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsAlnum(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_isblank")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsBlank(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_iscntrl")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsCntrl(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_isdigit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsDigit(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_isxdigit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsXDigit(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_ispunct")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsPunct(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_isspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsSpace(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_isupper")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsUpper(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_islower")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsLower(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_isprint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsPrint(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_isgraph")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsGraph(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_toupper")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _ToUpper(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_tolower")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _ToLower(int x);

    [LibraryImport(DllName, EntryPoint = "SDL_crc16")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial ushort _Crc16(ushort crc, byte* data, nuint length);

    [LibraryImport(DllName, EntryPoint = "SDL_crc32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial uint _Crc32(uint crc, byte* data, nuint length);

    [LibraryImport(DllName, EntryPoint = "SDL_murmur3_32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial uint _Murmur332(void* data, nuint len, uint seed);

    [LibraryImport(DllName, EntryPoint = "SDL_memcpy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _MemCpy(void* dst, void* src, nuint len);

    [LibraryImport(DllName, EntryPoint = "SDL_memmove")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _MemMove(void* dst, void* src, nuint len);

    [LibraryImport(DllName, EntryPoint = "SDL_memset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _MemSet(void* dst, int c, nuint len);

    [LibraryImport(DllName, EntryPoint = "SDL_memset4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _MemSet4(void* dst, uint val, nuint dwords);

    [LibraryImport(DllName, EntryPoint = "SDL_memcmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _MemCmp(void* s1, void* s2, nuint len);

    [LibraryImport(DllName, EntryPoint = "SDL_wcslen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _WcsLen(char* str);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsnlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _WcsNLen(char* str, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcslcpy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _WcsLCpy(char* dst, char* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcslcat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _WcsLCat(char* dst, char* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsdup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial char* _WcsDup(char* str);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsstr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial char* _WcsStr(char* haystack, char* needle);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsnstr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial char* _WcsNStr(char* haystack, char* needle, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcscmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _WcsCmp(char* str1, char* str2);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsncmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _WcsNCmp(char* str1, char* str2, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcscasecmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _WcsCaseCmp(char* str1, char* str2);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsncasecmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _WcsNCaseCmp(char* str1, char* str2, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcstol")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial long _WcsToL(char* str, char** endptr, int @base);

    [LibraryImport(DllName, EntryPoint = "SDL_strlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _StrLen(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strnlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _StrNLen(byte* str, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strlcpy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _StrLCpy(byte* dst, byte* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_utf8strlcpy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _Utf8StrLCpy(byte* dst, byte* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strlcat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _StrLCat(byte* dst, byte* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strdup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrDup(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strndup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrNDup(byte* str, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strrev")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrRev(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strupr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrUpr(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strlwr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrLwr(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strchr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrChr(byte* str, int c);

    [LibraryImport(DllName, EntryPoint = "SDL_strrchr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrRChr(byte* str, int c);

    [LibraryImport(DllName, EntryPoint = "SDL_strstr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrStr(byte* haystack, byte* needle);

    [LibraryImport(DllName, EntryPoint = "SDL_strnstr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrNStr(byte* haystack, byte* needle, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strcasestr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrCaseStr(byte* haystack, byte* needle);

    [LibraryImport(DllName, EntryPoint = "SDL_strtok_r")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrTokR(byte* str, byte* delim, byte** saveptr);

    [LibraryImport(DllName, EntryPoint = "SDL_utf8strlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _Utf8StrLen(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_utf8strnlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _Utf8StrNLen(byte* str, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_itoa")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _IToA(int value, byte* str, int radix);

    [LibraryImport(DllName, EntryPoint = "SDL_uitoa")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _UIToA(uint value, byte* str, int radix);

    [LibraryImport(DllName, EntryPoint = "SDL_ltoa")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _LToA(long value, byte* str, int radix);

    [LibraryImport(DllName, EntryPoint = "SDL_ultoa")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _ULToA(ulong value, byte* str, int radix);

    [LibraryImport(DllName, EntryPoint = "SDL_atoi")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _AToI(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_atof")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial double _AToF(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strtol")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial long _StrToL(byte* str, byte** endptr, int @base);

    [LibraryImport(DllName, EntryPoint = "SDL_strtoul")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial ulong _StrToUL(byte* str, byte** endptr, int @base);

    [LibraryImport(DllName, EntryPoint = "SDL_strtod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial double _StrToD(byte* str, byte** endptr);

    [LibraryImport(DllName, EntryPoint = "SDL_strcmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _StrCmp(byte* str1, byte* str2);

    [LibraryImport(DllName, EntryPoint = "SDL_strncmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _StrNCmp(byte* str1, byte* str2, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strcasecmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _StrCaseCmp(byte* str1, byte* str2);

    [LibraryImport(DllName, EntryPoint = "SDL_strncasecmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _StrNCaseCmp(byte* str1, byte* str2, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strpbrk")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _StrPBrk(byte* str, byte* breakset);

    [LibraryImport(DllName, EntryPoint = "SDL_StepUTF8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial uint _StepUtf8(byte** str, out nuint pslen);

    [LibraryImport(DllName, EntryPoint = "SDL_StepBackUTF8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial uint _StepBackUtf8(byte** str, out nuint pslen);

    [LibraryImport(DllName, EntryPoint = "SDL_UCS4ToUTF8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte* _Ucs4ToUtf8(uint codepoint, byte* dst);

    [LibraryImport(DllName, EntryPoint = "SDL_srand")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SRand(ulong seed);

    [LibraryImport(DllName, EntryPoint = "SDL_rand")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _Rand(int n);

    [LibraryImport(DllName, EntryPoint = "SDL_randf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _RandF();

    [LibraryImport(DllName, EntryPoint = "SDL_rand_bits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _RandBits();

    [LibraryImport(DllName, EntryPoint = "SDL_rand_r")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _RandR(ref ulong state, int n);

    [LibraryImport(DllName, EntryPoint = "SDL_randf_r")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _RandFR(ref ulong state);

    [LibraryImport(DllName, EntryPoint = "SDL_rand_bits_r")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _RandBits(ref ulong state);

    [LibraryImport(DllName, EntryPoint = "SDL_acos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Acos(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_acosf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _AcosF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_asin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Asin(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_asinf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _AsinF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_atan")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Atan(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_atanf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _AtanF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_atan2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Atan2(double y, double x);

    [LibraryImport(DllName, EntryPoint = "SDL_atan2f")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _Atan2F(float y, float x);

    [LibraryImport(DllName, EntryPoint = "SDL_ceil")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Ceil(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_ceilf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _CeilF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_copysign")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _CopySign(double x, double y);

    [LibraryImport(DllName, EntryPoint = "SDL_copysignf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _CopySignF(float x, float y);

    [LibraryImport(DllName, EntryPoint = "SDL_cos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Cos(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_cosf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _CosF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_exp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Exp(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_expf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _ExpF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_fabs")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _FAbs(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_fabsf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _FAbsF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_floor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Floor(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_floorf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _FloorF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_trunc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Trunc(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_truncf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _TruncF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_fmod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _FMod(double x, double y);

    [LibraryImport(DllName, EntryPoint = "SDL_fmodf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _FModF(float x, float y);

    [LibraryImport(DllName, EntryPoint = "SDL_isinf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsInf(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_isinff")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsInfF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_isnan")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsNaN(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_isnanf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsNaNF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_log")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Log(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_logf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _LogF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_log10")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Log10(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_log10f")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _Log10F(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_modf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _ModF(double x, out double y);

    [LibraryImport(DllName, EntryPoint = "SDL_modff")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _ModFF(float x, out float y);

    [LibraryImport(DllName, EntryPoint = "SDL_pow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Pow(double x, double y);

    [LibraryImport(DllName, EntryPoint = "SDL_powf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _PowF(float x, float y);

    [LibraryImport(DllName, EntryPoint = "SDL_round")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Round(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_roundf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _RoundF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_lround")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long _LRound(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_lroundf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long _LRoundF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_scalbn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _ScalBN(double x, int n);

    [LibraryImport(DllName, EntryPoint = "SDL_scalbnf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _ScalBnf(float x, int n);

    [LibraryImport(DllName, EntryPoint = "SDL_sin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Sin(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_sinf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _SinF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_sqrt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Sqrt(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_sqrtf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _SqrtF(float x);

    [LibraryImport(DllName, EntryPoint = "SDL_tan")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _Tan(double x);

    [LibraryImport(DllName, EntryPoint = "SDL_tanf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _TanF(float x);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_iconv_open",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IconVData _IconVOpen(string toCode, string fromCode);

    [LibraryImport(DllName, EntryPoint = "SDL_iconv_close")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IconVClose(IconVData cd);

    [LibraryImport(DllName, EntryPoint = "SDL_iconv")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _IconV(
        IconVData cd,
        byte** inbuf,
        out nuint inBytesLeft,
        byte** outbuf,
        out nuint outBytesLeft
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_iconv_string",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string? _IconVString(
        string toCode,
        string fromCode,
        string inbuf,
        nuint inBytesLeft
    );

    public static uint FourCC(char a, char b, char c, char d)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (byte)a | ((uint)(byte)b << 8) | ((uint)(byte)c << 16) | ((uint)(byte)d << 24);
    }

    public static unsafe void InitInterface(ref IOStreamInterface iface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (IOStreamInterface* pIface = &iface)
        {
            ZeroP<IOStreamInterface>(pIface);
            pIface->Version = (uint)sizeof(IOStreamInterface);
        }
    }

    public static unsafe void InitInterface(ref StorageInterface iface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (StorageInterface* pIface = &iface)
        {
            ZeroP<StorageInterface>(pIface);
            pIface->Version = (uint)sizeof(StorageInterface);
        }
    }

    public static unsafe void* MAlloc(nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MAlloc(size);
    }

    public static unsafe void* CAlloc(nuint nmemb, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CAlloc(nmemb, size);
    }

    public static unsafe void* Realloc(void* mem, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Realloc(mem, size);
    }

    public static unsafe void Free(void* mem)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _Free(mem);
    }

    public static unsafe void GetOriginalMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GetOriginalMemoryFunctions(mallocFunc, callocFunc, reallocFunc, freeFunc);
    }

    public static unsafe void GetMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GetMemoryFunctions(mallocFunc, callocFunc, reallocFunc, freeFunc);
    }

    public static unsafe bool SetMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(mallocFunc);
        ArgumentNullException.ThrowIfNull(callocFunc);
        ArgumentNullException.ThrowIfNull(reallocFunc);
        ArgumentNullException.ThrowIfNull(freeFunc);
        return _SetMemoryFunctions(mallocFunc, callocFunc, reallocFunc, freeFunc);
    }

    public static unsafe void* AlignedAlloc(nuint alignment, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AlignedAlloc(alignment, size);
    }

    public static unsafe void AlignedFree(void* mem)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _AlignedFree(mem);
    }

    public static int GetNumAllocations()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumAllocations();
    }

    public static Environment GetEnvironment()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetEnvironment();
    }

    public static Environment CreateEnvironment(bool populated)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateEnvironment(populated);
    }

    public static string? GetEnvironmentVariable(Environment env, string name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _GetEnvironmentVariable(env, name);
    }

    public static unsafe byte** GetEnvironmentVariables(Environment env)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetEnvironmentVariables(env);
    }

    public static bool SetEnvironmentVariable(
        Environment env,
        string name,
        string? value,
        bool overwrite
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _SetEnvironmentVariable(env, name, value, overwrite);
    }

    public static bool UnsetEnvironmentVariable(Environment env, string name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _UnsetEnvironmentVariable(env, name);
    }

    public static void DestroyEnvironment(Environment env)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyEnvironment(env);
    }

    public static string? GetEnv(string name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _GetEnv(name);
    }

    public static string? GetEnvUnsafe(string name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _GetEnvUnsafe(name);
    }

    public static bool SetEnvUnsafe(Environment env, string name, string? value, bool overwrite)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _SetEnvUnsafe(env, name, value, overwrite);
    }

    public static bool UnsetEnvUnsafe(Environment env, string name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _UnsetEnvUnsafe(env, name);
    }

    public static unsafe void QSort(
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, int> compare
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(@base);
        ArgumentNullException.ThrowIfNull(compare);
        _QSort(@base, nmemb, size, compare);
    }

    public static unsafe void* BSearch(
        void* key,
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, int> compare
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(@base);
        ArgumentNullException.ThrowIfNull(compare);
        return _BSearch(key, @base, nmemb, size, compare);
    }

    public static unsafe void QSortR(
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, void*, int> compare,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(@base);
        ArgumentNullException.ThrowIfNull(compare);
        _QSortR(@base, nmemb, size, compare, userdata);
    }

    public static unsafe void* BSearchR(
        void* key,
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, void*, int> compare,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(@base);
        ArgumentNullException.ThrowIfNull(compare);
        return _BSearchR(key, @base, nmemb, size, compare, userdata);
    }

    public static int Abs(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Abs(x);
    }

    public static int Min(int x, int y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return x < y ? x : y;
    }

    public static int Max(int x, int y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return x > y ? x : y;
    }

    public static int Clamp(int x, int a, int b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return x < a ? a : (x > b ? b : x);
    }

    public static bool IsAlpha(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsAlpha(x);
    }

    public static bool IsAlnum(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsAlnum(x);
    }

    public static bool IsBlank(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsBlank(x);
    }

    public static bool IsCntrl(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsCntrl(x);
    }

    public static bool IsDigit(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsDigit(x);
    }

    public static bool IsXDigit(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsXDigit(x);
    }

    public static bool IsPunct(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsPunct(x);
    }

    public static bool IsSpace(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsSpace(x);
    }

    public static bool IsUpper(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsUpper(x);
    }

    public static bool IsLower(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsLower(x);
    }

    public static bool IsPrint(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsPrint(x);
    }

    public static bool IsGraph(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsGraph(x);
    }

    public static int ToUpper(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ToUpper(x);
    }

    public static int ToLower(int x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ToLower(x);
    }

    public static unsafe ushort Crc16(ushort crc, byte* data, nuint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(data);
        return _Crc16(crc, data, length);
    }

    public static unsafe uint Crc32(uint crc, byte* data, nuint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(data);
        return _Crc32(crc, data, length);
    }

    public static unsafe uint Murmur332(void* data, nuint len, uint seed)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(data);
        return _Murmur332(data, len, seed);
    }

    public static unsafe void* MemCpy(void* dst, void* src, nuint len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        ArgumentNullException.ThrowIfNull(src);
        return _MemCpy(dst, src, len);
    }

    public static unsafe void* MemMove(void* dst, void* src, nuint len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        ArgumentNullException.ThrowIfNull(src);
        return _MemMove(dst, src, len);
    }

    public static unsafe void* MemSet(void* dst, int c, nuint len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        return _MemSet(dst, c, len);
    }

    public static unsafe void* MemSet4(void* dst, uint val, nuint dwords)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        return _MemSet4(dst, val, dwords);
    }

    public static unsafe void* Zero<T>(T x)
        where T : unmanaged
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return MemSet(&x, 0, (nuint)sizeof(T));
    }

    public static unsafe void* ZeroP<T>(void* x)
        where T : unmanaged
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(x);
        return MemSet(x, 0, (nuint)sizeof(T));
    }

    public static unsafe void* ZeroA<T>(void* x)
        where T : unmanaged
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(x);
        return MemSet(x, 0, (nuint)sizeof(T));
    }

    public static unsafe int MemCmp(void* s1, void* s2, nuint len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(s1);
        ArgumentNullException.ThrowIfNull(s2);
        return _MemCmp(s1, s2, len);
    }

    public static unsafe nuint WcsLen(char* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _WcsLen(str);
    }

    public static unsafe nuint WcsNLen(char* str, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _WcsNLen(str, maxlen);
    }

    public static unsafe nuint WcsLCpy(char* dst, char* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        ArgumentNullException.ThrowIfNull(src);
        return _WcsLCpy(dst, src, maxlen);
    }

    public static unsafe nuint WcsLCat(char* dst, char* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        ArgumentNullException.ThrowIfNull(src);
        return _WcsLCat(dst, src, maxlen);
    }

    public static unsafe char* WcsDup(char* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _WcsDup(str);
    }

    public static unsafe char* WcsStr(char* haystack, char* needle)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(haystack);
        ArgumentNullException.ThrowIfNull(needle);
        return _WcsStr(haystack, needle);
    }

    public static unsafe char* WcsNStr(char* haystack, char* needle, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(haystack);
        ArgumentNullException.ThrowIfNull(needle);
        return _WcsNStr(haystack, needle, maxlen);
    }

    public static unsafe int WcsCmp(char* str1, char* str2)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);
        return _WcsCmp(str1, str2);
    }

    public static unsafe int WcsNCmp(char* str1, char* str2, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);
        return _WcsNCmp(str1, str2, maxlen);
    }

    public static unsafe int WcsCaseCmp(char* str1, char* str2)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);
        return _WcsCaseCmp(str1, str2);
    }

    public static unsafe int WcsNCaseCmp(char* str1, char* str2, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);
        return _WcsNCaseCmp(str1, str2, maxlen);
    }

    public static unsafe long WcsToL(char* str, char** endptr, int @base)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _WcsToL(str, endptr, @base);
    }

    public static unsafe nuint StrLen(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrLen(str);
    }

    public static unsafe nuint StrNLen(byte* str, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrNLen(str, maxlen);
    }

    public static unsafe nuint StrLCpy(byte* dst, byte* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        ArgumentNullException.ThrowIfNull(src);
        return _StrLCpy(dst, src, maxlen);
    }

    public static unsafe nuint Utf8StrLCpy(byte* dst, byte* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        ArgumentNullException.ThrowIfNull(src);
        return _Utf8StrLCpy(dst, src, maxlen);
    }

    public static unsafe nuint StrLCat(byte* dst, byte* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        ArgumentNullException.ThrowIfNull(src);
        return _StrLCat(dst, src, maxlen);
    }

    public static unsafe byte* StrDup(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrDup(str);
    }

    public static unsafe byte* StrNDup(byte* str, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrNDup(str, maxlen);
    }

    public static unsafe byte* StrRev(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrRev(str);
    }

    public static unsafe byte* StrUpr(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrUpr(str);
    }

    public static unsafe byte* StrLwr(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrLwr(str);
    }

    public static unsafe byte* StrChr(byte* str, int c)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrChr(str, c);
    }

    public static unsafe byte* StrRChr(byte* str, int c)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrRChr(str, c);
    }

    public static unsafe byte* StrStr(byte* haystack, byte* needle)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(haystack);
        ArgumentNullException.ThrowIfNull(needle);
        return _StrStr(haystack, needle);
    }

    public static unsafe byte* StrNStr(byte* haystack, byte* needle, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(haystack);
        ArgumentNullException.ThrowIfNull(needle);
        return _StrNStr(haystack, needle, maxlen);
    }

    public static unsafe byte* StrCaseStr(byte* haystack, byte* needle)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(haystack);
        ArgumentNullException.ThrowIfNull(needle);
        return _StrCaseStr(haystack, needle);
    }

    public static unsafe byte* StrTokR(byte* str, byte* delim, byte** saveptr)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(delim);
        ArgumentNullException.ThrowIfNull(saveptr);
        return _StrTokR(str, delim, saveptr);
    }

    public static unsafe int Utf8StrLen(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _Utf8StrLen(str);
    }

    public static unsafe int Utf8StrNLen(byte* str, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _Utf8StrNLen(str, maxlen);
    }

    public static unsafe byte* IToA(int value, byte* str, int radix)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _IToA(value, str, radix);
    }

    public static unsafe byte* UIToA(uint value, byte* str, int radix)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _UIToA(value, str, radix);
    }

    public static unsafe byte* LToA(long value, byte* str, int radix)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _LToA(value, str, radix);
    }

    public static unsafe byte* ULToA(ulong value, byte* str, int radix)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _ULToA(value, str, radix);
    }

    public static unsafe int AToI(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _AToI(str);
    }

    public static unsafe double AToF(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _AToF(str);
    }

    public static unsafe long StrToL(byte* str, byte** endptr, int @base)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrToL(str, endptr, @base);
    }

    public static unsafe ulong StrToUL(byte* str, byte** endptr, int @base)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrToUL(str, endptr, @base);
    }

    public static unsafe double StrToD(byte* str, byte** endptr)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StrToD(str, endptr);
    }

    public static unsafe int StrCmp(byte* str1, byte* str2)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);
        return _StrCmp(str1, str2);
    }

    public static unsafe int StrNCmp(byte* str1, byte* str2, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);
        return _StrNCmp(str1, str2, maxlen);
    }

    public static unsafe int StrCaseCmp(byte* str1, byte* str2)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);
        return _StrCaseCmp(str1, str2);
    }

    public static unsafe int StrNCaseCmp(byte* str1, byte* str2, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str1);
        ArgumentNullException.ThrowIfNull(str2);
        return _StrNCaseCmp(str1, str2, maxlen);
    }

    public static unsafe byte* StrPBrk(byte* str, byte* breakset)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        ArgumentNullException.ThrowIfNull(breakset);
        return _StrPBrk(str, breakset);
    }

    public static unsafe uint StepUtf8(byte** str, out nuint pslen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StepUtf8(str, out pslen);
    }

    public static unsafe uint StepBackUtf8(byte** str, out nuint pslen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(str);
        return _StepBackUtf8(str, out pslen);
    }

    public static unsafe byte* Ucs4ToUtf8(uint codepoint, byte* dst)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(dst);
        return _Ucs4ToUtf8(codepoint, dst);
    }

    public static void SRand(ulong seed)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SRand(seed);
    }

    public static int Rand(int n)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentOutOfRangeException.ThrowIfNegative(n);
        return _Rand(n);
    }

    public static float RandF()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RandF();
    }

    public static uint RandBits()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RandBits();
    }

    public static int RandR(ref ulong state, int n)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentOutOfRangeException.ThrowIfNegative(n);
        return _RandR(ref state, n);
    }

    public static float RandFR(ref ulong state)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RandFR(ref state);
    }

    public static uint RandBitsR(ref ulong state)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RandBits(ref state);
    }

    public static double Acos(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Acos(x);
    }

    public static float AcosF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AcosF(x);
    }

    public static double Asin(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Asin(x);
    }

    public static float AsinF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AsinF(x);
    }

    public static double Atan(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Atan(x);
    }

    public static float AtanF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AtanF(x);
    }

    public static double Atan2(double y, double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Atan2(y, x);
    }

    public static float Atan2F(float y, float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Atan2F(y, x);
    }

    public static double Ceil(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Ceil(x);
    }

    public static float CeilF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CeilF(x);
    }

    public static double CopySign(double x, double y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CopySign(x, y);
    }

    public static float CopySignF(float x, float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CopySignF(x, y);
    }

    public static double Cos(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Cos(x);
    }

    public static float CosF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CosF(x);
    }

    public static double Exp(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Exp(x);
    }

    public static float ExpF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ExpF(x);
    }

    public static double FAbs(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FAbs(x);
    }

    public static float FAbsF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FAbsF(x);
    }

    public static double Floor(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Floor(x);
    }

    public static float FloorF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FloorF(x);
    }

    public static double Trunc(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Trunc(x);
    }

    public static float TruncF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TruncF(x);
    }

    public static double FMod(double x, double y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FMod(x, y);
    }

    public static float FModF(float x, float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FModF(x, y);
    }

    public static bool IsInf(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsInf(x);
    }

    public static bool IsInfF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsInfF(x);
    }

    public static bool IsNaN(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsNaN(x);
    }

    public static bool IsNaNF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsNaNF(x);
    }

    public static double Log(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Log(x);
    }

    public static float LogF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LogF(x);
    }

    public static double Log10(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Log10(x);
    }

    public static float Log10F(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Log10F(x);
    }

    public static double ModF(double x, out double y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ModF(x, out y);
    }

    public static float ModFF(float x, out float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ModFF(x, out y);
    }

    public static double Pow(double x, double y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Pow(x, y);
    }

    public static float PowF(float x, float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PowF(x, y);
    }

    public static double Round(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Round(x);
    }

    public static float RoundF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RoundF(x);
    }

    public static long LRound(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LRound(x);
    }

    public static long LRoundF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LRoundF(x);
    }

    public static double ScalBN(double x, int n)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ScalBN(x, n);
    }

    public static float ScalBnf(float x, int n)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ScalBnf(x, n);
    }

    public static double Sin(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Sin(x);
    }

    public static float SinF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SinF(x);
    }

    public static double Sqrt(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Sqrt(x);
    }

    public static float SqrtF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SqrtF(x);
    }

    public static double Tan(double x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Tan(x);
    }

    public static float TanF(float x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TanF(x);
    }

    public static IconVData IconVOpen(string toCode, string fromCode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(toCode);
        ArgumentNullException.ThrowIfNull(fromCode);
        return _IconVOpen(toCode, fromCode);
    }

    public static bool IconVClose(IconVData cd)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IconVClose(cd);
    }

    public static unsafe nuint IconV(
        IconVData cd,
        byte** inbuf,
        out nuint inBytesLeft,
        byte** outbuf,
        out nuint outBytesLeft
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(inbuf);
        ArgumentNullException.ThrowIfNull(outbuf);
        return _IconV(cd, inbuf, out inBytesLeft, outbuf, out outBytesLeft);
    }

    public static string? IconVString(
        string toCode,
        string fromCode,
        string inbuf,
        nuint inBytesLeft
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(toCode);
        ArgumentNullException.ThrowIfNull(fromCode);
        ArgumentNullException.ThrowIfNull(inbuf);
        return _IconVString(toCode, fromCode, inbuf, inBytesLeft);
    }

    public static string? IconVUtf8Locale(string s)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(s);
        return IconVString("", "UTF-8", s, (nuint)(s.Length + 1));
    }

    public static string? IconVUtf8Ucs2(string s)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(s);
        return IconVString("UCS-2", "UTF-8", s, (nuint)(s.Length + 1));
    }

    public static string? IconVUtf8Ucs4(string s)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(s);
        return IconVString("UCS-4", "UTF-8", s, (nuint)(s.Length + 1));
    }

    public static string? IconVWcharUtf8(string s)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(s);
        return IconVString("UTF-8", "WCHAR_T", s, (nuint)((s.Length + 1) * sizeof(char)));
    }

    public readonly record struct Time(long Value)
    {
        public static implicit operator long(Time time) => time.Value;

        public static implicit operator Time(long value) => new(value);
    }

    public readonly record struct Environment(nint Value)
    {
        public bool IsNull => Value == 0;

        public static implicit operator nint(Environment env) => env.Value;

        public static implicit operator Environment(nint value) => new(value);
    }

    public readonly record struct IconVData(nint Value)
    {
        public bool IsNull => Value == 0;

        public static implicit operator nint(IconVData env) => env.Value;

        public static implicit operator IconVData(nint value) => new(value);
    }
}
