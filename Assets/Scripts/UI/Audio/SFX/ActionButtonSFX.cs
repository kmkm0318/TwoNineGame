using UnityEngine;

/// <summary>
/// ActionButton의 SFX 처리를 담당하는 클래스
/// </summary>
[RequireComponent(typeof(ActionButton))]
public class ActionButtonSFX : UISFX
{
    [Header("SFX Type")]
    [SerializeField] private SFXType _normalSFXType = SFXType.None;
    [SerializeField] private SFXType _highlightedSFXType = SFXType.None;
    [SerializeField] private SFXType _pressedSFXType = SFXType.None;
    [SerializeField] private SFXType _disabledSFXType = SFXType.None;
    [SerializeField] private SFXType _selectedSFXType = SFXType.None;

    #region 레퍼런스
    private ActionButton _actionButton;
    #endregion

    private void Awake()
    {
        // ActionButton 컴포넌트 가져오기
        _actionButton = GetComponent<ActionButton>();

        // ActionButton이 없을 경우 패스
        if (_actionButton == null)
        {
            $"ActionButton이 없습니다.".LogWarning(this);
            return;
        }

        // 액션 버튼의 이벤트에 SFX 재생 함수 등록
        _actionButton.OnNormal += () => PlaySFX(_normalSFXType);
        _actionButton.OnHighlighted += () => PlaySFX(_highlightedSFXType);
        _actionButton.OnPressed += () => PlaySFX(_pressedSFXType);
        _actionButton.OnDisabled += () => PlaySFX(_disabledSFXType);
        _actionButton.OnSelected += () => PlaySFX(_selectedSFXType);
    }
}