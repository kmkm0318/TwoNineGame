using UnityEngine;

/// <summary>
/// PointerHandler의 애니메이션 처리를 담당하는 클래스
/// </summary>
[RequireComponent(typeof(PointerHandler))]
public class PointerHandlerAnimation : UIAnimation
{
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
        _pointerHandler.OnPointerEntered += () => PlayScaleAnimation(_largeScale, _animationDuration, _animationEase);
        _pointerHandler.OnPointerExited += () => PlayScaleAnimation(_originalScale, _animationDuration, _animationEase);
        _pointerHandler.OnPointerDowned += () => PlayScaleAnimation(_smallScale, _animationDuration, _animationEase);
        _pointerHandler.OnPointerUpped += () => PlayScaleAnimation(_originalScale, _animationDuration, _animationEase);
        _pointerHandler.OnPointerClicked += () => PlayScaleAnimation(_originalScale, _animationDuration, _animationEase);
    }
}