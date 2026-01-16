using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 포인터 이벤트를 처리하는 클래스
/// </summary>
public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    #region 이벤트
    public event Action OnPointerEntered;
    public event Action OnPointerExited;
    public event Action OnPointerDowned;
    public event Action OnPointerUpped;
    public event Action OnPointerClicked;
    #endregion

    #region 인터페이스 구현
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClicked?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDowned?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEntered?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExited?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpped?.Invoke();
    }
    #endregion
}