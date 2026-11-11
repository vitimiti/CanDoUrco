# Can Do Urco

This repository contains reusable libraries and other projects.

## Contents

- [Libraries](#libraries)
  - [CanDoUrco.Logging](#candourcologging)
  - [CanDoUrco.Sdl3](#candourcosdl3)

## Libraries

### CanDoUrco.Logging

A logging library built on `Microsoft.Extensions.Logging`. It provides named log
categories, configurable logging through `CoreLog.Initialize`, and typed log
level helpers.

### CanDoUrco.Sdl3

Unsafe, low-level C# bindings for SDL3. The API closely follows SDL's native
types and pointer-based interfaces; applications are responsible for managing
native resources and memory safely. The library targets .NET 10 and loads the
SDL3 native library at runtime (`SDL3.dll` on Windows, `libSDL3.so.0` on Linux,
and `libSDL3.0.dylib` on macOS).
