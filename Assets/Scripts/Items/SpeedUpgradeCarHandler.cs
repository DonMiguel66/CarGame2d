using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGame2D
{
    public class SpeedUpgradeCarHandler : IUpgradeCarHandler
    {
        private readonly float _speed;

        public SpeedUpgradeCarHandler(float speed)
        {
            _speed = speed;
        }

        public IUpgradableCar Upgrade(IUpgradableCar upgradableCar)
        {
            upgradableCar.Speed = _speed;
            return upgradableCar;
        }
    }
}
