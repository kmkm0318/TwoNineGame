using System;
using UnityEngine;

/// <summary>
/// 결과 UI의 프레젠터 클래스
/// </summary>
public class ResultPresenter : MonoBehaviour, IShowHide
{
    [Header("UI References")]
    [SerializeField] private ResultUI _resultUI;

    [Header("Confirm Text")]
    [SerializeField] private string _retryConfirmKey = "UI_RetryConfirm";

    #region 레퍼런스
    private GameManager _gameManager;
    private ConfirmPresenter _confirmPresenter;
    #endregion

    private void OnDestroy()
    {
        // 이벤트 해제
        UnregisterEvents();
    }

    #region 초기화
    public void Init(GameManager gameManager, ConfirmPresenter confirmPresenter)
    {
        // 레퍼런스 할당
        _gameManager = gameManager;
        _confirmPresenter = confirmPresenter;

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
        _resultUI.OnRetryButtonClicked += HandleOnRetryButtonClicked;
        _resultUI.OnRestartButtonClicked += _gameManager.StartGame;
        _resultUI.OnExitButtonClicked += _gameManager.ReturnToHome;
    }

    private void UnregisterEvents()
    {
        _resultUI.OnRetryButtonClicked -= HandleOnRetryButtonClicked;
        _resultUI.OnRestartButtonClicked -= _gameManager.StartGame;
        _resultUI.OnExitButtonClicked -= _gameManager.ReturnToHome;
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleOnRetryButtonClicked()
    {
        // 재시도 확인 메시지 가져오기
        var message = LocalizationManager.Instance.GetLocalizedText(_retryConfirmKey);

        // 재시도 확인 UI 표시
        _confirmPresenter.Show(message, _gameManager.RetryGame);
    }
    #endregion

    #region UI 업데이트
    public void SetScore(int score) => _resultUI.UpdateScoreText(score);
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5f, Action onComplete = null)
    {
        // 재시도 버튼 표시 여부 설정
        _resultUI.ShowRetryButton(_gameManager.CanRetry());

        // 결과 UI 표시
        _resultUI.Show(duration, onComplete);
    }
    public void Hide(float duration = 0.5f, Action onComplete = null) => _resultUI.Hide(duration, onComplete);
    #endregion
}