set(SDL_TOOLCHAIN_AVAILABLE TRUE)
if(EXISTS "${SDL_TOOLCHAIN_FILE}")
    include("${SDL_TOOLCHAIN_FILE}")
else()
    set(SDL_TOOLCHAIN_AVAILABLE FALSE)
    message(WARNING
        "SDL native build skipped: no toolchain file at ${SDL_TOOLCHAIN_FILE}. "
        "Provide one with -p:SdlToolchainFile=/absolute/path/to/toolchain.cmake.")
endif()
file(WRITE "${SDL_PROBE_RESULT}" "${SDL_TOOLCHAIN_AVAILABLE}")
