using System;
using UnityEngine;

/// <summary>
/// 확인 UI의 프레젠터 클래스
/// </summary>
public class ConfirmPresenter : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private ConfirmUI _confirmUI;

    #region 이벤트
    private Action _onConfirm;
    private Action _onCancel;
    #endregion

    public void Init()
    {
        // ConfirmUI의 이벤트 구독
        _confirmUI.OnConfirm += HandleConfirm;
        _confirmUI.OnCancel += HandleCancel;
    }

    #region 버튼 클릭 핸들러
    private void HandleConfirm()
    {
        // 확인 이벤트 호출
        _onConfirm?.Invoke();

        // UI 숨기기
        _confirmUI.Hide(0f);
    }
    private void HandleCancel()
    {
        // 취소 이벤트 호출
        _onCancel?.Invoke();

        // UI 숨기기
        _confirmUI.Hide(0f);
    }
    #endregion

    public void Show(string message, Action onConfirm, Action onCancel = null)
    {
        // 이벤트 핸들러 설정
        _onConfirm = onConfirm;
        _onCancel = onCancel;

        // 메시지 설정 및 UI 표시
        _confirmUI.SetMessage(message);
        _confirmUI.Show(0f);
    }

    public void TryCancel() => HandleCancel();
}