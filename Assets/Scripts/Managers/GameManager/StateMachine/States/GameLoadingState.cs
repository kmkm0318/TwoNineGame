/// <summary>
/// 게임 로딩 상태
/// </summary>
public class GameLoadingState : GameBaseState
{
    #region 레퍼런스
    private RoundManager _roundManager;
    private NumberManager _numberManager;
    #endregion

    public GameLoadingState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _roundManager = gameManager.RoundManager;
        _numberManager = gameManager.NumberManager;
    }

    public override void OnEnter()
    {
        // 라운드 매니저 초기화
        _roundManager.Reset();

        // 숫자 매니저 초기화
        _numberManager.Reset();

        // 라운드 시작 상태로 전환
        StateMachine.ChangeState(Factory.RoundStartState);
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}