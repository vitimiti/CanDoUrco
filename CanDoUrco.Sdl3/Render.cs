// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// oBTain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    public const int RendererVSyncDisabled = 0;
    public const int RendererVSyncAdaptive = -1;
    public const int DebugTextFontCharacterSize = 8;

    public static ReadOnlySpan<byte> SoftwareRender => "software\0"u8;
    public static ReadOnlySpan<byte> GpuRenderer => "gpu\0"u8;

    public static ReadOnlySpan<byte> PropRendererCreateNameString => "SDL.renderer.create.name\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateWindowPointer =>
        "SDL.renderer.create.window\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateSurfacePointer =>
        "SDL.renderer.create.surface\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateOutputColorspaceNumber =>
        "SDL.renderer.create.output_colorspace\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreatePresentVSyncNumber =>
        "SDL.renderer.create.present_vsync\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateGpuDevicePointer =>
        "SDL.renderer.create.gpu.device\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateGpuShadersSpirvBoolean =>
        "SDL.renderer.create.gpu.shaders_spirv\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateGpuShadersDxilBoolean =>
        "SDL.renderer.create.gpu.shaders_dxil\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateGpuShadersMslBoolean =>
        "SDL.renderer.create.gpu.shaders_msl\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateVulkanInstancePointer =>
        "SDL.renderer.create.vulkan.instance\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateVulkanSurfaceNumber =>
        "SDL.renderer.create.vulkan.surface\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateVulkanPhysicalDevicePointer =>
        "SDL.renderer.create.vulkan.physical_device\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateVulkanDevicePointer =>
        "SDL.renderer.create.vulkan.device\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateVulkanGraphicsQueueFamilyIndexNumber =>
        "SDL.renderer.create.vulkan.graphics_queue_family_index\0"u8;
    public static ReadOnlySpan<byte> PropRendererCreateVulkanPresentQueueFamilyIndexNumber =>
        "SDL.renderer.create.vulkan.present_queue_family_index\0"u8;

    public static ReadOnlySpan<byte> PropRendererNameString => "SDL.renderer.name\0"u8;
    public static ReadOnlySpan<byte> PropRendererWindowPointer => "SDL.renderer.window\0"u8;
    public static ReadOnlySpan<byte> PropRendererSurfacePointer => "SDL.renderer.surface\0"u8;
    public static ReadOnlySpan<byte> PropRendererVSyncNumber => "SDL.renderer.vsync\0"u8;
    public static ReadOnlySpan<byte> PropRendererMaxTextureSizeNumber =>
        "SDL.renderer.max_texture_size\0"u8;
    public static ReadOnlySpan<byte> PropRendererTextureFormatsPointer =>
        "SDL.renderer.texture_formats\0"u8;
    public static ReadOnlySpan<byte> PropRendererTextureWrappingBoolean =>
        "SDL.renderer.texture_wrapping\0"u8;
    public static ReadOnlySpan<byte> PropRendererOutputColorspaceNumber =>
        "SDL.renderer.output_colorspace\0"u8;
    public static ReadOnlySpan<byte> PropRendererHdrEnabledBoolean =>
        "SDL.renderer.HDR_enabled\0"u8;
    public static ReadOnlySpan<byte> PropRendererSdrWhitePointFloat =>
        "SDL.renderer.SDR_white_point\0"u8;
    public static ReadOnlySpan<byte> PropRendererHdrHeadroomFloat =>
        "SDL.renderer.HDR_headroom\0"u8;
    public static ReadOnlySpan<byte> PropRendererD3D9DevicePointer =>
        "SDL.renderer.d3d9.device\0"u8;
    public static ReadOnlySpan<byte> PropRendererD3D11DevicePointer =>
        "SDL.renderer.d3d11.device\0"u8;
    public static ReadOnlySpan<byte> PropRendererD3D11SwapchainPointer =>
        "SDL.renderer.d3d11.swap_chain\0"u8;
    public static ReadOnlySpan<byte> PropRendererD3D12DevicePointer =>
        "SDL.renderer.d3d12.device\0"u8;
    public static ReadOnlySpan<byte> PropRendererD3D12SwapchainPointer =>
        "SDL.renderer.d3d12.swap_chain\0"u8;
    public static ReadOnlySpan<byte> PropRendererD3D12CommandQueuePointer =>
        "SDL.renderer.d3d12.command_queue\0"u8;
    public static ReadOnlySpan<byte> PropRendererVulkanInstancePointer =>
        "SDL.renderer.vulkan.instance\0"u8;
    public static ReadOnlySpan<byte> PropRendererVulkanSurfaceNumber =>
        "SDL.renderer.vulkan.surface\0"u8;
    public static ReadOnlySpan<byte> PropRendererVulkanPhysicalDevicePointer =>
        "SDL.renderer.vulkan.physical_device\0"u8;
    public static ReadOnlySpan<byte> PropRendererVulkanDevicePointer =>
        "SDL.renderer.vulkan.device\0"u8;
    public static ReadOnlySpan<byte> PropRendererVulkanGraphicsQueueFamilyIndexNumber =>
        "SDL.renderer.vulkan.graphics_queue_family_index\0"u8;
    public static ReadOnlySpan<byte> PropRendererVulkanPresentQueueFamilyIndexNumber =>
        "SDL.renderer.vulkan.present_queue_family_index\0"u8;
    public static ReadOnlySpan<byte> PropRendererVulkanSwapchainImageCountNumber =>
        "SDL.renderer.vulkan.swapchain_image_count\0"u8;
    public static ReadOnlySpan<byte> PropRendererGpuDevicePointer => "SDL.renderer.gpu.device\0"u8;

    public static ReadOnlySpan<byte> PropTextureCreateColorspaceNumber =>
        "SDL.texture.create.colorspace\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateFormatNumber =>
        "SDL.texture.create.format\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateAccessNumber =>
        "SDL.texture.create.access\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateWidthNumber => "SDL.texture.create.width\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateHeightNumber =>
        "SDL.texture.create.height\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreatePalettePointer =>
        "SDL.texture.create.palette\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateSdrWhitePointFloat =>
        "SDL.texture.create.SDR_white_point\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateHdrHeadroomFloat =>
        "SDL.texture.create.HDR_headroom\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateD3D11TexturePointer =>
        "SDL.texture.create.d3d11.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateD3D11TextureUPointer =>
        "SDL.texture.create.d3d11.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateD3D11TextureVPointer =>
        "SDL.texture.create.d3d11.texture_v\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateD3D12TexturePointer =>
        "SDL.texture.create.d3d12.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateD3D12TextureUPointer =>
        "SDL.texture.create.d3d12.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateD3D12TextureVPointer =>
        "SDL.texture.create.d3d12.texture_v\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateMetalPixelBufferPointer =>
        "SDL.texture.create.metal.pixelbuffer\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateOpenGLTextureNumber =>
        "SDL.texture.create.opengl.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateOpenGLTextureUVNumber =>
        "SDL.texture.create.opengl.texture_uv\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateOpenGLTextureUNumber =>
        "SDL.texture.create.opengl.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateOpenGLTextureVNumber =>
        "SDL.texture.create.opengl.texture_v\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateOpenGLES2TextureNumber =>
        "SDL.texture.create.opengles2.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateOpenGLES2TextureUVNumber =>
        "SDL.texture.create.opengles2.texture_uv\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateOpenGLES2TextureUNumber =>
        "SDL.texture.create.opengles2.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateOpenGLES2TextureVNumber =>
        "SDL.texture.create.opengles2.texture_v\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateVulkanTextureNumber =>
        "SDL.texture.create.vulkan.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateVulkanLayoutNumber =>
        "SDL.texture.create.vulkan.layout\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateGpuTexturePointer =>
        "SDL.texture.create.gpu.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateGpuTextureUVPointer =>
        "SDL.texture.create.gpu.texture_uv\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateGpuTextureUPointer =>
        "SDL.texture.create.gpu.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureCreateGpuTextureVPointer =>
        "SDL.texture.create.gpu.texture_v\0"u8;

    public static ReadOnlySpan<byte> PropTextureColorspaceNumber => "SDL.texture.colorspace\0"u8;
    public static ReadOnlySpan<byte> PropTextureFormatNumber => "SDL.texture.format\0"u8;
    public static ReadOnlySpan<byte> PropTextureAccessNumber => "SDL.texture.access\0"u8;
    public static ReadOnlySpan<byte> PropTextureWidthNumber => "SDL.texture.width\0"u8;
    public static ReadOnlySpan<byte> PropTextureHeightNumber => "SDL.texture.height\0"u8;
    public static ReadOnlySpan<byte> PropTextureSdrWhitePointFloat =>
        "SDL.texture.SDR_white_point\0"u8;
    public static ReadOnlySpan<byte> PropTextureHdrHeadroomFloat => "SDL.texture.HDR_headroom\0"u8;
    public static ReadOnlySpan<byte> PropTextureD3D11TexturePointer =>
        "SDL.texture.d3d11.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureD3D11TextureUPointer =>
        "SDL.texture.d3d11.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureD3D11TextureVPointer =>
        "SDL.texture.d3d11.texture_v\0"u8;
    public static ReadOnlySpan<byte> PropTextureD3D12TexturePointer =>
        "SDL.texture.d3d12.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureD3D12TextureUPointer =>
        "SDL.texture.d3d12.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureD3D12TextureVPointer =>
        "SDL.texture.d3d12.texture_v\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGLTextureNumber =>
        "SDL.texture.opengl.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGLTextureUVNumber =>
        "SDL.texture.opengl.texture_uv\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGLTextureUNumber =>
        "SDL.texture.opengl.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGLTextureVNumber =>
        "SDL.texture.opengl.texture_v\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGLTextureTargetNumber =>
        "SDL.texture.opengl.target\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGLTexWFloat => "SDL.texture.opengl.tex_w\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGLTexHFloat => "SDL.texture.opengl.tex_h\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGles2TextureNumber =>
        "SDL.texture.opengles2.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGles2TextureUVNumber =>
        "SDL.texture.opengles2.texture_uv\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGles2TextureUNumber =>
        "SDL.texture.opengles2.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGles2TextureVNumber =>
        "SDL.texture.opengles2.texture_v\0"u8;
    public static ReadOnlySpan<byte> PropTextureOpenGles2TextureTargetNumber =>
        "SDL.texture.opengles2.target\0"u8;
    public static ReadOnlySpan<byte> PropTextureVulkanTextureNumber =>
        "SDL.texture.vulkan.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureGpuTexturePointer => "SDL.texture.gpu.texture\0"u8;
    public static ReadOnlySpan<byte> PropTextureGpuTextureUVPointer =>
        "SDL.texture.gpu.texture_uv\0"u8;
    public static ReadOnlySpan<byte> PropTextureGpuTextureUPointer =>
        "SDL.texture.gpu.texture_u\0"u8;
    public static ReadOnlySpan<byte> PropTextureGpuTextureVPointer =>
        "SDL.texture.gpu.texture_v\0"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumRenderDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumRenderDrivers();

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetRenderDriver(int index);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateWindowAndRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _CreateWindowAndRenderer(
        byte* title,
        int width,
        int height,
        WindowFlags windowFlags,
        out Window window,
        out Renderer renderer
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Renderer _CreateRenderer(Window window, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateRendererWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Renderer _CreateRendererWithProperties(PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPURenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Renderer _CreateGPURenderer(GpuDevice device, Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGPURendererDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuDevice _GetGPURendererDevice(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateSoftwareRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Renderer _CreateSoftwareRenderer(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Renderer _GetRenderer(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _GetRenderWindow(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRendererName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetRendererName(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRendererProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetRendererProperties(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderOutputSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderOutputSize(Renderer renderer, out int w, out int h);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentRenderOutputSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetCurrentRenderOutputSize(
        Renderer renderer,
        out int w,
        out int h
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Texture* _CreateTexture(
        Renderer renderer,
        PixelFormat format,
        TextureAccess access,
        int w,
        int h
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateTextureFromSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Texture* _CreateTextureFromSurface(Renderer renderer, Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateTextureWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Texture* _CreateTextureWithProperties(
        Renderer renderer,
        PropertiesID props
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetTextureProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetTextureProperties(Texture* texture);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRendererFromTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Renderer _GetRendererFromTexture(Texture* texture);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTextureSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTextureSize(Texture* texture, out int w, out int h);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTexturePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetTexturePalette(Texture* texture, Palette* palette);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTexturePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette* _GetTexturePalette(Texture* texture);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTextureColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetTextureColorMod(Texture* texture, byte r, byte g, byte b);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTextureColorModFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetTextureColorModFloat(
        Texture* texture,
        float r,
        float g,
        float b
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetTextureColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTextureColorMod(
        Texture* texture,
        out byte r,
        out byte g,
        out byte b
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetTextureColorModFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTextureColorModFloat(
        Texture* texture,
        out float r,
        out float g,
        out float b
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetTextureAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetTextureAlphaMod(Texture* texture, byte alpha);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTextureAlphaModFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetTextureAlphaModFloat(Texture* texture, float alpha);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTextureAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTextureAlphaMod(Texture* texture, out byte alpha);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTextureAlphaModFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTextureAlphaModFloat(Texture* texture, out float alpha);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTextureBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetTextureBlendMode(Texture* texture, BlendMode blendMode);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTextureBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTextureBlendMode(Texture* texture, out BlendMode blendMode);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetTextureScaleMode(Texture* texture, ScaleMode scaleMode);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTextureScaleMode(Texture* texture, out ScaleMode scaleMode);

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _UpdateTexture(
        Texture* texture,
        Rect* rect,
        void* pixels,
        int pitch
    );

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateYUVTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _UpdateYuvTexture(
        Texture* texture,
        Rect* rect,
        byte* yPlane,
        int yPitch,
        byte* uPlane,
        int uPitch,
        byte* vPlane,
        int vPitch
    );

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateNVTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _UpdateNVTexture(
        Texture* texture,
        Rect* rect,
        byte* yPlane,
        int yPitch,
        byte* uvPlane,
        int uvPitch
    );

    [LibraryImport(DllName, EntryPoint = "SDL_LockTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _LockTexture(
        Texture* texture,
        Rect* rect,
        out void* pixels,
        out int pitch
    );

    [LibraryImport(DllName, EntryPoint = "SDL_LockTextureToSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _LockTextureToSurface(
        Texture* texture,
        Rect* rect,
        out Surface* surface
    );

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnlockTexture(Texture* texture);

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderTarget(Renderer renderer, Texture* texture);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Texture* _GetRenderTarget(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderLogicalPresentation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderLogicalPresentation(
        Renderer renderer,
        int w,
        int h,
        RendererLogicalPresentation mode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderLogicalPresentation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderLogicalPresentation(
        Renderer renderer,
        out int w,
        out int h,
        out RendererLogicalPresentation mode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderLogicalPresentationRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderLogicalPresentationRect(
        Renderer renderer,
        out FRect rect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderCoordinatesFromWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderCoordinatesFromWindow(
        Renderer renderer,
        float windowX,
        float windowY,
        out float x,
        out float y
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderCoordinatesToWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderCoordinatesToWindow(
        Renderer renderer,
        float x,
        float y,
        out float windowX,
        out float windowY
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ConvertEventToRenderCoordinates")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ConvertEventToRenderCoordinates(
        Renderer renderer,
        ref Event @event
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderViewport(Renderer renderer, Rect* rect);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderViewport(Renderer renderer, out Rect rect);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderViewportSet")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderViewportSet(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderSafeArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderSafeArea(Renderer renderer, out Rect rect);

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderClipRect(Renderer renderer, Rect* rect);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderClipRect(Renderer renderer, out Rect rect);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderClipEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderClipEnabled(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderScale(Renderer renderer, float scaleX, float scaleY);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderScale(
        Renderer renderer,
        out float scaleX,
        out float scaleY
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderDrawColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderDrawColor(
        Renderer renderer,
        byte r,
        byte g,
        byte b,
        byte a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderDrawColorFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderDrawColorFloat(
        Renderer renderer,
        float r,
        float g,
        float b,
        float a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderDrawColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderDrawColor(
        Renderer renderer,
        out byte r,
        out byte g,
        out byte b,
        out byte a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderDrawColorFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderDrawColorFloat(
        Renderer renderer,
        out float r,
        out float g,
        out float b,
        out float a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderColorScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderColorScale(Renderer renderer, float scale);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderColorScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderColorScale(Renderer renderer, out float scale);

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderDrawBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderDrawBlendMode(Renderer renderer, BlendMode blendMode);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderDrawBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderDrawBlendMode(Renderer renderer, out BlendMode blendMode);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderClear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderClear(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderPoint(Renderer renderer, float x, float y);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderPoints(Renderer renderer, FPoint* points, int count);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderLine(
        Renderer renderer,
        float x1,
        float y1,
        float x2,
        float y2
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderLines")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderLines(Renderer renderer, FPoint* points, int count);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderRect(Renderer renderer, FRect* rect);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderRects(Renderer renderer, FRect* rects, int count);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderFillRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderFillRect(Renderer renderer, FRect* rect);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderFillRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderFillRects(Renderer renderer, FRect* rects, int count);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderTexture(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        FRect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderTextureRotated")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderTextureRotated(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        FRect* dstRect,
        double angle,
        FPoint* center,
        FlipMode flip
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderTextureAffine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderTextureAffine(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        FPoint* origin,
        FPoint* right,
        FPoint* down
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderTextureTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderTextureTiled(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        float scale,
        FRect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderTexture9Grid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderTexture9Grid(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        float leftWidth,
        float rightWidth,
        float topHeight,
        float bottomHeight,
        float scale,
        FRect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderTexture9GridTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderTexture9GridTiled(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        float leftWidth,
        float rightWidth,
        float topHeight,
        float bottomHeight,
        float scale,
        FRect* dstRect,
        float tileScale
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderGeometry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderGeometry(
        Renderer renderer,
        Texture* texture,
        Vertex* vertices,
        int numVertices,
        int* indices,
        int numIndices
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderGeometryRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderGeometryRaw(
        Renderer renderer,
        Texture* texture,
        float* xy,
        int xyStride,
        FColor* color,
        int colorStride,
        float* uv,
        int uvStride,
        int numVertices,
        void* indices,
        int numIndices,
        int sizeIndices
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderTextureAddressMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderTextureAddressMode(
        Renderer renderer,
        TextureAddressMode uMode,
        TextureAddressMode vMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderTextureAddressMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderTextureAddressMode(
        Renderer renderer,
        out TextureAddressMode uMode,
        out TextureAddressMode vMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RenderReadPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _RenderReadPixels(Renderer renderer, Rect* rect);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderPresent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderPresent(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyTexture(Texture* texture);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyRenderer(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_FlushRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _FlushRenderer(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderMetalLayer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _GetRenderMetalLayer(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderMetalCommandEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _GetRenderMetalCommandEncoder(Renderer renderer);

    [LibraryImport(DllName, EntryPoint = "SDL_AddVulkanRenderSemaphores")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _AddVulkanRenderSemaphores(
        Renderer renderer,
        uint waitStageMask,
        long waitSemaphore,
        long signalSemaphore
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRenderVSync(Renderer renderer, int vsync);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRenderVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetRenderVSync(Renderer renderer, out int vsync);

    [LibraryImport(DllName, EntryPoint = "SDL_RenderDebugText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RenderDebugText(Renderer renderer, float x, float y, byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_SetDefaultTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetDefaultTextureScaleMode(Renderer renderer, ScaleMode scaleMode);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDefaultTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetDefaultTextureScaleMode(
        Renderer renderer,
        out ScaleMode scaleMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPURenderState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuRenderState _CreateGPURenderState(
        Renderer renderer,
        in GpuRenderStateCreateInfo createInfo
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPURenderStateFragmentUniforms")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetGPURenderStateFragmentUniforms(
        GpuRenderState state,
        uint slotIndex,
        void* data,
        uint length
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPURenderState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetGPURenderState(Renderer renderer, GpuRenderState state);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyGPURenderState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyGPURenderState(GpuRenderState state);

    public static int GetNumRenderDrivers()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumRenderDrivers();
    }

    public static byte* GetRenderDriver(int index)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderDriver(index);
    }

    public static Bool CreateWindowAndRenderer(
        byte* title,
        int width,
        int height,
        WindowFlags windowFlags,
        out Window window,
        out Renderer renderer
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateWindowAndRenderer(
            title,
            width,
            height,
            windowFlags,
            out window,
            out renderer
        );
    }

    public static Renderer CreateRenderer(Window window, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateRenderer(window, name);
    }

    public static Renderer CreateRendererWithProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateRendererWithProperties(props);
    }

    public static Renderer CreateGPURenderer(GpuDevice device, Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGPURenderer(device, window);
    }

    public static GpuDevice GetGPURendererDevice(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGPURendererDevice(renderer);
    }

    public static Renderer CreateSoftwareRenderer(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateSoftwareRenderer(surface);
    }

    public static Renderer GetRenderer(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderer(window);
    }

    public static Window GetRenderWindow(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderWindow(renderer);
    }

    public static byte* GetRendererName(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRendererName(renderer);
    }

    public static Bool GetRenderOutputSize(Renderer renderer, out int w, out int h)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderOutputSize(renderer, out w, out h);
    }

    public static Bool GetCurrentRenderOutputSize(Renderer renderer, out int w, out int h)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCurrentRenderOutputSize(renderer, out w, out h);
    }

    public static Texture* CreateTexture(
        Renderer renderer,
        PixelFormat format,
        TextureAccess access,
        int w,
        int h
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateTexture(renderer, format, access, w, h);
    }

    public static Texture* CreateTextureFromSurface(Renderer renderer, Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateTextureFromSurface(renderer, surface);
    }

    public static Texture* CreateTextureWithProperties(Renderer renderer, PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateTextureWithProperties(renderer, props);
    }

    public static PropertiesID GetTextureProperties(Texture* texture)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTextureProperties(texture);
    }

    public static Renderer GetRendererFromTexture(Texture* texture)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRendererFromTexture(texture);
    }

    public static Bool GetTextureSize(Texture* texture, out int w, out int h)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTextureSize(texture, out w, out h);
    }

    public static Bool SetTexturePalette(Texture* texture, Palette* palette)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetTexturePalette(texture, palette);
    }

    public static Palette* GetTexturePalette(Texture* texture)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTexturePalette(texture);
    }

    public static Bool SetTextureColorMod(Texture* texture, byte r, byte g, byte b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetTextureColorMod(texture, r, g, b);
    }

    public static Bool SetTextureColorModFloat(Texture* texture, float r, float g, float b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetTextureColorModFloat(texture, r, g, b);
    }

    public static Bool GetTextureColorMod(Texture* texture, out byte r, out byte g, out byte b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTextureColorMod(texture, out r, out g, out b);
    }

    public static Bool GetTextureColorModFloat(
        Texture* texture,
        out float r,
        out float g,
        out float b
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTextureColorModFloat(texture, out r, out g, out b);
    }

    public static Bool SetTextureAlphaMod(Texture* texture, byte alpha)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetTextureAlphaMod(texture, alpha);
    }

    public static Bool SetTextureAlphaModFloat(Texture* texture, float alpha)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetTextureAlphaModFloat(texture, alpha);
    }

    public static Bool GetTextureAlphaMod(Texture* texture, out byte alpha)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTextureAlphaMod(texture, out alpha);
    }

    public static Bool GetTextureAlphaModFloat(Texture* texture, out float alpha)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTextureAlphaModFloat(texture, out alpha);
    }

    public static Bool SetTextureBlendMode(Texture* texture, BlendMode blendMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetTextureBlendMode(texture, blendMode);
    }

    public static Bool GetTextureBlendMode(Texture* texture, out BlendMode blendMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTextureBlendMode(texture, out blendMode);
    }

    public static Bool SetTextureScaleMode(Texture* texture, ScaleMode scaleMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetTextureScaleMode(texture, scaleMode);
    }

    public static Bool GetTextureScaleMode(Texture* texture, out ScaleMode scaleMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTextureScaleMode(texture, out scaleMode);
    }

    public static Bool UpdateTexture(Texture* texture, Rect* rect, void* pixels, int pitch)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UpdateTexture(texture, rect, pixels, pitch);
    }

    public static Bool UpdateYuvTexture(
        Texture* texture,
        Rect* rect,
        byte* yPlane,
        int yPitch,
        byte* uPlane,
        int uPitch,
        byte* vPlane,
        int vPitch
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UpdateYuvTexture(texture, rect, yPlane, yPitch, uPlane, uPitch, vPlane, vPitch);
    }

    public static Bool UpdateNVTexture(
        Texture* texture,
        Rect* rect,
        byte* yPlane,
        int yPitch,
        byte* uvPlane,
        int uvPitch
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UpdateNVTexture(texture, rect, yPlane, yPitch, uvPlane, uvPitch);
    }

    public static Bool LockTexture(Texture* texture, Rect* rect, out void* pixels, out int pitch)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LockTexture(texture, rect, out pixels, out pitch);
    }

    public static Bool LockTextureToSurface(Texture* texture, Rect* rect, out Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LockTextureToSurface(texture, rect, out surface);
    }

    public static void UnlockTexture(Texture* texture)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnlockTexture(texture);
    }

    public static Bool SetRenderTarget(Renderer renderer, Texture* texture)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderTarget(renderer, texture);
    }

    public static Texture* GetRenderTarget(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderTarget(renderer);
    }

    public static Bool SetRenderLogicalPresentation(
        Renderer renderer,
        int w,
        int h,
        RendererLogicalPresentation mode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderLogicalPresentation(renderer, w, h, mode);
    }

    public static Bool GetRenderLogicalPresentation(
        Renderer renderer,
        out int w,
        out int h,
        out RendererLogicalPresentation mode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderLogicalPresentation(renderer, out w, out h, out mode);
    }

    public static Bool GetRenderLogicalPresentationRect(Renderer renderer, out FRect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderLogicalPresentationRect(renderer, out rect);
    }

    public static Bool RenderCoordinatesFromWindow(
        Renderer renderer,
        float windowX,
        float windowY,
        out float x,
        out float y
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderCoordinatesFromWindow(renderer, windowX, windowY, out x, out y);
    }

    public static Bool RenderCoordinatesToWindow(
        Renderer renderer,
        float x,
        float y,
        out float windowX,
        out float windowY
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderCoordinatesToWindow(renderer, x, y, out windowX, out windowY);
    }

    public static Bool ConvertEventToRenderCoordinates(Renderer renderer, ref Event @event)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ConvertEventToRenderCoordinates(renderer, ref @event);
    }

    public static Bool SetRenderViewport(Renderer renderer, Rect* rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderViewport(renderer, rect);
    }

    public static Bool GetRenderViewport(Renderer renderer, out Rect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderViewport(renderer, out rect);
    }

    public static Bool RenderViewportSet(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderViewportSet(renderer);
    }

    public static Bool GetRenderSafeArea(Renderer renderer, out Rect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderSafeArea(renderer, out rect);
    }

    public static Bool SetRenderClipRect(Renderer renderer, Rect* rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderClipRect(renderer, rect);
    }

    public static Bool GetRenderClipRect(Renderer renderer, out Rect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderClipRect(renderer, out rect);
    }

    public static Bool RenderClipEnabled(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderClipEnabled(renderer);
    }

    public static Bool SetRenderScale(Renderer renderer, float scaleX, float scaleY)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderScale(renderer, scaleX, scaleY);
    }

    public static Bool GetRenderScale(Renderer renderer, out float scaleX, out float scaleY)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderScale(renderer, out scaleX, out scaleY);
    }

    public static Bool SetRenderDrawColor(Renderer renderer, byte r, byte g, byte b, byte a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderDrawColor(renderer, r, g, b, a);
    }

    public static Bool SetRenderDrawColor(Renderer renderer, float r, float g, float b, float a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderDrawColorFloat(renderer, r, g, b, a);
    }

    public static Bool GetRenderDrawColor(
        Renderer renderer,
        out byte r,
        out byte g,
        out byte b,
        out byte a
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderDrawColor(renderer, out r, out g, out b, out a);
    }

    public static Bool GetRenderDrawColorFloat(
        Renderer renderer,
        out float r,
        out float g,
        out float b,
        out float a
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderDrawColorFloat(renderer, out r, out g, out b, out a);
    }

    public static Bool SetRenderColorScale(Renderer renderer, float scale)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderColorScale(renderer, scale);
    }

    public static Bool GetRenderColorScale(Renderer renderer, out float scale)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderColorScale(renderer, out scale);
    }

    public static Bool SetRenderDrawBlendMode(Renderer renderer, BlendMode blendMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderDrawBlendMode(renderer, blendMode);
    }

    public static Bool GetRenderDrawBlendMode(Renderer renderer, out BlendMode blendMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderDrawBlendMode(renderer, out blendMode);
    }

    public static Bool RenderClear(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderClear(renderer);
    }

    public static Bool RenderPoint(Renderer renderer, float x, float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderPoint(renderer, x, y);
    }

    public static Bool RenderPoints(Renderer renderer, FPoint* points, int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderPoints(renderer, points, count);
    }

    public static Bool RenderLine(Renderer renderer, float x1, float y1, float x2, float y2)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderLine(renderer, x1, y1, x2, y2);
    }

    public static Bool RenderLines(Renderer renderer, FPoint* points, int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderLines(renderer, points, count);
    }

    public static Bool RenderRect(Renderer renderer, FRect* rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderRect(renderer, rect);
    }

    public static Bool RenderRects(Renderer renderer, FRect* rects, int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderRects(renderer, rects, count);
    }

    public static Bool RenderFillRect(Renderer renderer, FRect* rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderFillRect(renderer, rect);
    }

    public static Bool RenderFillRects(Renderer renderer, FRect* rects, int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderFillRects(renderer, rects, count);
    }

    public static Bool RenderTexture(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        FRect* dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderTexture(renderer, texture, srcRect, dstRect);
    }

    public static Bool RenderTextureRotated(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        FRect* dstRect,
        double angle,
        FPoint* center,
        FlipMode flip
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderTextureRotated(renderer, texture, srcRect, dstRect, angle, center, flip);
    }

    public static Bool RenderTextureAffine(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        FPoint* origin,
        FPoint* right,
        FPoint* down
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderTextureAffine(renderer, texture, srcRect, origin, right, down);
    }

    public static Bool RenderTextureTiled(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        float scale,
        FRect* dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderTextureTiled(renderer, texture, srcRect, scale, dstRect);
    }

    public static Bool RenderTexture9Grid(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        float leftWidth,
        float rightWidth,
        float topHeight,
        float bottomHeight,
        float scale,
        FRect* dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderTexture9Grid(
            renderer,
            texture,
            srcRect,
            leftWidth,
            rightWidth,
            topHeight,
            bottomHeight,
            scale,
            dstRect
        );
    }

    public static Bool RenderTexture9GridTiled(
        Renderer renderer,
        Texture* texture,
        FRect* srcRect,
        float leftWidth,
        float rightWidth,
        float topHeight,
        float bottomHeight,
        float scale,
        FRect* dstRect,
        float tileScale
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderTexture9GridTiled(
            renderer,
            texture,
            srcRect,
            leftWidth,
            rightWidth,
            topHeight,
            bottomHeight,
            scale,
            dstRect,
            tileScale
        );
    }

    public static Bool RenderGeometry(
        Renderer renderer,
        Texture* texture,
        Vertex* vertices,
        int numVertices,
        int* indices,
        int numIndices
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderGeometry(renderer, texture, vertices, numVertices, indices, numIndices);
    }

    public static Bool RenderGeometryRaw(
        Renderer renderer,
        Texture* texture,
        float* xy,
        int xyStride,
        FColor* color,
        int colorStride,
        float* uv,
        int uvStride,
        int numVertices,
        void* indices,
        int numIndices,
        int sizeIndices
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderGeometryRaw(
            renderer,
            texture,
            xy,
            xyStride,
            color,
            colorStride,
            uv,
            uvStride,
            numVertices,
            indices,
            numIndices,
            sizeIndices
        );
    }

    public static Bool SetRenderTextureAddressMode(
        Renderer renderer,
        TextureAddressMode uMode,
        TextureAddressMode vMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderTextureAddressMode(renderer, uMode, vMode);
    }

    public static Bool GetRenderTextureAddressMode(
        Renderer renderer,
        out TextureAddressMode uMode,
        out TextureAddressMode vMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderTextureAddressMode(renderer, out uMode, out vMode);
    }

    public static Surface* RenderReadPixels(Renderer renderer, Rect* rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderReadPixels(renderer, rect);
    }

    public static Bool RenderPresent(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderPresent(renderer);
    }

    public static void DestroyTexture(Texture* texture)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyTexture(texture);
    }

    public static void DestroyRenderer(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyRenderer(renderer);
    }

    public static Bool FlushRenderer(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FlushRenderer(renderer);
    }

    public static void* GetRenderMetalLayer(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderMetalLayer(renderer);
    }

    public static void* GetRenderMetalCommandEncoder(Renderer renderer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderMetalCommandEncoder(renderer);
    }

    public static Bool AddVulkanRenderSemaphores(
        Renderer renderer,
        uint waitStageMask,
        long waitSemaphore,
        long signalSemaphore
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddVulkanRenderSemaphores(renderer, waitStageMask, waitSemaphore, signalSemaphore);
    }

    public static Bool SetRenderVSync(Renderer renderer, int vsync)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRenderVSync(renderer, vsync);
    }

    public static Bool GetRenderVSync(Renderer renderer, out int vsync)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRenderVSync(renderer, out vsync);
    }

    public static Bool RenderDebugText(Renderer renderer, float x, float y, byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenderDebugText(renderer, x, y, str);
    }

    public static Bool SetDefaultTextureScaleMode(Renderer renderer, ScaleMode scaleMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetDefaultTextureScaleMode(renderer, scaleMode);
    }

    public static Bool GetDefaultTextureScaleMode(Renderer renderer, out ScaleMode scaleMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDefaultTextureScaleMode(renderer, out scaleMode);
    }

    public static GpuRenderState CreateGPURenderState(
        Renderer renderer,
        in GpuRenderStateCreateInfo createInfo
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGPURenderState(renderer, in createInfo);
    }

    public static Bool SetGPURenderStateFragmentUniforms(
        GpuRenderState state,
        uint slotIndex,
        void* data,
        uint length
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetGPURenderStateFragmentUniforms(state, slotIndex, data, length);
    }

    public static Bool SetGPURenderState(Renderer renderer, GpuRenderState state)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetGPURenderState(renderer, state);
    }

    public static void DestroyGPURenderState(GpuRenderState state)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyGPURenderState(state);
    }

    public enum TextureAccess
    {
        Static,
        Streaming,
        Target,
    }

    public enum TextureAddressMode
    {
        Invalid = -1,
        Audio,
        Clamp,
        Wrap,
    }

    public enum RendererLogicalPresentation
    {
        Disabled,
        Stretch,
        Letterbox,
        Overscan,
        IntegerScale,
    }

    public readonly record struct Renderer(nint Value)
    {
        public static implicit operator nint(Renderer renderer) => renderer.Value;

        public static implicit operator Renderer(nint value) => new(value);

        public static implicit operator bool(Renderer renderer) => renderer.Value != 0;
    }

    public readonly record struct GpuRenderState(nint Value)
    {
        public static implicit operator nint(GpuRenderState state) => state.Value;

        public static implicit operator GpuRenderState(nint value) => new(value);

        public static implicit operator bool(GpuRenderState state) => state.Value != 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public FPoint Position;
        public FColor Color;
        public FPoint TexCoord;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Texture
    {
        public PixelFormat Format;
        public int W;
        public int H;
        public int RefCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuRenderStateCreateInfo
    {
        public GpuShader FragmentShader;
        public int NumSamplerBindings;
        public GpuTextureSamplerBinding* SamplerBindings;
        public int NumStorageTextures;
        public GpuTexture* StorageTextures;
        public int NumStorageBuffers;
        public GpuBuffer* StorageBuffers;
        public PropertiesID Props;
    }
}
