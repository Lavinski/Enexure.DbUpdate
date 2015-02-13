using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Enexure.DbUpdate.Builder;
using Enexure.DbUpdate.Engine;
using Enexure.DbUpdate.Output;

/// <summary>
/// Configuration extensions for the standard stuff.
/// </summary>
// NOTE: DO NOT MOVE THIS TO A NAMESPACE
// Since the class just contains extension methods, we leave it in the root so that it is always discovered
// and people don't have to manually add using statements.
// ReSharper disable CheckNamespace
public static class StandardExtensions
// ReSharper restore CheckNamespace
{
	internal static UpgradeEngineBuilder WithDefaults(this UpgradeEngineBuilder builder)
	{
		builder.Configure(c => c.TransactionMode = TransactionMode.TransactionPerUpgrade);
		builder.Configure(c => c.Journal = new DefaultJournal());
		return builder;
	}

	public static UpgradeEngineBuilder WithConnection(this UpgradeEngineBuilder builder, DbConnection dbConnection)
	{
		builder.Configure(c => c.DbConnection = dbConnection);
		builder.Configure(c => c.TransactionMode = TransactionMode.TransactionPerUpgrade);
		builder.Configure(c => c.Journal = new DefaultJournal());
		return builder;
	}

	/// <summary>Logs to a custom logger.</summary>
	/// <param name="builder"></param>
	/// <param name="log">The logger.</param>
	/// <returns>The same builder</returns>
	public static UpgradeEngineBuilder LogTo(this UpgradeEngineBuilder builder, ILog log)
	{
		return builder.Configure(c => c.Log = log);
	}

	/// <summary>
	/// Logs to the console using pretty colours.
	/// </summary>
	/// <param name="builder">The builder.</param>
	/// <returns>The same builder</returns>
	public static UpgradeEngineBuilder LogToConsole(this UpgradeEngineBuilder builder)
	{
		return LogTo(builder, new ConsoleLog());
	}

	/// <summary>
	/// Logs to System.Diagnostics.Trace.
	/// </summary>
	/// <param name="builder">The builder.</param>
	/// <returns>The same builder</returns>
	public static UpgradeEngineBuilder LogToTrace(this UpgradeEngineBuilder builder)
	{
		return LogTo(builder, new TraceLog());
	}

	/// <summary>
	/// Uses a custom journal for recording which scripts were executed.
	/// </summary>
	/// <param name="builder">The builder.</param>
	/// <param name="journal">The custom journal.</param>
	/// <returns>The same builder</returns>
	public static UpgradeEngineBuilder JournalTo(this UpgradeEngineBuilder builder, IJournal journal)
	{
		return builder.Configure(c => c.Journal = journal);
	}

	/// <summary>
	/// Adds all scripts found as embedded resources in the given assembly, with a custom filter (you'll need to exclude non- .SQL files yourself).
	/// </summary>
	/// <param name="builder">The builder.</param>
	/// <param name="assembly">The assembly.</param>
	/// <returns>The same builder</returns>
	public static UpgradeEngineBuilder WithUpgradesInAssembly(this UpgradeEngineBuilder builder, Assembly assembly) // , Func<string, bool> filter
	{
		return builder.Configure(c => c.TargetAssembly = assembly);
	}

	///// <summary>
	///// Run creates a new connection for each script, without a transaction
	///// </summary>
	///// <param name="builder"></param>
	///// <returns></returns>
	//public static UpgradeEngineBuilder WithoutTransaction(this UpgradeEngineBuilder builder)
	//{
	//	builder.Configure(c => c.TransactionMode = TransactionMode.NoTransaction);
	//
	//	return builder;
	//}

	/// <summary>
	/// Run Enexure.DbUpdate in a single transaction
	/// </summary>
	/// <param name="builder"></param>
	/// <returns></returns>
	public static UpgradeEngineBuilder WithTransaction(this UpgradeEngineBuilder builder)
	{
		builder.Configure(c => c.TransactionMode = TransactionMode.SingleTransaction);

		return builder;
	}

	/// <summary>
	/// Run each script in it's own transaction
	/// </summary>
	/// <param name="builder"></param>
	/// <returns></returns>
	public static UpgradeEngineBuilder WithTransactionPerScript(this UpgradeEngineBuilder builder)
	{
		builder.Configure(c => c.TransactionMode = TransactionMode.TransactionPerUpgrade);

		return builder;
	}

}
