namespace Enexure.DbUpdate.Output
{
	/// <summary>
	/// Implemented by objects which record the internal details of the database migration.
	/// </summary>
	public interface ILog
	{
		/// <summary>
		/// Writes an informational message to the log.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		void Information(string format, params object[] args);

		/// <summary>
		/// Writes an error message to the log.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		void Error(string format, params object[] args);

		/// <summary>
		/// Writes a warning message to the log.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		void Warning(string format, params object[] args);
	}
}
