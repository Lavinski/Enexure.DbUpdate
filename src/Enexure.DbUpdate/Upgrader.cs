using System.Data.Common;
using System.Diagnostics;
using Enexure.DbUpdate.Builder;

namespace Enexure.DbUpdate
{
	public static class Upgrader
	{
		public static UpgradeEngineBuilder WithDatabase(string providerName, string connectionString)
		{
			var dbProviderFactory = DbProviderFactories.GetFactory(providerName);
			var dbConnection = dbProviderFactory.CreateConnection();

			Debug.Assert(dbConnection != null, "dbConnection != null");
			dbConnection.ConnectionString = connectionString;

			var a = new UpgradeEngineBuilder();

			a.WithDefaults();
			a.WithConnection(dbConnection);

			return a;
		}
	}
}
