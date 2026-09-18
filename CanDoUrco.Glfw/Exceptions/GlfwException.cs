using System.Runtime.InteropServices;

namespace CanDoUrco.Glfw.Exceptions;

public sealed class GlfwException(string? message, int errorCode)
    : ExternalException(message, errorCode);
