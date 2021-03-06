#pragma once

#ifdef _WIN32
#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif // !WIN32_LEAN_AND_MEAN
#include <windows.h>
#define XR_USE_GRAPHICS_API_D3D11
#include <d3d11.h>
#endif

#include <vulkan/vulkan.h>
#define XR_USE_GRAPHICS_API_VULKAN

struct IUnityXRTrace;
extern IUnityXRTrace* s_Trace;

#if DEBUG_LOG_EVERY_FUNC_CALL
#define LOG_FUNC() s_Trace->Trace(kXRLogTypeDebug, "[Mock] %s\n", __FUNCTION__)
#else
#define LOG_FUNC()
#endif

#if DEBUG_TRACE
#define TRACE(STRING, ...) s_Trace->Trace(kXRLogTypeDebug, STRING, ##__VA_ARGS__)
#else
#define TRACE(STRING, ...)
#endif

#define XR_NO_PROTOTYPES
#include "openxr/openxr.h"
#include "openxr/openxr_platform.h"
