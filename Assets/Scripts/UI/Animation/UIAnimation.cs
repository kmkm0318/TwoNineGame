using DG.Tweening;
using UnityEngine;

/// <summary>
/// DotweenAnimation 재생을 담당하는 UI 기본 클래스
/// </summary>
public abstract class UIAnimation : MonoBehaviour
{
    #region 애니메이션 설정
    [Header("Animation Settings")]

    [Header("Scale Settings")]
    [SerializeField] private float _originalScale = 1f;
    [SerializeField] private float _positiveScale = 1.1f;
    [SerializeField] private float _negativeScale = 0.9f;
    [SerializeField] private float _scaleDuration = 0.2f;
    [SerializeField] private Ease _scaleEase = Ease.OutBack;

    [Header("MoveY Settings")]
    [SerializeField] private float _originalPositionY = 0f;
    [SerializeField] private float _positivePositionY = 10f;
    [SerializeField] private float _negativePositionY = -10f;
    [SerializeField] private float _moveYDuration = 0.2f;
    [SerializeField] private Ease _moveYEase = Ease.OutBack;

    [Header("Punch RotationZ Settings")]
    [SerializeField] private float _punchRotationZAngle = 30f;
    [SerializeField] private float _punchRotationZDuration = 0.5f;
    [SerializeField] private int _punchRotationZVibrato = 10;
    [SerializeField] private float _punchRotationZElasticity = 1f;
    [SerializeField] private Ease _punchRotationZEase = Ease.Unset;
    #endregion

    #region 변수
    private Tween _currentTween;
    #endregion

    #region 애니메이션 재생
    protected void PlayAnimation(UIAnimationType type, UIAnimationState state)
    {
        // 현재 재생 중인 애니메이션이 있으면 종료
        _currentTween?.Kill();

        // 애니메이션 타입에 따른 분기 처리
        switch (type)
        {
            case UIAnimationType.Scale:
                float targetScale = state == UIAnimationState.Positive ? _positiveScale : state == UIAnimationState.Negative ? _negativeScale : _originalScale;
                PlayScaleAnimation(targetScale, _scaleDuration, _scaleEase);
                break;
            case UIAnimationType.MoveY:
                float targetPositionY = state == UIAnimationState.Positive ? _positivePositionY : state == UIAnimationState.Negative ? _negativePositionY : _originalPositionY;
                PlayMoveYAnimation(targetPositionY, _moveYDuration, _moveYEase);
                break;
            case UIAnimationType.PunchRotationZ:
                float angle = state == UIAnimationState.Positive ? _punchRotationZAngle : -_punchRotationZAngle;
                PlayPunchRotationZAnimation(angle, _punchRotationZDuration, _punchRotationZVibrato, _punchRotationZElasticity, _punchRotationZEase);
                break;
            case UIAnimationType.None:
            default:
                // 애니메이션 없음
                break;
        }
    }

    protected void PlayScaleAnimation(float targetScale, float duration, Ease ease)
    {
        // 스케일 애니메이션 재생
        _currentTween = transform.DOScale(targetScale, duration).SetEase(ease);
    }

    protected void PlayMoveYAnimation(float targetPositionY, float duration, Ease ease)
    {
        // LocalMoveY 애니메이션 재생
        _currentTween = transform.DOLocalMoveY(targetPositionY, duration).SetEase(ease);
    }

    protected void PlayPunchRotationZAnimation(float angle, float duration, int vibrato, float elasticity, Ease ease)
    {
        // 펀치 회전은 Z축 기준으로
        Vector3 punch = new(0f, 0f, angle);

        // 펀치 회전 애니메이션 재생
        _currentTween = transform.DOPunchRotation(punch, duration, vibrato, elasticity).SetEase(ease);
    }
    #endregion
}
