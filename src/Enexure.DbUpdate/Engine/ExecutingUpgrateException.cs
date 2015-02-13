using System;

namespace Enexure.DbUpdate.Engine
{
	public class ExecutingUpgrateException : Exception
	{
		public ExecutingUpgrateException(Type upgrade, Exception ex)
			: base(string.Format("Exception while running {0}", upgrade.Name), ex)
		{

		}
	}
}
