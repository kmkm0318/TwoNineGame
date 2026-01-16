using DG.Tweening;
using UnityEngine;

/// <summary>
/// DotweenAnimation 재생을 담당하는 UI 기본 클래스
/// </summary>
public abstract class UIAnimation : MonoBehaviour
{
    protected void PlayScaleAnimation(float targetScale, float duration = 0.5f, Ease ease = Ease.OutBack)
    {
        // 현재 재생 중인 애니메이션이 있으면 종료
        transform.DOKill();

        // 스케일 애니메이션 재생
        transform.DOScale(targetScale, duration).SetEase(ease);
    }
}