
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 UI를 담당하는 클래스
/// </summary>
public class GameUI : MonoBehaviour, IShowHide
{
    [Header("UI Components")]
    [SerializeField] private ShowHideUI _showHideUI;
    [SerializeField] private Button _pauseButton;

    #region 이벤트
    public event Action OnPauseButtonClicked;
    #endregion

    private void Awake()
    {
        // 버튼 초기화
        _pauseButton.onClick.AddListener(() => OnPauseButtonClicked?.Invoke());
    }

    #region Show, Hide
    public void Show(float duration = 0.5f, Action onComplete = null) => _showHideUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5f, Action onComplete = null) => _showHideUI.Hide(duration, onComplete);
    #endregion
}