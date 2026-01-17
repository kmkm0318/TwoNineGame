using System;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// UI 표시 및 숨김을 담당하는 클래스
/// </summary>
public class ShowHideUI : MonoBehaviour, IShowHide
{
    [Header("UI Components")]
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private RectTransform _panel;
    [SerializeField] private Vector2 _showPosition = Vector2.zero;
    [SerializeField] private Vector2 _hidePosition = new(0f, -1000f);
    [SerializeField] private Ease _animationEase = Ease.OutCubic;

    #region 변수
    private Sequence _currentSequence;
    #endregion

    public virtual void Show(float duration = 0.5f, Action onComplete = null)
    {
        // 이전 애니메이션이 있으면 종료
        _currentSequence?.Kill();

        // 오브젝트 활성화
        gameObject.SetActive(true);

        // duration이 0 이하이거나 Background와 Panel이 없는 경우 즉시 표시
        if (duration <= 0f || (_background == null && _panel == null))
        {
            if (_background != null)
            {
                _background.alpha = 1f;
            }

            if (_panel != null)
            {
                _panel.anchoredPosition = _showPosition;
            }

            onComplete?.Invoke();
        }
        else
        {
            // 새 시퀀스 생성
            _currentSequence = DOTween.Sequence().SetUpdate(true);

            if (_background != null)
            {
                // 배경 페이드 인
                _currentSequence.Join(_background.DOFade(1f, duration).From(0f));
            }

            if (_panel != null)
            {
                // 패널 이동
                _currentSequence.Join(_panel.DOAnchorPos(_showPosition, duration).From(_hidePosition).SetEase(_animationEase));
            }

            // 완료 콜백 설정
            _currentSequence.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
    }

    public virtual void Hide(float duration = 0.5f, Action onComplete = null)
    {
        // 이전 애니메이션이 있으면 종료
        _currentSequence?.Kill();

        // duration이 0 이하이거나 Background와 Panel이 없는 경우 즉시 숨김
        if (duration <= 0f || (_background == null && _panel == null))
        {
            if (_background != null)
            {
                _background.alpha = 0f;
            }

            if (_panel != null)
            {
                _panel.anchoredPosition = _hidePosition;
            }

            gameObject.SetActive(false);
            onComplete?.Invoke();
        }
        else
        {
            // 새 시퀀스 생성
            _currentSequence = DOTween.Sequence();

            // 배경 페이드 아웃
            _currentSequence.Append(_background.DOFade(0f, duration).From(1f));

            // 패널 이동
            _currentSequence.Join(_panel.DOAnchorPos(_hidePosition, duration).From(_showPosition).SetEase(_animationEase));

            // 완료 콜백 설정
            _currentSequence.OnComplete(() =>
            {
                // 오브젝트 비활성화
                gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }
    }
}