using CarGame2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class RewardController : BasicController
{
    private RewardView _rewardView;
    private List<ContainerSlotRewardView> _slots = new List<ContainerSlotRewardView>();
    private CurrencyController _currencyController;
    private ProfilePlayerModel _profilePlayer;
    private bool _isGetReward;

    public RewardController (Transform placeForUI, ProfilePlayerModel profilePlayer, RewardView rewardView, CurrencyView currencyView )
    {
        _profilePlayer = profilePlayer; 

        _rewardView = Object.Instantiate(rewardView, placeForUI);
        AddGameObject(_rewardView.gameObject);

        _currencyController = new CurrencyController(placeForUI, currencyView);
        AddController(_currencyController);
    }

    public void RefreshView()
    {
        InitSlots();

        _rewardView.StartCoroutine(RewardsStateUpdater());

        RefreshUI();

        SubscribesButtons();
    }

    private void SubscribesButtons()
    {
        _rewardView.GetRewardButton.onClick.AddListener(ClaimReward);
        _rewardView.ResetButton.onClick.AddListener(ResetTimer);
        _rewardView.CloseButton.onClick.AddListener(CloseWindow);
    }

    private void ResetTimer()
    {
        PlayerPrefs.DeleteAll();
    }

    private void ClaimReward()
    {
        if (!_isGetReward)
            return;

        var reward = _rewardView.Rewards[_rewardView.CurrentSlotInActive];

        switch (reward.RewardType)
        {
            case RewardType.Candy:
                CurrencyView.Instance.AddCandy(reward.CountCurrency);
                break;
            case RewardType.Diamond:
                CurrencyView.Instance.AddDiamond(reward.CountCurrency);
                break;
        }

        _rewardView.TimeGetReward = DateTime.UtcNow;
        _rewardView.CurrentSlotInActive = (_rewardView.CurrentSlotInActive + 1) % _rewardView.Rewards.Count;

        RefreshRewardState();
    }

    private void InitSlots()
    {
        for (var i = 0; i < _rewardView.Rewards.Count; i++)
        {
            var instatiateSlot = Object.Instantiate(_rewardView.ContainerSlotRewardView, _rewardView.MountRootSlotsReward, false);
            _slots.Add(instatiateSlot);
        }
    }

    private IEnumerator RewardsStateUpdater()
    {
        while(true)
        {
            RefreshRewardState();
            yield return new WaitForSeconds(1);
        }
    }

    private void RefreshRewardState()
    {
        _isGetReward = true;
        if(_rewardView.TimeGetReward.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _rewardView.TimeGetReward.Value;
            if(timeSpan.Seconds > _rewardView.TimeDeadline)
            {
                _rewardView.TimeGetReward = null;
                _rewardView.CurrentSlotInActive = 0;
            }
            else if(timeSpan.Seconds < _rewardView.TimeCooldown)
            {
                _isGetReward = false;
            }
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        _rewardView.GetRewardButton.interactable = _isGetReward;

        if(_isGetReward)
        {
            _rewardView.TimerNewReward.text = "Reward recived!";
        }
        else
        {
            if(_rewardView.TimeGetReward !=null)
            {
                var nextClaimTime = _rewardView.TimeGetReward.Value.AddSeconds(_rewardView.TimeCooldown);
                var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                var timeGetReward = $"{currentClaimCooldown.Days:D2} : {currentClaimCooldown.Hours:D2} : {currentClaimCooldown.Minutes:D2} : {currentClaimCooldown.Seconds:D2}";
                _rewardView.TimerNewReward.text = $"Time to next reward: {timeGetReward}";
            }
        }

        for (var i=0; i < _slots.Count; i++)
            _slots[i].SetData(_rewardView.Rewards[i], i + 1, i == _rewardView.CurrentSlotInActive, _rewardView.RewardTimePeriodType);
    }
    private void CloseWindow()
    {
        _profilePlayer.CurrentState.Value = GameState.Start;
    }

    protected override void OnDispose()
    {
        _rewardView.GetRewardButton.onClick.RemoveAllListeners();
        _rewardView.ResetButton.onClick.RemoveAllListeners();
        _rewardView.CloseButton.onClick.RemoveAllListeners();

        base.OnDispose();

    }


}
