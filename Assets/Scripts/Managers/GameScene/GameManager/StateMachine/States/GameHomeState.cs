/// <summary>
/// 게임 홈 상태
/// </summary>
public class GameHomeState : GameBaseState
{
    public GameHomeState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {

    }

    public override void OnEnter()
    {
        // TODO: 홈 UI 표시

        // 지금은 게임 로딩 상태로 바로 전환
        StateMachine.ChangeState(Factory.LoadingState);
    }

    public override void OnExit()
    {
        // TODO: 홈 UI 숨기기
    }

    public override void OnUpdate()
    {

    }
}