using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Enexure.DbUpdate.Builder;
using Enexure.Fire.Data;

namespace Enexure.DbUpdate.Engine
{
	/// <summary>
	/// This class orchestrates the database upgrade process.
	/// </summary>
	public class UpgradeEngine
	{
		private readonly UpgradeConfiguration configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="UpgradeEngine"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public UpgradeEngine(UpgradeConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void EnsureIsUpToDate()
		{
			using (var session = new Session(configuration.DbConnection)) {
				EnsureIsUpToDate(session, FindUpgrades(configuration.TargetAssembly), configuration);
			}
		}

		private static IEnumerable<Type> FindUpgrades(Assembly assembly)
		{
			var upgradeInterface = typeof(IUpgrade);

			return assembly
				.GetTypes()
				.Where(upgradeInterface.IsAssignableFrom);
		}

		private static void EnsureIsUpToDate(ISession session, IEnumerable<Type> upgrades, UpgradeConfiguration configuration)
		{
			var pendingUpdates = FindPendingUpdates(session, upgrades, configuration);

			if (configuration.TransactionMode == TransactionMode.TransactionPerUpgrade) {
				session.Commit();
			}

			if (pendingUpdates.Any()) {
				configuration.Log.Information("There are {0} pending updates", pendingUpdates.Count());
				ApplyUpdates(session, pendingUpdates, configuration);

				if (configuration.TransactionMode != TransactionMode.TransactionPerUpgrade) {
					session.Commit();
				}

			} else {
				configuration.Log.Information("Database is up to date");
			}
		}

		private static IReadOnlyList<Type> FindPendingUpdates(ISession session, IEnumerable<Type> allUpgrades, UpgradeConfiguration configuration)
		{
			var executed = new HashSet<string>(configuration.Journal.GetExecutedUpgrades(session, configuration.Log));
			return allUpgrades.Where(x => !executed.Contains(x.FullName)).ToList();
		}

		private static void ApplyUpdates(ISession session, IEnumerable<Type> pendingUpdates, UpgradeConfiguration configuration)
		{
			var updates = pendingUpdates
				.Select(updateType => (IUpgrade)Activator.CreateInstance(updateType))
				.OrderBy(update => update.GetType().Name);

			foreach (var update in updates) {
				var typeName = update.GetType().Name;
				var now = DateTimeOffset.Now;

				try {
					configuration.Log.Information("Applying update {0}", typeName);

					update.Execute(session);
					configuration.Journal.MarkUpgradeAsDone(session, update, now);

					if (configuration.TransactionMode == TransactionMode.TransactionPerUpgrade) {
						session.Commit();
					}

				} catch (Exception ex) {
					throw new ExecutingUpgrateException(update.GetType(), ex);
				}
			}
		}
	}
}