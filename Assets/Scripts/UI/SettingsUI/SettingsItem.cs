using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 설정 아이템 클래스
/// 이름, 값, 좌우 버튼을 포함
/// </summary>
public class SettingsItem : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    #region 이벤트
    /// <summary>
    /// 값을 감소시키는 버튼
    /// </summary>
    public event Action OnLeftButtonClicked;
    /// <summary>
    /// 값을 증가시키는 버튼
    /// </summary>
    public event Action OnRightButtonClicked;
    #endregion

    private void Awake()
    {
        // 버튼 클릭 이벤트 등록
        _leftButton.onClick.AddListener(() => OnLeftButtonClicked?.Invoke());
        _rightButton.onClick.AddListener(() => OnRightButtonClicked?.Invoke());
    }

    #region UI 설정
    public void SetNameText(string name) => _nameText.text = name;
    public void SetValueText(string value) => _valueText.text = value;
    #endregion
}