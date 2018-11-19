
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
		/// [GL] Value of GL_TEXTURE_SPARSE_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		public const int TEXTURE_SPARSE_ARB = 0x91A6;

		/// <summary>
		/// [GL] Value of GL_VIRTUAL_PAGE_SIZE_INDEX_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		public const int VIRTUAL_PAGE_SIZE_INDEX_ARB = 0x91A7;

		/// <summary>
		/// [GL] Value of GL_NUM_SPARSE_LEVELS_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		public const int NUM_SPARSE_LEVELS_ARB = 0x91AA;

		/// <summary>
		/// [GL] Value of GL_NUM_VIRTUAL_PAGE_SIZES_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		public const int NUM_VIRTUAL_PAGE_SIZES_ARB = 0x91A8;

		/// <summary>
		/// [GL] Value of GL_VIRTUAL_PAGE_SIZE_X_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		[RequiredByFeature("GL_AMD_sparse_texture")]
		public const int VIRTUAL_PAGE_SIZE_X_ARB = 0x9195;

		/// <summary>
		/// [GL] Value of GL_VIRTUAL_PAGE_SIZE_Y_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		[RequiredByFeature("GL_AMD_sparse_texture")]
		public const int VIRTUAL_PAGE_SIZE_Y_ARB = 0x9196;

		/// <summary>
		/// [GL] Value of GL_VIRTUAL_PAGE_SIZE_Z_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		[RequiredByFeature("GL_AMD_sparse_texture")]
		public const int VIRTUAL_PAGE_SIZE_Z_ARB = 0x9197;

		/// <summary>
		/// [GL] Value of GL_MAX_SPARSE_TEXTURE_SIZE_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		[RequiredByFeature("GL_AMD_sparse_texture")]
		public const int MAX_SPARSE_TEXTURE_SIZE_ARB = 0x9198;

		/// <summary>
		/// [GL] Value of GL_MAX_SPARSE_3D_TEXTURE_SIZE_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		[RequiredByFeature("GL_AMD_sparse_texture")]
		public const int MAX_SPARSE_3D_TEXTURE_SIZE_ARB = 0x9199;

		/// <summary>
		/// [GL] Value of GL_SPARSE_TEXTURE_FULL_ARRAY_CUBE_MIPMAPS_ARB symbol.
		/// </summary>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		public const int SPARSE_TEXTURE_FULL_ARRAY_CUBE_MIPMAPS_ARB = 0x91A9;

		/// <summary>
		/// [GL] glTexPageCommitmentARB: Binding for glTexPageCommitmentARB.
		/// </summary>
		/// <param name="target">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="level">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="xoffset">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="yoffset">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="zoffset">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="width">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="height">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="depth">
		/// A <see cref="T:int"/>.
		/// </param>
		/// <param name="commit">
		/// A <see cref="T:bool"/>.
		/// </param>
		[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
		[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
		public static void TexPageCommitmentARB(int target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, bool commit)
		{
			Debug.Assert(Delegates.pglTexPageCommitmentARB != null, "pglTexPageCommitmentARB not implemented");
			Delegates.pglTexPageCommitmentARB(target, level, xoffset, yoffset, zoffset, width, height, depth, commit);
			LogCommand("glTexPageCommitmentARB", null, target, level, xoffset, yoffset, zoffset, width, height, depth, commit			);
			DebugCheckErrors(null);
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
			[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate void glTexPageCommitmentARB(int target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, [MarshalAs(UnmanagedType.I1)] bool commit);

			[RequiredByFeature("GL_ARB_sparse_texture", Api = "gl|glcore")]
			[RequiredByFeature("GL_EXT_sparse_texture", Api = "gles2", EntryPoint = "glTexPageCommitmentEXT")]
			[ThreadStatic]
			internal static glTexPageCommitmentARB pglTexPageCommitmentARB;

		}
	}

}