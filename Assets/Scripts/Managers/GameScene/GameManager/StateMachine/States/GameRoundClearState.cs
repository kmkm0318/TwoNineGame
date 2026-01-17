/// <summary>
/// 게임 라운드 클리어 상태
/// </summary>
public class GameRoundClearState : GameBaseState
{
    public GameRoundClearState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {

    }

    public override void OnEnter()
    {
        // TODO: 라운드 클리어 UI 표시 등 추가 작업

        // 다음 라운드 시작 상태로 전환
        StateMachine.ChangeState(Factory.RoundStartState);
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}