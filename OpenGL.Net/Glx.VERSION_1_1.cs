
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
	public partial class Glx
	{
		/// <summary>
		/// [GLX] Value of GLX_VENDOR symbol.
		/// </summary>
		[RequiredByFeature("GLX_VERSION_1_1")]
		public const int VENDOR = 0x1;

		/// <summary>
		/// [GLX] Value of GLX_VERSION symbol.
		/// </summary>
		[RequiredByFeature("GLX_VERSION_1_1")]
		public const int VERSION = 0x2;

		/// <summary>
		/// [GLX] Value of GLX_EXTENSIONS symbol.
		/// </summary>
		[RequiredByFeature("GLX_VERSION_1_1")]
		public const int EXTENSIONS = 0x3;

		/// <summary>
		/// [GL2.1] glXQueryExtensionsString: return list of supported extensions
		/// </summary>
		/// <param name="dpy">
		/// Specifies the connection to the X server.
		/// </param>
		/// <param name="screen">
		/// Specifies the screen number.
		/// </param>
		[RequiredByFeature("GLX_VERSION_1_1")]
		public static string QueryExtensionsString(IntPtr dpy, int screen)
		{
			IntPtr retValue;

			Debug.Assert(Delegates.pglXQueryExtensionsString != null, "pglXQueryExtensionsString not implemented");
			retValue = Delegates.pglXQueryExtensionsString(dpy, screen);
			LogCommand("glXQueryExtensionsString", PtrToString(retValue), dpy, screen			);
			DebugCheckErrors(retValue);

			return (PtrToString(retValue));
		}

		/// <summary>
		/// [GL2.1] glXQueryServerString: return string describing the server
		/// </summary>
		/// <param name="dpy">
		/// Specifies the connection to the X server.
		/// </param>
		/// <param name="screen">
		/// Specifies the screen number.
		/// </param>
		/// <param name="name">
		/// Specifies which string is returned: one of Glx.VENDOR, Glx.VERSION, or Glx.EXTENSIONS.
		/// </param>
		[RequiredByFeature("GLX_VERSION_1_1")]
		public static string QueryServerString(IntPtr dpy, int screen, int name)
		{
			IntPtr retValue;

			Debug.Assert(Delegates.pglXQueryServerString != null, "pglXQueryServerString not implemented");
			retValue = Delegates.pglXQueryServerString(dpy, screen, name);
			LogCommand("glXQueryServerString", PtrToString(retValue), dpy, screen, name			);
			DebugCheckErrors(retValue);

			return (PtrToString(retValue));
		}

		/// <summary>
		/// [GL2.1] glXGetClientString: return a string describing the client
		/// </summary>
		/// <param name="dpy">
		/// Specifies the connection to the X server.
		/// </param>
		/// <param name="name">
		/// Specifies which string is returned. The symbolic constants Glx.VENDOR, Glx.VERSION, and Glx.EXTENSIONS are accepted.
		/// </param>
		[RequiredByFeature("GLX_VERSION_1_1")]
		public static string GetClientString(IntPtr dpy, int name)
		{
			IntPtr retValue;

			Debug.Assert(Delegates.pglXGetClientString != null, "pglXGetClientString not implemented");
			retValue = Delegates.pglXGetClientString(dpy, name);
			LogCommand("glXGetClientString", PtrToString(retValue), dpy, name			);
			DebugCheckErrors(retValue);

			return (PtrToString(retValue));
		}

		internal static unsafe partial class Delegates
		{
			[RequiredByFeature("GLX_VERSION_1_1")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate IntPtr glXQueryExtensionsString(IntPtr dpy, int screen);

			[RequiredByFeature("GLX_VERSION_1_1")]
			internal static glXQueryExtensionsString pglXQueryExtensionsString;

			[RequiredByFeature("GLX_VERSION_1_1")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate IntPtr glXQueryServerString(IntPtr dpy, int screen, int name);

			[RequiredByFeature("GLX_VERSION_1_1")]
			internal static glXQueryServerString pglXQueryServerString;

			[RequiredByFeature("GLX_VERSION_1_1")]
			[SuppressUnmanagedCodeSecurity]
			internal delegate IntPtr glXGetClientString(IntPtr dpy, int name);

			[RequiredByFeature("GLX_VERSION_1_1")]
			internal static glXGetClientString pglXGetClientString;

		}
	}

}
