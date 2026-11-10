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
    public static GpuTextureUsageFlags GpuTextureUsageSampler => 1U << 0;
    public static GpuTextureUsageFlags GpuTextureUsageColorTarget => 1U << 1;
    public static GpuTextureUsageFlags GpuTextureUsageDepthStencilTarget => 1U << 2;
    public static GpuTextureUsageFlags GpuTextureUsageGraphicsStorageRead => 1U << 3;
    public static GpuTextureUsageFlags GpuTextureUsageComputeStorageRead => 1U << 4;
    public static GpuTextureUsageFlags GpuTextureUsageComputeStorageWrite => 1U << 5;
    public static GpuTextureUsageFlags GpuTextureUsageComputeStorageSimultaneousReadWrite =>
        1U << 6;

    public static GpuBufferUsageFlags GpuBufferUsageVertex => 1U << 0;
    public static GpuBufferUsageFlags GpuBufferUsageIndex => 1U << 1;
    public static GpuBufferUsageFlags GpuBufferUsageIndirect => 1U << 2;
    public static GpuBufferUsageFlags GpuBufferUsageGraphicsStorageRead => 1U << 3;
    public static GpuBufferUsageFlags GpuBufferUsageComputeStorageRead => 1U << 4;
    public static GpuBufferUsageFlags GpuBufferUsageComputeStorageWrite => 1U << 5;

    public static GpuShaderFormat GpuShaderFormatInvalid => 0;
    public static GpuShaderFormat GpuShaderFormatPrivate => 1U << 0;
    public static GpuShaderFormat GpuShaderFormatSpirv => 1U << 1;
    public static GpuShaderFormat GpuShaderFormatDxbc => 1U << 2;
    public static GpuShaderFormat GpuShaderFormatDxil => 1U << 3;
    public static GpuShaderFormat GpuShaderFormatMsl => 1U << 4;
    public static GpuShaderFormat GpuShaderFormatMetalLib => 1U << 5;

    public static GpuColorComponentFlags GpuColorComponentR => 1 << 0;
    public static GpuColorComponentFlags GpuColorComponentG => 1 << 1;
    public static GpuColorComponentFlags GpuColorComponentB => 1 << 2;
    public static GpuColorComponentFlags GpuColorComponentA => 1 << 3;

    public static ReadOnlySpan<byte> PropGpuDeviceCreateDebugModeBoolean =>
        "SDL.gpu.device.create.debugmode\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreatePreferLowPowerBoolean =>
        "SDL.gpu.device.create.preferlowpoer\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateVerboseBoolean =>
        "SDL.gpu.device.create.verbose\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateNameString =>
        "SDL.gpu.device.create.name\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateFreatureClipDistanceBoolean =>
        "SDL.gpu.device.create.feature.clip_distance\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateFeatureDepthCalmpingBoolean =>
        "SDL.gpu.device.create.feature.depth_clamping\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateFeatureIndirectDrawFirstInstanceBoolean =>
        "SDL.gpu.device.create.feature.indirect_draw_first_instance\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateFeatureAnisotropyBoolean =>
        "SDL.gpu.device.create.feature.anisotropy\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateShadersPrivateBoolean =>
        "SDL.gpu.device.create.shaders.private\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateShadersSpirvBoolean =>
        "SDL.gpu.device.create.shaders.spirv\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateShadersDxbcBoolean =>
        "SDL.gpu.device.create.shaders.dxbc\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateShadersDxilBoolean =>
        "SDL.gpu.device.create.shaders.dxil\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateShadersMslBoolean =>
        "SDL.gpu.device.create.shaders.msl\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateShadersMetalLibBoolean =>
        "SDL.gpu.device.create.shaders.metallib\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateD3D12AllowFewerResourceSlotsBoolean =>
        "SDL.gpu.device.create.d3d12.allowtier1resourcebinding\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateD3D12SemanticNameString =>
        "SDL.gpu.device.create.d3d12.semantic\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateD3D12AgilitySdkVersionNumber =>
        "SDL.gpu.device.create.d3d12.agility_sdk_version\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateD3D12AgilitySdkPathString =>
        "SDL.gpu.device.create.d3d12.agility_sdk_path\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateVulkanRequireHardwareAccelerationBoolean =>
        "SDL.gpu.device.create.vulkan.requirehardwareacceleration\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateVulkanOptionsPointer =>
        "SDL.gpu.device.create.vulkan.options\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceCreateMetalAllowMacFamily1Boolean =>
        "SDL.gpu.device.create.metal.allowmacfamily1\0"u8;

    public static ReadOnlySpan<byte> PropGpuDeviceNameString => "SDL.gpu.device.name\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceDriverNameString =>
        "SDL.gpu.device.driver_name\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceDriverVersionString =>
        "SDL.gpu.device.driver_version\0"u8;
    public static ReadOnlySpan<byte> PropGpuDeviceDriverInfoString =>
        "SDL.gpu.device.driver_info\0"u8;

    public static ReadOnlySpan<byte> PropGpuComputePipelineCreateNameString =>
        "SDL.gpu.computepipeline.create.name\0"u8;

    public static ReadOnlySpan<byte> PropGpuGraphicsPipelineCreateNameString =>
        "SDL.gpu.graphicspipeline.create.name\0"u8;

    public static ReadOnlySpan<byte> PropGpuSamplerCreateNameString =>
        "SDL.gpu.sampler.create.name\0"u8;

    public static ReadOnlySpan<byte> PropGpuShaderCreateNameString =>
        "SDL.gpu.shader.create.name\0"u8;

    public static ReadOnlySpan<byte> PropGpuTextureCreateD3D12ClearRFloat =>
        "SDL.gpu.texture.create.d3d12.clear.r\0"u8;
    public static ReadOnlySpan<byte> PropGpuTextureCreateD3D12ClearGFloat =>
        "SDL.gpu.texture.create.d3d12.clear.g\0"u8;
    public static ReadOnlySpan<byte> PropGpuTextureCreateD3D12ClearBFloat =>
        "SDL.gpu.texture.create.d3d12.clear.b\0"u8;
    public static ReadOnlySpan<byte> PropGpuTextureCreateD3D12ClearAFloat =>
        "SDL.gpu.texture.create.d3d12.clear.a\0"u8;
    public static ReadOnlySpan<byte> PropGpuTextureCreateD3D12ClearDepthFloat =>
        "SDL.gpu.texture.create.d3d12.clear.depth\0"u8;
    public static ReadOnlySpan<byte> PropGpuTextureCreateD3D12ClearStencilNumber =>
        "SDL.gpu.texture.create.d3d12.clear.stencil\0"u8;
    public static ReadOnlySpan<byte> PropGpuTextureCreateNameString =>
        "SDL.gpu.texture.create.name\0"u8;

    public static ReadOnlySpan<byte> PropGpuBufferCreateNameString =>
        "SDL.gpu.buffer.create.name\0"u8;

    public static ReadOnlySpan<byte> PropGpuTransferBufferCreateNameString =>
        "SDL.gpu.transferbuffer.create.name\0"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_GPUSupportsShaderFormats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GpuSupportsShaderFormats(GpuShaderFormat formatFlags, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_GPUSupportsProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GpuSupportsProperties(PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPUDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuDevice _CreateGpuDevice(
        GpuShaderFormat formatFlags,
        Bool debugMode,
        byte* name
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPUDeviceWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuDevice _CreateGpuDeviceWithProperties(PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyGPUDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyGpuDevice(GpuDevice device);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumGPUDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumGpuDrivers();

    [LibraryImport(DllName, EntryPoint = "SDL_GetGPUDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGpuDriver(int index);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGPUDeviceDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGpuDeviceDriver(GpuDevice device);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGPUShaderFormats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuShaderFormat _GetGpuShaderFormats(GpuDevice device);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGPUDeviceProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetGpuDeviceProperties(GpuDevice device);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPUComputePipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuComputePipeline _CreateGpuComputePipeline(
        GpuDevice device,
        in GpuComputePipelineCreateInfo createInfo
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPUGraphicsPipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuGraphicsPipeline _CreateGpuGraphicsPipeline(
        GpuDevice device,
        in GpuGraphicsPipelineCreateInfo createInfo
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPUSampler")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuSampler _CreateGpuSampler(
        GpuDevice device,
        in GpuSamplerCreateInfo createInfo
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPUShader")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuShader _CreateGpuShader(
        GpuDevice device,
        in GpuShaderCreateInfo createInfo
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuTexture _CreateGpuTexture(
        GpuDevice device,
        in GpuTextureCreateInfo createInfo
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPUBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuBuffer _CreateGpuBuffer(
        GpuDevice device,
        in GpuBufferCreateInfo createInfo
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateGPUTransferBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuTransferBuffer _CreateGpuTransferBuffer(
        GpuDevice device,
        in GpuTransferBufferCreateInfo createInfo
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPUBufferName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetGpuBufferName(GpuDevice device, GpuBuffer buffer, byte* text);

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPUTextureName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetGpuTextureName(
        GpuDevice device,
        GpuTexture texture,
        byte* text
    );

    [LibraryImport(DllName, EntryPoint = "SDL_InsertGPUDebugLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _InsertGpuDebugLabel(GpuCommandBuffer commandBuffer, byte* text);

    [LibraryImport(DllName, EntryPoint = "SDL_PushGPUDebugGroup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _PushGpuDebugGroup(GpuCommandBuffer commandBuffer, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_PopGPUDebugGroup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _PopGpuDebugGroup(GpuCommandBuffer commandBuffer);

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseGpuTexture(GpuDevice device, GpuTexture texture);

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseGPUSampler")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseGpuSampler(GpuDevice device, GpuSampler sampler);

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseGPUBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseGpuBuffer(GpuDevice device, GpuBuffer buffer);

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseGPUTransferBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseGpuTransferBuffer(
        GpuDevice device,
        GpuTransferBuffer buffer
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseGPUComputePipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseGpuComputePipeline(
        GpuDevice device,
        GpuComputePipeline pipeline
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseGPUShader")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseGpuShader(GpuDevice device, GpuShader shader);

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseGPUGraphicsPipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseGpuGraphicsPipeline(
        GpuDevice device,
        GpuGraphicsPipeline pipeline
    );

    [LibraryImport(DllName, EntryPoint = "SDL_AcquireGPUCommandBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuCommandBuffer _AcquireGpuCommandBuffer(GpuDevice device);

    [LibraryImport(DllName, EntryPoint = "SDL_PushGPUVertexUniformData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _PushGpuVertexUniformData(
        GpuCommandBuffer commandBuffer,
        uint slotIndex,
        void* data,
        uint length
    );

    [LibraryImport(DllName, EntryPoint = "SDL_PushGPUFragmentUniformData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _PushGpuFragmentUniformData(
        GpuCommandBuffer commandBuffer,
        uint slotIndex,
        void* data,
        uint length
    );

    [LibraryImport(DllName, EntryPoint = "SDL_PushGPUComputeUniformData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _PushGpuComputeUniformData(
        GpuCommandBuffer commandBuffer,
        uint slotIndex,
        void* data,
        uint length
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BeginGPURenderPass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuRenderPass _BeginGpuRenderPass(
        GpuCommandBuffer commandBuffer,
        GpuColorTargetInfo* colorTargetInfos,
        uint numColorTargets,
        GpuDepthStencilTargetInfo* depthStencilTargetInfo
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUGraphicsPipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuGraphicsPipeline(
        GpuRenderPass renderPass,
        GpuGraphicsPipeline pipeline
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPUViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetGpuViewport(GpuRenderPass renderPass, in GpuViewport viewport);

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPUScissor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetGpuScissor(GpuRenderPass renderPass, in Rect scissor);

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPUBlendConstants")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetGpuBlendConstants(
        GpuRenderPass renderPass,
        FColor blendConstants
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPUStencilReference")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetGpuStencilReference(GpuRenderPass renderPass, byte reference);

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUVertexBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuVertexBuffers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuBufferBinding* bindings,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUIndexBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuIndexBuffer(
        GpuRenderPass renderPass,
        in GpuBufferBinding binding,
        GpuIndexElementSize indexElementSize
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUVertexSamplers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuVertexSamplers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuTextureSamplerBinding* textureSamplerBindings,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUVertexStorageTextures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuVertexStorageTextures(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuTexture* storageTextures,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUVertexStorageBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuVertexStorageBuffers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuBuffer* storageBuffers,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUFragmentSamplers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuFragmentSamplers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuTextureSamplerBinding* textureSamplerBindings,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUFragmentStorageTextures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuFragmentStorageTextures(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuTexture* storageTextures,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUFragmentStorageBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuFragmentStorageBuffers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuBuffer* storageBuffers,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DrawGPUIndexedPrimitives")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DrawGpuIndexedPrimitives(
        GpuRenderPass renderPass,
        uint numIndices,
        uint numInstances,
        uint firstIndex,
        int vertexOffset,
        uint firstInstance
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DrawGPUPrimitives")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DrawGpuPrimitives(
        GpuRenderPass renderPass,
        uint numVertices,
        uint numInstances,
        uint firstVertex,
        uint firstInstance
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DrawGPUPrimitivesIndirect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DrawGpuPrimitivesIndirect(
        GpuRenderPass renderPass,
        GpuBuffer buffer,
        uint offset,
        uint drawCount
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DrawGPUIndexedPrimitivesIndirect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DrawGpuIndexedPrimitivesIndirect(
        GpuRenderPass renderPass,
        GpuBuffer buffer,
        uint offset,
        uint drawCount
    );

    [LibraryImport(DllName, EntryPoint = "SDL_EndGPURenderPass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _EndGpuRenderPass(GpuRenderPass renderPass);

    [LibraryImport(DllName, EntryPoint = "SDL_BeginGPUComputePass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuComputePass _BeginGpuComputePass(
        GpuCommandBuffer commandBuffer,
        GpuStorageTextureReadWriteBinding* storageTextureBindings,
        uint numStorageTextureBindings,
        GpuStorageBufferReadWriteBinding* storageBufferBindings,
        uint numStorageBufferBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUComputePipeline")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuComputePipeline(
        GpuComputePass computePass,
        GpuComputePipeline computePipeline
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUComputeSamplers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuComputeSamplers(
        GpuComputePass computePass,
        uint firstSlot,
        GpuTextureSamplerBinding* textureSamplerBindings,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUComputeStorageTextures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuComputeStorageTextures(
        GpuComputePass computePass,
        uint firstSlot,
        GpuTexture* storageTextures,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindGPUComputeStorageBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BindGpuComputeStorageBuffers(
        GpuComputePass computePass,
        uint firstSlot,
        GpuBuffer* storageBuffers,
        uint numBindings
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DispatchGPUCompute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DispatchGpuCompute(
        GpuComputePass computePass,
        uint groupCountX,
        uint groupCountY,
        uint groupCountZ
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DispatchGPUComputeIndirect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DispatchGpuComputeIndirect(
        GpuComputePass computePass,
        GpuBuffer buffer,
        uint offset
    );

    [LibraryImport(DllName, EntryPoint = "SDL_EndGPUComputePass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _EndGpuComputePass(GpuComputePass computePass);

    [LibraryImport(DllName, EntryPoint = "SDL_MapGPUTransferBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _MapGpuTransferBuffer(
        GpuDevice device,
        GpuTransferBuffer transferBuffer,
        Bool cycle
    );

    [LibraryImport(DllName, EntryPoint = "SDL_UnmapGPUTransferBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnmapGpuTransferBuffer(
        GpuDevice device,
        GpuTransferBuffer transferBuffer
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BeginGPUCopyPass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuCopyPass _BeginGpuCopyPass(GpuCommandBuffer commandBuffer);

    [LibraryImport(DllName, EntryPoint = "SDL_UploadToGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UploadToGpuTexture(
        GpuCopyPass copyPass,
        in GpuTextureTransferInfo source,
        in GpuTextureRegion destionation,
        Bool cycle
    );

    [LibraryImport(DllName, EntryPoint = "SDL_UploadToGPUBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UploadToGpuBuffer(
        GpuCopyPass copyPass,
        in GpuTransferBufferLocation source,
        in GpuBufferRegion destination,
        Bool cycle
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CopyGPUTextureToTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CopyGpuTextureToTexture(
        GpuCopyPass copyPass,
        in GpuTextureLocation source,
        in GpuTextureLocation destination,
        uint w,
        uint h,
        uint d,
        Bool cycle
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CopyGPUBufferToBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CopyGpuBufferToBuffer(
        GpuCopyPass copyPass,
        in GpuBufferLocation source,
        in GpuBufferLocation destination,
        uint size,
        Bool cycle
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DownloadFromGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DownloadFromGpuTexture(
        GpuCopyPass copyPass,
        in GpuTextureRegion source,
        in GpuTextureTransferInfo destination
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DownloadFromGPUBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DownloadFromGpuBuffer(
        GpuCopyPass copyPass,
        in GpuBufferRegion source,
        in GpuTransferBufferLocation destination
    );

    [LibraryImport(DllName, EntryPoint = "SDL_EndGPUCopyPass")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _EndGpuCopyPass(GpuCopyPass copyPass);

    [LibraryImport(DllName, EntryPoint = "SDL_GenerateMipmapsForGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GenerateMipmapsForGpuTexture(
        GpuCommandBuffer commandBuffer,
        GpuTexture texture
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitGPUTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BlitGpuTexture(
        GpuCommandBuffer commandBuffer,
        in GpuBlitInfo info
    );

    [LibraryImport(DllName, EntryPoint = "SDL_WindowSupportsGPUSwapchainComposition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WindowSupportsGpuSwapchainComposition(
        GpuDevice device,
        Window window,
        GpuSwapchainComposition swapchainComposition
    );

    [LibraryImport(DllName, EntryPoint = "SDL_WindowSupportsGPUPresentMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WindowSupportsGpuPresentMode(
        GpuDevice device,
        Window window,
        GpuPresentMode presentMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ClaimWindowForGPUDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ClaimWindowForGpuDevice(GpuDevice device, Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseWindowFromGPUDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseWindowFromGpuDevice(GpuDevice device, Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPUSwapchainParameters")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetGpuSwapchainParameters(
        GpuDevice device,
        Window window,
        GpuSwapchainComposition swapchainComposition,
        GpuPresentMode presentMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetGPUAllowedFramesInFlight")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetGpuAllowedFramesInFlight(GpuDevice device, uint framesInFlight);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGPUSwapchainTextureFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuTextureFormat _GetGpuSwapchainTextureFormat(
        GpuDevice device,
        Window window
    );

    [LibraryImport(DllName, EntryPoint = "SDL_AcquireGPUSwapchainTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _AcquireGpuSwapchainTexture(
        GpuDevice device,
        Window window,
        out GpuTexture swapchainTexture,
        out uint swapchainTextureWidth,
        out uint swapchainTextureHeight
    );

    [LibraryImport(DllName, EntryPoint = "SDL_WaitForGPUSwapchain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WaitForGpuSwapchain(GpuDevice device, Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitAndAcquireGPUSwapchainTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WaitAndAcquireGpuSwapchainTexture(
        GpuDevice device,
        Window window,
        out GpuTexture swapchainTexture,
        out uint swapchainTextureWidth,
        out uint swapchainTextureHeight
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SubmitGPUCommandBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SubmitGpuCommandBuffer(GpuCommandBuffer commandBuffer);

    [LibraryImport(DllName, EntryPoint = "SDL_SubmitGPUCommandBufferAndAcquireFence")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuFence _SubmitGpuCommandBufferAndAcquireFence(
        GpuCommandBuffer commandBuffer
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CancelGPUCommandBuffer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _CancelGpuCommandBuffer(GpuCommandBuffer commandBuffer);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitForGPUIdle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WaitForGpuIdle(GpuDevice device);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitForGPUFences")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WaitForGpuFences(
        GpuDevice device,
        Bool waitAll,
        GpuFence* fences,
        uint numFences
    );

    [LibraryImport(DllName, EntryPoint = "SDL_QueryGPUFence")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _QueryGpuFence(GpuDevice device, GpuFence fence);

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseGPUFence")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseGpuFence(GpuDevice device, GpuFence fence);

    [LibraryImport(DllName, EntryPoint = "SDL_GPUTextureFormatTexelBlockSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _GpuTextureFormatTexelBlockSize(GpuTextureFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_GPUTextureSupportsFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GpuTextureSupportsFormat(
        GpuDevice device,
        GpuTextureFormat format,
        GpuTextureType type,
        GpuTextureUsageFlags usage
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GPUTextureSupportsSampleCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GpuTextureSupportsSampleCount(
        GpuDevice device,
        GpuTextureFormat format,
        GpuSampleCount sampleCount
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CalculateGPUTextureFormatSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _CalculateGPUTextureFormatSize(
        GpuTextureFormat format,
        uint width,
        uint height,
        uint depthOrLayerCount
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetPixelFormatFromGPUTextureFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat _GetPixelFormatFromGPUTextureFormat(GpuTextureFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGPUTextureFormatFromPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GpuTextureFormat _GetGPUTextureFormatFromPixelFormat(PixelFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_GDKSuspendGPU")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GdkSuspendGPU(GpuDevice device);

    [LibraryImport(DllName, EntryPoint = "SDL_GDKResumeGPU")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GdkResumeGPU(GpuDevice device);

    public static Bool GpuSupportsShaderFormats(GpuShaderFormat formatFlags, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GpuSupportsShaderFormats(formatFlags, name);
    }

    public static Bool GpuSupportsProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GpuSupportsProperties(props);
    }

    public static GpuDevice CreateGpuDevice(GpuShaderFormat formatFlags, Bool debugMode, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGpuDevice(formatFlags, debugMode, name);
    }

    public static GpuDevice CreateGpuDeviceWithProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGpuDeviceWithProperties(props);
    }

    public static void DestroyGpuDevice(GpuDevice device)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyGpuDevice(device);
    }

    public static int GetNumGpuDrivers()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumGpuDrivers();
    }

    public static byte* GetGpuDriver(int index)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGpuDriver(index);
    }

    public static byte* GetGpuDeviceDriver(GpuDevice device)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGpuDeviceDriver(device);
    }

    public static GpuShaderFormat GetGpuShaderFormats(GpuDevice device)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGpuShaderFormats(device);
    }

    public static PropertiesID GetGpuDeviceProperties(GpuDevice device)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGpuDeviceProperties(device);
    }

    public static GpuComputePipeline CreateGpuComputePipeline(
        GpuDevice device,
        in GpuComputePipelineCreateInfo createInfo
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGpuComputePipeline(device, in createInfo);
    }

    public static GpuGraphicsPipeline CreateGpuGraphicsPipeline(
        GpuDevice device,
        in GpuGraphicsPipelineCreateInfo createInfo
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGpuGraphicsPipeline(device, in createInfo);
    }

    public static GpuSampler CreateGpuSampler(GpuDevice device, in GpuSamplerCreateInfo createInfo)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGpuSampler(device, in createInfo);
    }

    public static GpuShader CreateGpuShader(GpuDevice device, in GpuShaderCreateInfo createInfo)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGpuShader(device, in createInfo);
    }

    public static GpuTexture CreateGpuTexture(GpuDevice device, in GpuTextureCreateInfo createInfo)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGpuTexture(device, in createInfo);
    }

    public static GpuBuffer CreateGpuBuffer(GpuDevice device, in GpuBufferCreateInfo createInfo)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGpuBuffer(device, in createInfo);
    }

    public static GpuTransferBuffer CreateGpuTransferBuffer(
        GpuDevice device,
        in GpuTransferBufferCreateInfo createInfo
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateGpuTransferBuffer(device, in createInfo);
    }

    public static void SetGpuBufferName(GpuDevice device, GpuBuffer buffer, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetGpuBufferName(device, buffer, name);
    }

    public static void SetGpuTextureName(GpuDevice device, GpuTexture texture, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetGpuTextureName(device, texture, name);
    }

    public static void InsertGpuDebugLabel(GpuCommandBuffer commandBuffer, byte* text)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _InsertGpuDebugLabel(commandBuffer, text);
    }

    public static void PushGpuDebugGroup(GpuCommandBuffer commandBuffer, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _PushGpuDebugGroup(commandBuffer, name);
    }

    public static void PopGpuDebugGroup(GpuCommandBuffer commandBuffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _PopGpuDebugGroup(commandBuffer);
    }

    public static void ReleaseGpuTexture(GpuDevice device, GpuTexture texture)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseGpuTexture(device, texture);
    }

    public static void ReleaseGpuSampler(GpuDevice device, GpuSampler sampler)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseGpuSampler(device, sampler);
    }

    public static void ReleaseGpuBuffer(GpuDevice device, GpuBuffer buffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseGpuBuffer(device, buffer);
    }

    public static void ReleaseGpuTransferBuffer(GpuDevice device, GpuTransferBuffer buffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseGpuTransferBuffer(device, buffer);
    }

    public static void ReleaseGpuComputePipeline(GpuDevice device, GpuComputePipeline pipeline)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseGpuComputePipeline(device, pipeline);
    }

    public static void ReleaseGpuShader(GpuDevice device, GpuShader shader)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseGpuShader(device, shader);
    }

    public static void ReleaseGpuGraphicsPipeline(GpuDevice device, GpuGraphicsPipeline pipeline)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseGpuGraphicsPipeline(device, pipeline);
    }

    public static GpuCommandBuffer AcquireGpuCommandBuffer(GpuDevice device)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AcquireGpuCommandBuffer(device);
    }

    public static void PushGpuVertexUniformData(
        GpuCommandBuffer commandBuffer,
        uint slotIndex,
        void* data,
        uint length
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _PushGpuVertexUniformData(commandBuffer, slotIndex, data, length);
    }

    public static void PushGpuFragmentUniformData(
        GpuCommandBuffer commandBuffer,
        uint slotIndex,
        void* data,
        uint length
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _PushGpuFragmentUniformData(commandBuffer, slotIndex, data, length);
    }

    public static void PushGpuComputeUniformData(
        GpuCommandBuffer commandBuffer,
        uint slotIndex,
        void* data,
        uint length
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _PushGpuComputeUniformData(commandBuffer, slotIndex, data, length);
    }

    public static GpuRenderPass BeginGpuRenderPass(
        GpuCommandBuffer commandBuffer,
        GpuColorTargetInfo* colorTargetInfos,
        uint numColorTargets,
        GpuDepthStencilTargetInfo* depthStencilTargetInfo
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BeginGpuRenderPass(
            commandBuffer,
            colorTargetInfos,
            numColorTargets,
            depthStencilTargetInfo
        );
    }

    public static void BindGpuGraphicsPipeline(
        GpuRenderPass renderPass,
        GpuGraphicsPipeline pipeline
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuGraphicsPipeline(renderPass, pipeline);
    }

    public static void SetGpuViewport(GpuRenderPass renderPass, in GpuViewport viewport)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetGpuViewport(renderPass, in viewport);
    }

    public static void SetGpuScissor(GpuRenderPass renderPass, in Rect scissor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetGpuScissor(renderPass, in scissor);
    }

    public static void SetGpuBlendConstants(GpuRenderPass renderPass, FColor blendConstants)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetGpuBlendConstants(renderPass, blendConstants);
    }

    public static void SetGpuStencilReference(GpuRenderPass renderPass, byte reference)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetGpuStencilReference(renderPass, reference);
    }

    public static void BindGpuVertexBuffers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuBufferBinding* bindings,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuVertexBuffers(renderPass, firstSlot, bindings, numBindings);
    }

    public static void BindGpuIndexBuffer(
        GpuRenderPass renderPass,
        in GpuBufferBinding binding,
        GpuIndexElementSize indexElementSize
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuIndexBuffer(renderPass, in binding, indexElementSize);
    }

    public static void BindGpuVertexSamplers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuTextureSamplerBinding* textureSamplerBindings,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuVertexSamplers(renderPass, firstSlot, textureSamplerBindings, numBindings);
    }

    public static void BindGpuVertexStorageTextures(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuTexture* storageTextures,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuVertexStorageTextures(renderPass, firstSlot, storageTextures, numBindings);
    }

    public static void BindGpuVertexStorageBuffers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuBuffer* storageBuffers,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuVertexStorageBuffers(renderPass, firstSlot, storageBuffers, numBindings);
    }

    public static void BindGpuFragmentSamplers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuTextureSamplerBinding* textureSamplerBindings,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuFragmentSamplers(renderPass, firstSlot, textureSamplerBindings, numBindings);
    }

    public static void BindGpuFragmentStorageTextures(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuTexture* storageTextures,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuFragmentStorageTextures(renderPass, firstSlot, storageTextures, numBindings);
    }

    public static void BindGpuFragmentStorageBuffers(
        GpuRenderPass renderPass,
        uint firstSlot,
        GpuBuffer* storageBuffers,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuFragmentStorageBuffers(renderPass, firstSlot, storageBuffers, numBindings);
    }

    public static void DrawGpuIndexedPrimitives(
        GpuRenderPass renderPass,
        uint numIndices,
        uint numInstances,
        uint firstIndex,
        int vertexOffset,
        uint firstInstance
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DrawGpuIndexedPrimitives(
            renderPass,
            numIndices,
            numInstances,
            firstIndex,
            vertexOffset,
            firstInstance
        );
    }

    public static void DrawGpuPrimitives(
        GpuRenderPass renderPass,
        uint numVertices,
        uint numInstances,
        uint firstVertex,
        uint firstInstance
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DrawGpuPrimitives(renderPass, numVertices, numInstances, firstVertex, firstInstance);
    }

    public static void DrawGpuPrimitivesIndirect(
        GpuRenderPass renderPass,
        GpuBuffer buffer,
        uint offset,
        uint drawCount
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DrawGpuPrimitivesIndirect(renderPass, buffer, offset, drawCount);
    }

    public static void DrawGpuIndexedPrimitivesIndirect(
        GpuRenderPass renderPass,
        GpuBuffer buffer,
        uint offset,
        uint drawCount
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DrawGpuIndexedPrimitivesIndirect(renderPass, buffer, offset, drawCount);
    }

    public static void EndGpuRenderPass(GpuRenderPass renderPass)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _EndGpuRenderPass(renderPass);
    }

    public static GpuComputePass BeginGpuComputePass(
        GpuCommandBuffer commandBuffer,
        GpuStorageTextureReadWriteBinding* storageTextureBindings,
        uint numStorageTextureBindings,
        GpuStorageBufferReadWriteBinding* storageBufferBindings,
        uint numStorageBufferBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BeginGpuComputePass(
            commandBuffer,
            storageTextureBindings,
            numStorageTextureBindings,
            storageBufferBindings,
            numStorageBufferBindings
        );
    }

    public static void BindGpuComputePipeline(
        GpuComputePass computePass,
        GpuComputePipeline computePipeline
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuComputePipeline(computePass, computePipeline);
    }

    public static void BindGpuComputeSamplers(
        GpuComputePass computePass,
        uint firstSlot,
        GpuTextureSamplerBinding* textureSamplerBindings,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuComputeSamplers(computePass, firstSlot, textureSamplerBindings, numBindings);
    }

    public static void BindGpuComputeStorageTextures(
        GpuComputePass computePass,
        uint firstSlot,
        GpuTexture* storageTextures,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuComputeStorageTextures(computePass, firstSlot, storageTextures, numBindings);
    }

    public static void BindGpuComputeStorageBuffers(
        GpuComputePass computePass,
        uint firstSlot,
        GpuBuffer* storageBuffers,
        uint numBindings
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BindGpuComputeStorageBuffers(computePass, firstSlot, storageBuffers, numBindings);
    }

    public static void DispatchGpuCompute(
        GpuComputePass computePass,
        uint groupCountX,
        uint groupCountY,
        uint groupCountZ
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DispatchGpuCompute(computePass, groupCountX, groupCountY, groupCountZ);
    }

    public static void DispatchGpuComputeIndirect(
        GpuComputePass computePass,
        GpuBuffer buffer,
        uint offset
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DispatchGpuComputeIndirect(computePass, buffer, offset);
    }

    public static void EndGpuComputePass(GpuComputePass computePass)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _EndGpuComputePass(computePass);
    }

    public static void* MapGpuTransferBuffer(
        GpuDevice device,
        GpuTransferBuffer transferBuffer,
        Bool cycle
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MapGpuTransferBuffer(device, transferBuffer, cycle);
    }

    public static void UnmapGpuTransferBuffer(GpuDevice device, GpuTransferBuffer transferBuffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnmapGpuTransferBuffer(device, transferBuffer);
    }

    public static GpuCopyPass BeginGpuCopyPass(GpuCommandBuffer commandBuffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BeginGpuCopyPass(commandBuffer);
    }

    public static void UploadToGpuTexture(
        GpuCopyPass copyPass,
        in GpuTextureTransferInfo source,
        in GpuTextureRegion destination,
        Bool cycle
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UploadToGpuTexture(copyPass, in source, in destination, cycle);
    }

    public static void UploadToGpuBuffer(
        GpuCopyPass copyPass,
        in GpuTransferBufferLocation source,
        in GpuBufferRegion destination,
        Bool cycle
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UploadToGpuBuffer(copyPass, in source, in destination, cycle);
    }

    public static void CopyGpuTextureToTexture(
        GpuCopyPass copyPass,
        in GpuTextureLocation source,
        in GpuTextureLocation destination,
        uint w,
        uint h,
        uint d,
        Bool cycle
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _CopyGpuTextureToTexture(copyPass, in source, in destination, w, h, d, cycle);
    }

    public static void CopyGpuBufferToBuffer(
        GpuCopyPass copyPass,
        in GpuBufferLocation source,
        in GpuBufferLocation destination,
        uint size,
        Bool cycle
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _CopyGpuBufferToBuffer(copyPass, in source, in destination, size, cycle);
    }

    public static void DownloadFromGpuTexture(
        GpuCopyPass copyPass,
        in GpuTextureRegion source,
        in GpuTextureTransferInfo destination
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DownloadFromGpuTexture(copyPass, in source, in destination);
    }

    public static void DownloadFromGpuBuffer(
        GpuCopyPass copyPass,
        in GpuBufferRegion source,
        in GpuTransferBufferLocation destination
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DownloadFromGpuBuffer(copyPass, in source, in destination);
    }

    public static void EndGpuCopyPass(GpuCopyPass copyPass)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _EndGpuCopyPass(copyPass);
    }

    public static void GenerateMipmapsForGpuTexture(
        GpuCommandBuffer commandBuffer,
        GpuTexture texture
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GenerateMipmapsForGpuTexture(commandBuffer, texture);
    }

    public static void BlitGpuTexture(GpuCommandBuffer commandBuffer, in GpuBlitInfo info)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BlitGpuTexture(commandBuffer, in info);
    }

    public static Bool WindowSupportsGpuSwapchainComposition(
        GpuDevice device,
        Window window,
        GpuSwapchainComposition swapchainComposition
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WindowSupportsGpuSwapchainComposition(device, window, swapchainComposition);
    }

    public static Bool WindowSupportsGpuPresentMode(
        GpuDevice device,
        Window window,
        GpuPresentMode presentMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WindowSupportsGpuPresentMode(device, window, presentMode);
    }

    public static Bool ClaimWindowForGpuDevice(GpuDevice device, Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClaimWindowForGpuDevice(device, window);
    }

    public static void ReleaseWindowFromGpuDevice(GpuDevice device, Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseWindowFromGpuDevice(device, window);
    }

    public static Bool SetGpuSwapchainParameters(
        GpuDevice device,
        Window window,
        GpuSwapchainComposition swapchainComposition,
        GpuPresentMode presentMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetGpuSwapchainParameters(device, window, swapchainComposition, presentMode);
    }

    public static Bool SetGpuAllowedFramesInFlight(GpuDevice device, uint framesInFlight)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetGpuAllowedFramesInFlight(device, framesInFlight);
    }

    public static GpuTextureFormat GetGpuSwapchainTextureFormat(GpuDevice device, Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGpuSwapchainTextureFormat(device, window);
    }

    public static Bool AcquireGpuSwapchainTexture(
        GpuDevice device,
        Window window,
        out GpuTexture swapchainTexture,
        out uint swapchainTextureWidth,
        out uint swapchainTextureHeight
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AcquireGpuSwapchainTexture(
            device,
            window,
            out swapchainTexture,
            out swapchainTextureWidth,
            out swapchainTextureHeight
        );
    }

    public static Bool WaitForGpuSwapchain(GpuDevice device, Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WaitForGpuSwapchain(device, window);
    }

    public static Bool WaitAndAcquireGpuSwapchainTexture(
        GpuDevice device,
        Window window,
        out GpuTexture swapchainTexture,
        out uint swapchainTextureWidth,
        out uint swapchainTextureHeight
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WaitAndAcquireGpuSwapchainTexture(
            device,
            window,
            out swapchainTexture,
            out swapchainTextureWidth,
            out swapchainTextureHeight
        );
    }

    public static Bool SubmitGpuCommandBuffer(GpuCommandBuffer commandBuffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SubmitGpuCommandBuffer(commandBuffer);
    }

    public static GpuFence SubmitGpuCommandBufferAndAcquireFence(GpuCommandBuffer commandBuffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SubmitGpuCommandBufferAndAcquireFence(commandBuffer);
    }

    public static Bool CancelGpuCommandBuffer(GpuCommandBuffer commandBuffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CancelGpuCommandBuffer(commandBuffer);
    }

    public static Bool WaitForGpuIdle(GpuDevice device)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WaitForGpuIdle(device);
    }

    public static Bool WaitForGpuFences(
        GpuDevice device,
        Bool waitAll,
        GpuFence* fences,
        uint numFences
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WaitForGpuFences(device, waitAll, fences, numFences);
    }

    public static Bool QueryGpuFence(GpuDevice device, GpuFence fence)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _QueryGpuFence(device, fence);
    }

    public static void ReleaseGpuFence(GpuDevice device, GpuFence fence)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseGpuFence(device, fence);
    }

    public static uint GpuTextureFormatTexelBlockSize(GpuTextureFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GpuTextureFormatTexelBlockSize(format);
    }

    public static Bool GpuTextureSupportsFormat(
        GpuDevice device,
        GpuTextureFormat format,
        GpuTextureType type,
        GpuTextureUsageFlags usage
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GpuTextureSupportsFormat(device, format, type, usage);
    }

    public static Bool GpuTextureSupportsSampleCount(
        GpuDevice device,
        GpuTextureFormat format,
        GpuSampleCount sampleCount
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GpuTextureSupportsSampleCount(device, format, sampleCount);
    }

    public static uint CalculateGPUTextureFormatSize(
        GpuTextureFormat format,
        uint width,
        uint height,
        uint depthOrLayerCount
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CalculateGPUTextureFormatSize(format, width, height, depthOrLayerCount);
    }

    public static PixelFormat GetPixelFormatFromGPUTextureFormat(GpuTextureFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPixelFormatFromGPUTextureFormat(format);
    }

    public static GpuTextureFormat GetGPUTextureFormatFromPixelFormat(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGPUTextureFormatFromPixelFormat(format);
    }

    public static void GdkSuspendGPU(GpuDevice device)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GdkSuspendGPU(device);
    }

    public static void GdkResumeGPU(GpuDevice device)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GdkResumeGPU(device);
    }

    public enum GpuPrimitiveType
    {
        TriangleList,
        TriangleStrip,
        LineList,
        LineStrip,
        PointList,
    }

    public enum GpuLoadOp
    {
        Load,
        Clear,
        DontCare,
    }

    public enum GpuStoreOp
    {
        Store,
        DotCare,
        Resolve,
        ResolveAndStore,
    }

    public enum GpuIndexElementSize
    {
        Size16Bit,
        Size32Bit,
    }

    public enum GpuTextureFormat
    {
        Invalid,
        A8UNorm,
        R8UNorm,
        R8G8UNorm,
        R8G8B8A8UNorm,
        R16UNorm,
        R16G16UNorm,
        R16G16B16A16UNorm,
        R10G10B10A2UNorm,
        B5G6R5UNorm,
        B5G5R5A1UNorm,
        B4G4R4A4UNorm,
        B8G8R8A8UNorm,
        BC1RgbaUNorm,
        BC2RgbaUNorm,
        BC3RgbaUNorm,
        BC4RUNorm,
        BC5RGUNorm,
        BC7RgbaUNorm,
        BG6HRgbFloat,
        BC6HRgbUFloat,
        R8SNorm,
        R8G8SNorm,
        R8G8B8A8SNorm,
        R16SNorm,
        R16G16SNorm,
        R16G16B16A16SNorm,
        R16Float,
        R16G16Float,
        R16G16B16A16Float,
        R32Float,
        R32G32Float,
        R32G32B32A32Float,
        R11G11B10UFloat,
        R8UInt,
        R8G8UInt,
        R8G8B8A8UInt,
        R16UInt,
        R16G16UInt,
        R16G16B16A16UInt,
        R32UInt,
        R32G32UInt,
        R32G32B32A32UInt,
        R8Int,
        R8G8Int,
        R8G8B8A8Int,
        R16Int,
        R16G16Int,
        R16G16B16A16Int,
        R32Int,
        R32G32Int,
        R32G32B32A32Int,
        R8G8B8A8UNormSrgb,
        B8G8R8A8UNormSrgb,
        BC1RgbaUnormSrgb,
        BC2RgbaUNormSrgb,
        BC3RgbaUNormSrgb,
        BC7RgbaUNormSrgb,
        D16UNorm,
        D24UNorm,
        D32Float,
        D24UNormS8UInt,
        D32FloatS8UInt,
        Astc4x4UNorm,
        Astc5x4UNorm,
        Astc5x5UNorm,
        Astc6x5UNorm,
        Astc6x6UNorm,
        Astc8x5UNorm,
        Astc8x6UNorm,
        Astc8x8UNorm,
        Astc10x5UNorm,
        Astc10x6UNorm,
        Astc10x8UNorm,
        Astc10x10UNorm,
        Astc12x10UNorm,
        Astc12x12UNorm,
        Astc4x4UNormSrgb,
        Astc5x4UNormSrgb,
        Astc5x5UNormSrgb,
        Astc6x5UNormSrgb,
        Astc6x6UNormSrgb,
        Astc8x5UNormSrgb,
        Astc8x6UNormSrgb,
        Astc8x8UNormSrgb,
        Astc10x5UNormSrgb,
        Astc10x6UNormSrgb,
        Astc10x8UNormSrgb,
        Astc10x10UNormSrgb,
        Astc12x10UNormSrgb,
        Astc12x12UNormSrgb,
        Astc4x4Float,
        Astc5x4Float,
        Astc5x5Float,
        Astc6x5Float,
        Astc6x6Float,
        Astc8x5Float,
        Astc8x6Float,
        Astc8x8Float,
        Astc10x5Float,
        Astc10x6Float,
        Astc10x8Float,
        Astc10x10Float,
        Astc12x10Float,
        Astc12x12Float,
    }

    public enum GpuTextureType
    {
        Type2D,
        Type2DArray,
        Type3D,
        Cube,
        CubeArray,
    }

    public enum GpuSampleCount
    {
        Count1,
        Count2,
        Count4,
        Count8,
    }

    public enum GpuCubeMapFace
    {
        PositiveX,
        NegativeX,
        PositiveY,
        NegativeY,
        PositiveZ,
        NegativeZ,
    }

    public enum GpuTransferBufferUsage
    {
        Upload,
        Download,
    }

    public enum GpuShaderStage
    {
        Vertex,
        Fragment,
    }

    public enum GpuVertexElementFormat
    {
        Invalid,
        Int,
        Int2,
        Int3,
        Int4,
        UInt,
        UInt2,
        UInt3,
        UInt4,
        Float,
        Float2,
        Float3,
        Float4,
        Byte2,
        Byte4,
        UByte2,
        UByte4,
        Byte2Norm,
        Byte4Norm,
        UByte2Norm,
        UByte4Norm,
        Short2,
        Short4,
        UShort2,
        UShort4,
        Short2Norm,
        Short4Norm,
        UShort2Norm,
        UShort4Norm,
        Half2,
        Half4,
    }

    public enum GpuVertexInputRate
    {
        Vertex,
        Instance,
    }

    public enum GpuFillMode
    {
        Fill,
        Line,
    }

    public enum GpuCullMode
    {
        None,
        Front,
        Back,
    }

    public enum GpuFrontFace
    {
        CounterClockwise,
        Clockwise,
    }

    public enum GpuCompareOp
    {
        Invalid,
        Never,
        Less,
        Equal,
        LessOrEqual,
        Greater,
        NotEqual,
        GreaterOrEqual,
        Always,
    }

    public enum GpuStencilOp
    {
        Invalid,
        Keep,
        Zero,
        Replace,
        IncrementAndClamp,
        DecrementAndClamp,
        Invert,
        IncrementAndWrap,
        DecrementAndWrap,
    }

    public enum GpuBlendOp
    {
        Invalid,
        Add,
        Subtract,
        ReverseSubtract,
        Min,
        Max,
    }

    public enum GpuBlendFactor
    {
        Invalid,
        Zero,
        One,
        SrcColor,
        OneMinusSrcColor,
        DstColor,
        OneMinusDstColor,
        SrcAlpha,
        OneMinusSrcAlpha,
        DstAlpha,
        OneMinusDstAlpha,
        ConstantColor,
        OneMinusConstantColor,
        SrcAlphaSaturate,
    }

    public enum GpuFilter
    {
        Nearest,
        Linear,
    }

    public enum GpuSamplerMipmapMode
    {
        Nearest,
        Linear,
    }

    public enum GpuSamplerAddressMode
    {
        Repeat,
        MirroredRepeat,
        ClampToEdge,
    }

    public enum GpuPresentMode
    {
        VSync,
        Immediate,
        Mailbox,
    }

    public enum GpuSwapchainComposition
    {
        Sdr,
        SdrLinear,
        HdrExtendedLinear,
        Hdr10ST2084,
    }

    public readonly record struct GpuDevice(nint Value)
    {
        public static implicit operator nint(GpuDevice gpuDevice) => gpuDevice.Value;

        public static implicit operator GpuDevice(nint value) => new(value);
    }

    public readonly record struct GpuBuffer(nint Value)
    {
        public static implicit operator nint(GpuBuffer gpuBuffer) => gpuBuffer.Value;

        public static implicit operator GpuBuffer(nint value) => new(value);
    }

    public readonly record struct GpuTransferBuffer(nint Value)
    {
        public static implicit operator nint(GpuTransferBuffer gpuTransferBuffer) =>
            gpuTransferBuffer.Value;

        public static implicit operator GpuTransferBuffer(nint value) => new(value);
    }

    public readonly record struct GpuTexture(nint Value)
    {
        public static implicit operator nint(GpuTexture gpuTexture) => gpuTexture.Value;

        public static implicit operator GpuTexture(nint value) => new(value);
    }

    public readonly record struct GpuSampler(nint Value)
    {
        public static implicit operator nint(GpuSampler gpuSampler) => gpuSampler.Value;

        public static implicit operator GpuSampler(nint value) => new(value);
    }

    public readonly record struct GpuShader(nint Value)
    {
        public static implicit operator nint(GpuShader gpuShader) => gpuShader.Value;

        public static implicit operator GpuShader(nint value) => new(value);
    }

    public readonly record struct GpuComputePipeline(nint Value)
    {
        public static implicit operator nint(GpuComputePipeline gpuComputePipeline) =>
            gpuComputePipeline.Value;

        public static implicit operator GpuComputePipeline(nint value) => new(value);
    }

    public readonly record struct GpuGraphicsPipeline(nint Value)
    {
        public static implicit operator nint(GpuGraphicsPipeline gpuGraphicsPipeline) =>
            gpuGraphicsPipeline.Value;

        public static implicit operator GpuGraphicsPipeline(nint value) => new(value);
    }

    public readonly record struct GpuCommandBuffer(nint Value)
    {
        public static implicit operator nint(GpuCommandBuffer gpuCommandBuffer) =>
            gpuCommandBuffer.Value;

        public static implicit operator GpuCommandBuffer(nint value) => new(value);
    }

    public readonly record struct GpuRenderPass(nint Value)
    {
        public static implicit operator nint(GpuRenderPass gpuRenderPass) => gpuRenderPass.Value;

        public static implicit operator GpuRenderPass(nint value) => new(value);
    }

    public readonly record struct GpuComputePass(nint Value)
    {
        public static implicit operator nint(GpuComputePass gpuComputePass) => gpuComputePass.Value;

        public static implicit operator GpuComputePass(nint value) => new(value);
    }

    public readonly record struct GpuCopyPass(nint Value)
    {
        public static implicit operator nint(GpuCopyPass gpuCopyPass) => gpuCopyPass.Value;

        public static implicit operator GpuCopyPass(nint value) => new(value);
    }

    public readonly record struct GpuFence(nint Value)
    {
        public static implicit operator nint(GpuFence gpuFence) => gpuFence.Value;

        public static implicit operator GpuFence(nint value) => new(value);
    }

    public readonly record struct GpuTextureUsageFlags(uint Value)
    {
        public static GpuTextureUsageFlags operator |(
            GpuTextureUsageFlags left,
            GpuTextureUsageFlags right
        ) => new(left.Value | right.Value);

        public static GpuTextureUsageFlags operator &(
            GpuTextureUsageFlags left,
            GpuTextureUsageFlags right
        ) => new(left.Value & right.Value);

        public static GpuTextureUsageFlags operator ^(
            GpuTextureUsageFlags left,
            GpuTextureUsageFlags right
        ) => new(left.Value ^ right.Value);

        public static GpuTextureUsageFlags operator ~(GpuTextureUsageFlags value) =>
            new(~value.Value);

        public static implicit operator uint(GpuTextureUsageFlags gpuTextureUsageFlags) =>
            gpuTextureUsageFlags.Value;

        public static implicit operator GpuTextureUsageFlags(uint value) => new(value);
    }

    public readonly record struct GpuBufferUsageFlags(uint Value)
    {
        public static GpuBufferUsageFlags operator |(
            GpuBufferUsageFlags left,
            GpuBufferUsageFlags right
        ) => new(left.Value | right.Value);

        public static GpuBufferUsageFlags operator &(
            GpuBufferUsageFlags left,
            GpuBufferUsageFlags right
        ) => new(left.Value & right.Value);

        public static GpuBufferUsageFlags operator ^(
            GpuBufferUsageFlags left,
            GpuBufferUsageFlags right
        ) => new(left.Value ^ right.Value);

        public static GpuBufferUsageFlags operator ~(GpuBufferUsageFlags value) =>
            new(~value.Value);

        public static implicit operator uint(GpuBufferUsageFlags gpuBufferUsageFlags) =>
            gpuBufferUsageFlags.Value;

        public static implicit operator GpuBufferUsageFlags(uint value) => new(value);
    }

    public readonly record struct GpuShaderFormat(uint Value)
    {
        public static GpuShaderFormat operator |(GpuShaderFormat left, GpuShaderFormat right) =>
            new(left.Value | right.Value);

        public static GpuShaderFormat operator &(GpuShaderFormat left, GpuShaderFormat right) =>
            new(left.Value & right.Value);

        public static GpuShaderFormat operator ^(GpuShaderFormat left, GpuShaderFormat right) =>
            new(left.Value ^ right.Value);

        public static GpuShaderFormat operator ~(GpuShaderFormat value) => new(~value.Value);

        public static implicit operator uint(GpuShaderFormat gpuShaderFormat) =>
            gpuShaderFormat.Value;

        public static implicit operator GpuShaderFormat(uint value) => new(value);
    }

    public readonly record struct GpuColorComponentFlags(byte Value)
    {
        public static GpuColorComponentFlags operator |(
            GpuColorComponentFlags left,
            GpuColorComponentFlags right
        ) => new((byte)(left.Value | right.Value));

        public static GpuColorComponentFlags operator &(
            GpuColorComponentFlags left,
            GpuColorComponentFlags right
        ) => new((byte)(left.Value & right.Value));

        public static GpuColorComponentFlags operator ^(
            GpuColorComponentFlags left,
            GpuColorComponentFlags right
        ) => new((byte)(left.Value ^ right.Value));

        public static GpuColorComponentFlags operator ~(GpuColorComponentFlags value) =>
            new((byte)(~value.Value));

        public static implicit operator byte(GpuColorComponentFlags gpuColorComponentFlags) =>
            gpuColorComponentFlags.Value;

        public static implicit operator GpuColorComponentFlags(byte value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuViewport
    {
        public float X;
        public float Y;
        public float W;
        public float H;
        public float MinDepth;
        public float MaxDepth;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuTextureTransferInfo
    {
        public GpuTransferBuffer TransferBuffer;
        public uint Offset;
        public uint PixelsPerRow;
        public uint RowsPerLayer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuTransferBufferLocation
    {
        public GpuTransferBuffer TransferBuffer;
        public uint Offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuTextureLocation
    {
        public GpuTexture Texture;
        public uint MipLevel;
        public uint Layer;
        public uint X;
        public uint Y;
        public uint Z;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuTextureRegion
    {
        public GpuTexture Texture;
        public uint MipLevel;
        public uint Layer;
        public uint X;
        public uint Y;
        public uint Z;
        public uint W;
        public uint H;
        public uint D;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuBlitRegion
    {
        public GpuTexture Texture;
        public uint MipLevel;
        public uint LayerOrDepthPlane;
        public uint X;
        public uint Y;
        public uint Z;
        public uint W;
        public uint H;
        public uint D;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuBufferLocation
    {
        public GpuBuffer Buffer;
        public uint Offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuBufferRegion
    {
        public GpuBuffer Buffer;
        public uint Offset;
        public uint Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuIndirectDrawCommand
    {
        public uint NumVertices;
        public uint NumInstances;
        public uint FirstVertex;
        public uint FirstInstance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuIndexedIndirectDrawCommand
    {
        public uint NumIndices;
        public uint NumInstances;
        public uint FirstIndex;
        public int VertexOffset;
        public uint FirstInstance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuIndirectDispatchCommand
    {
        public uint GroupCountX;
        public uint GroupCountY;
        public uint GroupCountZ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuSamplerCreateInfo
    {
        public GpuFilter MinFilter;
        public GpuFilter MagFilter;
        public GpuSamplerMipmapMode MipmapMode;
        public GpuSamplerAddressMode AddressModeU;
        public GpuSamplerAddressMode AddressModeV;
        public GpuSamplerAddressMode AddressModeW;
        public float MipLodBias;
        public float MaxAnisotropy;
        public GpuCompareOp CompareOp;
        public float MinLod;
        public float MaxLod;
        public Bool EnableAnisotropy;
        public Bool EnableCompare;
        private readonly byte _padding1;
        private readonly byte _padding2;
        public PropertiesID Props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuVertexBufferDescription
    {
        public uint Slot;
        public uint Pitch;
        public GpuVertexInputRate InputRate;
        public uint InstanceStepRate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuVertexAttribute
    {
        public uint Location;
        public uint BufferSlot;
        public GpuVertexElementFormat Format;
        public uint Offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuVertexInputState
    {
        public GpuVertexBufferDescription* VertexBuffers;
        public uint NumVertexBuffers;
        public GpuVertexAttribute* VertexAttributes;
        public uint NumVertexAttributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuStencilOpState
    {
        public GpuStencilOp FailOp;
        public GpuStencilOp PassOp;
        public GpuStencilOp DepthFailOp;
        public GpuCompareOp CompareOp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuColorTargetBlendState
    {
        public GpuBlendFactor SrcColorBlendFactor;
        public GpuBlendFactor DstColorBlendFactor;
        public GpuBlendOp ColorBlendOp;
        public GpuBlendFactor SrcAlphaBlendFactor;
        public GpuBlendFactor DstAlphaBlendFactor;
        public GpuBlendOp AlphaBlendOp;
        public GpuColorComponentFlags ColorWriteMask;
        public Bool EnableBlend;
        public Bool EnableColorWriteMask;
        private readonly byte _padding1;
        private readonly byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuShaderCreateInfo
    {
        public nuint CodeSize;
        public byte* Code;
        public byte* EntryPoint;
        public GpuShaderFormat Format;
        public GpuShaderStage Stage;
        public uint NumSamplers;
        public uint NumStorageTextures;
        public uint NumStorageBuffers;
        public uint NumUniformBuffers;
        public PropertiesID Props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuTextureCreateInfo
    {
        public GpuTextureType Type;
        public GpuTextureFormat Format;
        public GpuTextureUsageFlags Usage;
        public uint Width;
        public uint Height;
        public uint LayerCountOrDepth;
        public uint NumLevels;
        public GpuSampleCount SampleCount;
        public PropertiesID Props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuBufferCreateInfo
    {
        public GpuBufferUsageFlags Usage;
        public uint Size;
        public PropertiesID Props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuTransferBufferCreateInfo
    {
        public GpuTransferBufferUsage Usage;
        public uint Size;
        public PropertiesID Props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuRasterizerState
    {
        public GpuFillMode FillMode;
        public GpuCullMode CullMode;
        public GpuFrontFace FrontFace;
        public float DepthBiasConstantFactor;
        public float DepthBiasClamp;
        public float DepthBiasSlopeFactor;
        public Bool EnableDepthBias;
        public Bool EnableDepthClip;
        private readonly byte _padding1;
        private readonly byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuMultisampleState
    {
        public GpuSampleCount SampleCount;
        public uint SampleMask;
        public Bool EnableMask;
        public Bool EnableAlphaToCoverage;
        private readonly byte _padding1;
        private readonly byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuDepthStencilState
    {
        public GpuCompareOp CompareOp;
        public GpuStencilOpState BackStencilState;
        public GpuStencilOpState FrontStencilState;
        public byte CompareMask;
        public byte WriteMask;
        public Bool EnableDepthTest;
        public Bool EnableDepthWrite;
        public Bool EnableStencilTest;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuColorTargetDescription
    {
        public GpuTextureFormat Format;
        public GpuColorTargetBlendState BlendState;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuGraphicsPipelineTargetInfo
    {
        public GpuColorTargetDescription* ColorTargetDescriptions;
        public uint NumColorTargets;
        public GpuTextureFormat DepthStencilFormat;
        public Bool HasDepthStencilTarget;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuGraphicsPipelineCreateInfo
    {
        public GpuShader VertexShader;
        public GpuShader FragmentShader;
        public GpuVertexInputState VertexInputState;
        public GpuPrimitiveType PrimitiveType;
        public GpuRasterizerState RasterizerState;
        public GpuMultisampleState MultisampleState;
        public GpuDepthStencilState DepthStencilState;
        public GpuGraphicsPipelineTargetInfo TargetInfo;
        public PropertiesID Props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuComputePipelineCreateInfo
    {
        public nuint CodeSize;
        public byte* Code;
        public byte* EntryPoint;
        public GpuShaderFormat Format;
        public uint NumSamplers;
        public uint NumReadOnlyStorageTextures;
        public uint NumReadOnlyStorageBuffers;
        public uint NumReadWriteStorageTextures;
        public uint NumReadWriteStorageBuffers;
        public uint NumUniformBuffers;
        public uint ThreadCountX;
        public uint ThreadCountY;
        public uint ThreadCountZ;
        public PropertiesID Props;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuColorTargetInfo
    {
        public GpuTexture Texture;
        public uint MipLevel;
        public uint LayerOrDepthPlane;
        public FColor CLearColor;
        public GpuLoadOp LoadOp;
        public GpuStoreOp StoreOp;
        public GpuTexture ResolveTexture;
        public uint ResolveMipLevel;
        public uint ResolveLayer;
        public Bool Cycle;
        public Bool CycleResolveTexture;
        private readonly byte _padding1;
        private readonly byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuDepthStencilTargetInfo
    {
        public GpuTexture Texture;
        public float ClearDepth;
        public GpuLoadOp LoadOp;
        public GpuStoreOp StoreOp;
        public GpuLoadOp StencilLoadOp;
        public GpuStoreOp StencilStoreOp;
        public Bool Cycle;
        public byte ClearStencil;
        public byte MipLevel;
        public byte Layer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuBlitInfo
    {
        public GpuBlitRegion Source;
        public GpuBlitRegion Destination;
        public GpuLoadOp LoadOp;
        public FColor ClearColor;
        public FlipMode FlipMode;
        public GpuFilter Filter;
        public Bool Cycle;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuBufferBinding
    {
        public GpuBuffer Buffer;
        public uint Offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuTextureSamplerBinding
    {
        public GpuTexture Texture;
        public GpuSampler Sampler;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuStorageBufferReadWriteBinding
    {
        public GpuBuffer Buffer;
        public Bool Cycle;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuStorageTextureReadWriteBinding
    {
        public GpuTexture Texture;
        public uint MipLevel;
        public uint Layer;
        public Bool Cycle;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GpuVulkanOptions
    {
        public uint VulkanApiVersion;
        public void* FeatureList;
        public void* Vulkan10PhysicalDeviceFeatures;
        public uint DeviceExtensionCount;
        public byte** DeviceExtensionNames;
        public uint InstanceExtensionCount;
        public byte** InstanceExtensionNames;
    }
}
