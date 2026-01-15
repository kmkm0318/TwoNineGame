/// <summary>
/// 게임 결과 상태
/// </summary>
public class GameResultState : GameBaseState
{
    #region 레퍼런스
    private RoundManager _roundManager;
    #endregion

    public GameResultState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _roundManager = gameManager.RoundManager;
    }

    public override void OnEnter()
    {
        // TODO: 게임 결과 UI 표시 등 추가 작업

        // 최종 라운드 가져오기
        var finalRound = _roundManager.CurrentRound;

        // 로그 표시
        $"게임 오버! 최종 라운드: {finalRound}".Log();

        // 로딩 상태로 전환
        StateMachine.ChangeState(Factory.LoadingState);
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}