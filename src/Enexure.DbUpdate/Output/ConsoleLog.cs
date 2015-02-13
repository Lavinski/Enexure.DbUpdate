﻿using System;

namespace Enexure.DbUpdate.Output
{
	/// <summary>
	/// A log that writes to the console in a colorful way.
	/// </summary>
	public class ConsoleLog : ILog
	{
		/// <summary>
		/// Writes an informational message to the log.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public void Information(string format, params object[] args)
		{
			Write(ConsoleColor.White, format, args);
		}

		/// <summary>
		/// Writes an error message to the log.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public void Error(string format, params object[] args)
		{
			Write(ConsoleColor.Red, format, args);
		}

		/// <summary>
		/// Writes a warning message to the log.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public void Warning(string format, params object[] args)
		{
			Write(ConsoleColor.Yellow, format, args);
		}

		private static void Write(ConsoleColor color, string format, object[] args)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(format, args);
			Console.ResetColor();
		}
	}
}