/// <summary>
/// 게임 라운드 실패 상태
/// </summary>
public class GameRoundFailState : GameBaseState
{
    public GameRoundFailState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {

    }

    public override void OnEnter()
    {
        // TODO: 라운드 실패 UI 표시 등 추가 작업

        // 게임 결과 상태로 전환
        StateMachine.ChangeState(Factory.ResultState);
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}