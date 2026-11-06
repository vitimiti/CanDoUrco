// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
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
    private static partial void* _MAlloc(nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_calloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _CAlloc(nuint nmemb, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_realloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _Realloc(void* mem, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _Free(void* mem);

    [LibraryImport(DllName, EntryPoint = "SDL_GetOriginalMemoryFunctions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GetOriginalMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetMemoryFunctions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GetMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetMemoryFunctions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    );

    [LibraryImport(DllName, EntryPoint = "SDL_aligned_alloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _AlignedAlloc(nuint alignment, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_aligned_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _AlignedFree(void* mem);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumAllocations")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumAllocations();

    [LibraryImport(DllName, EntryPoint = "SDL_GetEnvironment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Environment _GetEnvironment();

    [LibraryImport(DllName, EntryPoint = "SDL_CreateEnvironment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Environment _CreateEnvironment(
        [MarshalAs(UnmanagedType.I1)] bool populated
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetEnvironmentVariable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetEnvironmentVariable(Environment env, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_GetEnvironmentVariables")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte** _GetEnvironmentVariables(Environment env);

    [LibraryImport(DllName, EntryPoint = "SDL_SetEnvironmentVariable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetEnvironmentVariable(
        Environment env,
        byte* name,
        byte* value,
        [MarshalAs(UnmanagedType.I1)] bool overwrite
    );

    [LibraryImport(DllName, EntryPoint = "SDL_UnsetEnvironmentVariable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _UnsetEnvironmentVariable(Environment env, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyEnvironment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyEnvironment(Environment env);

    [LibraryImport(DllName, EntryPoint = "SDL_getenv")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetEnv(byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_getenv_unsafe")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetEnvUnsafe(byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_setenv_unsafe")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetEnvUnsafe(
        Environment env,
        byte* name,
        byte* value,
        [MarshalAs(UnmanagedType.I1)] bool overwrite
    );

    [LibraryImport(DllName, EntryPoint = "SDL_unsetenv_unsafe")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _UnsetEnvUnsafe(Environment env, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_qsort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _QSort(
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, int> compare
    );

    [LibraryImport(DllName, EntryPoint = "SDL_bsearch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _BSearch(
        void* key,
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, int> compare
    );

    [LibraryImport(DllName, EntryPoint = "SDL_qsort_r")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _QSortR(
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, void*, int> compare,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_bsearch_r")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _BSearchR(
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
    private static partial ushort _Crc16(ushort crc, byte* data, nuint length);

    [LibraryImport(DllName, EntryPoint = "SDL_crc32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _Crc32(uint crc, byte* data, nuint length);

    [LibraryImport(DllName, EntryPoint = "SDL_murmur3_32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _Murmur332(void* data, nuint len, uint seed);

    [LibraryImport(DllName, EntryPoint = "SDL_memcpy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _MemCpy(void* dst, void* src, nuint len);

    [LibraryImport(DllName, EntryPoint = "SDL_memmove")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _MemMove(void* dst, void* src, nuint len);

    [LibraryImport(DllName, EntryPoint = "SDL_memset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _MemSet(void* dst, int c, nuint len);

    [LibraryImport(DllName, EntryPoint = "SDL_memset4")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _MemSet4(void* dst, uint val, nuint dwords);

    [LibraryImport(DllName, EntryPoint = "SDL_memcmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _MemCmp(void* s1, void* s2, nuint len);

    [LibraryImport(DllName, EntryPoint = "SDL_wcslen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _WcsLen(char* str);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsnlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _WcsNLen(char* str, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcslcpy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _WcsLCpy(char* dst, char* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcslcat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _WcsLCat(char* dst, char* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsdup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial char* _WcsDup(char* str);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsstr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial char* _WcsStr(char* haystack, char* needle);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsnstr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial char* _WcsNStr(char* haystack, char* needle, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcscmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _WcsCmp(char* str1, char* str2);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsncmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _WcsNCmp(char* str1, char* str2, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcscasecmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _WcsCaseCmp(char* str1, char* str2);

    [LibraryImport(DllName, EntryPoint = "SDL_wcsncasecmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _WcsNCaseCmp(char* str1, char* str2, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_wcstol")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long _WcsToL(char* str, char** endptr, int @base);

    [LibraryImport(DllName, EntryPoint = "SDL_strlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _StrLen(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strnlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _StrNLen(byte* str, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strlcpy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _StrLCpy(byte* dst, byte* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_utf8strlcpy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _Utf8StrLCpy(byte* dst, byte* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strlcat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _StrLCat(byte* dst, byte* src, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strdup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrDup(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strndup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrNDup(byte* str, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strrev")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrRev(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strupr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrUpr(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strlwr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrLwr(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strchr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrChr(byte* str, int c);

    [LibraryImport(DllName, EntryPoint = "SDL_strrchr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrRChr(byte* str, int c);

    [LibraryImport(DllName, EntryPoint = "SDL_strstr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrStr(byte* haystack, byte* needle);

    [LibraryImport(DllName, EntryPoint = "SDL_strnstr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrNStr(byte* haystack, byte* needle, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strcasestr")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrCaseStr(byte* haystack, byte* needle);

    [LibraryImport(DllName, EntryPoint = "SDL_strtok_r")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrTokR(byte* str, byte* delim, byte** saveptr);

    [LibraryImport(DllName, EntryPoint = "SDL_utf8strlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _Utf8StrLen(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_utf8strnlen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _Utf8StrNLen(byte* str, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_itoa")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _IToA(int value, byte* str, int radix);

    [LibraryImport(DllName, EntryPoint = "SDL_uitoa")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _UIToA(uint value, byte* str, int radix);

    [LibraryImport(DllName, EntryPoint = "SDL_ltoa")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _LToA(long value, byte* str, int radix);

    [LibraryImport(DllName, EntryPoint = "SDL_ultoa")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _ULToA(ulong value, byte* str, int radix);

    [LibraryImport(DllName, EntryPoint = "SDL_atoi")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _AToI(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_atof")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _AToF(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_strtol")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long _StrToL(byte* str, byte** endptr, int @base);

    [LibraryImport(DllName, EntryPoint = "SDL_strtoul")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong _StrToUL(byte* str, byte** endptr, int @base);

    [LibraryImport(DllName, EntryPoint = "SDL_strtod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial double _StrToD(byte* str, byte** endptr);

    [LibraryImport(DllName, EntryPoint = "SDL_strcmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _StrCmp(byte* str1, byte* str2);

    [LibraryImport(DllName, EntryPoint = "SDL_strncmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _StrNCmp(byte* str1, byte* str2, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strcasecmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _StrCaseCmp(byte* str1, byte* str2);

    [LibraryImport(DllName, EntryPoint = "SDL_strncasecmp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _StrNCaseCmp(byte* str1, byte* str2, nuint maxlen);

    [LibraryImport(DllName, EntryPoint = "SDL_strpbrk")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _StrPBrk(byte* str, byte* breakset);

    [LibraryImport(DllName, EntryPoint = "SDL_StepUTF8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _StepUtf8(byte** str, out nuint pslen);

    [LibraryImport(DllName, EntryPoint = "SDL_StepBackUTF8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _StepBackUtf8(byte** str, out nuint pslen);

    [LibraryImport(DllName, EntryPoint = "SDL_UCS4ToUTF8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _Ucs4ToUtf8(uint codepoint, byte* dst);

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

    [LibraryImport(DllName, EntryPoint = "SDL_iconv_open")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IconVData _IconVOpen(byte* toCode, byte* fromCode);

    [LibraryImport(DllName, EntryPoint = "SDL_iconv_close")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IconVClose(IconVData cd);

    [LibraryImport(DllName, EntryPoint = "SDL_iconv")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _IconV(
        IconVData cd,
        byte** inbuf,
        out nuint inBytesLeft,
        byte** outbuf,
        out nuint outBytesLeft
    );

    [LibraryImport(DllName, EntryPoint = "SDL_iconv_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _IconVString(
        byte* toCode,
        byte* fromCode,
        byte* inbuf,
        nuint inBytesLeft
    );

    public static uint FourCC(char a, char b, char c, char d)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (byte)a | ((uint)(byte)b << 8) | ((uint)(byte)c << 16) | ((uint)(byte)d << 24);
    }

    public static void InitInterface(ref IOStreamInterface iface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (IOStreamInterface* pIface = &iface)
        {
            ZeroP<IOStreamInterface>(pIface);
            pIface->Version = (uint)sizeof(IOStreamInterface);
        }
    }

    public static void InitInterface(ref StorageInterface iface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (StorageInterface* pIface = &iface)
        {
            ZeroP<StorageInterface>(pIface);
            pIface->Version = (uint)sizeof(StorageInterface);
        }
    }

    public static void* MAlloc(nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MAlloc(size);
    }

    public static void* CAlloc(nuint nmemb, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CAlloc(nmemb, size);
    }

    public static void* Realloc(void* mem, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Realloc(mem, size);
    }

    public static void Free(void* mem)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _Free(mem);
    }

    public static void GetOriginalMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GetOriginalMemoryFunctions(mallocFunc, callocFunc, reallocFunc, freeFunc);
    }

    public static void GetMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GetMemoryFunctions(mallocFunc, callocFunc, reallocFunc, freeFunc);
    }

    public static bool SetMemoryFunctions(
        delegate* unmanaged[Cdecl]<nuint, void*> mallocFunc,
        delegate* unmanaged[Cdecl]<nuint, nuint, void*> callocFunc,
        delegate* unmanaged[Cdecl]<void*, nuint, void*> reallocFunc,
        delegate* unmanaged[Cdecl]<void*, void> freeFunc
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetMemoryFunctions(mallocFunc, callocFunc, reallocFunc, freeFunc);
    }

    public static void* AlignedAlloc(nuint alignment, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AlignedAlloc(alignment, size);
    }

    public static void AlignedFree(void* mem)
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

    public static byte* GetEnvironmentVariable(Environment env, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetEnvironmentVariable(env, name);
    }

    public static byte** GetEnvironmentVariables(Environment env)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetEnvironmentVariables(env);
    }

    public static bool SetEnvironmentVariable(
        Environment env,
        byte* name,
        byte* value,
        bool overwrite
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetEnvironmentVariable(env, name, value, overwrite);
    }

    public static bool UnsetEnvironmentVariable(Environment env, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UnsetEnvironmentVariable(env, name);
    }

    public static void DestroyEnvironment(Environment env)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyEnvironment(env);
    }

    public static byte* GetEnv(byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetEnv(name);
    }

    public static byte* GetEnvUnsafe(byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetEnvUnsafe(name);
    }

    public static bool SetEnvUnsafe(Environment env, byte* name, byte* value, bool overwrite)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetEnvUnsafe(env, name, value, overwrite);
    }

    public static bool UnsetEnvUnsafe(Environment env, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UnsetEnvUnsafe(env, name);
    }

    public static void QSort(
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, int> compare
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _QSort(@base, nmemb, size, compare);
    }

    public static void* BSearch(
        void* key,
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, int> compare
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BSearch(key, @base, nmemb, size, compare);
    }

    public static void QSortR(
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, void*, int> compare,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _QSortR(@base, nmemb, size, compare, userdata);
    }

    public static void* BSearchR(
        void* key,
        void* @base,
        nuint nmemb,
        nuint size,
        delegate* unmanaged[Cdecl]<void*, void*, void*, int> compare,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
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

    public static ushort Crc16(ushort crc, byte* data, nuint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Crc16(crc, data, length);
    }

    public static uint Crc32(uint crc, byte* data, nuint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Crc32(crc, data, length);
    }

    public static uint Murmur332(void* data, nuint len, uint seed)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Murmur332(data, len, seed);
    }

    public static void* MemCpy(void* dst, void* src, nuint len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MemCpy(dst, src, len);
    }

    public static void* MemMove(void* dst, void* src, nuint len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MemMove(dst, src, len);
    }

    public static void* MemSet(void* dst, int c, nuint len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MemSet(dst, c, len);
    }

    public static void* MemSet4(void* dst, uint val, nuint dwords)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MemSet4(dst, val, dwords);
    }

    public static void* Zero<T>(T x)
        where T : unmanaged
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return MemSet(&x, 0, (nuint)sizeof(T));
    }

    public static void* ZeroP<T>(void* x)
        where T : unmanaged
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return MemSet(x, 0, (nuint)sizeof(T));
    }

    public static void* ZeroA<T>(void* x)
        where T : unmanaged
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return MemSet(x, 0, (nuint)sizeof(T));
    }

    public static int MemCmp(void* s1, void* s2, nuint len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MemCmp(s1, s2, len);
    }

    public static nuint WcsLen(char* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsLen(str);
    }

    public static nuint WcsNLen(char* str, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsNLen(str, maxlen);
    }

    public static nuint WcsLCpy(char* dst, char* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsLCpy(dst, src, maxlen);
    }

    public static nuint WcsLCat(char* dst, char* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsLCat(dst, src, maxlen);
    }

    public static char* WcsDup(char* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsDup(str);
    }

    public static char* WcsStr(char* haystack, char* needle)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsStr(haystack, needle);
    }

    public static char* WcsNStr(char* haystack, char* needle, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsNStr(haystack, needle, maxlen);
    }

    public static int WcsCmp(char* str1, char* str2)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsCmp(str1, str2);
    }

    public static int WcsNCmp(char* str1, char* str2, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsNCmp(str1, str2, maxlen);
    }

    public static int WcsCaseCmp(char* str1, char* str2)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsCaseCmp(str1, str2);
    }

    public static int WcsNCaseCmp(char* str1, char* str2, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsNCaseCmp(str1, str2, maxlen);
    }

    public static long WcsToL(char* str, char** endptr, int @base)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WcsToL(str, endptr, @base);
    }

    public static nuint StrLen(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrLen(str);
    }

    public static nuint StrNLen(byte* str, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrNLen(str, maxlen);
    }

    public static nuint StrLCpy(byte* dst, byte* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrLCpy(dst, src, maxlen);
    }

    public static nuint Utf8StrLCpy(byte* dst, byte* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Utf8StrLCpy(dst, src, maxlen);
    }

    public static nuint StrLCat(byte* dst, byte* src, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrLCat(dst, src, maxlen);
    }

    public static byte* StrDup(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrDup(str);
    }

    public static byte* StrNDup(byte* str, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrNDup(str, maxlen);
    }

    public static byte* StrRev(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrRev(str);
    }

    public static byte* StrUpr(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrUpr(str);
    }

    public static byte* StrLwr(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrLwr(str);
    }

    public static byte* StrChr(byte* str, int c)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrChr(str, c);
    }

    public static byte* StrRChr(byte* str, int c)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrRChr(str, c);
    }

    public static byte* StrStr(byte* haystack, byte* needle)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrStr(haystack, needle);
    }

    public static byte* StrNStr(byte* haystack, byte* needle, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrNStr(haystack, needle, maxlen);
    }

    public static byte* StrCaseStr(byte* haystack, byte* needle)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrCaseStr(haystack, needle);
    }

    public static byte* StrTokR(byte* str, byte* delim, byte** saveptr)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrTokR(str, delim, saveptr);
    }

    public static int Utf8StrLen(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Utf8StrLen(str);
    }

    public static int Utf8StrNLen(byte* str, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Utf8StrNLen(str, maxlen);
    }

    public static byte* IToA(int value, byte* str, int radix)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IToA(value, str, radix);
    }

    public static byte* UIToA(uint value, byte* str, int radix)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UIToA(value, str, radix);
    }

    public static byte* LToA(long value, byte* str, int radix)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LToA(value, str, radix);
    }

    public static byte* ULToA(ulong value, byte* str, int radix)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ULToA(value, str, radix);
    }

    public static int AToI(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AToI(str);
    }

    public static double AToF(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AToF(str);
    }

    public static long StrToL(byte* str, byte** endptr, int @base)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrToL(str, endptr, @base);
    }

    public static ulong StrToUL(byte* str, byte** endptr, int @base)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrToUL(str, endptr, @base);
    }

    public static double StrToD(byte* str, byte** endptr)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrToD(str, endptr);
    }

    public static int StrCmp(byte* str1, byte* str2)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrCmp(str1, str2);
    }

    public static int StrNCmp(byte* str1, byte* str2, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrNCmp(str1, str2, maxlen);
    }

    public static int StrCaseCmp(byte* str1, byte* str2)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrCaseCmp(str1, str2);
    }

    public static int StrNCaseCmp(byte* str1, byte* str2, nuint maxlen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrNCaseCmp(str1, str2, maxlen);
    }

    public static byte* StrPBrk(byte* str, byte* breakset)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StrPBrk(str, breakset);
    }

    public static uint StepUtf8(byte** str, out nuint pslen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StepUtf8(str, out pslen);
    }

    public static uint StepBackUtf8(byte** str, out nuint pslen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StepBackUtf8(str, out pslen);
    }

    public static byte* Ucs4ToUtf8(uint codepoint, byte* dst)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
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

    public static IconVData IconVOpen(byte* toCode, byte* fromCode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IconVOpen(toCode, fromCode);
    }

    public static bool IconVClose(IconVData cd)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IconVClose(cd);
    }

    public static nuint IconV(
        IconVData cd,
        byte** inbuf,
        out nuint inBytesLeft,
        byte** outbuf,
        out nuint outBytesLeft
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IconV(cd, inbuf, out inBytesLeft, outbuf, out outBytesLeft);
    }

    public static byte* IconVString(byte* toCode, byte* fromCode, byte* inbuf, nuint inBytesLeft)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IconVString(toCode, fromCode, inbuf, inBytesLeft);
    }

    public static byte* IconVUtf8Locale(byte* s)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (byte* toCodePtr = ""u8)
        fixed (byte* fromCodePtr = "UTF-8"u8)
        {
            return IconVString(toCodePtr, fromCodePtr, s, (nuint)(StrLen(s) + 1));
        }
    }

    public static byte* IconVUtf8Ucs2(byte* s)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (byte* toCodePtr = "UCS-2"u8)
        fixed (byte* fromCodePtr = "UTF-8"u8)
        {
            return IconVString(toCodePtr, fromCodePtr, s, (nuint)(StrLen(s) + 1));
        }
    }

    public static byte* IconVUtf8Ucs4(byte* s)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (byte* toCodePtr = "UCS-4"u8)
        fixed (byte* fromCodePtr = "UTF-8"u8)
        {
            return IconVString(toCodePtr, fromCodePtr, s, (nuint)(StrLen(s) + 1));
        }
    }

    public static byte* IconVWcharUtf8(byte* s)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (byte* toCodePtr = "UTF-8"u8)
        fixed (byte* fromCodePtr = "WCHAR_T"u8)
        {
            return IconVString(toCodePtr, fromCodePtr, s, (nuint)((StrLen(s) + 1) * sizeof(char)));
        }
    }

    public readonly record struct Time(long Value)
    {
        public static implicit operator long(Time time) => time.Value;

        public static implicit operator Time(long value) => new(value);
    }

    public readonly record struct Environment(nint Value)
    {
        public static implicit operator nint(Environment env) => env.Value;

        public static implicit operator Environment(nint value) => new(value);
    }

    public readonly record struct IconVData(nint Value)
    {
        public static implicit operator nint(IconVData env) => env.Value;

        public static implicit operator IconVData(nint value) => new(value);
    }
}
