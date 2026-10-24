# Use the native compiler on Linux x64. Other hosts must supply a Linux x64
# compiler and sysroot; never fall back to their host compiler or libraries.
set(_sdl_linux_x64_native_host FALSE)
if(
    CMAKE_HOST_SYSTEM_NAME STREQUAL "Linux"
    AND CMAKE_HOST_SYSTEM_PROCESSOR MATCHES "^(x86_64|amd64|AMD64)$"
)
    set(_sdl_linux_x64_native_host TRUE)
endif()

if(NOT _sdl_linux_x64_native_host OR CMAKE_SYSROOT)
    if(NOT CMAKE_C_COMPILER)
        message(
            FATAL_ERROR
            "Cross-building linux-x64 requires -DCMAKE_C_COMPILER=<Linux x64 compiler>."
        )
    endif()

    if(
        NOT CMAKE_SYSROOT
        OR NOT IS_ABSOLUTE "${CMAKE_SYSROOT}"
        OR NOT IS_DIRECTORY "${CMAKE_SYSROOT}"
    )
        message(
            FATAL_ERROR
            "Cross-building linux-x64 requires -DCMAKE_SYSROOT=<absolute target sysroot directory>."
        )
    endif()

    set(CMAKE_SYSTEM_NAME Linux)
    list(PREPEND CMAKE_FIND_ROOT_PATH "${CMAKE_SYSROOT}")
    list(REMOVE_DUPLICATES CMAKE_FIND_ROOT_PATH)

    set(CMAKE_FIND_ROOT_PATH_MODE_PROGRAM NEVER)
    set(CMAKE_FIND_ROOT_PATH_MODE_LIBRARY ONLY)
    set(CMAKE_FIND_ROOT_PATH_MODE_INCLUDE ONLY)
    set(CMAKE_FIND_ROOT_PATH_MODE_PACKAGE ONLY)

    # FindPkgConfig invokes a host executable, so it needs separate isolation.
    file(TO_CMAKE_PATH "${CMAKE_SYSROOT}" _sdl_linux_x64_sysroot)
    set(ENV{PKG_CONFIG_SYSROOT_DIR} "${_sdl_linux_x64_sysroot}")

    cmake_path(
        CONVERT
            "${_sdl_linux_x64_sysroot}/usr/lib/x86_64-linux-gnu/pkgconfig;${_sdl_linux_x64_sysroot}/usr/lib64/pkgconfig;${_sdl_linux_x64_sysroot}/usr/lib/pkgconfig;${_sdl_linux_x64_sysroot}/usr/share/pkgconfig;${_sdl_linux_x64_sysroot}/lib/x86_64-linux-gnu/pkgconfig;${_sdl_linux_x64_sysroot}/lib64/pkgconfig;${_sdl_linux_x64_sysroot}/lib/pkgconfig"
        TO_NATIVE_PATH_LIST _sdl_linux_x64_pkgconfig
    )

    set(ENV{PKG_CONFIG_LIBDIR} "${_sdl_linux_x64_pkgconfig}")
    set(ENV{PKG_CONFIG_PATH} "")
    set(PKG_CONFIG_USE_CMAKE_PREFIX_PATH FALSE)
endif()

set(CMAKE_SYSTEM_PROCESSOR x86_64)

# try_compile projects must receive the same cross-build inputs.
list(
    APPEND CMAKE_TRY_COMPILE_PLATFORM_VARIABLES
    CMAKE_C_COMPILER
    CMAKE_C_COMPILER_TARGET
    CMAKE_SYSROOT
)

list(REMOVE_DUPLICATES CMAKE_TRY_COMPILE_PLATFORM_VARIABLES)
