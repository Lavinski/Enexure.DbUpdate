using System;
using System.Collections.Generic;
using System.Linq;
using Enexure.DbUpdate.Output;
using Enexure.Fire.Data;

namespace Enexure.DbUpdate.Engine
{
	/// <summary>
	/// An implementation of the <see cref="IJournal"/> interface which tracks version numbers for a 
	/// database using a table called dbo.Updates.
	/// </summary>
	public class DefaultJournal : IJournal
	{
		private const string RecordUpgrade = "Insert Into Upgrades (Id, DateAdded) Values (?, ?)";
		private const string UpgradesTableExistsQuery = "Select Table_Name From Information_Schema.Tables Where Table_Name = 'Upgrades'";

		private const string CreateUpgradesTable = 
			"Create Table Upgrades (" +
			"	Id NVarChar(256) NOT NULL PRIMARY KEY, " +
			"	DateAdded DateTimeOffset NOT NULL" +
			")";

		/// <summary>Loads the names of all previously executed upgrades.</summary>
		/// <returns>Upgrade.FullName</returns>
		public IEnumerable<string> GetExecutedUpgrades(ISession session, ILog log)
		{
			var tableMissing = session.CreateCommand(UpgradesTableExistsQuery).ExecuteScalar<string>() == null;

			if (tableMissing) {
				log.Information("The {0} table could not be found. The database is assumed to be at version 0.", "Upgrades");

				session.CreateCommand(CreateUpgradesTable).ExecuteNonQuery();
				return Enumerable.Empty<string>();

			} else {
				return session.CreateCommand("Select Id, DateAdded From Upgrades")
					.ExecuteQuery()
					.ToList<dynamic>()
					.Select(x => (string)x.Id)
					.ToList();
			}
		}

		/// <summary>Marks an upgrade as completed in the database.</summary>
		/// <param name="session">The session to use.</param>
		/// <param name="upgrade">The upgrade to execute.</param>
		/// <param name="now">The current datetime.</param>
		public void MarkUpgradeAsDone(ISession session, IUpgrade upgrade, DateTimeOffset now)
		{
			session.CreateCommand(RecordUpgrade, upgrade.GetType().FullName, now).ExecuteNonQuery();
		}
	}
}