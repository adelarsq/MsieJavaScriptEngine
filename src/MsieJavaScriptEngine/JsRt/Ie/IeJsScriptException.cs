#if !NETSTANDARD1_3
using System;
using System.Runtime.Serialization;
#endif

namespace MsieJavaScriptEngine.JsRt.Ie
{
	/// <summary>
	/// �IE� script exception
	/// </summary>
#if !NETSTANDARD1_3
	[Serializable]
#endif
	internal sealed class IeJsScriptException : JsException
	{
		/// <summary>
		/// The error
		/// </summary>
#if !NETSTANDARD1_3
		[NonSerialized]
#endif
		private readonly IeJsValue _error;

		/// <summary>
		/// Gets a JavaScript object representing the script error
		/// </summary>
		public IeJsValue Error
		{
			get { return _error; }
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="IeJsScriptException"/> class
		/// </summary>
		/// <param name="code">The error code returned</param>
		/// <param name="error">The JavaScript error object</param>
		public IeJsScriptException(JsErrorCode code, IeJsValue error)
			: this(code, error, "JavaScript Exception")
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="IeJsScriptException"/> class
		/// </summary>
		/// <param name="code">The error code returned</param>
		/// <param name="error">The JavaScript error object</param>
		/// <param name="message">The error message</param>
		public IeJsScriptException(JsErrorCode code, IeJsValue error, string message)
			: base(code, message)
		{
			_error = error;
		}
#if !NETSTANDARD1_3

		/// <summary>
		/// Initializes a new instance of the <see cref="IeJsScriptException"/> class
		/// </summary>
		/// <param name="info">The serialization info</param>
		/// <param name="context">The streaming context</param>
		private IeJsScriptException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }
#endif
	}
}