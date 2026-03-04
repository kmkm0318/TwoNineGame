using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 확인 UI를 관리하는 클래스
/// </summary>
public class ConfirmUI : MonoBehaviour, IShowHide
{
    [Header("UI Elements")]
    [SerializeField] private ShowHideUI _showHideUI;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _backgroundButton;

    #region 이벤트
    public event Action OnConfirm;
    public event Action OnCancel;
    #endregion

    private void Awake()
    {
        // 버튼 클릭 이벤트 등록
        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        _cancelButton.onClick.AddListener(OnCancelButtonClicked);
        _backgroundButton.onClick.AddListener(OnCancelButtonClicked);
    }

    #region 버튼 클릭 핸들러
    private void OnConfirmButtonClicked() => OnConfirm?.Invoke();
    private void OnCancelButtonClicked() => OnCancel?.Invoke();
    #endregion

    #region UI 설정
    public void SetMessage(string message) => _messageText.text = message;
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5f, Action onComplete = null) => _showHideUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5f, Action onComplete = null) => _showHideUI.Hide(duration, onComplete);
    #endregion
}