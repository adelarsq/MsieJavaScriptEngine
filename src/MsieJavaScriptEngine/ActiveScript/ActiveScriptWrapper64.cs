﻿#if !NETSTANDARD1_3
using System;

using EXCEPINFO = System.Runtime.InteropServices.ComTypes.EXCEPINFO;

using MsieJavaScriptEngine.ActiveScript.Debugging;
using MsieJavaScriptEngine.Helpers;

namespace MsieJavaScriptEngine.ActiveScript
{
	/// <summary>
	/// 64-bit Active Script wrapper
	/// </summary>
	internal sealed class ActiveScriptWrapper64 : ActiveScriptWrapperBase
	{
		/// <summary>
		/// Pointer to an instance of 64-bit Active Script parser
		/// </summary>
		private IntPtr _pActiveScriptParse64;

		/// <summary>
		/// Pointer to an instance of 64-bit Active Script debugger
		/// </summary>
		private IntPtr _pActiveScriptDebug64;

		/// <summary>
		/// Pointer to an instance of 64-bit debug stack frame sniffer
		/// </summary>
		private IntPtr _pDebugStackFrameSniffer64;

		/// <summary>
		/// Instance of 64-bit Active Script parser
		/// </summary>
		private IActiveScriptParse64 _activeScriptParse64;

		/// <summary>
		/// Instance of 64-bit debug stack frame sniffer
		/// </summary>
		private IDebugStackFrameSnifferEx64 _debugStackFrameSniffer64;


		/// <summary>
		/// Constructs an instance of the 64-bit Active Script wrapper
		/// </summary>
		/// <param name="engineMode">JS engine mode</param>
		/// <param name="enableDebugging">Flag for whether to enable script debugging features</param>
		public ActiveScriptWrapper64(JsEngineMode engineMode, bool enableDebugging)
			: base(engineMode, enableDebugging)
		{
			_pActiveScriptParse64 = ComHelpers.QueryInterface<IActiveScriptParse64>(_pActiveScript);
			_activeScriptParse64 = (IActiveScriptParse64)_activeScript;

			if (_enableDebugging)
			{
				_pActiveScriptDebug64 = ComHelpers.QueryInterface<IActiveScriptDebug64>(_pActiveScript);
				if (engineMode == JsEngineMode.Classic)
				{
					_pDebugStackFrameSniffer64 = ComHelpers.QueryInterfaceNoThrow<IDebugStackFrameSnifferEx64>(
						_pActiveScript);
					_debugStackFrameSniffer64 = _activeScript as IDebugStackFrameSnifferEx64;
				}
			}
		}


		#region ActiveScriptWrapperBase overrides

		protected override uint InnerEnumCodeContextsOfPosition(UIntPtr sourceContext, uint offset,
			uint length, out IEnumDebugCodeContexts enumContexts)
		{
			var del = ComHelpers.GetMethodDelegate<RawEnumCodeContextsOfPosition64>(_pActiveScriptDebug64,5);
			uint result = del(_pActiveScriptDebug64, sourceContext.ToUInt64(), offset, length,
				out enumContexts);

			return result;
		}

		#region IActiveScriptWrapper implementation

		public override void InitNew()
		{
			_activeScriptParse64.InitNew();
		}

		public override object ParseScriptText(string code, string itemName, object context,
			string delimiter, UIntPtr sourceContextCookie, uint startingLineNumber, ScriptTextFlags flags)
		{
			object result;
			EXCEPINFO exceptionInfo;

			_activeScriptParse64.ParseScriptText(
				code,
				itemName,
				context,
				delimiter,
				sourceContextCookie,
				startingLineNumber,
				flags,
				out result,
				out exceptionInfo);

			return result;
		}

		public override void EnumStackFrames(out IEnumDebugStackFrames enumFrames)
		{
			if (_debugStackFrameSniffer64 != null)
			{
				_debugStackFrameSniffer64.EnumStackFrames(out enumFrames);
			}
			else
			{
				enumFrames = new NullEnumDebugStackFrames();
			}
		}

		#endregion

		#region IDisposable implementation

		public override void Dispose()
		{
			if (_disposedFlag.Set())
			{
				_debugStackFrameSniffer64 = null;
				_activeScriptParse64 = null;

				ComHelpers.ReleaseAndEmpty(ref _pDebugStackFrameSniffer64);
				ComHelpers.ReleaseAndEmpty(ref _pActiveScriptDebug64);
				ComHelpers.ReleaseAndEmpty(ref _pActiveScriptParse64);

				base.Dispose();
			}
		}

		#endregion

		#endregion
	}
}
#endif