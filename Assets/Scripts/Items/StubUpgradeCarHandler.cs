using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGame2D
{
    public class StubUpgradeCarHandler : IUpgradeCarHandler
    {
        public static readonly IUpgradeCarHandler Default = new StubUpgradeCarHandler();

        public IUpgradableCar Upgrade(IUpgradableCar upgradableCar)
        {
            return upgradableCar;
        }

    }
}
