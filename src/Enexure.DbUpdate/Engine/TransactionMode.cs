using System;

namespace Enexure.DbUpdate.Engine
{
	/// <summary>
	/// The transaction strategy to use
	/// </summary>
	public enum TransactionMode
	{
		// <summary>
		// Upgrades are run without a transaction
		// </summary>
		//NoTransaction,

		/// <summary>
		/// A single transaction for all upgrades
		/// </summary>
		SingleTransaction,

		/// <summary>
		/// A new connection and transaction per upgrade
		/// </summary>
		TransactionPerUpgrade
	}
}