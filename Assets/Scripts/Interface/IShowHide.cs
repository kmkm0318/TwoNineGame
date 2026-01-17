using System;

/// <summary>
/// UI 표시 및 숨김 기능을 위한 인터페이스
/// </summary>
public interface IShowHide
{
    void Show(float duration = 0.5f, Action onComplete = null);
    void Hide(float duration = 0.5f, Action onComplete = null);
}