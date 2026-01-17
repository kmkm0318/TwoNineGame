using UnityEngine;

/// <summary>
/// ActionButton의 애니메이션 처리를 담당하는 클래스
/// </summary>
[RequireComponent(typeof(ActionButton))]
public class ActionButtonAnimation : UIAnimation
{
    #region 레퍼런스
    private ActionButton _actionButton;
    #endregion

    private void Awake()
    {
        // ActionButton 컴포넌트 가져오기
        _actionButton = GetComponent<ActionButton>();

        // ActionButton이 없을 경우 패스
        if (_actionButton == null)
        {
            $"ActionButton이 없습니다.".LogWarning(this);
            return;
        }

        // 액션 버튼의 상태 변화 이벤트에 애니메이션 재생 메서드 등록
        _actionButton.OnNormal += () => PlayScaleAnimation(_originalScale, _animationDuration, _animationEase);
        _actionButton.OnHighlighted += () => PlayScaleAnimation(_largeScale, _animationDuration, _animationEase);
        _actionButton.OnPressed += () => PlayScaleAnimation(_smallScale, _animationDuration, _animationEase);
        _actionButton.OnDisabled += () => PlayScaleAnimation(_originalScale, _animationDuration, _animationEase);
        _actionButton.OnSelected += () => PlayScaleAnimation(_largeScale, _animationDuration, _animationEase);
    }
}