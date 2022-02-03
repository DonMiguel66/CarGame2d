using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AdvancedFightWindowView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _wantedLevelTxt;

    [SerializeField]
    private TMP_Text _countMoneyTxt;

    [SerializeField]
    private TMP_Text _countHealthTxt;

    [SerializeField]
    private TMP_Text _countPowerTxt;

    [SerializeField]
    private TMP_Text _countPowerEnemyTxt;

    [SerializeField]
    private TMP_Text _resultFightTxt;

    [SerializeField]
    private TMP_Text _knifeSkillsPlayer;
    [SerializeField]
    private TMP_Text _pistolSkillsPlayer;
    [SerializeField]
    private TMP_Text _knifeSkillsEnemy;
    [SerializeField]
    private TMP_Text _pistolSkillsEnemy;


    [SerializeField]
    private Button _increaseHealthButton;

    [SerializeField]
    private Button _decreasedHealthButton;

    [SerializeField]
    private Button _increaseMoneyButton;

    [SerializeField]
    private Button _decreasedMoneyButton;

    [SerializeField]
    private Button _increasePowerButton;

    [SerializeField]
    private Button _decreasedPowerButton;

    [SerializeField]
    private Button _knifeFightButton;

    [SerializeField]
    private Button _pistolFightButton;

    [SerializeField]
    private Button _lvlUpButton;

    [SerializeField]
    private Button _lvlDownButton;

    [SerializeField]
    private Button _goAwayButton;

    private int _valueWantedLevelPlayer;
    private int _allCountHealthPlayer;
    private int _allCountMoneyPlayer;
    private int _allCountPowerPlayer;

    private int _valueKnifeSkillsPlayer = 2;
    private int _valuePistolSkillsPlayer = 4;
    private int _valueKnifeSkillsEnemy = 3;
    private int _valuePistolSkillsEnemy= 5;

    private Health _health;
    private Money _money;
    private Power _power;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = new Enemy("Bad Cow");
        _knifeSkillsEnemy.text = $"Enemy Knife Skills {_valueKnifeSkillsEnemy}";
        _pistolSkillsEnemy.text = $"Enemy Knife Skills {_valuePistolSkillsEnemy}";

        _knifeSkillsPlayer.text = $"Player Knife Skills {_valueKnifeSkillsPlayer}";
        _pistolSkillsPlayer.text = $"Player Knife Skills {_valuePistolSkillsPlayer}";

        _money = new Money();
        _money.Attach(_enemy);

        _power = new Power();
        _power.Attach(_enemy);

        _health = new Health();
        _health.Attach(_enemy);

        _increaseHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _decreasedHealthButton.onClick.AddListener(() => ChangeHealth(false));

        _increaseMoneyButton.onClick.AddListener(() => ChangeMoney(true));
        _decreasedMoneyButton.onClick.AddListener(() => ChangeMoney(false));

        _increasePowerButton.onClick.AddListener(() => ChangePower(true));
        _decreasedPowerButton.onClick.AddListener(() => ChangePower(false));

        _lvlUpButton.onClick.AddListener(() => ChangeWantedLevel(true));
        _lvlDownButton.onClick.AddListener(() => ChangeWantedLevel(false));

        _knifeFightButton.onClick.AddListener(KnifeFight);
        _pistolFightButton.onClick.AddListener(PistolFight);

        _goAwayButton.onClick.AddListener(GoAway);
    }


    private void OnDestroy()
    {
        _money.Detach(_enemy);

        _power.Detach(_enemy);

        _health.Detach(_enemy);

        _increaseHealthButton.onClick.RemoveAllListeners();
        _decreasedHealthButton.onClick.RemoveAllListeners();

        _increaseMoneyButton.onClick.RemoveAllListeners();
        _decreasedMoneyButton.onClick.RemoveAllListeners();

        _increasePowerButton.onClick.RemoveAllListeners();
        _decreasedPowerButton.onClick.RemoveAllListeners();

        _lvlUpButton.onClick.RemoveAllListeners();
        _lvlDownButton.onClick.RemoveAllListeners();

        _knifeFightButton.onClick.RemoveAllListeners();
        _pistolFightButton.onClick.RemoveAllListeners();

        _goAwayButton.onClick.RemoveAllListeners();
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            _allCountHealthPlayer++;
        else if (_allCountHealthPlayer > 0)
            _allCountHealthPlayer--;
        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }
    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            _allCountMoneyPlayer++;
        else if (_allCountMoneyPlayer > 0)
            _allCountMoneyPlayer--;
        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }
    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            _allCountPowerPlayer++;
        else if (_allCountPowerPlayer > 0)
            _allCountPowerPlayer--;
        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }
    private void ChangeWantedLevel(bool isAddCount)
    {
        if (isAddCount)
            _valueWantedLevelPlayer++;
        else if (_valueWantedLevelPlayer > 0)
            _valueWantedLevelPlayer--;
        ChangeDataWindow(_valueWantedLevelPlayer, DataType.WantedLevel);
        if (_valueWantedLevelPlayer > 2)
            ActivateButton(_goAwayButton, false);
    }

    private void ActivateButton(Button btn, bool state)
    {
        btn.gameObject.SetActive(state);
    }

    private void ChangePowerWithFightType(FightType fightType)
    {
        int changedPowerValue = 0;
        switch(fightType)
        {
            case FightType.Knife:
                changedPowerValue = _allCountPowerPlayer + _valueKnifeSkillsPlayer;
                ChangeDataWindow(changedPowerValue, DataType.KnifeSkill);
                break;
            case FightType.Pistol:
                changedPowerValue = _allCountPowerPlayer + _valuePistolSkillsPlayer;
                ChangeDataWindow(changedPowerValue, DataType.PistolSkill);
                break;
        }
    }

    private void ChangeDataWindow(int countChangedData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                _countHealthTxt.text = $"Player Health: {countChangedData}";
                _health.CountHealt = countChangedData;
                break;
            case DataType.Money:
                _countMoneyTxt.text = $"Player Money: {countChangedData}";
                _money.CountMoney = countChangedData;
                break;
            case DataType.Power:
                _countPowerTxt.text = $"Player Power: {countChangedData}";
                _power.CountPower = countChangedData;
                break;
            case DataType.WantedLevel:
                _wantedLevelTxt.text = $"WANTED LEVEL \n{countChangedData}";
                break;
            case DataType.PistolSkill:
                _power.CountPower = countChangedData;
                break;
            case DataType.KnifeSkill:
                _power.CountPower = countChangedData;
                break;
        }
        _countPowerEnemyTxt.text = $"Enemy Power: {_enemy.Power}";
    }
    private void KnifeFight()
    {
        ChangePowerWithFightType(FightType.Knife);
        var enemyPower = _enemy.Power + _valueKnifeSkillsEnemy;
        _countPowerEnemyTxt.text = $"Enemy Power: {enemyPower}";
        var result = _allCountPowerPlayer >= enemyPower ? "Win" : "Lose";
        _resultFightTxt.text = $"{result}";
    }

    private void PistolFight()
    {
        ChangePowerWithFightType(FightType.Pistol);
        var enemyPower = _enemy.Power + _valuePistolSkillsEnemy;
        _countPowerEnemyTxt.text = $"Enemy Power: {enemyPower}";
        var result = _allCountPowerPlayer >= enemyPower ? "Win" : "Lose";
        _resultFightTxt.text = $"{result}";
    }

    private void GoAway()
    {
        _resultFightTxt.text = $"You escaped the fight!";
    }

}