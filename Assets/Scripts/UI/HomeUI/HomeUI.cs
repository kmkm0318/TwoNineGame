using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 홈 UI를 담당하는 클래스
/// </summary>
public class HomeUI : MonoBehaviour
{
    #region UI 레퍼런스
    [Header("UI Components")]
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
}