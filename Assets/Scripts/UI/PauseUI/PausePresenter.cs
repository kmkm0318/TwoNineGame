using System;
using UnityEngine;

/// <summary>
/// 게임 일시정지 프레젠터
/// </summary>
public class PausePresenter : MonoBehaviour, IShowHide
{
    [Header("UI References")]
    [SerializeField] private PauseUI _pauseUI;

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
    }
    #endregion

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        _pauseUI.OnResumeClicked += _gameManager.ResumeGame;
        _pauseUI.OnRestartClicked += _gameManager.StartGame;
        _pauseUI.OnExitClicked += _gameManager.ReturnToHome;
    }

    private void UnregisterEvents()
    {
        _pauseUI.OnResumeClicked -= _gameManager.ResumeGame;
        _pauseUI.OnRestartClicked -= _gameManager.StartGame;
        _pauseUI.OnExitClicked -= _gameManager.ReturnToHome;
    }
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5F, Action onComplete = null) => _pauseUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5F, Action onComplete = null) => _pauseUI.Hide(duration, onComplete);
    #endregion
}