using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Enexure.DbUpdate.Engine;
using Enexure.DbUpdate.Output;

namespace Enexure.DbUpdate.Builder
{
	/// <summary>
	/// Represents the configuration of an UpgradeEngine.
	/// </summary>
	public class UpgradeConfiguration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UpgradeConfiguration"/> class.
		/// </summary>
		public UpgradeConfiguration()
		{
			Log = new TraceLog();
			TransactionMode = TransactionMode.TransactionPerUpgrade;
		}

		/// <summary>
		/// Specifies the transaction mode
		/// </summary>
		public TransactionMode TransactionMode { get; set; }

		/// <summary>
		/// Specifies the database connection
		/// </summary>
		public DbConnection DbConnection { get; set; }

		/// <summary>
		/// Gets or sets a log for recording details about the upgrades.
		/// </summary>
		public ILog Log { get; set; }

		/// <summary>
		/// Gets or sets the journal, which tracks the upgrades that have already been run.
		/// </summary>
		public IJournal Journal { get; set; }

		/// <summary>
		/// Gets or sets the t arget assembly in which to scan for upgrades.
		/// </summary>
		public Assembly TargetAssembly { get; set; }

		/// <summary>
		/// Ensures all expectations have been met regarding this configuration.
		/// </summary>
		public void Validate()
		{
			if (DbConnection == null) throw new ArgumentException("A log is required to build a database upgrader. Please use one of the logging extension methods");
			if (Log == null) throw new ArgumentException("A log is required to build a database upgrader. Please use one of the logging extension methods");
			if (Journal == null) throw new ArgumentException("A journal is required. Please use one of the Journal extension methods before calling Build()");
		}

	}
}