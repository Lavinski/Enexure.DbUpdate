using System.Diagnostics;

namespace Enexure.DbUpdate.Output
{
	/// <summary>
	/// A log that writes to System.Diagnostics.Trace.
	/// </summary>
	public class TraceLog : ILog
	{
		/// <summary>
		/// Writes an informational message to the log.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public void Information(string format, params object[] args)
		{
			Trace.WriteLine("INFO:  " + string.Format(format, args));
		}

		/// <summary>
		/// Writes an error message to the log.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public void Error(string format, params object[] args)
		{
			Trace.WriteLine("ERROR: " + string.Format(format, args));
		}

		/// <summary>
		/// Writes a warning message to the log.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		public void Warning(string format, params object[] args)
		{
			Trace.WriteLine("WARN:  " + string.Format(format, args));
		}
	}
}