using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 진행 바 UI 컴포넌트
/// </summary>
public class ProgressBar : MonoBehaviour
{
    #region 에디터 변수
    [Header("UI Elements")]

    [Header("Slider Settings")]
    [SerializeField] private Slider _slider;
    [SerializeField] private bool _isWholeNumber = false;

    [Header("Animation Settings")]
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private Ease _animationEase = Ease.OutBack;

    [Header("Fill Image Settings")]
    [SerializeField] private Image _fillImage;
    [SerializeField] private bool _useGradient = false;
    [SerializeField] private Gradient _fillGradient;

    [Header("Value Text Settings")]
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private bool _showMaxValue = false;
    [SerializeField] private string _valueTextFormat = "#.#";
    #endregion

    #region 변수
    private Tween _currentTween;
    #endregion

    #region 프로퍼티
    public float Value => _slider.value;
    public float MaxValue => _slider.maxValue;
    public float NormalizedValue => _slider.normalizedValue;
    #endregion

    #region 이벤트
    public Action<float> OnValueChanged;
    public Action<float> OnMaxValueChanged;
    #endregion

    private void Awake()
    {
        // 슬라이더의 전체 숫자 설정
        _slider.wholeNumbers = _isWholeNumber;

        // 슬라이더 값 변경 이벤트 등록
        _slider.onValueChanged.AddListener(HandleOnValueChanged);
    }

    #region UI 이벤트 핸들러
    private void HandleOnValueChanged(float value)
    {
        // 색 업데이트
        UpdateColor();

        // 값 변경 시 이벤트 호출
        OnValueChanged?.Invoke(value);
    }
    #endregion

    #region 슬라이더 설정
    public void SetValue(float value)
    {
        // 현재 진행 중인 트윈이 있으면 종료
        _currentTween?.Kill();

        // 텍스트 업데이트
        UpdateText(value);

        if (_animationDuration <= 0f)
        {
            // 즉시 값 설정
            _slider.value = value;
        }
        else
        {
            // 트윈을 사용하여 값 애니메이션
            _currentTween = _slider
                .DOValue(value, _animationDuration)
                .SetEase(_animationEase);
        }
    }

    public void SetMaxValue(float maxValue)
    {
        // 최대값 설정
        _slider.maxValue = maxValue;

        // 텍스트 업데이트
        UpdateText(Value);

        // 이벤트 호출
        OnMaxValueChanged?.Invoke(maxValue);
    }
    #endregion

    #region 색 설정
    private void UpdateColor()
    {
        // 그라디언트를 사용하지 않는 경우 패스
        if (!_useGradient) return;

        // 그라디언트를 사용하는 경우 색상 업데이트
        SetFillColor(NormalizedValue);
    }

    public void SetFillColor(Color color)
    {
        // 채우기 이미지가 없으면 패스
        if (_fillImage == null) return;

        // 채우기 이미지 색상 설정
        _fillImage.color = color;
    }

    public void SetFillColor(float normalizedValue)
    {
        // 채우기 이미지 색상을 그라디언트에 따라 설정
        SetFillColor(_fillGradient.Evaluate(normalizedValue));
    }
    #endregion

    #region 텍스트 설정
    private void UpdateText(float value)
    {
        // 텍스트가 없으면 패스
        if (_valueText == null) return;

        // 값 텍스트 생성
        string curValueText = value.ToString(_valueTextFormat);
        string maxValueText = MaxValue.ToString(_valueTextFormat);
        string valueText = _showMaxValue ? $"{curValueText}/{maxValueText}" : $"{curValueText}";

        // 값 텍스트 업데이트
        _valueText.text = valueText;
    }
    #endregion
}