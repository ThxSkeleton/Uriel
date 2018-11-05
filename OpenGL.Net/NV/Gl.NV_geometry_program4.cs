
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
		/// [GL] Value of GL_GEOMETRY_PROGRAM_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_geometry_program4")]
		public const int GEOMETRY_PROGRAM_NV = 0x8C26;

		/// <summary>
		/// [GL] Value of GL_MAX_PROGRAM_OUTPUT_VERTICES_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_geometry_program4")]
		public const int MAX_PROGRAM_OUTPUT_VERTICES_NV = 0x8C27;

		/// <summary>
		/// [GL] Value of GL_MAX_PROGRAM_TOTAL_OUTPUT_COMPONENTS_NV symbol.
		/// </summary>
		[RequiredByFeature("GL_NV_geometry_program4")]
		public const int MAX_PROGRAM_TOTAL_OUTPUT_COMPONENTS_NV = 0x8C28;

		/// <summary>
		/// [GL] glProgramVertexLimitNV: Binding for glProgramVertexLimitNV.
		/// </summary>
		/// <param name="target">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="limit">
		/// A <see cref="T:int"/>.
		/// </param>
		[RequiredByFeature("GL_NV_geometry_program4")]
		public static void ProgramVertexLimitNV(int target, int limit)
		{
			Debug.Assert(Delegates.pglProgramVertexLimitNV != null, "pglProgramVertexLimitNV not implemented");
			Delegates.pglProgramVertexLimitNV(target, limit);
			LogCommand("glProgramVertexLimitNV", null, target, limit			);
			DebugCheckErrors(null);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_NV_geometry_program4")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glProgramVertexLimitNV(int target, int limit);

			[RequiredByFeature("GL_NV_geometry_program4")]
			[ThreadStatic]
			internal static glProgramVertexLimitNV pglProgramVertexLimitNV;

		}
	}

}
