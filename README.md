# Can Do Urco

This is a collection of libraries, programs, tools and others for multiple projects.

This readme is to be completed.

## Contents

- [Libraries](#libraries)
  - [CanDoUrco.Glfw](#candourcoglfw)
  - [CanDoUrco.OpenGl.Generator](#candourcoopenglgenerator)
  - [CanDoUrco.Utilities](#candourcoutilities)

## Libraries

- [CanDourco.Glfw](#candourcoglfw): A safe import library for GLFW v3.5.1
- [CanDoUrco.Utilities](#candourcoutilities): Shared utilities, such as crash reporting
- [CanDoUrco.OpenGl.Generator](#candourcoopenglgenerator): A source generator that creates safe OpenGL bindings from the Khronos `gl.xml`

### CanDoUrco.Glfw

This library imports GLFW v3.5.1 and makes it safer.
For example, using the official [getting started example](https://www.glfw.org/docs/latest/quick.html), to create a classic GLFW window program, you would do:

```c
#include <GLFW/glfw3.h>

#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>

static void error_callback(int error, const char* description)
{
    fprintf(stderr, "Error: %s\n", description);
}

static void key_callback(GLFWwindow* window, int key, int scancode, int action, int mods)
{
    if (key == GLFW_KEY_ESCAPE && action == GLFW_PRESS)
    {
        glfwSetWindowShouldClose(window, GLFW_TRUE);
    }
}

int main(void)
{
    glfwSetErrorCallback(error_callback);
    if (!glfwInit())
    {
        exit(EXIT_FAILURE);
    }

    glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
    glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
    glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

    GLFWwindow* window = glfwCreateWindow(640, 480, "OpenGL Triangle", NULL, NULL);
    if (!window)
    {
        glfwTerminate();
        exit(EXIT_FAILURE);
    }

    glfwSetKeyCallback(window, key_callback);
    glfwMakeContextCurrent(window);

    // Load OpenGL here as required

    glfwSwapInterval(1);

    // Initialize whatever is required for OpenGL drawing here

    while (!glfwWindowShouldClose(window))
    {
        int width, int height;
        glfwGetFramebufferSize(window, &width, &height);

        // Draw with OpenGL, use `glfwGetTime()` to get the current GLFW time if required.

        glfwSwapBuffers(window);
        glfwPollEvents();
    }

    glfwDestroyWindow(window);
    glfwTerminate();
    exit(EXIT_SUCCESS);
}
```

However, with this library, you can do the same in CSharp, in this form:

```csharp
using System.Drawing;
using CanDoUrco.Glfw;
using CanDoUrco.Glfw.Input;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Utilities;
using CanDoUrco.Glfw.Video;

using var context = new GlfwNativeContext();
using var window = new GlfwWindow(
    new Size(640, 480),
    "OpenGL Triangle",
    options: (options) =>
    {
        options.ContextVersion = new Version(3, 3);
        options.OpenGlProfile = GlfwOpenGlProfile.Core;
    }
);

window.KeyAction += (_, args) =>
{
    if (args.Key is GlfwKey.Escape)
    {
        window.SetShouldClose(value: true);
    }
};

window.MakeContextCurrent();

// Load OpenGL as required.

GlfwContextUtilities.SetSwapInterval(interval: 1);

// Initialize whatever is required for OpenGL drawing here

while (!window.ShouldClose())
{
    var framebufferSize = window.GetFramebufferSize();

    // Draw with OpenGL, use `GlfwTimeUtilities.GetTime()` to get the current GLFW time if required.

    window.SwapBuffers();
    GlfwEvents.Poll();
}
```

Note that you don't require to check for any errors in C#. This is because, in this case, we aren't actually acting on the errors.
All GLFW methods that may error will throw a `GlfwException` to indicate the message and the error code automatically.
If you wish to grab these errors and do something about it, you need to surround GLFW calls in `try/catch` blocks.

### CanDoUrco.OpenGl.Generator

A Roslyn incremental source generator that reads the Khronos `gl.xml` registry (embedded in the generator) and writes safe C# OpenGL bindings for the version, profile and extensions you ask for.

Add it to your project as an analyzer, and enable unsafe code (the generated wrappers pin spans):

```xml
<PropertyGroup>
  <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
</PropertyGroup>
<ItemGroup>
  <ProjectReference Include="..\CanDoUrco.OpenGl.Generator\CanDoUrco.OpenGl.Generator.csproj"
                    OutputItemType="Analyzer"
                    ReferenceOutputAssembly="false" />
</ItemGroup>
```

Then declare a `static partial class` and mark it with the attributes:

```csharp
using CanDoUrco.OpenGl.Generator;

[OpenGl("3.3", Profile = OpenGlProfile.Core)]
[OpenGl("ARB_debug_output")]
public static partial class Gl;
```

- A value like `"3.3"` is an OpenGL version, and `Profile` is `Core`, `Compatibility` or `ES`. Anything else is an extension name (with or without the `GL_` prefix).
- The generated class has the same accessibility as your declaration.
- Only the functions and enums that belong to the selected version, profile and extensions are generated.
- Invalid versions and unknown or unsupported extensions are reported as warnings `OGL001`, `OGL002` and `OGL003`.

#### What gets generated

- **Methods** drop the `gl` prefix (`glGenBuffers` becomes `Gl.GenBuffers`). Each one has a private delegate and a private pointer that is loaded lazily with `OpenGlLibraryLoader.LoadMethod<T>` on the first call.
- **Enums** are grouped by the registry `group`, end with `Enum` (`ClearBufferMaskEnum`), drop the `GL_` prefix on members, use the right underlying type and are `[Flags]` for bitmasks. Constants without a group go in `UngroupedEnum`.
- **Types** are the plain C# equivalents (`uint`, `int`, `float`, ...). `GLboolean` is exposed as `bool` and marshalled to a `byte`.
- **Pointers** never appear publicly: arrays become `Span<T>` (`ReadOnlySpan<T>` if `const`), single values become `ref` (`in` if `const`), and array length parameters are computed for you. `void*` data has a generic `Span<T>` overload and an `nint` overload (for buffer offsets). Pass `[]` for a `NULL` array.
- **Strings** you pass in are converted to UTF-8 and freed after the call. Strings OpenGL returns (such as `GetString`) are static, so they are converted but never freed.

```csharp
Span<uint> buffers = stackalloc uint[1];
Gl.GenBuffers(buffers);
Gl.BindBuffer(Gl.BufferTargetARBEnum.ArrayBuffer, buffers[0]);
Gl.Clear(Gl.ClearBufferMaskEnum.ColorBufferBit);
```

See the [Playground](Playground/Program.cs) for a complete triangle example together with `CanDoUrco.Glfw`.

#### Loading functions

Functions are first looked up in the exports of the native OpenGL library. If that fails (on Windows, anything newer than OpenGL 1.1), a "proc address" loader is used as the fallback. If your project references `CanDoUrco.Glfw` and you set nothing, `GlfwContextUtilities.GetProcAddress` is the default fallback. You can provide the loader in two ways:

```csharp
// Per class: a static method `nint (string)`. Use a fully qualified name, since the generated code does not see your usings.
[OpenGl("3.3", Profile = OpenGlProfile.Core, LoaderMethod = "global::CanDoUrco.Glfw.Utilities.GlfwContextUtilities.GetProcAddress")]
internal static partial class Gl;

// Or globally, before the first OpenGL call:
OpenGlLibraryLoader.ProcAddressLoader = GlfwContextUtilities.GetProcAddress;
```

Remember that a context must be current (for GLFW, `window.MakeContextCurrent()`) before the first call to an OpenGL function.

### CanDoUrco.Utilities

Shared utilities for the other projects.

#### CrashReporter

Shows unhandled exceptions in a dialog, so applications without a terminal (set `<OutputType>WinExe</OutputType>`) don't crash silently.

```csharp
return CrashReporter.Run(() =>
{
    // Your program.
});
```

- **Windows:** a native `MessageBox`.
- **macOS:** an `osascript` alert.
- **Linux/BSD:** `kdialog` on KDE (otherwise `zenity` first), falling back to the other and then `xmessage`.
- **Fallback:** if no dialog is possible (for example no display), the error is written to stderr.

Only managed exceptions can be reported. Native crashes, such as an access violation in a driver, cannot be caught.

#### Matrix4x4Extensions

`CanDoUrco.Utilities.Mathematics` has helpers to pass `System.Numerics.Matrix4x4` values to OpenGL without copying them element by element.

```csharp
Span<float> mvp = stackalloc float[16];
(model * projection).CopyTo(mvp);
Gl.UniformMatrix4fv(location, 1, transpose: false, mvp);
```

- `CopyTo(Span<float>)` copies the 16 elements in row-major order without allocating.
- `ToArray()` returns them as a new `float[]`.

`System.Numerics` uses row vectors, so the multiplication order and the `transpose` argument must match your shader.
