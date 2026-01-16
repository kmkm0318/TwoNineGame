using DG.Tweening;
using UnityEngine;

/// <summary>
/// DotweenAnimation 재생을 담당하는 UI 기본 클래스
/// </summary>
public abstract class UIAnimation : MonoBehaviour
{
    private DOTweenAnimation _currentAnimation;

    protected void PlayAnimation(DOTweenAnimation animation)
    {
        // 애니메이션이 없으면 패스
        if (animation == null) return;

        // 현재 재생 중인 애니메이션이 있으면 중지
        if (_currentAnimation != null)
        {
            _currentAnimation.DOPause();
        }

        // 현재 애니메이션 설정
        _currentAnimation = animation;

        // 애니메이션 재생
        animation.DORestart();
    }
}