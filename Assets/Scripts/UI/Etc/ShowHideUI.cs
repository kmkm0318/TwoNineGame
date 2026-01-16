using UnityEngine;

/// <summary>
/// UI 표시 및 숨김을 담당하는 클래스
/// </summary>
public class ShowHideUI : MonoBehaviour, IShowHide
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}