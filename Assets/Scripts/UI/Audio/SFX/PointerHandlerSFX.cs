using UnityEngine;

/// <summary>
/// PointerHandler의 SFX 재생을 담당하는 클래스
/// </summary>
[RequireComponent(typeof(PointerHandler))]
public class PointerHandlerSFX : UISFX
{
    [Header("SFX 타입")]
    [SerializeField] private SFXType _pointerEnterSFXType = SFXType.ButtonHover;
    [SerializeField] private SFXType _pointerExitSFXType = SFXType.None;
    [SerializeField] private SFXType _pointerDownSFXType = SFXType.None;
    [SerializeField] private SFXType _pointerUpSFXType = SFXType.None;
    [SerializeField] private SFXType _pointerClickSFXType = SFXType.ButtonClick;

    #region 레퍼런스
    private PointerHandler _pointerHandler;
    #endregion

    private void Awake()
    {
        // PointerHandler 컴포넌트 가져오기
        _pointerHandler = GetComponent<PointerHandler>();

        // PointerHandler가 없을 경우 패스
        if (_pointerHandler == null)
        {
            $"PointerHandler가 없습니다.".LogWarning(this);
            return;
        }

        // 포인터 핸들러의 이벤트에 SFX 재생 함수 등록
        _pointerHandler.OnPointerEntered += () => PlaySFX(_pointerEnterSFXType);
        _pointerHandler.OnPointerExited += () => PlaySFX(_pointerExitSFXType);
        _pointerHandler.OnPointerDowned += () => PlaySFX(_pointerDownSFXType);
        _pointerHandler.OnPointerUpped += () => PlaySFX(_pointerUpSFXType);
        _pointerHandler.OnPointerClicked += () => PlaySFX(_pointerClickSFXType);
    }
}