using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 일시정지 UI
/// </summary>
public class PauseUI : MonoBehaviour, IShowHide
{
    [Header("UI Elements")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    #region 이벤트
    public event Action OnResumeClicked;
    public event Action OnRestartClicked;
    public event Action OnExitClicked;
    #endregion

    private void Awake()
    {
        // 버튼 초기화
        InitButtons();
    }

    #region 초기화
    private void InitButtons()
    {
        _resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());
        _restartButton.onClick.AddListener(() => OnRestartClicked?.Invoke());
        _exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
    }
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5F, Action onComplete = null)
    {
        // 즉시 보이기
        gameObject.SetActive(true);

        // 완료 콜백 호출
        onComplete?.Invoke();
    }

    public void Hide(float duration = 0.5F, Action onComplete = null)
    {
        // 즉시 숨기기
        gameObject.SetActive(false);

        // 완료 콜백 호출
        onComplete?.Invoke();
    }
    #endregion
}
