using System;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 애니메이션 관련 확장 메서드를 제공하는 유틸리티 클래스
/// </summary>
public static class AnimationExtensions
{
    #region 상수
    public const float DEFAULT_ANIMATION_DURATION = 0.5f;
    #endregion

    /// <summary>
    /// CanvasGroup를 서서히 보이게 합니다.
    /// </summary>
    public static void Show(this CanvasGroup canvasGroup, float duration = DEFAULT_ANIMATION_DURATION, Action onComplete = null)
    {
        // 상호작용 및 블록 레이캐스트 활성화
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // 페이드 인 애니메이션 실행
        canvasGroup.DOFade(1f, duration).OnComplete(() => onComplete?.Invoke());
    }
}