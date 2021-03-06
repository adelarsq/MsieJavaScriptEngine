﻿using System;

using MsieJavaScriptEngine.Helpers;
using MsieJavaScriptEngine.Utilities;

namespace MsieJavaScriptEngine
{
	/// <summary>
	/// Base class of the inner JS engine
	/// </summary>
	internal abstract class InnerJsEngineBase : IInnerJsEngine
	{
		/// <summary>
		/// JS engine settings
		/// </summary>
		protected JsEngineSettings _settings;

		/// <summary>
		/// Name of JS engine mode
		/// </summary>
		protected readonly string _engineModeName;

		/// <summary>
		/// Flag that object is destroyed
		/// </summary>
		protected StatedFlag _disposedFlag = new StatedFlag();


		/// <summary>
		/// Constructs an instance of the inner JS engine
		/// </summary>
		/// <param name="settings">JS engine settings</param>
		protected InnerJsEngineBase(JsEngineSettings settings)
		{
			_settings = settings;
			_engineModeName = JsEngineModeHelpers.GetModeName(_settings.EngineMode);
		}


		#region IInnerJsEngine implementation

		public string Mode
		{
			get { return _engineModeName; }
		}

		public abstract bool SupportsScriptPrecompilation { get; }


		public abstract PrecompiledScript Precompile(string code, string documentName);

		public abstract object Evaluate(string expression, string documentName);

		public abstract void Execute(string code, string documentName);

		public abstract void Execute(PrecompiledScript precompiledScript);

		public abstract object CallFunction(string functionName, params object[] args);

		public abstract bool HasVariable(string variableName);

		public abstract object GetVariableValue(string variableName);

		public abstract void SetVariableValue(string variableName, object value);

		public abstract void RemoveVariable(string variableName);

		public abstract void EmbedHostObject(string itemName, object value);

		public abstract void EmbedHostType(string itemName, Type type);

		public abstract void Interrupt();

		public abstract void CollectGarbage();

		#endregion

		#region IDisposable implementation

		public abstract void Dispose();

		#endregion
	}
}