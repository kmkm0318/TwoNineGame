using DG.Tweening;
using UnityEngine;

/// <summary>
/// ActionButton의 애니메이션 처리를 담당하는 클래스
/// </summary>
[RequireComponent(typeof(ActionButton))]
public class ActionButtonAnimation : UIAnimation
{
    [Header("Animations")]
    [SerializeField] private DOTweenAnimation _normalAnimation;
    [SerializeField] private DOTweenAnimation _highlightedAnimation;
    [SerializeField] private DOTweenAnimation _pressedAnimation;
    [SerializeField] private DOTweenAnimation _disabledAnimation;
    [SerializeField] private DOTweenAnimation _selectedAnimation;

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
        _actionButton.OnNormal += () => PlayAnimation(_normalAnimation);
        _actionButton.OnHighlighted += () => PlayAnimation(_highlightedAnimation);
        _actionButton.OnPressed += () => PlayAnimation(_pressedAnimation);
        _actionButton.OnDisabled += () => PlayAnimation(_disabledAnimation);
        _actionButton.OnSelected += () => PlayAnimation(_selectedAnimation);
    }
}