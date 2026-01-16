/// <summary>
/// 게임 결과 상태
/// </summary>
public class GameResultState : GameBaseState
{
    #region 레퍼런스
    private RoundManager _roundManager;
    private UserDataManager _userDataManager;
    #endregion

    public GameResultState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _roundManager = gameManager.RoundManager;
        _userDataManager = gameManager.UserDataManager;
    }

    public override void OnEnter()
    {
        // TODO: 게임 결과 UI 표시 등 추가 작업

        // 최고 점수 가져오기
        var newHighScore = _roundManager.CurrentRound;

        // 최고 점수 갱신 시도
        _userDataManager.UpdateHighScore(newHighScore);

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