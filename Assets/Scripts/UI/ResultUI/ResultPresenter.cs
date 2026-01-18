using System;
using UnityEngine;

/// <summary>
/// 결과 UI의 프레젠터 클래스
/// </summary>
public class ResultPresenter : MonoBehaviour, IShowHide
{
    [Header("UI References")]
    [SerializeField] private ResultUI _resultUI;

    #region 레퍼런스
    private GameManager _gameManager;
    #endregion

    private void OnDestroy()
    {
        // 이벤트 해제
        UnregisterEvents();
    }

    #region 초기화
    public void Init(GameManager gameManager)
    {
        // 레퍼런스 할당
        _gameManager = gameManager;

        // 이벤트 구독
        RegisterEvents();

        // UI 초기화
        InitUI();
    }

    private void InitUI()
    {
        // UI 초기화
        _resultUI.Init();
    }
    #endregion

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        _resultUI.OnRestartButtonClicked += _gameManager.StartGame;
        _resultUI.OnExitButtonClicked += _gameManager.ReturnToHome;
    }

    private void UnregisterEvents()
    {
        _resultUI.OnRestartButtonClicked -= _gameManager.StartGame;
        _resultUI.OnExitButtonClicked -= _gameManager.ReturnToHome;
    }
    #endregion

    #region UI 업데이트
    public void SetScore(int score) => _resultUI.UpdateScoreText(score);
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5f, Action onComplete = null) => _resultUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5f, Action onComplete = null) => _resultUI.Hide(duration, onComplete);
    #endregion
}