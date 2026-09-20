# Can Do Urco

This is a collection of libraries, programs, tools and others for multiple projects.

This readme is to be completed.

## Contents

- [Libraries](#libraries)
  - [CanDoUrco.Glfw](#candourcoglfw)

## Libraries

- [CanDourco.Glfw](#candourcoglfw): A safe import library for GLFW v3.5.1

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
