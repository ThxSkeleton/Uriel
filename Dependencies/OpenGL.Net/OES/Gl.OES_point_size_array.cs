
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
		/// <para>
		/// [GLES1.1] Gl.EnableClientState: If enabled, the point size array controls the sizes used to render points and point 
		/// sprites. In this case the point size defined by Gl.PointSize is ignored. The point sizes supplied in the point size 
		/// arrays will be the sizes used to render both points and point sprites. See Gl.PointSize
		/// </para>
		/// <para>
		/// [GLES1.1] Gl.Get: params returns a single boolean value indicating whether the point size array is enabled. The initial 
		/// value is Gl.FALSE. See Gl.PointSizePointerOES.
		/// </para>
		/// </summary>
		[RequiredByFeature("GL_OES_point_size_array", Api = "gles1")]
		public const int POINT_SIZE_ARRAY_OES = 0x8B9C;

		/// <summary>
		/// [GLES1.1] Gl.Get: params returns one value, the data type of each point size in the point array. See 
		/// Gl.PointSizePointerOES.
		/// </summary>
		[RequiredByFeature("GL_OES_point_size_array", Api = "gles1")]
		public const int POINT_SIZE_ARRAY_TYPE_OES = 0x898A;

		/// <summary>
		/// [GLES1.1] Gl.Get: params returns one value, the byte offset between consecutive point sizes in the point size array. See 
		/// Gl.PointSizePointerOES.
		/// </summary>
		[RequiredByFeature("GL_OES_point_size_array", Api = "gles1")]
		public const int POINT_SIZE_ARRAY_STRIDE_OES = 0x898B;

		/// <summary>
		/// [GL] Value of GL_POINT_SIZE_ARRAY_POINTER_OES symbol.
		/// </summary>
		[RequiredByFeature("GL_OES_point_size_array", Api = "gles1")]
		public const int POINT_SIZE_ARRAY_POINTER_OES = 0x898C;

		/// <summary>
		/// [GLES1.1] Gl.Get: params returns one value, the point size array buffer binding. See Gl.PointSizePointerOES.
		/// </summary>
		[RequiredByFeature("GL_OES_point_size_array", Api = "gles1")]
		public const int POINT_SIZE_ARRAY_BUFFER_BINDING_OES = 0x8B9F;

		/// <summary>
		/// [GLES1.1] glPointSizePointerOES: define an array of point sizes
		/// </summary>
		/// <param name="type">
		/// Specifies the data type of each point size in the array. Symbolic constant Gl.FIXED is accepted. However, the common 
		/// profile also accepts the symbolic constant Gl.FLOAT. The initial value is Gl.FIXED for the common lite profile, or 
		/// Gl.FLOAT for the common profile.
		/// </param>
		/// <param name="stride">
		/// Specifies the byte offset between consecutive point sizes. If <paramref name="stride"/> is 0, the point sizes are 
		/// understood to be tightly packed in the array. The initial value is Gl..
		/// </param>
		/// <param name="pointer">
		/// Specifies a pointer to the point size of the first vertex in the array. The initial value is Gl..
		/// </param>
		[RequiredByFeature("GL_OES_point_size_array", Api = "gles1")]
		public static void PointSizePointerOES(int type, int stride, IntPtr pointer)
		{
			Debug.Assert(Delegates.pglPointSizePointerOES != null, "pglPointSizePointerOES not implemented");
			Delegates.pglPointSizePointerOES(type, stride, pointer);
			LogCommand("glPointSizePointerOES", null, type, stride, pointer			);
			DebugCheckErrors(null);
		}

		/// <summary>
		/// [GLES1.1] glPointSizePointerOES: define an array of point sizes
		/// </summary>
		/// <param name="type">
		/// Specifies the data type of each point size in the array. Symbolic constant Gl.FIXED is accepted. However, the common 
		/// profile also accepts the symbolic constant Gl.FLOAT. The initial value is Gl.FIXED for the common lite profile, or 
		/// Gl.FLOAT for the common profile.
		/// </param>
		/// <param name="stride">
		/// Specifies the byte offset between consecutive point sizes. If <paramref name="stride"/> is 0, the point sizes are 
		/// understood to be tightly packed in the array. The initial value is Gl..
		/// </param>
		/// <param name="pointer">
		/// Specifies a pointer to the point size of the first vertex in the array. The initial value is Gl..
		/// </param>
		[RequiredByFeature("GL_OES_point_size_array", Api = "gles1")]
		public static void PointSizePointerOES(int type, int stride, object pointer)
		{
			GCHandle pin_pointer = GCHandle.Alloc(pointer, GCHandleType.Pinned);
			try {
				PointSizePointerOES(type, stride, pin_pointer.AddrOfPinnedObject());
			} finally {
				pin_pointer.Free();
			}
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_OES_point_size_array", Api = "gles1")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glPointSizePointerOES(int type, int stride, IntPtr pointer);

			[RequiredByFeature("GL_OES_point_size_array", Api = "gles1")]
			[ThreadStatic]
			internal static glPointSizePointerOES pglPointSizePointerOES;

		}
	}

}