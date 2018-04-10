﻿using System;
using System.Linq;

namespace MsieJavaScriptEngine.Utilities
{
	/// <summary>
	/// Extensions for String
	/// </summary>
	internal static class StringExtensions
	{
		/// <summary>
		/// Returns a value indicating whether the specified quoted string occurs within this string
		/// </summary>
		/// <param name="source">Instance of <see cref="String"/></param>
		/// <param name="value">The string without quotes to seek</param>
		/// <returns>true if the quoted value occurs within this string; otherwise, false</returns>
		public static bool ContainsQuotedValue(this string source, string value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			bool result = source.Contains("'" + value + "'") || source.Contains("\"" + value + "\"");

			return result;
		}

		/// <summary>
		/// Removes leading occurrence of the specified string from the current <see cref="String"/> object
		/// </summary>
		/// <param name="source">Instance of <see cref="String"/></param>
		/// <param name="trimString">An string to remove</param>
		/// <returns>The string that remains after removing of the specified string from the start of
		/// the current string</returns>
		public static string TrimStart(this string source, string trimString)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (trimString == null)
			{
				throw new ArgumentNullException("trimString");
			}

			if (source.Length == 0 || trimString.Length == 0)
			{
				return source;
			}

			string result = source;
			if (source.StartsWith(trimString, StringComparison.Ordinal))
			{
				result = source.Substring(trimString.Length);
			}

			return result;
		}

		/// <summary>
		/// Converts a first letter of string to capital
		/// </summary>
		/// <param name="source">Instance of <see cref="String"/></param>
		/// <returns>The string starting with a capital letter</returns>
		public static string CapitalizeFirstLetter(this string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			int length = source.Length;
			if (length == 0)
			{
				return source;
			}

			string result;
			char firstCharacter = source.First();

			if (char.IsLower(firstCharacter))
			{
				result = char.ToUpperInvariant(firstCharacter).ToString();
				if (length > 1)
				{
					result += source.Substring(1);
				}
			}
			else
			{
				result = source;
			}

			return result;
		}
	}
}