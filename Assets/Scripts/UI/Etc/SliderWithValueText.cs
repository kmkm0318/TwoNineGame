using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 슬라이더 값에 따라 텍스트를 업데이트하는 클래스
/// </summary>
public class SliderWithValueText : MonoBehaviour
{
    #region 값 타입 Enum
    /// <summary>
    /// 값의 타입
    /// </summary>
    public enum ValueType
    {
        // 그대로 표시
        Default,
        // 백분율로 표시
        Percentage,
    }
    #endregion

    [Header("UI Components")]
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _valuetext;

    [Header("Text Settings")]
    [SerializeField] private ValueType _valueType;
    [SerializeField] private string _format = "0.00";

    private void Awake()
    {
        // 슬라이더 값 변경 시 텍스트 업데이트
        _slider.onValueChanged.AddListener(UpdateValueText);

        // 초기 값 설정
        UpdateValueText(_slider.value);
    }

    private void UpdateValueText(float value)
    {
        // 값 타입에 따라 텍스트 포맷팅
        switch (_valueType)
        {
            case ValueType.Default:
                // 일반 값으로 표시
                _valuetext.text = value.ToString(_format);
                break;
            case ValueType.Percentage:
                // 백분율로 표시
                _valuetext.text = (value * 100f).ToString(_format) + "%";
                break;
        }
    }
}