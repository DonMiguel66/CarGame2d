using System.Collections.Generic;
public class DataPlayer
{
    private List<IEnemy> _enemies = new List<IEnemy>();

    public void Attach(IEnemy enemy)
    {
        _enemies.Add(enemy);
    }
    public void Detach(IEnemy enemy)
    {
        _enemies.Remove(enemy);
    }

    protected void Notifer(DataType dataType)
    {
        foreach (var enemy in _enemies)
        {
            enemy.Update(this, dataType);
        }
    }
}

public class Money :DataPlayer
{
    private int _countMoney;

    public int CountMoney
    {
        get => _countMoney;
        set 
        {
            if(_countMoney !=value)
            {
                _countMoney = value;
                Notifer(DataType.Money);
            }    
        }
    }
}

public class Health : DataPlayer
{
    private int _countHealth;

    public int CountHealt
    {
        get => _countHealth;
        set
        {
            if (_countHealth != value)
            {
                _countHealth = value;
                Notifer(DataType.Health);
            }
        }
    }
}

public class Power : DataPlayer
{
    private int _countPower;

    public int CountPower
    {
        get => _countPower;
        set
        {
            if (_countPower != value)
            {
                _countPower = value;
                Notifer(DataType.Power);
            }
        }
    }
}

public class KnifeSkill : DataPlayer
{
    private int _countKnifeSkill;

    public int CountKnifeSkill
    {
        get => _countKnifeSkill;
        set
        {
            if (_countKnifeSkill != value)
            {
                _countKnifeSkill = value;
                Notifer(DataType.KnifeSkill);
            }
        }
    }
}
public class PistolSkill : DataPlayer
{
    private int _countPistolSkill;

    public int CountPistolSkill
    {
        get => _countPistolSkill;
        set
        {
            if (_countPistolSkill != value)
            {
                _countPistolSkill = value;
                Notifer(DataType.PistolSkill);
            }
        }
    }
}