using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// 소인수 파티클 클래스
/// 넘버 버튼을 클릭했을 때 생성되는 소인수를 표현하는 오브젝트 클래스
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class PrimeFactorParticle : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text _numberText;

    #region 컴포넌트
    private RectTransform _rectTransform;
    #endregion

    private void Awake()
    {
        // 컴포넌트 캐싱
        _rectTransform = GetComponent<RectTransform>();
    }

    public void PlayAnimation(int number, Vector2 startPosition, Vector2 endPosition, float duration, Ease ease, Action onComplete = null)
    {
        // 숫자 설정
        _numberText.text = number.ToString();

        // 위치 초기화
        _rectTransform.localPosition = startPosition;

        // 투명도 초기화
        _numberText.alpha = 1f;

        // 시퀀스 생성
        Sequence sequence = DOTween.Sequence();

        // 이동 애니메이션 추가
        sequence.Join(_rectTransform.DOLocalMove(endPosition, duration).SetEase(ease));

        // 투명도 애니메이션 추가
        sequence.Join(_numberText.DOFade(0, duration));

        // 완료 콜백 추가
        sequence.OnComplete(() => onComplete?.Invoke());
    }
}