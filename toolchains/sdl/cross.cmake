# Shared discovery is also run in script mode before MSBuild invokes configure.
set(SDL_TOOLCHAIN_AVAILABLE TRUE)
if(NOT CMAKE_HOST_SYSTEM_PROCESSOR)
    cmake_host_system_information(
        RESULT CMAKE_HOST_SYSTEM_PROCESSOR
        QUERY OS_PLATFORM
    )
endif()

set(_sdl_native FALSE)
if(
    CMAKE_HOST_SYSTEM_NAME STREQUAL SDL_TARGET_SYSTEM
    AND CMAKE_HOST_SYSTEM_PROCESSOR MATCHES "${SDL_HOST_PROCESSORS}"
)
    set(_sdl_native TRUE)
endif()

if(NOT _sdl_native OR CMAKE_SYSROOT)
    if(NOT CMAKE_C_COMPILER)
        find_program(CMAKE_C_COMPILER NAMES ${SDL_COMPILERS})
    endif()

    if(CMAKE_C_COMPILER)
        find_program(_sdl_compiler_path NAMES "${CMAKE_C_COMPILER}")
    endif()

    if(NOT _sdl_compiler_path)
        set(SDL_TOOLCHAIN_AVAILABLE FALSE)
        message(
            WARNING
            "SDL ${SDL_TARGET_RID} will not be built: no target C compiler was found. "
            "Install a toolchain providing ${SDL_COMPILERS} and add its bin directory to PATH, "
            "or pass -p:SdlCCompiler=/absolute/path/to/compiler to dotnet build. "
            "Linux cross-builds also need target libc/development libraries (SdlSysroot); "
            "Windows builds need matching MinGW-w64 headers, import libraries and resource tools. "
            "The managed build can continue, but SDL will be unavailable at runtime."
        )

        return()
    endif()

    if(
        CMAKE_SYSROOT
        AND (
            NOT IS_ABSOLUTE "${CMAKE_SYSROOT}"
            OR NOT IS_DIRECTORY "${CMAKE_SYSROOT}"
        )
    )
        set(SDL_TOOLCHAIN_AVAILABLE FALSE)
        message(
            WARNING
            "SDL ${SDL_TARGET_RID} will not be built: SdlSysroot must name an existing absolute target sysroot directory."
        )

        return()
    endif()

    set(CMAKE_SYSTEM_NAME "${SDL_TARGET_SYSTEM}")
    set(CMAKE_C_COMPILER "${_sdl_compiler_path}")
    get_filename_component(_sdl_bin "${CMAKE_C_COMPILER}" DIRECTORY)
    if(NOT CMAKE_CXX_COMPILER)
        if(CMAKE_C_COMPILER MATCHES "clang")
            set(_sdl_cxx_names "${SDL_TRIPLE}-clang++" "${SDL_TRIPLE}-g++")
        else()
            set(_sdl_cxx_names "${SDL_TRIPLE}-g++" "${SDL_TRIPLE}-clang++")
        endif()
        find_program(
            CMAKE_CXX_COMPILER
            NAMES ${_sdl_cxx_names}
            HINTS "${_sdl_bin}"
        )
    endif()

    if(SDL_TARGET_SYSTEM STREQUAL "Windows")
        find_program(_sdl_cxx_path NAMES "${CMAKE_CXX_COMPILER}")
        find_program(
            CMAKE_RC_COMPILER
            NAMES "${SDL_TRIPLE}-windres"
            HINTS "${_sdl_bin}"
        )

        find_program(_sdl_rc_path NAMES "${CMAKE_RC_COMPILER}")
        if(NOT _sdl_cxx_path OR NOT _sdl_rc_path)
            set(SDL_TOOLCHAIN_AVAILABLE FALSE)
            message(
                WARNING
                "SDL ${SDL_TARGET_RID} will not be built: Windows SDL needs a target C++ "
                "compiler and resource compiler as well as C. Install the complete MinGW-w64 "
                "or LLVM-MinGW toolchain (${_sdl_cxx_names}, ${SDL_TRIPLE}-windres), "
                "or specify SdlCxxCompiler and SdlResourceCompiler alongside SdlCCompiler."
            )
            return()
        endif()
    endif()
    if(CMAKE_SYSROOT)
        list(PREPEND CMAKE_FIND_ROOT_PATH "${CMAKE_SYSROOT}")
    else()
        execute_process(
            COMMAND "${CMAKE_C_COMPILER}" -print-sysroot
            OUTPUT_VARIABLE _sdl_sysroot
            OUTPUT_STRIP_TRAILING_WHITESPACE
            RESULT_VARIABLE _sdl_sysroot_result
        )

        if(
            _sdl_sysroot_result EQUAL 0
            AND _sdl_sysroot
            AND NOT _sdl_sysroot STREQUAL "/"
        )
            list(PREPEND CMAKE_FIND_ROOT_PATH "${_sdl_sysroot}")
        endif()
    endif()
    if(SDL_TARGET_SYSTEM STREQUAL "Windows")
        find_program(
            _sdl_pkg_config
            NAMES "${SDL_TRIPLE}-pkg-config"
            HINTS "${_sdl_bin}"
        )

        if(_sdl_pkg_config)
            set(PKG_CONFIG_EXECUTABLE
                "${_sdl_pkg_config}"
                CACHE FILEPATH
                "Target pkg-config"
            )
        endif()
    endif()

    set(CMAKE_FIND_ROOT_PATH_MODE_PROGRAM NEVER)
    set(CMAKE_FIND_ROOT_PATH_MODE_LIBRARY ONLY)
    set(CMAKE_FIND_ROOT_PATH_MODE_INCLUDE ONLY)
    set(CMAKE_FIND_ROOT_PATH_MODE_PACKAGE ONLY)
    set(ENV{PKG_CONFIG_PATH} "")
    if(SDL_TARGET_SYSTEM STREQUAL "Linux" OR NOT _sdl_pkg_config)
        # An empty search directory prevents pkg-config from using host packages.
        set(ENV{PKG_CONFIG_LIBDIR} "/nonexistent-sdl-target-pkgconfig")
        if(CMAKE_FIND_ROOT_PATH)
            list(GET CMAKE_FIND_ROOT_PATH 0 _sdl_root)
            file(TO_CMAKE_PATH "${_sdl_root}" _sdl_root)
            set(ENV{PKG_CONFIG_SYSROOT_DIR} "${_sdl_root}")
            cmake_path(
                CONVERT
                    "${_sdl_root}/usr/lib/${SDL_TRIPLE}/pkgconfig;${_sdl_root}/usr/lib/pkgconfig;${_sdl_root}/usr/lib64/pkgconfig;${_sdl_root}/usr/share/pkgconfig"
                TO_NATIVE_PATH_LIST _sdl_pkg_dirs
            )

            set(ENV{PKG_CONFIG_LIBDIR} "${_sdl_pkg_dirs}")
        endif()
        set(PKG_CONFIG_USE_CMAKE_PREFIX_PATH FALSE)
    endif()
endif()

set(CMAKE_SYSTEM_PROCESSOR "${SDL_TARGET_PROCESSOR}")
list(
    APPEND CMAKE_TRY_COMPILE_PLATFORM_VARIABLES
    CMAKE_C_COMPILER
    CMAKE_C_COMPILER_TARGET
    CMAKE_CXX_COMPILER
    CMAKE_CXX_COMPILER_TARGET
    CMAKE_RC_COMPILER
    CMAKE_SYSROOT
)
list(REMOVE_DUPLICATES CMAKE_TRY_COMPILE_PLATFORM_VARIABLES)
