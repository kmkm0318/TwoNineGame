using System;
using UnityEngine;

/// <summary>
/// 게임 씬의 UI 프리젠터 클래스
/// </summary>
public class GamePresenter : MonoBehaviour, IShowHide
{
    [Header("UI Components")]
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private RoundUI _roundUI;
    [SerializeField] private NumberUI _numberUI;

    #region 레퍼런스
    private RoundManager _roundManager;
    private NumberManager _numberManager;
    #endregion

    private void OnDestroy()
    {
        // 이벤트 해제
        UnregisterEvents();
    }

    #region 초기화
    public void Init(RoundManager roundManager, NumberManager numberManager)
    {
        // 레퍼런스 할당
        _roundManager = roundManager;
        _numberManager = numberManager;

        // 이벤트 등록
        RegisterEvents();

        // UI 초기화
        InitUI();
    }

    private void InitUI()
    {
        _roundUI.SetScoreMaxValue(_roundManager.TargetScore);
        _roundUI.SetScoreValue(_roundManager.CurrentScore);
        _roundUI.SetRoundTimeMaxValue(_roundManager.MaxRoundTime);
        _roundUI.SetRoundTimeValue(_roundManager.CurrentRoundTime);

        _numberUI.Init();
        _numberUI.UpdateTargetMultipleText(_numberManager.CurrentTargetMultiple);
        _numberUI.UpdateNumberButtons(_numberManager.Numbers);
    }
    #endregion

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        _roundManager.OnTargetScoreChanged += _roundUI.SetScoreMaxValue;
        _roundManager.OnCurrentScoreChanged += _roundUI.SetScoreValue;
        _roundManager.OnMaxRoundTimeChanged += _roundUI.SetRoundTimeMaxValue;
        _roundManager.OnCurrentRoundTimeChanged += _roundUI.SetRoundTimeValue;

        _numberManager.OnCurrentTargetMultipleChanged += _numberUI.UpdateTargetMultipleText;
        _numberManager.OnNumbersChanged += _numberUI.UpdateNumberButtons;
        _numberManager.OnPrimeFactorAnimationRequested += _numberUI.PlayPrimeFactorParticleAnimation;

        _numberUI.OnNumberButtonClicked += _numberManager.HandleOnNumberButtonClicked;
    }

    private void UnregisterEvents()
    {
        _roundManager.OnTargetScoreChanged -= _roundUI.SetScoreMaxValue;
        _roundManager.OnCurrentScoreChanged -= _roundUI.SetScoreValue;
        _roundManager.OnMaxRoundTimeChanged -= _roundUI.SetRoundTimeMaxValue;
        _roundManager.OnCurrentRoundTimeChanged -= _roundUI.SetRoundTimeValue;

        _numberManager.OnCurrentTargetMultipleChanged -= _numberUI.UpdateTargetMultipleText;
        _numberManager.OnNumbersChanged -= _numberUI.UpdateNumberButtons;
        _numberManager.OnPrimeFactorAnimationRequested -= _numberUI.PlayPrimeFactorParticleAnimation;

        _numberUI.OnNumberButtonClicked -= _numberManager.HandleOnNumberButtonClicked;
    }
    #endregion

    #region UI 함수
    public void ShowNumberButtonsResultColor(int targetMultiple, Action onComplete = null) => _numberUI.ShowNumberButtonsResultColor(targetMultiple, onComplete);
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5f, Action onComplete = null) => _gameUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5f, Action onComplete = null) => _gameUI.Hide(duration, onComplete);
    #endregion
}