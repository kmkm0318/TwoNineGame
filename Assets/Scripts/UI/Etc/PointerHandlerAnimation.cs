using DG.Tweening;
using UnityEngine;

/// <summary>
/// PointerHandler의 애니메이션 처리를 담당하는 클래스
/// </summary>
[RequireComponent(typeof(PointerHandler))]
public class PointerHandlerAnimation : UIAnimation
{
    [Header("Animations")]
    [SerializeField] private DOTweenAnimation _pointerEnterAnimation;
    [SerializeField] private DOTweenAnimation _pointerExitAnimation;
    [SerializeField] private DOTweenAnimation _pointerDownAnimation;
    [SerializeField] private DOTweenAnimation _pointerUpAnimation;
    [SerializeField] private DOTweenAnimation _pointerClickAnimation;

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
        _pointerHandler.OnPointerEntered += () => PlayAnimation(_pointerEnterAnimation);
        _pointerHandler.OnPointerExited += () => PlayAnimation(_pointerExitAnimation);
        _pointerHandler.OnPointerDowned += () => PlayAnimation(_pointerDownAnimation);
        _pointerHandler.OnPointerUpped += () => PlayAnimation(_pointerUpAnimation);
        _pointerHandler.OnPointerClicked += () => PlayAnimation(_pointerClickAnimation);
    }
}