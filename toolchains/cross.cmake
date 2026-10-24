# Usable both as a CMake toolchain and by probe.cmake before configuration.
# Consumers can require optional tools with NATIVE_REQUIRE_CXX/RC.
set(NATIVE_TOOLCHAIN_AVAILABLE TRUE)
if(NOT CMAKE_HOST_SYSTEM_PROCESSOR)
    cmake_host_system_information(RESULT CMAKE_HOST_SYSTEM_PROCESSOR QUERY OS_PLATFORM)
endif()

set(_native_host FALSE)
if(CMAKE_HOST_SYSTEM_NAME STREQUAL NATIVE_TARGET_SYSTEM
   AND CMAKE_HOST_SYSTEM_PROCESSOR MATCHES "${NATIVE_HOST_PROCESSORS}")
    set(_native_host TRUE)
endif()

if(NOT _native_host OR CMAKE_SYSROOT)
    if(NOT CMAKE_C_COMPILER)
        find_program(CMAKE_C_COMPILER NAMES ${NATIVE_COMPILERS})
    endif()
    if(CMAKE_C_COMPILER)
        find_program(_native_compiler_path NAMES "${CMAKE_C_COMPILER}")
    endif()
    if(NOT _native_compiler_path)
        set(NATIVE_TOOLCHAIN_AVAILABLE FALSE)
        message(WARNING
            "Native ${NATIVE_TARGET_RID} build unavailable: no target C compiler found. "
            "Install a toolchain providing ${NATIVE_COMPILERS} and add its bin directory to PATH, "
            "or set CMAKE_C_COMPILER to the compiler's absolute path. "
            "Linux targets need target libc/development libraries and may need CMAKE_SYSROOT; "
            "Windows targets need matching MinGW-w64 headers and import libraries.")
        return()
    endif()
    if(CMAKE_SYSROOT AND
       (NOT IS_ABSOLUTE "${CMAKE_SYSROOT}" OR NOT IS_DIRECTORY "${CMAKE_SYSROOT}"))
        set(NATIVE_TOOLCHAIN_AVAILABLE FALSE)
        message(WARNING
            "Native ${NATIVE_TARGET_RID} build unavailable: CMAKE_SYSROOT must be an existing absolute directory.")
        return()
    endif()

    set(CMAKE_SYSTEM_NAME "${NATIVE_TARGET_SYSTEM}")
    set(CMAKE_C_COMPILER "${_native_compiler_path}")
    get_filename_component(_native_bin "${CMAKE_C_COMPILER}" DIRECTORY)
    if(CMAKE_C_COMPILER MATCHES "clang")
        set(_native_cxx_names "${NATIVE_TRIPLE}-clang++" "${NATIVE_TRIPLE}-g++")
    else()
        set(_native_cxx_names "${NATIVE_TRIPLE}-g++" "${NATIVE_TRIPLE}-clang++")
    endif()
    if(NOT CMAKE_CXX_COMPILER)
        find_program(CMAKE_CXX_COMPILER NAMES ${_native_cxx_names} HINTS "${_native_bin}")
    endif()
    if(NATIVE_REQUIRE_CXX)
        find_program(_native_cxx_path NAMES "${CMAKE_CXX_COMPILER}")
        if(NOT _native_cxx_path)
            set(NATIVE_TOOLCHAIN_AVAILABLE FALSE)
            message(WARNING
                "Native ${NATIVE_TARGET_RID} build unavailable: this consumer requires C++. "
                "Install ${_native_cxx_names} or set CMAKE_CXX_COMPILER.")
            return()
        endif()
    endif()
    if(NATIVE_TARGET_SYSTEM STREQUAL "Windows")
        find_program(CMAKE_RC_COMPILER NAMES "${NATIVE_TRIPLE}-windres" HINTS "${_native_bin}")
        if(NATIVE_REQUIRE_RC)
            find_program(_native_rc_path NAMES "${CMAKE_RC_COMPILER}")
            if(NOT _native_rc_path)
                set(NATIVE_TOOLCHAIN_AVAILABLE FALSE)
                message(WARNING
                    "Native ${NATIVE_TARGET_RID} build unavailable: this consumer requires a resource compiler. "
                    "Install ${NATIVE_TRIPLE}-windres or set CMAKE_RC_COMPILER.")
                return()
            endif()
        endif()
    endif()

    if(CMAKE_SYSROOT)
        list(PREPEND CMAKE_FIND_ROOT_PATH "${CMAKE_SYSROOT}")
    else()
        execute_process(COMMAND "${CMAKE_C_COMPILER}" -print-sysroot
            OUTPUT_VARIABLE _native_sysroot OUTPUT_STRIP_TRAILING_WHITESPACE
            RESULT_VARIABLE _native_sysroot_result)
        if(_native_sysroot_result EQUAL 0 AND _native_sysroot AND NOT _native_sysroot STREQUAL "/")
            list(PREPEND CMAKE_FIND_ROOT_PATH "${_native_sysroot}")
        endif()
    endif()
    if(NATIVE_TARGET_SYSTEM STREQUAL "Windows")
        find_program(_native_pkg_config NAMES "${NATIVE_TRIPLE}-pkg-config" HINTS "${_native_bin}")
        if(_native_pkg_config)
            set(PKG_CONFIG_EXECUTABLE "${_native_pkg_config}" CACHE FILEPATH "Target pkg-config")
        endif()
    endif()

    set(CMAKE_FIND_ROOT_PATH_MODE_PROGRAM NEVER)
    set(CMAKE_FIND_ROOT_PATH_MODE_LIBRARY ONLY)
    set(CMAKE_FIND_ROOT_PATH_MODE_INCLUDE ONLY)
    set(CMAKE_FIND_ROOT_PATH_MODE_PACKAGE ONLY)
    set(ENV{PKG_CONFIG_PATH} "")
    if(NATIVE_TARGET_SYSTEM STREQUAL "Linux" OR NOT _native_pkg_config)
        # Prevent pkg-config from discovering host libraries without a target root.
        set(ENV{PKG_CONFIG_LIBDIR} "/nonexistent-native-target-pkgconfig")
        if(CMAKE_FIND_ROOT_PATH)
            list(GET CMAKE_FIND_ROOT_PATH 0 _native_root)
            file(TO_CMAKE_PATH "${_native_root}" _native_root)
            set(ENV{PKG_CONFIG_SYSROOT_DIR} "${_native_root}")
            cmake_path(CONVERT
                "${_native_root}/usr/lib/${NATIVE_TRIPLE}/pkgconfig;${_native_root}/usr/lib/pkgconfig;${_native_root}/usr/lib64/pkgconfig;${_native_root}/usr/share/pkgconfig"
                TO_NATIVE_PATH_LIST _native_pkg_dirs)
            set(ENV{PKG_CONFIG_LIBDIR} "${_native_pkg_dirs}")
        endif()
        set(PKG_CONFIG_USE_CMAKE_PREFIX_PATH FALSE)
    endif()
endif()

set(CMAKE_SYSTEM_PROCESSOR "${NATIVE_TARGET_PROCESSOR}")
list(APPEND CMAKE_TRY_COMPILE_PLATFORM_VARIABLES
    CMAKE_C_COMPILER CMAKE_C_COMPILER_TARGET
    CMAKE_CXX_COMPILER CMAKE_CXX_COMPILER_TARGET CMAKE_RC_COMPILER CMAKE_SYSROOT
    NATIVE_REQUIRE_CXX NATIVE_REQUIRE_RC)
list(REMOVE_DUPLICATES CMAKE_TRY_COMPILE_PLATFORM_VARIABLES)
