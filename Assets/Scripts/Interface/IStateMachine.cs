/// <summary>
/// 상태 머신 인터페이스
/// </summary>
public interface IStateMachine
{
    /// <summary>
    /// 현재 상태
    /// </summary>
    IState CurrentState { get; }

    /// <summary>
    /// 상태 변경
    /// </summary>
    void ChangeState(IState newState);

    /// <summary>
    /// 상태 업데이트
    /// </summary>
    void UpdateState();
}