using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGame2D
{
    public interface IUpgradeCarHandler
    {
        IUpgradableCar Upgrade(IUpgradableCar upgradableCar);
    }

    public interface IUpgradableCar
    {
        float Speed { get; set; }
        void ResetSpeed();
    }
}
