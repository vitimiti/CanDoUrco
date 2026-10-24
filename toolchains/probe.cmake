set(NATIVE_TOOLCHAIN_AVAILABLE TRUE)
if(EXISTS "${NATIVE_TOOLCHAIN_FILE}")
    include("${NATIVE_TOOLCHAIN_FILE}")
else()
    set(NATIVE_TOOLCHAIN_AVAILABLE FALSE)
    message(WARNING
        "Native build unavailable: no toolchain file at ${NATIVE_TOOLCHAIN_FILE}. "
        "Set NATIVE_TOOLCHAIN_FILE to an existing CMake toolchain file.")
endif()
file(WRITE "${NATIVE_PROBE_RESULT}" "${NATIVE_TOOLCHAIN_AVAILABLE}")
