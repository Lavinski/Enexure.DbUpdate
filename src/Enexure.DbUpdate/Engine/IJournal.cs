using System;
using System.Collections.Generic;
using Enexure.DbUpdate.Output;
using Enexure.Fire.Data;

namespace Enexure.DbUpdate.Engine
{
	/// <summary>
	/// This interface is provided to allow different projects to store version information differently.
	/// </summary>
	public interface IJournal
	{
		/// <summary>Loads the names of all previously executed upgrades.</summary>
		/// <param name="session">The session to use.</param>
		/// <param name="log">The logger to use.</param>
		/// <returns>A list of Upgrade.FullName</returns>
		IEnumerable<string> GetExecutedUpgrades(ISession session, ILog log);

		/// <summary>Marks an upgrade as completed in the database.</summary>
		/// <param name="session">The session to use.</param>
		/// <param name="upgrade">The upgrade to execute.</param>
		/// <param name="now">The current datetime.</param>
		void MarkUpgradeAsDone(ISession session, IUpgrade upgrade, DateTimeOffset now);

	}
}