using UnityEngine;

/// <summary>
/// PointerHandler의 애니메이션 처리를 담당하는 클래스
/// </summary>
[RequireComponent(typeof(PointerHandler))]
public class PointerHandlerAnimation : UIAnimation
{
    [Header("PointerHandler Animation Settings")]
    [SerializeField] private UIAnimationType _enterAnimationType = UIAnimationType.Scale;
    [SerializeField] private UIAnimationType _exitAnimationType = UIAnimationType.Scale;
    [SerializeField] private UIAnimationType _downAnimationType = UIAnimationType.Scale;
    [SerializeField] private UIAnimationType _upAnimationType = UIAnimationType.Scale;
    [SerializeField] private UIAnimationType _clickAnimationType = UIAnimationType.Scale;

    #region 레퍼런스
    private PointerHandler _pointerHandler;
    #endregion

    private void Awake()
    {
        // PointerHandler 컴포넌트 가져오기
        _pointerHandler = GetComponent<PointerHandler>();

        // PointerHandler가 없을 경우 패스
        if (_pointerHandler == null)
        {
            $"PointerHandler가 없습니다.".LogWarning(this);
            return;
        }

        // 포인터 이벤트에 애니메이션 재생 메서드 등록
        _pointerHandler.OnPointerEntered += () => PlayAnimation(_enterAnimationType, true);
        _pointerHandler.OnPointerExited += () => PlayAnimation(_exitAnimationType, false);
        _pointerHandler.OnPointerDowned += () => PlayAnimation(_downAnimationType, false, true);
        _pointerHandler.OnPointerUpped += () => PlayAnimation(_upAnimationType, false);
        _pointerHandler.OnPointerClicked += () => PlayAnimation(_clickAnimationType, false);
    }
}