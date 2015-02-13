using System.Data;
using Enexure.Fire.Data;

namespace Enexure.DbUpdate
{
	public interface IUpgrade
	{
		void Execute(ISession session);
	}
}
