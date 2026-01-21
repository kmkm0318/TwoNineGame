using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 결과 UI를 담당하는 클래스
/// </summary>
public class ResultUI : MonoBehaviour, IShowHide
{
    [Header("UI Components")]
    [SerializeField] private ShowHideUI _showHideUI;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    #region 이벤트
    public event Action OnRetryButtonClicked;
    public event Action OnRestartButtonClicked;
    public event Action OnExitButtonClicked;
    #endregion

    #region 초기화
    public void Init()
    {
        // 버튼 클릭 이벤트 등록
        _retryButton.onClick.AddListener(() => OnRetryButtonClicked?.Invoke());
        _restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
        _exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());
    }
    #endregion

    #region UI 업데이트
    public void UpdateScoreText(int score) => _scoreText.text = score.ToString();
    public void ShowRetryButton(bool show) => _retryButton.gameObject.SetActive(show);
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5f, Action onComplete = null) => _showHideUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5f, Action onComplete = null) => _showHideUI.Hide(duration, onComplete);
    #endregion
}