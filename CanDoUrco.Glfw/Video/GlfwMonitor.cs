namespace CanDoUrco.Glfw.Video;

/// <summary>
/// Represents the physical monitor.
/// </summary>
public sealed class GlfwMonitor
{
    private readonly unsafe Native.Glfw.Monitor* _handle;

    internal unsafe GlfwMonitor(Native.Glfw.Monitor* handle) => _handle = handle;
}
