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
