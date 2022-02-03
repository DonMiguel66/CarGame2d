using UnityEngine;

public class Enemy : IEnemy
{
    private string _name;

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;
    private int _knifeSkillPlayer;
    private int _pistolSkillPlayer;

    public Enemy(string name)
    {
        _name = name;
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch(dataType)
        {
            case DataType.Health:
                var dataHealth = (Health)dataPlayer;
                _healthPlayer = dataHealth.CountHealt;
                break;
            case DataType.Money:
                var dataMoney = (Money)dataPlayer;
                _moneyPlayer = dataMoney.CountMoney;
                break;
            case DataType.Power:
                var dataPower = (Power)dataPlayer;
                _powerPlayer = dataPower.CountPower;
                break;
            case DataType.KnifeSkill:
                var dataKnifeSkill = (KnifeSkill)dataPlayer;
                _knifeSkillPlayer = dataKnifeSkill.CountKnifeSkill;
                break;
            case DataType.PistolSkill:
                var dataPistolSkill = (PistolSkill)dataPlayer;
                _pistolSkillPlayer = dataPistolSkill.CountPistolSkill;
                break;
        }
        Debug.Log($"Enemy {_name}, change {dataType}");
    }

    public int Power
    {
        get
        {
            var power = 1 + (_healthPlayer / 3 + _moneyPlayer * 2) - _powerPlayer;
            return power;
        }
    }

}
