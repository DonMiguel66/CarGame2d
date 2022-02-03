using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
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
    private Button _fightButton;

    [SerializeField]
    private Button _lvlUpButton;

    [SerializeField]
    private Button _lvlDownButton;

    [SerializeField]
    private Button _goAwayButton;

    [SerializeField]
    private Button _exitButton;

    public TMP_Text WantedLevelTxt => _wantedLevelTxt;
    public TMP_Text CountMoneyTxt  => _countMoneyTxt; 
    public TMP_Text CountHealthTxt => _countHealthTxt; 
    public TMP_Text CountPowerTxt => _countPowerTxt; 
    public TMP_Text CountPowerEnemyTxt  => _countPowerEnemyTxt; 
    public TMP_Text ResultFightTxt => _resultFightTxt;
    public Button IncreaseHealthButton  => _increaseHealthButton; 
    public Button DecreasedHealthButton  => _decreasedHealthButton; 
    public Button IncreaseMoneyButton => _increaseMoneyButton; 
    public Button DecreasedMoneyButton  => _decreasedMoneyButton; 
    public Button IncreasePowerButton => _increasePowerButton;
    public Button DecreasedPowerButton => _decreasedPowerButton; 
    public Button FightButton => _fightButton;
    public Button LvlUpButton  => _lvlUpButton;
    public Button LvlDownButton  => _lvlDownButton; 
    public Button GoAwayButton  => _goAwayButton;
    public Button ExitButton  => _exitButton;
}
