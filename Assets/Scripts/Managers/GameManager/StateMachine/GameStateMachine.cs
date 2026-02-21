/// <summary>
/// 게임 상태 머신
/// </summary>
public class GameStateMachine : IStateMachine
{
    public IState CurrentState { get; private set; }

    public void ChangeState(IState newState)
    {
        // 현재 상태 종료
        CurrentState?.OnExit();

        // 새로운 상태로 변경
        CurrentState = newState;

        // 새로운 상태 진입
        CurrentState.OnEnter();
    }

    public void UpdateState()
    {
        // 현재 상태 업데이트
        CurrentState?.OnUpdate();
    }
}