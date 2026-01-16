using System;
using UnityEngine.UI;

/// <summary>
/// 액션 버튼 클래스
/// Selectable의 상태 변화에 따른 이벤트를 제공
/// </summary>
public class ActionButton : Button
{
    #region 이벤트
    public event Action OnNormal;
    public event Action OnHighlighted;
    public event Action OnPressed;
    public event Action OnSelected;
    public event Action OnDisabled;
    #endregion

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        // 기본 상태 전환 처리
        base.DoStateTransition(state, instant);

        // 상태에 따른 이벤트 호출
        switch (state)
        {
            case SelectionState.Normal:
                OnNormal?.Invoke();
                break;
            case SelectionState.Highlighted:
                OnHighlighted?.Invoke();
                break;
            case SelectionState.Pressed:
                OnPressed?.Invoke();
                break;
            case SelectionState.Selected:
                OnSelected?.Invoke();
                break;
            case SelectionState.Disabled:
                OnDisabled?.Invoke();
                break;
        }
    }
}