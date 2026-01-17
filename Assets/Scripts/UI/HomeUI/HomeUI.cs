using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 홈 UI를 담당하는 클래스
/// </summary>
public class HomeUI : MonoBehaviour, IShowHide
{
    #region UI 레퍼런스
    [Header("UI Components")]
    [SerializeField] private ShowHideUI _showHideUI;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    #endregion

    #region 이벤트
    public event Action OnStartButtonClicked;
    public event Action OnLeaderboardButtonClicked;
    public event Action OnSettingsButtonClicked;
    public event Action OnExitButtonClicked;
    #endregion

    private void Awake()
    {
        // 버튼 클릭 이벤트 등록
        _startButton.onClick.AddListener(() => OnStartButtonClicked?.Invoke());
        _leaderboardButton.onClick.AddListener(() => OnLeaderboardButtonClicked?.Invoke());
        _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked?.Invoke());
        _exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());
    }

    #region Show, Hide
    public void Show(float duration = 0.5F, Action onComplete = null) => _showHideUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5F, Action onComplete = null) => _showHideUI.Hide(duration, onComplete);
    #endregion
}