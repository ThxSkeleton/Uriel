
// MIT License
// 
// Copyright (c) 2009-2017 Luca Piccioni
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// This file is automatically generated

#pragma warning disable 649, 1572, 1573

// ReSharper disable RedundantUsingDirective
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

using Khronos;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable JoinDeclarationAndInitializer

namespace OpenGL
{
	public partial class Gl
	{
		/// <summary>
		/// [GL] Value of GL_HANDLE_TYPE_D3D12_TILEPOOL_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
		public const int HANDLE_TYPE_D3D12_TILEPOOL_EXT = 0x9589;

		/// <summary>
		/// [GL] Value of GL_HANDLE_TYPE_D3D12_RESOURCE_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
		public const int HANDLE_TYPE_D3D12_RESOURCE_EXT = 0x958A;

		/// <summary>
		/// [GL] Value of GL_HANDLE_TYPE_D3D11_IMAGE_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
		public const int HANDLE_TYPE_D3D11_IMAGE_EXT = 0x958B;

		/// <summary>
		/// [GL] Value of GL_HANDLE_TYPE_D3D11_IMAGE_KMT_EXT symbol.
		/// </summary>
		[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
		public const int HANDLE_TYPE_D3D11_IMAGE_KMT_EXT = 0x958C;

		/// <summary>
		/// [GL] glImportMemoryWin32HandleEXT: Binding for glImportMemoryWin32HandleEXT.
		/// </summary>
		/// <param name="memory">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="size">
		/// A <see cref="T:ulong"/>.
		/// </param>
		/// <param name="handleType">
		/// A <see cref="T:ExternalHandleType"/>.
		/// </param>
		/// <param name="handle">
		/// A <see cref="T:IntPtr"/>.
		/// </param>
		[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
		public static void ImportMemoryWin32HandleEXT(uint memory, ulong size, ExternalHandleType handleType, IntPtr handle)
		{
			Debug.Assert(Delegates.pglImportMemoryWin32HandleEXT != null, "pglImportMemoryWin32HandleEXT not implemented");
			Delegates.pglImportMemoryWin32HandleEXT(memory, size, (int)handleType, handle);
			LogCommand("glImportMemoryWin32HandleEXT", null, memory, size, handleType, handle			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glImportMemoryWin32NameEXT: Binding for glImportMemoryWin32NameEXT.
		/// </summary>
		/// <param name="memory">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="size">
		/// A <see cref="T:ulong"/>.
		/// </param>
		/// <param name="handleType">
		/// A <see cref="T:ExternalHandleType"/>.
		/// </param>
		/// <param name="name">
		/// A <see cref="T:IntPtr"/>.
		/// </param>
		[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
		public static void ImportMemoryWin32NameEXT(uint memory, ulong size, ExternalHandleType handleType, IntPtr name)
		{
			Debug.Assert(Delegates.pglImportMemoryWin32NameEXT != null, "pglImportMemoryWin32NameEXT not implemented");
			Delegates.pglImportMemoryWin32NameEXT(memory, size, (int)handleType, name);
			LogCommand("glImportMemoryWin32NameEXT", null, memory, size, handleType, name			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GL] glImportMemoryWin32NameEXT: Binding for glImportMemoryWin32NameEXT.
		/// </summary>
		/// <param name="memory">
		/// A <see cref="T:uint"/>.
		/// </param>
		/// <param name="size">
		/// A <see cref="T:ulong"/>.
		/// </param>
		/// <param name="handleType">
		/// A <see cref="T:ExternalHandleType"/>.
		/// </param>
		/// <param name="name">
		/// A <see cref="T:object"/>.
		/// </param>
		[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
		public static void ImportMemoryWin32NameEXT(uint memory, ulong size, ExternalHandleType handleType, object name)
		{
			GCHandle pin_name = GCHandle.Alloc(name, GCHandleType.Pinned);
			try {
				ImportMemoryWin32NameEXT(memory, size, handleType, pin_name.AddrOfPinnedObject());
			} finally {
				pin_name.Free();
			}
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glImportMemoryWin32HandleEXT(uint memory, ulong size, int handleType, IntPtr handle);

			[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
			[ThreadStatic]
			internal static glImportMemoryWin32HandleEXT pglImportMemoryWin32HandleEXT;

			[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glImportMemoryWin32NameEXT(uint memory, ulong size, int handleType, IntPtr name);

			[RequiredByFeature("GL_EXT_memory_object_win32", Api = "gl|gles2")]
			[ThreadStatic]
			internal static glImportMemoryWin32NameEXT pglImportMemoryWin32NameEXT;

		}
	}

}