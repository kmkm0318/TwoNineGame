using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 숫자 버튼 UI를 담당하는 클래스
/// </summary>
public class NumberButton : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _numberText;

    #region 데이터
    private int _number;
    #endregion

    #region 이벤트
    public event Action<int> OnNumberButtonClicked;
    #endregion

    private void Awake()
    {
        // 버튼 클릭 이벤트 등록
        _button.onClick.AddListener(() => OnNumberButtonClicked?.Invoke(_number));
    }

    #region 초기화
    public void Init(int number)
    {
        // 숫자 설정
        _number = number;

        // UI 업데이트
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // 숫자 텍스트 업데이트
        _numberText.text = _number.ToString();
    }
    #endregion
}