using UnityEngine;
using UnityEngine.UI;

namespace CarGame2D
{
    public class FightWindowController : BasicController
    {
        private FightWindowView _fightWindowView;
        private ProfilePlayerModel _profilePlayerModel;

        private int _valueWantedLevelPlayer;
        private int _allCountHealthPlayer;
        private int _allCountMoneyPlayer;
        private int _allCountPowerPlayer;

        private Health _health;
        private Money _money;
        private Power _power;

        private Enemy _enemy;

        public FightWindowController(Transform placeForUI, ProfilePlayerModel profilePlayer, FightWindowView fightWindowView)
        {
            _profilePlayerModel = profilePlayer;
            _fightWindowView = Object.Instantiate(fightWindowView, placeForUI);
            AddGameObject(_fightWindowView.gameObject);
        }

        public void RefreshView()
        {
            _enemy = new Enemy("Bad Cow");

            _money = new Money();
            _money.Attach(_enemy);

            _power = new Power();
            _power.Attach(_enemy);

            _health = new Health();
            _health.Attach(_enemy);

            _fightWindowView.IncreaseHealthButton.onClick.AddListener(() => ChangeHealth(true));
            _fightWindowView.DecreasedHealthButton.onClick.AddListener(() => ChangeHealth(false));

            _fightWindowView.IncreaseMoneyButton.onClick.AddListener(() => ChangeMoney(true));
            _fightWindowView.DecreasedMoneyButton.onClick.AddListener(() => ChangeMoney(false));

            _fightWindowView.IncreasePowerButton.onClick.AddListener(() => ChangePower(true));
            _fightWindowView.DecreasedPowerButton.onClick.AddListener(() => ChangePower(false));

            _fightWindowView.LvlUpButton.onClick.AddListener(() => ChangeWantedLevel(true));
            _fightWindowView.LvlDownButton.onClick.AddListener(() => ChangeWantedLevel(false));

            _fightWindowView.FightButton.onClick.AddListener(Fight);

            _fightWindowView.GoAwayButton.onClick.AddListener(GoAway);
            _fightWindowView.ExitButton.onClick.AddListener(CloseWindow);
        }


        protected override void OnDispose()
        {
            _money.Detach(_enemy);

            _power.Detach(_enemy);

            _health.Detach(_enemy);

            _fightWindowView.IncreaseHealthButton.onClick.RemoveAllListeners();
            _fightWindowView.DecreasedHealthButton.onClick.RemoveAllListeners();

            _fightWindowView.IncreaseMoneyButton.onClick.RemoveAllListeners();
            _fightWindowView.DecreasedMoneyButton.onClick.RemoveAllListeners();

            _fightWindowView.IncreasePowerButton.onClick.RemoveAllListeners();
            _fightWindowView.DecreasedPowerButton.onClick.RemoveAllListeners();

            _fightWindowView.LvlUpButton.onClick.RemoveAllListeners();
            _fightWindowView.LvlDownButton.onClick.RemoveAllListeners();
            _fightWindowView.FightButton.onClick.RemoveAllListeners();

            _fightWindowView.GoAwayButton.onClick.RemoveAllListeners();
            _fightWindowView.ExitButton.onClick.RemoveAllListeners();
            base.OnDispose();
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
                ActivateButton(_fightWindowView.GoAwayButton, false);
            else
                ActivateButton(_fightWindowView.GoAwayButton, true);
           
        }
        private void ActivateButton(Button btn, bool state)
        {
            btn.gameObject.SetActive(state);
        }

        private void ChangeDataWindow(int countChangedData, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Health:
                    _fightWindowView.CountHealthTxt.text = $"Player Health: {countChangedData}";
                    _health.CountHealt = countChangedData;
                    break;
                case DataType.Money:
                    _fightWindowView.CountMoneyTxt.text = $"Player Money: {countChangedData}";
                    _money.CountMoney = countChangedData;
                    break;
                case DataType.Power:
                    _fightWindowView.CountPowerTxt.text = $"Player Power: {countChangedData}";
                    _power.CountPower = countChangedData;
                    break;
                case DataType.WantedLevel:
                    _fightWindowView.WantedLevelTxt.text = $"WANTED LEVEL \n{countChangedData}";
                    break;
            }
            _fightWindowView.CountPowerEnemyTxt.text = $"Enemy Power: {_enemy.Power}";
        }
        private void CloseWindow()
        {
            _profilePlayerModel.CurrentState.Value = GameState.Game;
        }

        private void Fight()
        {
            var result = _allCountPowerPlayer >= _enemy.Power ? "Win" : "Lose";
            _fightWindowView.ResultFightTxt.text = $"{result}";
        }
        private void GoAway()
        {
            _fightWindowView.ResultFightTxt.text = $"You escaped the fight!";
        }
    }
}
